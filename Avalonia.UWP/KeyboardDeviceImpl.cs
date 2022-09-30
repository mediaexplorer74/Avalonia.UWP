using Avalonia.Input;

namespace Avalonia.UWP
{
    internal class KeyboardDeviceImpl : IKeyboardDevice
    {
        public IInputElement FocusedElement
        {
            get
            {
                throw new System.NotImplementedException();
            }
        }

        public void SetFocusedElement(IInputElement element, 
            NavigationMethod method, InputModifiers modifiers)
        {
            throw new System.NotImplementedException();
        }
    }
}