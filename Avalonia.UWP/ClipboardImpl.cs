using Avalonia.Input.Platform;
using System.Threading.Tasks;

namespace Avalonia.UWP
{
    public class ClipboardImpl : IClipboard
    {
        public Task<string> GetTextAsync()
        {
            return Task.FromResult("");
        }

        public Task SetTextAsync(string text)
        {
            return Task.CompletedTask;
        }

        public Task ClearAsync()
        {
            return Task.CompletedTask;
        }
    }
}