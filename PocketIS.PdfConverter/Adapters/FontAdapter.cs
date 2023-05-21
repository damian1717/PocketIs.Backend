﻿using PdfSharp.Drawing;
using VetCV.HtmlRendererCore.Adapters;

namespace PocketIS.PdfConverter.Adapters
{
    // <summary>
    /// Adapter for WinForms Font object for core.
    /// </summary>
    internal sealed class FontAdapter : RFont
    {

        #region Fields and Consts

        /// <summary>
        /// the vertical offset of the font underline location from the top of the font.
        /// </summary>
        private double _underlineOffset = -1;

        /// <summary>
        /// Cached font height.
        /// </summary>
        private double _height = -1;

        /// <summary>
        /// Cached font whitespace width.
        /// </summary>
        private double _whitespaceWidth = -1;

        #endregion


        /// <summary>
        /// Init.
        /// </summary>
        public FontAdapter(XFont font)
        {
            Font = font;
        }

        /// <summary>
        /// the underline win-forms font.
        /// </summary>
        public XFont Font { get; }

        public override double Size => Font.Size;

        public override double UnderlineOffset => _underlineOffset;

        public override double Height => _height;

        public override double LeftPadding => _height / 6f;


        public override double GetWhitespaceWidth(RGraphics graphics)
        {
            if (_whitespaceWidth < 0)
            {
                _whitespaceWidth = graphics.MeasureString(" ", this).Width;
            }
            return _whitespaceWidth;
        }

        /// <summary>
        /// Set font metrics to be cached for the font for future use.
        /// </summary>
        /// <param name="height">the full height of the font</param>
        /// <param name="underlineOffset">the vertical offset of the font underline location from the top of the font.</param>
        internal void SetMetrics(int height, int underlineOffset)
        {
            _height = height;
            _underlineOffset = underlineOffset;
        }
    }
}
