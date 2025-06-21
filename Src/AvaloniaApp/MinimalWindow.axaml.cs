using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System;

namespace AvaloniaApp
{
    public partial class MinimalWindow : Avalonia.Controls.Window
    {
        private int _counter = 0;
        public MinimalWindow()
        {
            InitializeComponent();
            // Wire up the button click event (Avalonia 0.5.1 style)
            Button button = this.FindControl<Button>("CounterButton");
            TextBlock text = this.FindControl<TextBlock>("CounterText");
            if (button != null && text != null)
            {
                button.Click += (sender, e) =>
                {
                    _counter++;
                    text.Text = "Count: " + _counter;
                };
            }
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
