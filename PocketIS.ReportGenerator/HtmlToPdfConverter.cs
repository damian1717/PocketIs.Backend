using Microsoft.Extensions.Localization;
using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using PocketIS.Infrastucture.Extensions;
using PocketIS.PdfConverter;
using PocketIS.ReportGenerator.PdfSharp.Drawing.Layout;
using PocketIS.Services.Interfaces;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System.Net;
using VetCV.HtmlRendererCore.Core.Entities;

namespace PocketIS.ReportGenerator
{
    public static class HtmlToPdfConverter
    {
        public const double Dpi = 96;
        public const double Inch = 25.4;

        public static string ImagesFolder { get; set; }
        public static string CssFolder { get; set; }

        public static double MillimetersToUnits(double millimeters, double dpi = Dpi)
        {
            return millimeters / Inch * dpi;
        }

        public static byte[] RenderPdf()
        {
            using (PdfDocument document = new PdfDocument())
            {
                try
                {
                    System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

                    PdfPage page = document.AddPage();

                    XGraphics gfx = XGraphics.FromPdfPage(page);

                    XFont font = new XFont("Arial", 20);

                    gfx.DrawString("Hello, World!", font, XBrushes.Black, new XRect(0, 0, page.Width, page.Height), XStringFormats.Center);

                    using (var bytes = new MemoryStream())
                    {
                        document.Save(bytes);
                        return bytes.ToArray();
                    }
                }
                finally
                {
                    document.Dispose();
                }
            }
        }

        public static XSize FromMillimeters(double width, double height, double dpi = Dpi)
        {
            return new XSize(MillimetersToUnits(width, dpi), MillimetersToUnits(height, dpi));
        }

        public static async Task<byte[]> RenderHtmlPagesToPdfAsync<T>(
            IViewConfig viewConfig,
            IReportModel<T> model,
            IRenderRazorToStringService renderService,
            LayoutConfig layoutConfig)
        {
            var context = new ReportGenerationContext<T>(model, viewConfig, layoutConfig ?? MakeDefaultLayoutConfig());

            using (var containerDocument = new PdfDocument())
            {
                var subReports = new List<PdfDocumentModelPair<T>>();
                try
                {
                    // First pass to count page total number
                    foreach (var pageModel in model.GetSubReports())
                    {
                        await EnsureBodyHeightAsync(context, renderService, pageModel).ConfigureAwait(false);
                        var pageDocument = await RenderHtmlToPdfAsync(viewConfig.Body, pageModel, renderService, context.Layout.BodyConfig)
                            .ConfigureAwait(false);
                        subReports.Add(new PdfDocumentModelPair<T> { Document = pageDocument, Model = pageModel });
                        context.TotalPages += pageDocument.Pages.Count;
                    }

                    foreach (var subReport in subReports)
                    {
                        await DrawHeaderFooterAsync(context, renderService, subReport).ConfigureAwait(false);

                        if (IsWatermarkRequired())
                        {
                            foreach (var documentPage in subReport.Document.Pages)
                            {
                                ///AddWatermark(documentPage, localizer);
                            }
                        }

                        using (var document = ImportPdfDocument(subReport.Document))
                        {
                            foreach (var documentPage in document.Pages)
                            {
                                containerDocument.AddPage(documentPage);
                            }
                        }
                    }

                    using (var bytes = new MemoryStream())
                    {
                        containerDocument.Save(bytes);
                        return bytes.ToArray();
                    }
                }
                finally
                {
                    foreach (var item in subReports)
                    {
                        item.Document.Dispose();
                    }
                }
            }
        }

        private static async Task EnsureBodyHeightAsync<T>(
            ReportGenerationContext<T> context,
            IRenderRazorToStringService renderService,
            ReportSubReportModel<T> subReport)
        {
            if (subReport.ShowHeader)
            {
                var height = await SectionHeight(context.Views.Header, renderService, subReport, context.Layout.HeaderConfig)
                    .ConfigureAwait(false);
                context.Layout.BodyConfig.MarginTop = Math.Max(context.Layout.BodyConfig.MarginTop, height);
            }
            if (subReport.ShowFooter)
            {
                var height = await SectionHeight(context.Views.Footer, renderService, subReport, context.Layout.FooterConfig)
                    .ConfigureAwait(false);
                context.Layout.BodyConfig.MarginBottom = Math.Max(context.Layout.BodyConfig.MarginBottom, height);
            }
        }

        private static async Task<int> SectionHeight<T>(
            string viewName,
            IRenderRazorToStringService renderService,
            ReportSubReportModel<T> subReport,
            PdfGenerateConfig config)
        {
            var height = await GetHtmlHeight(viewName, subReport, renderService, config).ConfigureAwait(false);
            var pageSize = PdfGenerator.DocumentPageSize(config);

            if (pageSize.Height >= height)
            {
                return (int)Math.Ceiling(pageSize.Height);
            }
            config.ManualPageSize = new XSize(pageSize.Width, height);

            return height;
        }

        private static async Task<int> GetHtmlHeight(
            string viewName,
            object model,
            IRenderRazorToStringService renderService,
            PdfGenerateConfig pageConfig)
        {
            var html = await renderService.RenderToStringAsync(viewName, model, true).ConfigureAwait(false);
            var height = PdfGenerator.GetContentHeight(html, pageConfig, OnStyleSheetLoadPdfSharp, OnImageLoadPdfSharp);

            return height;
        }

        private static async Task<PdfDocument> RenderHtmlToPdfAsync(
            string viewName,
            object model,
            IRenderRazorToStringService renderService,
            PdfGenerateConfig pageConfig)
        {
            var html = await renderService.RenderToStringAsync(viewName, model, true).ConfigureAwait(false);
            var doc = new PdfDocument();
            PdfGenerator.AddPdfPages(doc, html, pageConfig, OnStyleSheetLoadPdfSharp, OnImageLoadPdfSharp);

            return doc;
        }

        private static async Task DrawHeaderFooterAsync<T>(
            ReportGenerationContext<T> context,
            IRenderRazorToStringService renderService,
            PdfDocumentModelPair<T> subReport)
        {
            foreach (var body in subReport.Document.Pages)
            {
                subReport.Model.TotalPages = context.TotalPages;
                subReport.Model.CurrentPage = context.CurrentPage;

                if (subReport.Model.ShowHeader)
                {
                    await DrawSectionAsync(
                        body,
                        context.Views.Header,
                        context.Layout.HeaderConfig,
                        header => new XRect(0, 0, header.Width, header.Height),
                        renderService,
                        subReport.Model).ConfigureAwait(false);
                }

                if (subReport.Model.ShowFooter)
                {
                    await DrawSectionAsync(
                        body,
                        context.Views.Footer,
                        context.Layout.FooterConfig,
                        footer => new XRect(0, body.Height - footer.Height, footer.Width, footer.Height),
                        renderService,
                        subReport.Model).ConfigureAwait(false);
                }

                ++context.CurrentPage;
            }
        }

        private static async Task DrawSectionAsync<T>(
            PdfPage body,
            string viewName,
            PdfGenerateConfig pdfConfig,
            Func<PdfPage, XRect> box,
            IRenderRazorToStringService renderService,
            ReportSubReportModel<T> model)
        {
            var section = await RenderHtmlToPdfAsync(viewName, model, renderService, pdfConfig)
                .ConfigureAwait(false);

            if (section.Pages.Count == 0)
                return;

            using (var stream = new MemoryStream())
            {
                section.Save(stream, false);
                stream.Seek(0, SeekOrigin.Begin);

                var image = XPdfForm.FromStream(stream);
                using (var graphics = XGraphics.FromPdfPage(body))
                {
                    graphics.DrawImage(image, box.Invoke(section.Pages[0]));
                }
            }
        }

        private static PdfDocument ImportPdfDocument(PdfDocument document)
        {
            using (var stream = new MemoryStream())
            {
                document.Save(stream, false);
                var importedDocument = PdfReader.Open(stream, PdfDocumentOpenMode.Import);

                return importedDocument;
            }
        }

        private static void OnStyleSheetLoadPdfSharp(object sender, HtmlStylesheetLoadEventArgs e)
        {
            if (e == null)
                return;

            var src = e.Src;
            if (!src.StartsWithOrdinal("http://") && !src.StartsWithOrdinal("https://"))
            {
                if (File.Exists(src))
                {
                    e.SetStyleSheetData = PdfGenerator.ParseStyleSheet(File.ReadAllText(src), false);
                }
            }
            else
            {
                using (var client = new WebClient())
                {
                    e.SetStyleSheetData = PdfGenerator.ParseStyleSheet(client.DownloadString(src), false);
                }
            }
        }

        private static void OnImageLoadPdfSharp(object sender, HtmlImageLoadEventArgs e)
        {
            if (e == null)
                return;

            var src = e.Src;

            if (src.StartsWithOrdinal("data:"))
                return;

            if (!src.StartsWithOrdinal("http://") && !src.StartsWithOrdinal("https://"))
            {
                src = Path.Combine(ImagesFolder, src.EnsurePrefixAbsent("/"));
                if (File.Exists(src))
                {
                    e.Callback(XImage.FromFile(src));
                }
                else
                {
                    // Return a dummy image, as at the moment PdfGenerator throws a
                    // null reference exception trying to draw a broken image icon.  
                    using (var image = new Image<Rgba32>(Configuration.Default, 1, 1, Rgba32.White))
                    {
                        using (var stream = new MemoryStream())
                        {
                            image.SaveAsPng(stream);
                            stream.Seek(0, SeekOrigin.Begin);
                            e.Callback(XImage.FromStream(stream));
                        }
                    }
                }
            }
            else
            {
                using (var client = new WebClient())
                {
                    using (var stream = new MemoryStream(client.DownloadData(src)))
                    {
                        e.Callback(XImage.FromStream(stream));
                    }
                }
            }
        }

        private static LayoutConfig MakeDefaultLayoutConfig()
        {
            return new LayoutConfig
            {
                HeaderConfig = new PdfGenerateConfig { ManualPageSize = FromMillimeters(210, 35) },
                BodyConfig = new PdfGenerateConfig
                {
                    ManualPageSize = FromMillimeters(210, 297),
                    MarginTop = (int)MillimetersToUnits(35),
                    MarginRight = (int)MillimetersToUnits(35),
                    MarginBottom = (int)MillimetersToUnits(20),
                    MarginLeft = (int)MillimetersToUnits(35)
                },
                FooterConfig = new PdfGenerateConfig { ManualPageSize = FromMillimeters(210, 20) }
            };
        }

        private static bool IsWatermarkRequired()
        {
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", EnvironmentVariableTarget.Process);
            return env == null || (!env.Equals("prod", StringComparison.OrdinalIgnoreCase) && !env.Equals("test", StringComparison.OrdinalIgnoreCase));
        }
        /*
        private static void AddWatermark(PdfPage page, IStringLocalizer localizer)
        {
            if (page.Width == 0 || page.Height == 0)
            {
                return;
            }
            var gfx = XGraphics.FromPdfPage(page, XGraphicsPdfPageOptions.Append);
            var font = new XFont("Arial", 46, XFontStyle.Bold);
            var text = localizer["Watermark"].Value;

            var tf = new TextFormatter(gfx) { Alignment = XParagraphAlignment.Justify };
            var size = tf.MeasureString(text, font, new XRect(0, 0, page.Height - (page.Height - page.Width) / 2, page.Width));

            gfx.TranslateTransform(page.Width / 2, page.Height / 2);
            gfx.RotateTransform(-Math.Atan(page.Height / page.Width) * 180 / Math.PI);
            gfx.TranslateTransform(-page.Width / 2, -page.Height / 2);

            XBrush brush = new XSolidBrush(XColor.FromArgb(16, 128, 128, 128));

            tf.DrawString(text, font, brush, new XRect((page.Width - size.Width) / 2, (page.Height - size.Height) / 2, size.Width, size.Height));
        }*/
    }
}
