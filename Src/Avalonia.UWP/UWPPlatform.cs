using Avalonia.Controls.Platform;
using Avalonia.Input;
using Avalonia.Input.Platform;
using Avalonia.Platform;
using Avalonia.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avalonia.UWP
{
    // Gitter discussion: https://gitter.im/AvaloniaUI/Avalonia?at=59cd68ff614889d4754ff3c7
    // Only NECESSARY things are IPlatformThreadingInterface and ITopLevel impl (and IWindowImpl on top)
    // Start with stubs for most of these--copy paste the stubs from https://github.com/AvaloniaUI/Avalonia/tree/d7e74229990205b1865586f44c65fb7571ba0d0e/src/Avalonia.DesignerSupport/Remote

    public class UWPPlatform // Avalonia 0.6.0-compatible
    {                
        public static void Initialize()
        {
            AvaloniaLocator.CurrentMutable                
                .Bind<IPlatformThreadingInterface>().ToSingleton<PlatformThreadingInterfaceImpl>()
                .Bind<IPlatformSettings>().ToSingleton<UWPPlatformSettings>()
                .Bind<IWindowingPlatform>().ToSingleton<WindowingPlatformImpl>()
                .Bind<IRenderLoop>().ToSingleton<RenderLoopImpl>()
                .Bind<IStandardCursorFactory>().ToSingleton<CursorFactoryImpl>()
                .Bind<IKeyboardDevice>().ToSingleton<KeyboardDeviceImpl>()
                .Bind<ISystemDialogImpl>().ToSingleton<SystemDialogImpl>()
                .Bind<IPlatformIconLoader>().ToSingleton<PlatformIconLoaderImpl>()
                .Bind<IClipboard>().ToSingleton<ClipboardImpl>();
        }
    }
}
