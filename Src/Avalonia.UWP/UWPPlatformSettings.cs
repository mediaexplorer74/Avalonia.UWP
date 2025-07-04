using System;
using Avalonia.Platform;

namespace Avalonia.UWP
{
    internal class UWPPlatformSettings : IPlatformSettings // Avalonia 0.6.0-compatible
    {
        public Size DoubleClickSize { get; set; } = new Size(4, 4); // UWP default

        public TimeSpan DoubleClickTime { get; set; } = TimeSpan.FromMilliseconds(500); // UWP default
    }
}