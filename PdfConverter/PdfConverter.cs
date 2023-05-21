using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System.IO;

namespace PocketIS.PdfConverter
{
    public static class PdfConverter
    {
        public static byte[] RenderPdf()
        {
            using (PdfDocument document = new PdfDocument())
            {
                try
                {
                    PdfPage page = new PdfPage();

                    XGraphics gfx = XGraphics.FromPdfPage(page);

                    XFont font = new XFont("Verdana", 20, XFontStyle.BoldItalic);

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
    }
}
