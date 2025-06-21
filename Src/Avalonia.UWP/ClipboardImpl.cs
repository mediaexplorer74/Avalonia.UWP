using Avalonia.Input.Platform;
using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;

namespace Avalonia.UWP
{
    public class ClipboardImpl : IClipboard
    {
        public Task<string> GetTextAsync()
        {
            try
            {
                // UWP clipboard API
                DataPackageView dataPackageView = Clipboard.GetContent();
                if (dataPackageView.Contains(StandardDataFormats.Text))
                {
                    Windows.Foundation.IAsyncOperation<string> op = dataPackageView.GetTextAsync();
                    return op.AsTask();
                }
            }
            catch
            {
                // Fallback to stub if UWP clipboard API is unavailable
                System.Diagnostics.Debug.WriteLine("[ClipboardImpl] GetTextAsync called (stub)");
            }
            return Task.FromResult("");
        }

        public Task SetTextAsync(string text)
        {
            try
            {
                var dataPackage = new DataPackage();
                dataPackage.SetText(text);
                Clipboard.SetContent(dataPackage);
                Clipboard.Flush();
            }
            catch
            {
                // Fallback to stub if UWP clipboard API is unavailable
                System.Diagnostics.Debug.WriteLine($"[ClipboardImpl] SetTextAsync called (stub), text: {text}");
            }
            return Task.CompletedTask;
        }

        public Task ClearAsync()
        {
            try
            {
                Clipboard.Clear();
            }
            catch
            {
                // Fallback to stub if UWP clipboard API is unavailable
                System.Diagnostics.Debug.WriteLine("[ClipboardImpl] ClearAsync called (stub)");
            }
            return Task.CompletedTask;
        }
    }
}