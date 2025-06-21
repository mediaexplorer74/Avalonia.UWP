using System;
using System.Threading;
using Avalonia.Platform;
using Windows.UI.Core;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using System.Reactive.Disposables;

namespace Avalonia.UWP
{
    internal class PlatformThreadingInterfaceImpl : IPlatformThreadingInterface // Avalonia 0.6.0-compatible
    {
        private bool _signaled;

        // Returns true if the current thread is the UI thread
        public bool CurrentThreadIsLoopThread
        {
            get
            {
                return CoreWindow.GetForCurrentThread().Dispatcher.HasThreadAccess;
            }
        }

        public event Action Signaled;

        event Action<Threading.DispatcherPriority?> IPlatformThreadingInterface.Signaled
        {
            add
            {
                value = default;
            }

            remove
            {
                value = default;
            }
        }

        // UWP manages its own event loop, so this is a no-op
        public void RunLoop(CancellationToken cancellationToken)
        {
            return;
        }

        // Signals the UI thread to process events
        public void Signal()
        {
            lock (this)
            {
                if (_signaled)
                {
                    return;
                }
                _signaled = true;
            }

#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            CoreWindow.GetForCurrentThread().Dispatcher.RunAsync
                (CoreDispatcherPriority.Normal, () =>
            {
                lock (this)
                {
                    _signaled = false;
                }
                Signaled?.Invoke();
            });
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
        }

        public void Signal(Threading.DispatcherPriority priority)
        {
            throw new NotImplementedException();
        }

        // Starts a timer that ticks on the UI thread
        public IDisposable StartTimer(TimeSpan interval, Action tick)
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = interval;
            timer.Tick += (s, e) =>
            {
                tick();
            };
            timer.Start();
            return Disposable.Create(timer.Stop);
        }

        public IDisposable StartTimer(Threading.DispatcherPriority priority, TimeSpan interval, Action tick)
        {
            throw new NotImplementedException();
        }
    }
}