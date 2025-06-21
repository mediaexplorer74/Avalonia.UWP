using Avalonia.Input;
using Avalonia.Platform;
using System;

namespace Avalonia.UWP
{
    internal class CursorFactoryImpl : IStandardCursorFactory
    {
        public IPlatformHandle GetCursor(StandardCursorType cursorType)
        {
            return new PlatformHandle(IntPtr.Zero, "STUB");
        }
    }
}