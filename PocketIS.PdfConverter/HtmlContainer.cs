﻿using PdfSharp.Drawing;
using PocketIS.PdfConverter.Adapters;
using PocketIS.PdfConverter.Utilities;
using System;
using VetCV.HtmlRendererCore.Adapters.Entities;
using VetCV.HtmlRendererCore.Core;
using VetCV.HtmlRendererCore.Core.Entities;
using VetCV.HtmlRendererCore.Core.Utils;

namespace PocketIS.PdfConverter
{
    /// <summary>
    /// Low level handling of Html Renderer logic, this class is used by <see cref="PdfGenerator"/>.
    /// </summary>
    /// <seealso cref="HtmlContainerInt"/>
    public sealed class HtmlContainer : IDisposable
    {
        /// <summary>
        /// Init.
        /// </summary>
        public HtmlContainer()
        {
            HtmlContainerInt = new HtmlContainerInt(PdfSharpAdapter.Instance)
            {
                AvoidAsyncImagesLoading = true,
                AvoidImagesLateLoading = true
            };
        }

        /// <summary>
        /// Raised when the set html document has been fully loaded.<br/>
        /// Allows manipulation of the html dom, scroll position, etc.
        /// </summary>
        public event EventHandler LoadComplete
        {
            add => HtmlContainerInt.LoadComplete += value;
            remove => HtmlContainerInt.LoadComplete -= value;
        }

        /// <summary>
        /// Raised when an error occurred during html rendering.<br/>
        /// </summary>
        /// <remarks>
        /// There is no guarantee that the event will be raised on the main thread, it can be raised on thread-pool thread.
        /// </remarks>
        public event EventHandler<HtmlRenderErrorEventArgs> RenderError
        {
            add => HtmlContainerInt.RenderError += value;
            remove => HtmlContainerInt.RenderError -= value;
        }

        /// <summary>
        /// Raised when a stylesheet is about to be loaded by file path or URI by link element.<br/>
        /// This event allows to provide the stylesheet manually or provide new source (file or Uri) to load from.<br/>
        /// If no alternative data is provided the original source will be used.<br/>
        /// </summary>
        public event EventHandler<HtmlStylesheetLoadEventArgs> StylesheetLoad
        {
            add => HtmlContainerInt.StylesheetLoad += value;
            remove => HtmlContainerInt.StylesheetLoad -= value;
        }

        /// <summary>
        /// Raised when an image is about to be loaded by file path or URI.<br/>
        /// This event allows to provide the image manually, if not handled the image will be loaded from file or download from URI.
        /// </summary>
        public event EventHandler<HtmlImageLoadEventArgs> ImageLoad
        {
            add => HtmlContainerInt.ImageLoad += value;
            remove => HtmlContainerInt.ImageLoad -= value;
        }

        /// <summary>
        /// The internal core html container
        /// </summary>
        internal HtmlContainerInt HtmlContainerInt { get; }

        /// <summary>
        /// the parsed stylesheet data used for handling the html
        /// </summary>
        public CssData CssData => HtmlContainerInt.CssData;

        /// <summary>
        /// Gets or sets a value indicating if anti-aliasing should be avoided for geometry like backgrounds and borders (default - false).
        /// </summary>
        public bool AvoidGeometryAntialias
        {
            get => HtmlContainerInt.AvoidGeometryAntialias;
            set => HtmlContainerInt.AvoidGeometryAntialias = value;
        }

        /// <summary>
        /// The scroll offset of the html.<br/>
        /// This will adjust the rendered html by the given offset so the content will be "scrolled".<br/>
        /// </summary>
        /// <example>
        /// Element that is rendered at location (50,100) with offset of (0,200) will not be rendered as it
        /// will be at -100 therefore outside the client rectangle.
        /// </example>
        public XPoint ScrollOffset
        {
            get => Utils.Convert(HtmlContainerInt.ScrollOffset);
            set => HtmlContainerInt.ScrollOffset = Utils.Convert(value);
        }

        /// <summary>
        /// The top-left most location of the rendered html.<br/>
        /// This will offset the top-left corner of the rendered html.
        /// </summary>
        public XPoint Location
        {
            get => Utils.Convert(HtmlContainerInt.Location);
            set => HtmlContainerInt.Location = Utils.Convert(value);
        }

        /// <summary>
        /// The max width and height of the rendered html.<br/>
        /// The max width will effect the html layout wrapping lines, resize images and tables where possible.<br/>
        /// The max height does NOT effect layout, but will not render outside it (clip).<br/>
        /// <see cref="ActualSize"/> can be exceed the max size by layout restrictions (unwrappable line, set image size, etc.).<br/>
        /// Set zero for unlimited (width\height separately).<br/>
        /// </summary>
        public XSize MaxSize
        {
            get => Utils.Convert(HtmlContainerInt.MaxSize);
            set => HtmlContainerInt.MaxSize = Utils.Convert(value);
        }

        /// <summary>
        /// The actual size of the rendered html (after layout)
        /// </summary>
        public XSize ActualSize
        {
            get => Utils.Convert(HtmlContainerInt.ActualSize);
            internal set => HtmlContainerInt.ActualSize = Utils.Convert(value);
        }

        public XSize PageSize
        {
            get => new XSize(HtmlContainerInt.PageSize.Width, HtmlContainerInt.PageSize.Height);
            set => HtmlContainerInt.PageSize = new RSize(value.Width, value.Height);
        }

        /// <summary>
        /// the top margin between the page start and the text
        /// </summary>
        public int MarginTop
        {
            get => HtmlContainerInt.MarginTop;
            set
            {
                if (value > -1)
                    HtmlContainerInt.MarginTop = value;
            }
        }

        /// <summary>
        /// the bottom margin between the page end and the text
        /// </summary>
        public int MarginBottom
        {
            get => HtmlContainerInt.MarginBottom;
            set
            {
                if (value > -1)
                    HtmlContainerInt.MarginBottom = value;
            }
        }

        /// <summary>
        /// the left margin between the page start and the text
        /// </summary>
        public int MarginLeft
        {
            get => HtmlContainerInt.MarginLeft;
            set
            {
                if (value > -1)
                    HtmlContainerInt.MarginLeft = value;
            }
        }

        /// <summary>
        /// the right margin between the page end and the text
        /// </summary>
        public int MarginRight
        {
            get => HtmlContainerInt.MarginRight;
            set
            {
                if (value > -1)
                    HtmlContainerInt.MarginRight = value;
            }
        }

        /// <summary>
        /// Set all 4 margins to the given value.
        /// </summary>
        /// <param name="value"></param>
        public void SetMargins(int value)
        {
            if (value > -1)
                HtmlContainerInt.SetMargins(value);
        }

        /// <summary>
        /// Init with optional document and stylesheet.
        /// </summary>
        /// <param name="htmlSource">the html to init with, init empty if not given</param>
        /// <param name="baseCssData">optional: the stylesheet to init with, init default if not given</param>
        public void SetHtml(string htmlSource, CssData baseCssData = null)
        {
            HtmlContainerInt.SetHtml(htmlSource, baseCssData);
        }

        /// <summary>
        /// Get html from the current DOM tree with style if requested.
        /// </summary>
        /// <param name="styleGen">Optional: controls the way styles are generated when html is generated (default: <see cref="HtmlGenerationStyle.Inline"/>)</param>
        /// <returns>generated html</returns>
        public string GetHtml(HtmlGenerationStyle styleGen = HtmlGenerationStyle.Inline)
        {
            return HtmlContainerInt.GetHtml(styleGen);
        }

        /// <summary>
        /// Get attribute value of element at the given x,y location by given key.<br/>
        /// If more than one element exist with the attribute at the location the inner most is returned.
        /// </summary>
        /// <param name="location">the location to find the attribute at</param>
        /// <param name="attribute">the attribute key to get value by</param>
        /// <returns>found attribute value or null if not found</returns>
        public string GetAttributeAt(XPoint location, string attribute)
        {
            return HtmlContainerInt.GetAttributeAt(Utils.Convert(location), attribute);
        }

        /// <summary>
        /// Measures the bounds of box and children, recursively.
        /// </summary>
        /// <param name="g">Device context to draw</param>
        public void PerformLayout(XGraphics g)
        {
            ArgChecker.AssertArgNotNull(g, "g");

            using (var ig = new GraphicsAdapter(g))
            {
                HtmlContainerInt.PerformLayout(ig);
            }
        }

        /// <summary>
        /// Render the html using the given device.
        /// </summary>
        /// <param name="g">the device to use to render</param>
        public void PerformPaint(XGraphics g)
        {
            ArgChecker.AssertArgNotNull(g, "g");

            using (var ig = new GraphicsAdapter(g))
            {
                HtmlContainerInt.PerformPaint(ig);
            }
        }

        public void Dispose()
        {
            HtmlContainerInt.Dispose();
        }
    }
}
