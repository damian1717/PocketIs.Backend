using VetCV.HtmlRendererCore.Adapters;

namespace PocketIS.PdfConverter.Adapters
{
    /// <summary>
    /// Adapter for WinForms brushes objects for core.
    /// </summary>
    internal sealed class BrushAdapter : RBrush
    {
        /// <summary>
        /// Init.
        /// </summary>
        public BrushAdapter(object brush)
        {
            Brush = brush;
        }

        /// <summary>
        /// The actual WinForms brush instance.
        /// </summary>
        public object Brush { get; }

        public override void Dispose()
        { }
    }
}
