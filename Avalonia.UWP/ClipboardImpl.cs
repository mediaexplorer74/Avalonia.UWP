using Avalonia.Input.Platform;
using System.Threading.Tasks;

namespace Avalonia.UWP
{
    public class ClipboardImpl : IClipboard
    {
        public Task<string> GetTextAsync()
        {
            System.Diagnostics.Debug.WriteLine("[ClipboardImpl] GetTextAsync called (stub)");
            // TODO: Integrate with UWP clipboard API
            return Task.FromResult("");
        }

        public Task SetTextAsync(string text)
        {
            System.Diagnostics.Debug.WriteLine($"[ClipboardImpl] SetTextAsync called (stub), text: {text}");
            // TODO: Integrate with UWP clipboard API
            return Task.CompletedTask;
        }

        public Task ClearAsync()
        {
            System.Diagnostics.Debug.WriteLine("[ClipboardImpl] ClearAsync called (stub)");
            // TODO: Integrate with UWP clipboard API
            return Task.CompletedTask;
        }
    }
}