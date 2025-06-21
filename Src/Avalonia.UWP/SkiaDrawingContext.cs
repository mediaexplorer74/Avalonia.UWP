using SkiaSharp;
using System;

namespace Avalonia.UWP
{
    internal class SkiaDrawingContext : IDisposable
    {
        private SKSurface surface;
        private SKCanvas canvas;
        private int width;
        private int height;
        private SKColorType colorType;
        private SKAlphaType alphaType;
        private SKColorSpace colorSpace;

        public SkiaDrawingContext(SKSurface surface, SKCanvas canvas, int width, int height, SKColorType colorType, SKAlphaType alphaType, SKColorSpace colorSpace)
        {
            this.surface = surface;
            this.canvas = canvas;
            this.width = width;
            this.height = height;
            this.colorType = colorType;
            this.alphaType = alphaType;
            this.colorSpace = colorSpace;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}