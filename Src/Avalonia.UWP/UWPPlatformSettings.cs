using System;
using Avalonia.Platform;

namespace Avalonia.UWP
{
    internal class UWPPlatformSettings : IPlatformSettings
    {
        public Size DoubleClickSize { get; set; }

        public TimeSpan DoubleClickTime { get; set; }
    }
}