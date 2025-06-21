using System.IO;
using Avalonia.Platform;

namespace Avalonia.UWP
{
    public class PlatformIconLoaderImpl : IPlatformIconLoader
    {
       public class IconStub : IWindowIconImpl
        {
            public void Save(Stream outputStream)
            {
                // UWP does not support saving window icons
            }
        }

        public IWindowIconImpl LoadIcon(string fileName)
        {
            // UWP does not support custom window icons
            return new IconStub();
        }

        public IWindowIconImpl LoadIcon(Stream stream)
        {
            // UWP does not support custom window icons
            return new IconStub();
        }

        public IWindowIconImpl LoadIcon(IBitmapImpl bitmap)
        {
            // UWP does not support custom window icons
            return new IconStub();
        }
    }
}