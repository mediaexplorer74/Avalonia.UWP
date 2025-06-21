
using Avalonia.UWP;
using System;

namespace Avalonia.Rendering
{
    internal class Renderer
    {
        private IRenderRoot renderRoot;
        private object high;

        public Renderer(IRenderRoot renderRoot, object high)
        {
            this.renderRoot = renderRoot;
            this.high = high;
        }

        internal void Paint(SkiaDrawingContext drawingContext)
        {
            throw new NotImplementedException();
        }
    }
}