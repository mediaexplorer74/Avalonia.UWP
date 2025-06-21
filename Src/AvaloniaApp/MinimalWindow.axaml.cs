using Avalonia.Controls;

namespace AvaloniaApp
{
    public partial class MinimalWindow : Window
    {
        private int _counter = 0;
        public MinimalWindow()
        {
            InitializeComponent();
            // Wire up the button click event (Avalonia 0.5.1 style)
            var button = this.FindControl<Button>("CounterButton");
            var text = this.FindControl<TextBlock>("CounterText");
            if (button != null && text != null)
            {
                button.Click += (sender, e) =>
                {
                    _counter++;
                    text.Text = "Count: " + _counter;
                };
            }
        }
    }
}
