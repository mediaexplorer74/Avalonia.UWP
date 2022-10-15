using Avalonia.Platform;
using System;
using System.Collections.Generic;
using Avalonia.Input;
using Avalonia.Input.Raw;
using Avalonia.Controls;
using System.Reactive.Disposables;

namespace Avalonia.UWP
{
    public class UWPWindowImpl : IWindowImpl, IEmbeddableWindowImpl, IPopupImpl
    {
        public UWPWindowImpl()
        {
            //
        }

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
           //
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
           //
        }

        public IDisposable ShowDialog()
        {
            return Disposable.Empty;
        }
    }
}
