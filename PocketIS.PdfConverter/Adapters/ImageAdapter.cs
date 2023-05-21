using PdfSharp.Drawing;
using VetCV.HtmlRendererCore.Adapters;

namespace PocketIS.PdfConverter.Adapters
{
    /// <summary>
    /// Adapter for WinForms Image object for core.
    /// </summary>
    internal sealed class ImageAdapter : RImage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public ImageAdapter(XImage image)
        {
            Image = image;
        }

        /// <summary>
        /// the underline win-forms image.
        /// </summary>
        public XImage Image { get; }

        public override double Width => Image.PixelWidth;

        public override double Height => Image.PixelHeight;

        public override void Dispose()
        {
            Image.Dispose();
        }
    }
}
