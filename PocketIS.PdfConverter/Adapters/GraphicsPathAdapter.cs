﻿using PdfSharp.Drawing;
using System;
using VetCV.HtmlRendererCore.Adapters;
using VetCV.HtmlRendererCore.Adapters.Entities;

namespace PocketIS.PdfConverter.Adapters
{
    /// <summary>
    /// Adapter for WinForms graphics path object for core.
    /// </summary>
    internal sealed class GraphicsPathAdapter : RGraphicsPath
    {
        /// <summary>
        /// the last point added to the path to begin next segment from
        /// </summary>
        private RPoint _lastPoint;

        /// <summary>
        /// The actual PdfSharp graphics path instance.
        /// </summary>
        public XGraphicsPath GraphicsPath { get; } = new XGraphicsPath();

        public override void Start(double x, double y)
        {
            _lastPoint = new RPoint(x, y);
        }

        public override void LineTo(double x, double y)
        {
            GraphicsPath.AddLine((float)_lastPoint.X, (float)_lastPoint.Y, (float)x, (float)y);
            _lastPoint = new RPoint(x, y);
        }

        public override void ArcTo(double x, double y, double size, Corner corner)
        {
            var left = (float)(Math.Min(x, _lastPoint.X) - (corner == Corner.TopRight || corner == Corner.BottomRight ? size : 0));
            var top = (float)(Math.Min(y, _lastPoint.Y) - (corner == Corner.BottomLeft || corner == Corner.BottomRight ? size : 0));
            GraphicsPath.AddArc(left, top, (float)size * 2, (float)size * 2, GetStartAngle(corner), 90);
            _lastPoint = new RPoint(x, y);
        }

        public override void Dispose()
        { }

        /// <summary>
        /// Get arc start angle for the given corner.
        /// </summary>
        private static int GetStartAngle(Corner corner)
        {
            var startAngle = corner switch
            {
                Corner.TopLeft => 180,
                Corner.TopRight => 270,
                Corner.BottomLeft => 90,
                Corner.BottomRight => 0,
                _ => throw new ArgumentOutOfRangeException(nameof(corner)),
            };
            return startAngle;
        }
    }
}
