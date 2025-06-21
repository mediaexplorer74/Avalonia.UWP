using Avalonia.Input;
using Avalonia.Input.Raw;
using System.ComponentModel;

namespace Avalonia.UWP
{
    internal class KeyboardDeviceImpl : IKeyboardDevice // Avalonia 0.6.0-compatible
    {
        public IInputElement FocusedElement
        {
            get;
            private set;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void ProcessRawEvent(RawInputEventArgs ev)
        {
            throw new System.NotImplementedException();
        }

        public void SetFocusedElement(IInputElement element, NavigationMethod method, InputModifiers modifiers)
        {
            FocusedElement = element;
        }
    }
}