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

            }
        }

        public IWindowIconImpl LoadIcon(string fileName)
        {
            return new IconStub();
        }

        public IWindowIconImpl LoadIcon(Stream stream)
        {
            return new IconStub();
        }

        public IWindowIconImpl LoadIcon(IBitmapImpl bitmap)
        {
            return new IconStub();
        }
    }
}