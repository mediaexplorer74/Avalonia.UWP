using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.UWP;
using SkiaSharp.Views.UWP;
using System;
using System.Diagnostics;
using Windows.UI.Xaml.Controls;
using static AvaloniaApp.MinimalWindow;
using static System.Net.Mime.MediaTypeNames;

namespace AvaloniaApp
{
    public sealed partial class MainPage : Page
    {
        // SkiaSharp PaintSurface event handler for proof-of-concept rendering
        // Store the parsed root control for Skia rendering
        private SkiaSharp.SKCanvas canvas;
        private UWPWindowImpl _parsedRoot;
        private Avalonia.Controls.Button _counterButton = new Avalonia.Controls.Button();
        private Avalonia.Controls.TextBlock _counterText = new Avalonia.Controls.TextBlock();
        private int _counter = 0;


        public MainPage()
        {
            this.InitializeComponent();

           
            // Wire up Avalonia backend with the SwapChainPanel host (create and show a minimal Avalonia window to test the pipeline):
            // Set the UWP SwapChainPanel as the Avalonia host
            Avalonia.UWP.WindowingPlatformImpl.HostPanel = this.AvaloniaHost;
            // Launch the Avalonia window (MinimalWindow.axaml)
            MinimalWindow win = new AvaloniaApp.MinimalWindow();
            //win.Show();

            InitializeAvaloniaHost();
        }

        /// <summary>
        /// Stub for initializing Avalonia rendering inside the SwapChainPanel.
        /// </summary>
        private void InitializeAvaloniaHost()
        {
            // Step 1: Create and add a SkiaSharp SKSwapChainPanel to the AvaloniaHost (UWP SwapChainPanel)
            var skiaPanel = new SkiaSharp.Views.UWP.SKSwapChainPanel();
            skiaPanel.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Stretch;
            skiaPanel.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Stretch;
            skiaPanel.PaintSurface += SkiaPanel_PaintSurface;
            skiaPanel.PointerPressed += SkiaPanel_PointerPressed;
            this.AvaloniaHost.Children.Clear();
            this.AvaloniaHost.Children.Add(skiaPanel);

            // Step 2: (Optional) Still create Avalonia UWPWindowImpl and bind it to the SwapChainPanel for future integration
            var uwpWindow = new Avalonia.UWP.UWPWindowImpl(this.AvaloniaHost);

            // Step 3: Parse MinimalWindow.axaml at runtime and create the control tree
            //tune path as needed 
            var axamlPath = System.IO.Path.Combine(Windows.ApplicationModel.Package.Current.InstalledLocation.Path, "MinimalWindow.axaml");
            _parsedRoot = AxamlRuntimeLoader.LoadMinimalWindowFromAxaml(axamlPath);
            FindNamedControls(_parsedRoot as IControl);
            if (_counterText != null)
                _counterText.Text = $"Count: {_counter}";

            // Step 4: Set the window content as the render root
            uwpWindow.SetInputRoot(_parsedRoot as IInputRoot);

            // Step 5: Optionally, trigger an initial redraw
            uwpWindow.RequestRedraw();
        }//InitializeAvaloniaHost


        // SkiaPanel_PaintSurface
        public /*override*/ void SkiaPanel_PaintSurface(object sender, SKPaintGLSurfaceEventArgs e)
        {
            //SkiaSharp.SKCanvas 
            canvas = e.Surface.Canvas;
            canvas.Clear(SkiaSharp.SKColors.LightCyan);
            if (_parsedRoot == null)
                return;
            // Recursively render the parsed controls
            RenderControlRecursive(_parsedRoot as IControl, canvas, 0, 0);
        }


        // Helper to recursively render controls
        private void RenderControlRecursive(IControl ctrl, SkiaSharp.SKCanvas canvas, double offsetX, double offsetY)
        {
            Debug.WriteLine("[!] RenderControlRecursive");
            if (ctrl == null) return;
            // Track the button's screen bounds for hit-testing
            if (ctrl == _counterButton)
            {
                _buttonBounds = new SkiaSharp.SKRect((float)offsetX, (float)offsetY, (float)(offsetX + _counterButton.Width), (float)(offsetY + _counterButton.Height));
            }
            switch (ctrl)
            {
                case Avalonia.Controls.Canvas canvasCtrl:
                    foreach (AvaloniaObject child in canvasCtrl.Children)
                    {
                        double left = Avalonia.Controls.Canvas.GetLeft(child);
                        double top = Avalonia.Controls.Canvas.GetTop(child);
                        RenderControlRecursive(child as IControl, canvas, offsetX + left, offsetY + top);
                    }
                    break;
                case Avalonia.Controls.Shapes.Rectangle rect:
                    using (var paint = new SkiaSharp.SKPaint { Color = ToSKColor(((Avalonia.Media.SolidColorBrush)rect.Fill)?.Color ?? Avalonia.Media.Colors.Transparent), Style = SkiaSharp.SKPaintStyle.Fill })
                    {
                        canvas.DrawRect((float)offsetX, (float)offsetY, (float)rect.Width, (float)rect.Height, paint);
                    }
                    if (rect.Stroke != null && rect.StrokeThickness > 0)
                    {
                        using (var paint = new SkiaSharp.SKPaint { Color = ToSKColor(((Avalonia.Media.SolidColorBrush)rect.Stroke).Color), Style = SkiaSharp.SKPaintStyle.Stroke, StrokeWidth = (float)rect.StrokeThickness })
                        {
                            canvas.DrawRect((float)offsetX, (float)offsetY, (float)rect.Width, (float)rect.Height, paint);
                        }
                    }
                    break;
                case Avalonia.Controls.Shapes.Ellipse ellipse:
                    using (var paint = new SkiaSharp.SKPaint { Color = ToSKColor(((Avalonia.Media.SolidColorBrush)ellipse.Fill)?.Color ?? Avalonia.Media.Colors.Transparent), Style = SkiaSharp.SKPaintStyle.Fill })
                    {
                        canvas.DrawOval((float)(offsetX + ellipse.Width / 2), (float)(offsetY + ellipse.Height / 2), (float)(ellipse.Width / 2), (float)(ellipse.Height / 2), paint);
                    }
                    if (ellipse.Stroke != null && ellipse.StrokeThickness > 0)
                    {
                        using (var paint = new SkiaSharp.SKPaint { Color = ToSKColor(((Avalonia.Media.SolidColorBrush)ellipse.Stroke).Color), Style = SkiaSharp.SKPaintStyle.Stroke, StrokeWidth = (float)ellipse.StrokeThickness })
                        {
                            canvas.DrawOval((float)(offsetX + ellipse.Width / 2), (float)(offsetY + ellipse.Height / 2), (float)(ellipse.Width / 2), (float)(ellipse.Height / 2), paint);
                        }
                    }
                    break;
                case Avalonia.Controls.Shapes.Line line:
                    using (var paint = new SkiaSharp.SKPaint { Color = ToSKColor(((Avalonia.Media.SolidColorBrush)line.Stroke)?.Color ?? Avalonia.Media.Colors.Black), Style = SkiaSharp.SKPaintStyle.Stroke, StrokeWidth = (float)line.StrokeThickness })
                    {
                        canvas.DrawLine((float)(offsetX + line.StartPoint.X), (float)(offsetY + line.StartPoint.Y), (float)(offsetX + line.EndPoint.X), (float)(offsetY + line.EndPoint.Y), paint);
                    }
                    break;
                case Avalonia.Controls.StackPanel stack:
                    double currX = offsetX, currY = offsetY;
                    foreach (AvaloniaObject child in stack.Children)
                    {
                        RenderControlRecursive(child as IControl, canvas, currX, currY);
                        if (stack.Orientation == Avalonia.Controls.Orientation.Horizontal)
                            currX += (child as IControl)?.Bounds.Width ?? 0;// + stack.Spacing;
                        else
                            currY += (child as IControl)?.Bounds.Height ?? 0;// + stack.Spacing;
                    }
                    break;
                case Avalonia.Controls.Button btn:
                    using (var paint = new SkiaSharp.SKPaint { Color = SkiaSharp.SKColors.LightGray, Style = SkiaSharp.SKPaintStyle.Fill })
                    {
                        canvas.DrawRect((float)offsetX, (float)offsetY, (float)btn.Width, (float)btn.Height, paint);
                    }
                    using (var paint = new SkiaSharp.SKPaint { Color = SkiaSharp.SKColors.Black, TextSize = 16 })
                    {
                        canvas.DrawText(btn.Content?.ToString() ?? "Button", (float)offsetX + 10, (float)offsetY + 20, paint);
                    }
                    break;
                case Avalonia.Controls.TextBlock tb:
                    using (var paint = new SkiaSharp.SKPaint { Color = SkiaSharp.SKColors.Black, TextSize = (float)tb.FontSize > 0 ? (float)tb.FontSize : 18 })
                    {
                        canvas.DrawText(tb.Text ?? "", (float)offsetX, (float)offsetY + 18, paint);
                    }
                    break;
                default:
                    // Unhandled control type
                    break;
            }
        }

        private SkiaSharp.SKColor ToSKColor(Avalonia.Media.Color color)
        {
            return new SkiaSharp.SKColor(color.R, color.G, color.B, color.A);
        }

        // Recursively find controls by name
        private void FindNamedControls(IControl ctrl)
        {
            if (ctrl == null) return;
            if (ctrl is Avalonia.Controls.Button btn)// && btn.Name == "CounterButton")
                _counterButton = btn;
            if (ctrl is Avalonia.Controls.TextBlock tb)// && tb.Name == "CounterText")
                _counterText = tb;
            if (ctrl is IContentControl contentControl && contentControl.Content is UWPWindowImpl content)
                FindNamedControls(content as IControl);
            if (ctrl is IPanel panel)
            {
                foreach (var child in panel.Children)
                    FindNamedControls(child);
            }
        }

        // Store button bounds for hit-testing
        private SkiaSharp.SKRect _buttonBounds;

        // Handle pointer pressed for button click
        private void SkiaPanel_PointerPressed(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            var pt = e.GetCurrentPoint((Windows.UI.Xaml.UIElement)sender).Position;
            var skPt = new SkiaSharp.SKPoint((float)pt.X, (float)pt.Y);
            if (_buttonBounds.Contains(skPt))
            {
                _counter++;
                if (_counterText != null)
                {
                    _counterText.Text = $"Count: {_counter}";
                    _counterText.Background = new Avalonia.Media.SolidColorBrush(Avalonia.Media.Colors.Green); //TEST
                    _counterText.FontSize= 24; //TEST
                    
                }
                ((SkiaSharp.Views.UWP.SKSwapChainPanel)sender).Invalidate();
               
            }
        }

    }
}
