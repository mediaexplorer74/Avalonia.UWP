using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Input.Raw;
using Avalonia.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Avalonia.UWP
{
    public class UWPPopupImpl : IPopupImpl
    {
        public void Dispose() { }
        public void Show() { /* UWP popups not implemented */ }
        public void Hide() { /* UWP popups not implemented */ }
        public void SetPosition(Avalonia.Point point) { /* UWP popups not implemented */ }

            public void BeginMoveDrag()
            {
                throw new NotImplementedException();
            }

            public void BeginResizeDrag(WindowEdge edge)
            {
                throw new NotImplementedException();
            }

            public void Activate()
            {
                throw new NotImplementedException();
            }

            public void Resize(Size clientSize)
            {
                throw new NotImplementedException();
            }

            public void Invalidate(Rect rect)
            {
                throw new NotImplementedException();
            }

            public void SetInputRoot(IInputRoot inputRoot)
            {
                throw new NotImplementedException();
            }

            public global::Avalonia.Point PointToClient(global::Avalonia.Point point)
            {
                throw new NotImplementedException();
            }

            public global::Avalonia.Point PointToScreen(global::Avalonia.Point point)
            {
                throw new NotImplementedException();
            }

            public void SetCursor(IPlatformHandle cursor)
            {
                throw new NotImplementedException();
            }

            public IPlatformHandle Handle => null;
        public double Scaling => 1.0;

            public global::Avalonia.Point Position { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
            public Action<global::Avalonia.Point> PositionChanged { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
            public Action Deactivated { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
            public Action Activated { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

            public Size MaxClientSize => throw new NotImplementedException();

            public Size ClientSize => throw new NotImplementedException();

            public IEnumerable<object> Surfaces => throw new NotImplementedException();

            public Action<RawInputEventArgs> Input { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
            public Action<Rect> Paint { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
            public Action<Size> Resized { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
            public Action<double> ScalingChanged { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
            public Action Closed { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        }
}

