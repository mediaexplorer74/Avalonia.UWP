using System;
using Avalonia.Rendering;

namespace Avalonia.UWP
{
    internal class RenderLoopImpl : IRenderLoop
    {
        private Windows.UI.Xaml.DispatcherTimer _timer;
        public event EventHandler<EventArgs> Tick;

        public RenderLoopImpl()
        {
            _timer = new Windows.UI.Xaml.DispatcherTimer();
            _timer.Interval = TimeSpan.FromMilliseconds(16); // ~60Hz
            _timer.Tick += (s, e) => Tick?.Invoke(this, EventArgs.Empty);
        }

        public void Start()
        {
            _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
        }
    }
}