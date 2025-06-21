using Avalonia.Input;

namespace Avalonia.UWP
{
    internal class KeyboardDeviceImpl : IKeyboardDevice
    {
        public IInputElement FocusedElement
        {
            get;
            private set;
        }

        public void SetFocusedElement(IInputElement element, NavigationMethod method, InputModifiers modifiers)
        {
            FocusedElement = element;
        }
    }
}