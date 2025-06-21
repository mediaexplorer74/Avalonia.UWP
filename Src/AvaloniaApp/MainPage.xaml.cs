using Windows.UI.Xaml.Controls;

namespace AvaloniaApp
{
    public sealed partial class MainPage : Page
    {
        
        public MainPage()
        {
            this.InitializeComponent();

           
            // Wire up Avalonia backend with the SwapChainPanel host:
            // Create and show a minimal Avalonia window to test the pipeline:
            // Set the UWP SwapChainPanel as the Avalonia host
            Avalonia.UWP.WindowingPlatformImpl.HostPanel = this.AvaloniaHost;
            // Launch the Avalonia window (MinimalWindow.axaml)
            MinimalWindow win = new AvaloniaApp.MinimalWindow();
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
