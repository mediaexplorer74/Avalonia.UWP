using Avalonia.Controls.Platform.Surfaces;
using Avalonia.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Input;
using Avalonia.Input.Raw;
using Windows.UI.Core;
using Windows.ApplicationModel.Core;
using Avalonia.Controls;

namespace Avalonia.UWP
{
    public abstract class TopLevelImpl : ITopLevelImpl
    {
        public TopLevelWindow CoreWindow {get;}
        protected TopLevelImpl()
        {
            CoreWindow = new TopLevelWindow(this);
        }

        public class TopLevelWindow : IFrameworkView
        {
            private TopLevelImpl _topLevel;

            public TopLevelWindow(TopLevelImpl topLevel)
            {
                _topLevel = topLevel;
            }

            public void Initialize(CoreApplicationView applicationView)
            {
                // not ready

                //throw new NotImplementedException();
            }

            public void SetWindow(CoreWindow window)
            {
                // not ready
                //throw new NotImplementedException();
            }

            public void Load(string entryPoint)
            {
                // not ready
                //throw new NotImplementedException();
            }

            public void Run()
            {
                // not ready
                //throw new NotImplementedException();
            }

            public void Uninitialize()
            {
                // not ready
                //throw new NotImplementedException();
            }
        }

        public Size ClientSize
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public double Scaling
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IEnumerable<object> Surfaces
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public Action<RawInputEventArgs> Input
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }
        public Action<Rect> Paint 
        { 
            get => throw new NotImplementedException(); 
            set => throw new NotImplementedException(); 
        }

        public Action<Size> Resized 
        { 
            get => throw new NotImplementedException(); 
            set => throw new NotImplementedException(); 
        }
        
        public Action<double> ScalingChanged 
        { 
            get => throw new NotImplementedException(); 
            set => throw new NotImplementedException(); 
        }
        
        public Action Closed 
        { 
            get => throw new NotImplementedException(); 
            set => throw new NotImplementedException(); 
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Invalidate(Rect rect)
        {
            throw new NotImplementedException();
        }

        public Point PointToClient(Point point)
        {
            throw new NotImplementedException();
        }

        public Point PointToScreen(Point point)
        {
            throw new NotImplementedException();
        }

        public void SetCursor(IPlatformHandle cursor)
        {
            throw new NotImplementedException();
        }

        public void SetInputRoot(IInputRoot inputRoot)
        {
            throw new NotImplementedException();
        }
    }
}
