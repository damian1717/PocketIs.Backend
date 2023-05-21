using PdfSharp.Drawing;

namespace PocketIS.PdfConverter.Adapters
{
    /// <summary>
    /// Because PdfSharp doesn't support texture brush we need to implement it ourselves.
    /// </summary>
    internal sealed class XTextureBrush
    {
        #region Fields/Consts

        /// <summary>
        /// The image to draw in the brush
        /// </summary>
        private readonly XImage _image;

        /// <summary>
        /// the transform the location of the image to handle center alignment
        /// </summary>
        private readonly XPoint _translateTransformLocation;

        #endregion


        /// <summary>
        /// Init.
        /// </summary>
        public XTextureBrush(XImage image, XPoint translateTransformLocation)
        {
            _image = image;
            _translateTransformLocation = translateTransformLocation;
        }

        /// <summary>
        /// Draw the texture image in the given graphics at the given location.
        /// </summary>
        public void DrawRectangle(XGraphics g, double x, double y, double width, double height)
        {
            var prevState = g.Save();
            g.IntersectClip(new XRect(x, y, width, height));

            var rx = _translateTransformLocation.X;
            double w = _image.PixelWidth, h = _image.PixelHeight;
            while (rx < x + width)
            {
                var ry = _translateTransformLocation.Y;
                while (ry < y + height)
                {
                    g.DrawImage(_image, rx, ry, w, h);
                    ry += h;
                }
                rx += w;
            }

            g.Restore(prevState);
        }
    }
}
