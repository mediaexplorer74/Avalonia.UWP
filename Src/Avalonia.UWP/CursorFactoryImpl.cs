using Avalonia.Input;
using Avalonia.Platform;
using System;

namespace Avalonia.UWP
{
    internal class CursorFactoryImpl : IStandardCursorFactory
    {
        public IPlatformHandle GetCursor(StandardCursorType cursorType)
        {
            // UWP does not support custom cursors in the same way as Win32. Always return stub.
            return new PlatformHandle(IntPtr.Zero, "UWP_CURSOR");
        }
    }
}