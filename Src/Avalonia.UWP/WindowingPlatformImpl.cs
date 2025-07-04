using Avalonia.Input;
using Avalonia.Platform;
using Windows.UI.Xaml.Controls;

namespace Avalonia.UWP
{
    public class WindowingPlatformImpl : IWindowingPlatform // Avalonia 0.6.0-compatible
    {        
        /// <summary>
        /// Host panel to use for window creation. Set this from the app before creating windows.
        /// </summary>
        public static SwapChainPanel HostPanel { get; set; }

        public IEmbeddableWindowImpl CreateEmbeddableWindow()
        {
            return new UWPWindowImpl(HostPanel);
        }

        public IPopupImpl CreatePopup()
        {
            return new UWPWindowImpl(HostPanel);
        }

        public IWindowImpl CreateWindow()
        {
            return new UWPWindowImpl(HostPanel);
        }
    }
}