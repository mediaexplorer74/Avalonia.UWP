using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Controls.Platform;
using Avalonia.Platform;

namespace Avalonia.UWP
{
    internal class SystemDialogImpl : ISystemDialogImpl
    {
        public Task<string[]> ShowFileDialogAsync(FileDialog dialog, IWindowImpl parent)
        {
            // not ready
            //throw new System.NotImplementedException();
            return default;
        }

        public Task<string> ShowFolderDialogAsync(OpenFolderDialog dialog, 
            IWindowImpl parent)
        {
            // not ready
            //throw new System.NotImplementedException();
            return default;
        }
    }
}