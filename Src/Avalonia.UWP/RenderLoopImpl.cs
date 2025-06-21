using System;
using Avalonia.Rendering;

namespace Avalonia.UWP
{
    internal class RenderLoopImpl : IRenderLoop
    {
        public event EventHandler<EventArgs> Tick;
    }
}