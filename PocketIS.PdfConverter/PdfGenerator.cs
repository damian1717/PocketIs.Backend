using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PocketIS.PdfConverter.Adapters;
using System;
using VetCV.HtmlRendererCore.Core;
using VetCV.HtmlRendererCore.Core.Entities;

namespace PocketIS.PdfConverter
{
    /// <summary>
    /// Converts HTML strings to PDF documents.
    /// </summary>
    public static class PdfGenerator
    {
        static PdfGenerator()
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        }

        /// <summary>
        /// Parse the given stylesheet to <see cref="CssData"/> object.<br/>
        /// If <paramref name="combineWithDefault"/> is true the parsed css blocks are added to the 
        /// default css data (as defined by W3), merged if class name already exists. If false only the data in the given stylesheet is returned.
        /// </summary>
        /// <seealso cref="http://www.w3.org/TR/CSS21/sample.html"/>
        /// <param name="stylesheet">the stylesheet source to parse</param>
        /// <param name="combineWithDefault">true - combine the parsed css data with default css data, false - return only the parsed css data</param>
        /// <returns>the parsed css data</returns>
        public static CssData ParseStyleSheet(string stylesheet, bool combineWithDefault = true)
        {
            return CssData.Parse(PdfSharpAdapter.Instance, stylesheet, combineWithDefault);
        }

        /// <summary>
        /// Create PDF pages from given HTML and appends them to the provided PDF document.<br/>
        /// </summary>
        /// <param name="document">PDF document to append pages to</param>
        /// <param name="html">HTML source to create PDF from</param>
        /// <param name="config">the configuration to use for the PDF generation (page size/page orientation/margins/etc.)</param>
        /// <param name="stylesheetLoad">optional: can be used to overwrite stylesheet resolution logic</param>
        /// <param name="imageLoad">optional: can be used to overwrite image resolution logic</param>
        /// <returns>the generated image of the html</returns>
        public static void AddPdfPages(PdfDocument document, string html, PdfGenerateConfig config, EventHandler<HtmlStylesheetLoadEventArgs> stylesheetLoad = null, EventHandler<HtmlImageLoadEventArgs> imageLoad = null)
        {
            if (string.IsNullOrEmpty(html))
                return;

            var orgPageSize = DocumentPageSize(config);

            using (var container = CreateContainer(html, config, orgPageSize, stylesheetLoad, imageLoad))
            {
                // while there is un-rendered HTML, create another PDF page and render with proper offset for the next page
                double scrollOffset = 0;
                while (scrollOffset > -container.ActualSize.Height)
                {
                    var page = document.AddPage();
                    page.Height = orgPageSize.Height;
                    page.Width = orgPageSize.Width;

                    using (var g = XGraphics.FromPdfPage(page))
                    {
                        g.IntersectClip(new XRect(0, 0, page.Width, page.Height));

                        container.ScrollOffset = new XPoint(0, scrollOffset);
                        container.PerformPaint(g);
                    }
                    scrollOffset -= container.PageSize.Height;
                }
            }
        }

        public static int GetContentHeight(string html, PdfGenerateConfig config, EventHandler<HtmlStylesheetLoadEventArgs> stylesheetLoad = null, EventHandler<HtmlImageLoadEventArgs> imageLoad = null)
        {
            if (string.IsNullOrEmpty(html))
                return 0;

            var orgPageSize = DocumentPageSize(config);

            using (var container = CreateContainer(html, config, orgPageSize, stylesheetLoad, imageLoad))
            {
                return (int)Math.Ceiling(container.ActualSize.Height);
            }
        }

        public static XSize DocumentPageSize(PdfGenerateConfig config)
        {
            // get the size of each page to layout the HTML in
            var pageSize = config.PageSize != PageSize.Undefined
                ? PageSizeConverter.ToSize(config.PageSize)
                : config.ManualPageSize;

            if (config.PageOrientation == PageOrientation.Landscape)
            {
                // invert page size for landscape
                pageSize = new XSize(pageSize.Height, pageSize.Width);
            }

            return pageSize;
        }

        private static HtmlContainer CreateContainer(string html, PdfGenerateConfig config, XSize orgPageSize, EventHandler<HtmlStylesheetLoadEventArgs> stylesheetLoad = null, EventHandler<HtmlImageLoadEventArgs> imageLoad = null)
        {
            var pageSize = new XSize(orgPageSize.Width - config.MarginLeft - config.MarginRight, orgPageSize.Height - config.MarginTop - config.MarginBottom);
            var container = new HtmlContainer
            {
                PageSize = pageSize,
                MaxSize = new XSize(pageSize.Width, 0),
                Location = new XPoint(config.MarginLeft, config.MarginTop),
                MarginBottom = config.MarginBottom,
                MarginLeft = config.MarginLeft,
                MarginRight = config.MarginRight,
                MarginTop = config.MarginTop
            };
            if (stylesheetLoad != null)
                container.StylesheetLoad += stylesheetLoad;
            if (imageLoad != null)
                container.ImageLoad += imageLoad;

            container.SetHtml(html);

            // layout the HTML with the page width restriction to know how many pages are required
            using (var measure = XGraphics.CreateMeasureContext(pageSize, XGraphicsUnit.Point, XPageDirection.Downwards))
            {
                container.PerformLayout(measure);
            }

            return container;
        }
    }
}
