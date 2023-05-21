using PdfSharp.Drawing;
using VetCV.HtmlRendererCore.Adapters;

namespace PocketIS.PdfConverter.Adapters
{
    /// <summary>
    /// Adapter for WinForms Font object for core.
    /// </summary>
    internal sealed class FontFamilyAdapter : RFontFamily
    {
        /// <summary>
        /// Init.
        /// </summary>
        public FontFamilyAdapter(XFontFamily fontFamily)
        {
            FontFamily = fontFamily;
        }

        /// <summary>
        /// the underline win-forms font family.
        /// </summary>
        public XFontFamily FontFamily { get; }

        public override string Name => FontFamily.Name;
    }
}
