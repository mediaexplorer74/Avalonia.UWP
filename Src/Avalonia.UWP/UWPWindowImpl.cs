using Avalonia.Platform;
using System;
using System.Collections.Generic;
using Avalonia.Input;  // Avalonia 0.5.1 uses InputModifiers, RawMouseEventType, RawMouseEventArgs, RawKeyEventType, RawKeyEventArgs (no Raw namespace)
using Avalonia.Controls;
using System.Reactive.Disposables;
using Avalonia.Input.Raw;
using SkiaSharp;

namespace Avalonia.UWP
{
    public class UWPWindowImpl : IWindowImpl, IEmbeddableWindowImpl, IPopupImpl
    {
        private Windows.UI.Xaml.Controls.SwapChainPanel _hostPanel;
        
        // SkiaSharp rendering surface
        private SkiaSharp.Views.UWP.SKSwapChainPanel _skiaPanel;
        
        // Placeholder for SkiaSharp's GRContext, etc.
        // private GRContext _skiaContext;
        // private SwapChainPanelRenderTarget _renderTarget;

        /// <summary>
        /// Minimal constructor for UWPWindowImpl. Accepts a SwapChainPanel as the rendering host.
        /// </summary>
        /// <param name="hostPanel">The SwapChainPanel to use for rendering.</param>
        public UWPWindowImpl(Windows.UI.Xaml.Controls.SwapChainPanel hostPanel = null)
        {
            _hostPanel = hostPanel;
            // Optionally, initialize rendering here or in Show().
        }

        /// <summary>
        /// Initializes the rendering surface/context. This is where you would set up Skia or Direct2D.
        /// </summary>
        private void InitializeRenderingSurface()
        {
            if (_hostPanel == null)
                return;

            // --- SkiaSharp Integration ---
            // Create an SKSwapChainPanel and add it to the host panel (if not already added)
            if (_skiaPanel == null)
            {
                _skiaPanel = new SkiaSharp.Views.UWP.SKSwapChainPanel();
                _skiaPanel.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Stretch;
                _skiaPanel.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Stretch;
                _skiaPanel.PaintSurface += SkiaPanel_PaintSurface;
                _hostPanel.Children.Clear();
                _hostPanel.Children.Add(_skiaPanel);
            }
            System.Diagnostics.Debug.WriteLine("[UWPWindowImpl] SkiaSharp SKSwapChainPanel initialized and added to host panel.");

            // Subscribe to UWP input events
            _hostPanel.PointerPressed += HostPanel_PointerPressed;
            _hostPanel.PointerMoved += HostPanel_PointerMoved;
            _hostPanel.PointerReleased += HostPanel_PointerReleased;
            _hostPanel.KeyDown += HostPanel_KeyDown;
            _hostPanel.KeyUp += HostPanel_KeyUp;
        }

        // Fields for Avalonia rendering integration
        private Avalonia.Rendering.IRenderRoot _renderRoot;
        private Avalonia.Skia.ISkiaGpuRenderTarget _skiaRenderTarget;

        // SkiaSharp PaintSurface event handler
        private void SkiaPanel_PaintSurface(object sender, SkiaSharp.Views.UWP.SKPaintSurfaceEventArgs e)
        {
            var canvas = e.Surface.Canvas;
            canvas.Clear(SkiaSharp.SKColors.White);

            // --- SkiaSharp Sample Drawing ---
            // Always draw a red rectangle and a green rectangle below it for the sample scenario
            using (var redPaint = new SkiaSharp.SKPaint { Color = SkiaSharp.SKColors.Red, Style = SkiaSharp.SKPaintStyle.Fill })
            {
                canvas.DrawRect(new SkiaSharp.SKRect(50, 50, 200, 150), redPaint);
            }
            using (var greenPaint = new SkiaSharp.SKPaint { Color = SkiaSharp.SKColors.Green, Style = SkiaSharp.SKPaintStyle.Fill })
            {
                canvas.DrawRect(new SkiaSharp.SKRect(50, 210, 200, 150), greenPaint);
            }

            // --- Avalonia Rendering Integration ---
            // If the Avalonia visual tree root is set, render it using Avalonia's immediate renderer
            if (_renderRoot != null)
            {
                // Create a Skia drawing context for Avalonia
                using (var drawingContext = new Avalonia.Skia.SkiaDrawingContext(
                    e.Surface,
                    e.Surface.Canvas,
                    e.Info.Width,
                    e.Info.Height,
                    e.Info.ColorType,
                    e.Info.AlphaType,
                    e.Info.ColorSpace))
                {
                    var renderer = new Avalonia.Rendering.Renderer(_renderRoot, Avalonia.Rendering.RenderLoopPriority.High);
                    renderer.Paint(drawingContext);
                }
            }
        }

        // Helper method to map UWP modifiers to Avalonia InputModifiers (0.5.1)
        private InputModifiers GetModifiers()
        {
            var w = Windows.UI.Core.CoreWindow.GetForCurrentThread();
            InputModifiers modifiers = InputModifiers.None;
            if (w.GetAsyncKeyState(Windows.UI.Core.VirtualKey.Control).HasFlag(Windows.UI.Core.CoreVirtualKeyStates.Down))
                modifiers |= InputModifiers.Control;
            if (w.GetAsyncKeyState(Windows.UI.Core.VirtualKey.Shift).HasFlag(Windows.UI.Core.CoreVirtualKeyStates.Down))
                modifiers |= InputModifiers.Shift;
            if (w.GetAsyncKeyState(Windows.UI.Core.VirtualKey.Menu).HasFlag(Windows.UI.Core.CoreVirtualKeyStates.Down))
                modifiers |= InputModifiers.Alt;
            return modifiers;
        }

        // Stub event handlers for UWP input events
        private void HostPanel_PointerPressed(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            var point = e.GetCurrentPoint(_hostPanel);
            var position = new Avalonia.Point(point.Position.X, point.Position.Y);
            var modifiers = GetModifiers();
            var args = new RawMouseEventArgs(
                null, // TODO: Provide IInputDevice
                (ulong)point.PointerId,
                _hostPanel,
                RawMouseEventType.LeftButtonDown,
                position,
                modifiers
            );
            System.Diagnostics.Debug.WriteLine($"[UWPWindowImpl] PointerPressed: {position} Modifiers: {modifiers}");
            Input?.Invoke(args);
        }

        private void HostPanel_PointerMoved(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            var point = e.GetCurrentPoint(_hostPanel);
            var position = new Avalonia.Point(point.Position.X, point.Position.Y);
            var modifiers = GetModifiers();
            var args = new RawMouseEventArgs(
                null, // TODO: Provide IInputDevice
                (ulong)point.PointerId,
                _hostPanel,
                RawMouseEventType.Move,
                position,
                modifiers
            );
            System.Diagnostics.Debug.WriteLine($"[UWPWindowImpl] PointerMoved: {position} Modifiers: {modifiers}");
            Input?.Invoke(args);
        }

        private void HostPanel_PointerReleased(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            var point = e.GetCurrentPoint(_hostPanel);
            var position = new Avalonia.Point(point.Position.X, point.Position.Y);
            var modifiers = GetModifiers();
            var args = new RawMouseEventArgs(
                null, // TODO: Provide IInputDevice
                (ulong)point.PointerId,
                _hostPanel,
                RawMouseEventType.LeftButtonUp,
                position,
                modifiers
            );
            System.Diagnostics.Debug.WriteLine($"[UWPWindowImpl] PointerReleased: {position} Modifiers: {modifiers}");
            Input?.Invoke(args);
        }

        private void HostPanel_KeyDown(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            var modifiers = GetModifiers();
            var args = new RawKeyEventArgs(
                null, // TODO: Provide IKeyboardDevice
                _hostPanel,
                RawKeyEventType.KeyDown,
                (Key)e.Key, // Simplified mapping
                modifiers
            );
            System.Diagnostics.Debug.WriteLine($"[UWPWindowImpl] KeyDown: {e.Key} Modifiers: {modifiers}");
            Input?.Invoke(args);
        }

        private void HostPanel_KeyUp(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            var modifiers = GetModifiers();
            var args = new RawKeyEventArgs(
                null, // TODO: Provide IKeyboardDevice
                _hostPanel,
                RawKeyEventType.KeyUp,
                (Key)e.Key, // Simplified mapping
                modifiers
            );
            System.Diagnostics.Debug.WriteLine($"[UWPWindowImpl] KeyUp: {e.Key} Modifiers: {modifiers}");
            Input?.Invoke(args);
        }

        /// <summary>
        /// Triggers a redraw of the window. Call this when Avalonia needs to repaint.
        /// </summary>
        public void RequestRedraw()
        {
            // TODO: Invalidate the SwapChainPanel and trigger the rendering pipeline
            System.Diagnostics.Debug.WriteLine("[UWPWindowImpl] RequestRedraw called (stub).");
            Paint?.Invoke(new Avalonia.Rect(0, 0, ClientSize.Width, ClientSize.Height));
        }

        /// <summary>
        /// The host SwapChainPanel for rendering.
        /// </summary>
        public Windows.UI.Xaml.Controls.SwapChainPanel HostPanel => _hostPanel;

        public Size ClientSize { get; set; }

        public double Scaling { get; set; }

        public IEnumerable<object> Surfaces { get; set; }

        public Action<RawInputEventArgs> Input { get; set; }
        public Action<Rect> Paint { get; set; }
        public Action<Size> Resized { get; set; }
        public Action<double> ScalingChanged { get; set; }
        public Action Closed { get; set; }
        public WindowState WindowState { get; set; }
        public Point Position { get; set; }
        public Action<Point> PositionChanged { get; set; }
        public Action Deactivated { get; set; }
        public Action Activated { get; set; }

        public IPlatformHandle Handle { get; set; }

        public Size MaxClientSize { get; set; }
        Action<RawInputEventArgs> ITopLevelImpl.Input { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public event Action LostFocus;

        public void Activate()
        {
            //
        }

        public void BeginMoveDrag()
        {
           // 
        }

        public void BeginResizeDrag(WindowEdge edge)
        {
            //
        }

        public void Dispose()
        {
            //
        }

        public void Hide()
        {
           // 
        }

        public void Invalidate(Rect rect)
        {
            //
        }

        public Point PointToClient(Point point)
        {
            return new Point(0, 0);
        }

        public Point PointToScreen(Point point)
        {
            return new Point(0, 0);
        }

        public void Resize(Size clientSize)
        {
            //
        }

        public void SetCursor(IPlatformHandle cursor)
        {
           // 
        }

        public void SetIcon(IWindowIconImpl icon)
        {
          //
        }

        public void SetInputRoot(IInputRoot inputRoot)
        {
            // Store the Avalonia visual tree root for rendering
            _renderRoot = inputRoot as Avalonia.Rendering.IRenderRoot;
            // Optionally, trigger a redraw
            RequestRedraw();
        }

        public void SetSystemDecorations(bool enabled)
        {
           //
        }

        public void SetTitle(string title)
        {
           //
        }

        public void Show()
        {
            // Minimal viable: just indicate window is shown. In future, start rendering loop here.
            System.Diagnostics.Debug.WriteLine("UWPWindowImpl.Show() called. Window is now visible.");
            // Initialize rendering surface (stub)
            InitializeRenderingSurface();
            // TODO: Start rendering loop and attach Avalonia visual tree to _hostPanel.
        }

        public IDisposable ShowDialog()
        {
            return Disposable.Empty;
        }
    }
}
