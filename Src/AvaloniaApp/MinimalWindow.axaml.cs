using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.UWP;
using System;
using System.Xml.Linq;

namespace AvaloniaApp
{
    public partial class MinimalWindow : Avalonia.Controls.Window
    {
        private int _counter = 0;
        public MinimalWindow()
        {
            //InitializeComponent();

            //Avalonia.Controls.Window
            UWPWindowImpl window = AxamlRuntimeLoader.LoadMinimalWindowFromAxaml("MinimalWindow.axaml");

            // Wire up the button click event (Avalonia 0.5.1 style)
            Button button = this.FindControl<Button>("CounterButton");
            TextBlock text = this.FindControl<TextBlock>("CounterText");
            
            /*if (button != null && text != null)
            {
                button.Click += (sender, e) =>
                {
                    _counter++;
                    text.Text = "Count: " + _counter;
                };
            }*/
            // Show the window, or add to your UWP hosting logic
            window.Show();

        }


        private void InitializeComponent()
        {
            //AvaloniaXamlLoader.Load(this);
            // Usage example:
            //var window = AxamlRuntimeLoader.LoadMinimalWindowFromAxaml("MinimalWindow.axaml");
            // Show the window, or add to your UWP hosting logic
            //window.Show();
        }
        public static class AxamlRuntimeLoader
        {
            public static /*Window*/UWPWindowImpl LoadMinimalWindowFromAxaml(string axamlPath)
            {
                //tune path as needed 
                axamlPath = System.IO.Path.Combine(Windows.ApplicationModel.Package.Current.InstalledLocation.Path, /*"Src", "AvaloniaApp",*/ axamlPath);
                var doc = XDocument.Load(axamlPath);
                var root = doc.Root;
                if (root == null || root.Name.LocalName != "Window")
                    throw new InvalidOperationException("Root element is not <Window>");

                //var window = new Window();
                var window = new UWPWindowImpl(); // Use UWPWindow implementation for UWP compatibility
                // Set Window properties
                if (root.Attribute("Width") != null)
                    window.Width = double.Parse(root.Attribute("Width").Value);
                if (root.Attribute("Height") != null)
                    window.Height = double.Parse(root.Attribute("Height").Value);
                if (root.Attribute("Title") != null)
                    window.Title = root.Attribute("Title").Value;

                // Find the main content (Grid)
                foreach (var child in root.Elements())
                {
                    var content = ParseElement(child);
                    if (content is Control ctrl)
                        window.Content = ctrl;
                }

                return window;
            }

            private static IControl ParseElement(XElement elem)
            {
                switch (elem.Name.LocalName)
                {
                    case "Grid":
                        var grid = new Grid();
                        if (elem.Attribute("Background") != null)
                            grid.Background = new SolidColorBrush(ParseColor(elem.Attribute("Background").Value));
                        foreach (var child in elem.Elements())
                            grid.Children.Add(ParseElement(child));
                        return grid;
                    case "Canvas":
                        var canvas = new Canvas();
                        foreach (var child in elem.Elements())
                        {
                            var ctrl = ParseElement(child);
                            SetCanvasProps(ctrl, child);
                            canvas.Children.Add(ctrl);
                        }
                        return canvas;
                    case "Rectangle":
                        var rect = new Avalonia.Controls.Shapes.Rectangle();
                        SetCanvasProps(rect, elem);
                        if (elem.Attribute("Width") != null)
                            rect.Width = double.Parse(elem.Attribute("Width").Value);
                        if (elem.Attribute("Height") != null)
                            rect.Height = double.Parse(elem.Attribute("Height").Value);
                        if (elem.Attribute("Fill") != null)
                            rect.Fill = new SolidColorBrush(ParseColor(elem.Attribute("Fill").Value));
                        if (elem.Attribute("Stroke") != null)
                            rect.Stroke = new SolidColorBrush(ParseColor(elem.Attribute("Stroke").Value));
                        if (elem.Attribute("StrokeThickness") != null)
                            rect.StrokeThickness = double.Parse(elem.Attribute("StrokeThickness").Value);
                        return rect;
                    case "Ellipse":
                        var ellipse = new Avalonia.Controls.Shapes.Ellipse();
                        SetCanvasProps(ellipse, elem);
                        if (elem.Attribute("Width") != null)
                            ellipse.Width = double.Parse(elem.Attribute("Width").Value);
                        if (elem.Attribute("Height") != null)
                            ellipse.Height = double.Parse(elem.Attribute("Height").Value);
                        if (elem.Attribute("Fill") != null)
                            ellipse.Fill = new SolidColorBrush(ParseColor(elem.Attribute("Fill").Value));
                        if (elem.Attribute("Stroke") != null)
                            ellipse.Stroke = new SolidColorBrush(ParseColor(elem.Attribute("Stroke").Value));
                        if (elem.Attribute("StrokeThickness") != null)
                            ellipse.StrokeThickness = double.Parse(elem.Attribute("StrokeThickness").Value);
                        return ellipse;
                    case "Line":
                        var line = new Avalonia.Controls.Shapes.Line();
                        if (elem.Attribute("StartPoint") != null)
                        {
                            var pt = ParsePoint(elem.Attribute("StartPoint").Value);
                            line.StartPoint = pt;
                        }
                        if (elem.Attribute("EndPoint") != null)
                        {
                            var pt = ParsePoint(elem.Attribute("EndPoint").Value);
                            line.EndPoint = pt;
                        }
                        if (elem.Attribute("Stroke") != null)
                            line.Stroke = new SolidColorBrush(ParseColor(elem.Attribute("Stroke").Value));
                        if (elem.Attribute("StrokeThickness") != null)
                            line.StrokeThickness = double.Parse(elem.Attribute("StrokeThickness").Value);
                        return line;
                    case "StackPanel":
                        var sp = new StackPanel();
                        if (elem.Attribute("Orientation") != null)
                            sp.Orientation = elem.Attribute("Orientation").Value == "Horizontal" ? Orientation.Horizontal : Orientation.Vertical;
                        //if (elem.Attribute("Spacing") != null)
                        //    sp.Spacing = double.Parse(elem.Attribute("Spacing").Value);
                        SetCanvasProps(sp, elem);
                        foreach (var child in elem.Elements())
                            sp.Children.Add(ParseElement(child));
                        return sp;
                    case "Button":
                        var btn = new Button();
                        //if (elem.Attribute("x:Name") != null)
                        //    btn.Name = elem.Attribute("x:Name").Value;
                        btn.Name = "Button"; //temporary name
                        if (elem.Attribute("Content") != null)
                            btn.Content = elem.Attribute("Content").Value;
                        if (elem.Attribute("Width") != null)
                            btn.Width = double.Parse(elem.Attribute("Width").Value);
                        if (elem.Attribute("Height") != null)
                            btn.Height = double.Parse(elem.Attribute("Height").Value);
                        return btn;
                    case "TextBlock":
                        var tb = new TextBlock();
                        //if (elem.Attribute("x:Name") != null)
                        //    tb.Name = elem.Attribute("x:Name").Value;
                        tb.Name = "TextBlock"; //temporary name
                        if (elem.Attribute("Text") != null)
                            tb.Text = elem.Attribute("Text").Value;
                        if (elem.Attribute("FontSize") != null)
                            tb.FontSize = double.Parse(elem.Attribute("FontSize").Value);
                        if (elem.Attribute("VerticalAlignment") != null && elem.Attribute("VerticalAlignment").Value == "Center")
                            tb.VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center;
                        return tb;
                    default:
                        // Unknown element, skip
                        return null;
                }
            }

            private static void SetCanvasProps(IControl ctrl, XElement elem)
            {
                if (ctrl == null) return;
                if (elem.Attribute("Canvas.Left") != null)
                    Canvas.SetLeft((Avalonia.AvaloniaObject)ctrl, double.Parse(elem.Attribute("Canvas.Left").Value));
                if (elem.Attribute("Canvas.Top") != null)
                    Canvas.SetTop((Avalonia.AvaloniaObject)ctrl, double.Parse(elem.Attribute("Canvas.Top").Value));
            }

            private static Avalonia.Media.Color ParseColor(string color)
            {
                // Basic named colors, extend as needed
                switch (color)
                {
                    case "Red":
                        return Avalonia.Media.Colors.Red;
                    case "Blue":
                        return Avalonia.Media.Colors.Blue;
                    case "Green":
                        return Avalonia.Media.Colors.Green;
                    case "Black":
                        return Avalonia.Media.Colors.Black;
                    case "LightGray":
                        return Avalonia.Media.Colors.LightGray;
                    default:
                        return Avalonia.Media.Colors.Transparent;
                }
            }
        }

        private static Avalonia.Point ParsePoint(string pt)
        {
            var parts = pt.Split(',');
            return new Avalonia.Point(double.Parse(parts[0]), double.Parse(parts[1]));
        }
    }
}
