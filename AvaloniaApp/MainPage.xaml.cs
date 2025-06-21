using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x419

namespace AvaloniaApp
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            // Wire up Avalonia backend with the SwapChainPanel host
            Avalonia.UWP.WindowingPlatformImpl.HostPanel = this.AvaloniaHost;

            // Create and show a minimal Avalonia window to test the pipeline.
            var win = new AvaloniaApp.MinimalWindow();
            win.Show();

            InitializeAvaloniaHost();
        }

        /// <summary>
        /// Stub for initializing Avalonia rendering inside the SwapChainPanel.
        /// </summary>
        private void InitializeAvaloniaHost()
        {
            // TODO: When Avalonia UWP backend is implemented, initialize rendering here.
            // For example, create a Skia/Direct2D surface and bind it to AvaloniaHost.
            // This is a placeholder for future Avalonia integration.
        }
    }
}
