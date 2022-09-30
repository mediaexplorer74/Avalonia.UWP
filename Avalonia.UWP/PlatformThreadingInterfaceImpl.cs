﻿using System;
using System.Threading;
using Avalonia.Platform;
using Windows.UI.Core;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using System.Reactive.Disposables;

namespace Avalonia.UWP
{
    internal class PlatformThreadingInterfaceImpl : IPlatformThreadingInterface
    {
        private bool _signaled;

        public bool CurrentThreadIsLoopThread
        {
            get
            {
                return CoreWindow.GetForCurrentThread().Dispatcher.HasThreadAccess;
            }
        }

        public event Action Signaled;

        public void RunLoop(CancellationToken cancellationToken)
        {
            // We use the CoreDispatcher's event loop instead of our own RunLoop.
            return;
        }

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
    }
}