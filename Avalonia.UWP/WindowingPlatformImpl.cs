using Avalonia.Input;
using Avalonia.Platform;

namespace Avalonia.UWP
{
    internal class WindowingPlatformImpl : IWindowingPlatform
    {        
        public IEmbeddableWindowImpl CreateEmbeddableWindow()
        {
            return new UWPWindowImpl();
        }

        public IPopupImpl CreatePopup()
        {
            return new UWPWindowImpl();
        }

        public IWindowImpl CreateWindow()
        {
            return new UWPWindowImpl();
        }
    }
}