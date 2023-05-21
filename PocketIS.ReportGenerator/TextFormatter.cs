using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using PdfSharp.Pdf.IO;

namespace PocketIS.ReportGenerator
{
    namespace PdfSharp.Drawing.Layout
    {
        /// <summary>
        /// Represents a very simple text formatter.
        /// </summary>
        public class TextFormatter
        {
            private enum BlockType
            {
                Text,
                LineBreak,
            }

            private XFont _font;
            private double _lineSpace;
            private double _cyAscent;
            private double _cyDescent;
            private double _spaceWidth;

            private readonly XGraphics _gfx;
            private readonly List<Block> _blocks = new List<Block>();

            /// <summary>
            /// Initializes a new instance of the <see cref="TextFormatter"/> class.
            /// </summary>
            public TextFormatter(XGraphics gfx)
            {
                _gfx = gfx ?? throw new ArgumentNullException(nameof(gfx));
            }

            /// <summary>
            /// Gets or sets the text.
            /// </summary>
            /// <value>The text.</value>
            public string Text { get; set; }

            /// <summary>
            /// Gets or sets the font.
            /// </summary>
            public XFont Font
            {
                get => _font;
                set
                {
                    _font = value ?? throw new ArgumentNullException(nameof(value));

                    _lineSpace = _font.GetHeight();
                    _cyAscent = _lineSpace * _font.CellAscent / _font.CellSpace;
                    _cyDescent = _lineSpace * _font.CellDescent / _font.CellSpace;

                    // HACK in XTextFormatter
                    _spaceWidth = _gfx.MeasureString("x x", value).Width;
                    _spaceWidth -= _gfx.MeasureString("xx", value).Width;
                }
            }


            /// <summary>
            /// Gets or sets the bounding box of the layout.
            /// </summary>
            public XRect LayoutRectangle { get; set; }

            /// <summary>
            /// Gets or sets the alignment of the text.
            /// </summary>
            public XParagraphAlignment Alignment { get; set; } = XParagraphAlignment.Left;

            /// <summary>
            /// Measures the text.
            /// </summary>
            /// <param name="text">The text to be drawn.</param>
            /// <param name="font">The font.</param>
            /// <param name="layoutRectangle">The layout rectangle.</param>
            public XSize MeasureString(string text, XFont font, XRect layoutRectangle)
            {
                Text = text ?? throw new ArgumentNullException(nameof(text));
                Font = font ?? throw new ArgumentNullException(nameof(font));
                LayoutRectangle = layoutRectangle;

                if (text.Length == 0)
                    return new XSize();

                CreateBlocks();
                var size = CreateLayout();

                return size;
            }

            /// <summary>
            /// Draws the text.
            /// </summary>
            /// <param name="text">The text to be drawn.</param>
            /// <param name="font">The font.</param>
            /// <param name="brush">The text brush.</param>
            /// <param name="layoutRectangle">The layout rectangle.</param>
            public void DrawString(string text, XFont font, XBrush brush, XRect layoutRectangle)
            {
                DrawString(text, font, brush, layoutRectangle, XStringFormats.TopLeft);
            }

            /// <summary>
            /// Draws the text.
            /// </summary>
            /// <param name="text">The text to be drawn.</param>
            /// <param name="font">The font.</param>
            /// <param name="brush">The text brush.</param>
            /// <param name="layoutRectangle">The layout rectangle.</param>
            /// <param name="format">The format. Must be <c>XStringFormat.TopLeft</c></param>
            public void DrawString(string text, XFont font, XBrush brush, XRect layoutRectangle, XStringFormat format)
            {
                if (brush == null)
                    throw new ArgumentNullException(nameof(brush));
                if (format.Alignment != XStringAlignment.Near || format.LineAlignment != XLineAlignment.Near)
                    throw new ArgumentException("Only TopLeft alignment is currently implemented.");

                Text = text ?? throw new ArgumentNullException(nameof(text));
                Font = font ?? throw new ArgumentNullException(nameof(font));
                LayoutRectangle = layoutRectangle;

                if (text.Length == 0)
                    return;

                CreateBlocks();

                CreateLayout();

                var dx = layoutRectangle.Location.X;
                var dy = layoutRectangle.Location.Y + _cyAscent;
                var count = _blocks.Count;
                for (var idx = 0; idx < count; idx++)
                {
                    var block = _blocks[idx];
                    if (block.Stop)
                        break;
                    if (block.Type == BlockType.LineBreak)
                        continue;
                    _gfx.DrawString(block.Text, font, brush, dx + block.Location.X, dy + block.Location.Y);
                }
            }

            private void CreateBlocks()
            {
                _blocks.Clear();
                var length = Text.Length;
                var inNonWhiteSpace = false;
                int startIndex = 0, blockLength = 0;
                for (var idx = 0; idx < length; idx++)
                {
                    var ch = Text[idx];

                    // Treat CR and CRLF as LF
                    if (ch == Chars.CR)
                    {
                        if (idx < length - 1 && Text[idx + 1] == Chars.LF)
                            idx++;
                        ch = Chars.LF;
                    }
                    if (ch == Chars.LF)
                    {
                        if (blockLength != 0)
                        {
                            var token = Text.Substring(startIndex, blockLength);
                            _blocks.Add(new Block(token, BlockType.Text, _gfx.MeasureString(token, _font).Width));
                        }
                        startIndex = idx + 1;
                        blockLength = 0;
                        _blocks.Add(new Block(BlockType.LineBreak));
                    }
                    else if (char.IsWhiteSpace(ch))
                    {
                        if (inNonWhiteSpace)
                        {
                            var token = Text.Substring(startIndex, blockLength);
                            _blocks.Add(new Block(token, BlockType.Text,
                              _gfx.MeasureString(token, _font).Width));
                            startIndex = idx + 1;
                            blockLength = 0;
                        }
                        else
                        {
                            blockLength++;
                        }
                    }
                    else
                    {
                        inNonWhiteSpace = true;
                        blockLength++;
                    }
                }
                if (blockLength != 0)
                {
                    var token = Text.Substring(startIndex, blockLength);
                    _blocks.Add(new Block(token, BlockType.Text,
                      _gfx.MeasureString(token, _font).Width));
                }
            }

            private XSize CreateLayout()
            {
                var textSize = new XSize();
                var rectWidth = LayoutRectangle.Width;
                var rectHeight = LayoutRectangle.Height - _cyAscent - _cyDescent;
                var firstIndex = 0;
                double x = 0, y = 0;
                var count = _blocks.Count;
                for (var idx = 0; idx < count; idx++)
                {
                    var block = _blocks[idx];
                    if (block.Type == BlockType.LineBreak)
                    {
                        if (Alignment == XParagraphAlignment.Justify)
                            _blocks[firstIndex].Alignment = XParagraphAlignment.Left;
                        AlignLine(firstIndex, idx - 1, rectWidth);
                        firstIndex = idx + 1;
                        x = 0;
                        y += _lineSpace;
                    }
                    else
                    {
                        var width = block.Width;
                        if ((x + width <= rectWidth || Math.Abs(x) < 0.0001) && block.Type != BlockType.LineBreak)
                        {
                            textSize.Width = Math.Max(textSize.Width, x + width);
                            block.Location = new XPoint(x, y);
                            x += width + _spaceWidth;
                        }
                        else
                        {
                            AlignLine(firstIndex, idx - 1, rectWidth);
                            firstIndex = idx;
                            y += _lineSpace;
                            if (y > rectHeight)
                            {
                                block.Stop = true;
                                break;
                            }
                            block.Location = new XPoint(0, y);
                            x = width + _spaceWidth;
                        }
                    }
                }
                if (firstIndex < count && Alignment != XParagraphAlignment.Justify)
                    AlignLine(firstIndex, count - 1, rectWidth);

                textSize.Height = y + _lineSpace;

                return textSize;
            }

            /// <summary>
            /// Align center, right or justify.
            /// </summary>
            private void AlignLine(int firstIndex, int lastIndex, double layoutWidth)
            {
                var blockAlignment = _blocks[firstIndex].Alignment;
                if (Alignment == XParagraphAlignment.Left || blockAlignment == XParagraphAlignment.Left)
                    return;

                var count = lastIndex - firstIndex + 1;
                if (count == 0)
                    return;

                var totalWidth = -_spaceWidth;
                for (var idx = firstIndex; idx <= lastIndex; idx++)
                    totalWidth += _blocks[idx].Width + _spaceWidth;

                var dx = Math.Max(layoutWidth - totalWidth, 0);
                //Debug.Assert(dx >= 0);
                if (Alignment != XParagraphAlignment.Justify)
                {
                    if (Alignment == XParagraphAlignment.Center)
                        dx /= 2;
                    for (var idx = firstIndex; idx <= lastIndex; idx++)
                    {
                        var block = _blocks[idx];
                        block.Location += new XSize(dx, 0);
                    }
                }
                else if (count > 1) // case: justify
                {
                    dx /= count - 1;
                    for (int idx = firstIndex + 1, i = 1; idx <= lastIndex; idx++, i++)
                    {
                        var block = _blocks[idx];
                        block.Location += new XSize(dx * i, 0);
                    }
                }
            }

            /// <summary>
            /// Represents a single word.
            /// </summary>
            private class Block
            {
                /// <summary>
                /// Initializes a new instance of the <see cref="Block"/> class.
                /// </summary>
                /// <param name="text">The text of the block.</param>
                /// <param name="type">The type of the block.</param>
                /// <param name="width">The width of the text.</param>
                public Block(string text, BlockType type, double width)
                {
                    Text = text;
                    Type = type;
                    Width = width;
                }

                /// <summary>
                /// Initializes a new instance of the <see cref="Block"/> class.
                /// </summary>
                /// <param name="type">The type.</param>
                public Block(BlockType type)
                {
                    Type = type;
                }

                /// <summary>
                /// The text represented by this block.
                /// </summary>
                public readonly string Text;

                /// <summary>
                /// The type of the block.
                /// </summary>
                public readonly BlockType Type;

                /// <summary>
                /// The width of the text.
                /// </summary>
                public readonly double Width;

                /// <summary>
                /// The location relative to the upper left corner of the layout rectangle.
                /// </summary>
                public XPoint Location;

                /// <summary>
                /// The alignment of this line.
                /// </summary>
                public XParagraphAlignment Alignment;

                /// <summary>
                /// A flag indicating that this is the last bock that fits in the layout rectangle.
                /// </summary>
                public bool Stop;
            }
        }
    }
}
