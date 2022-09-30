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
            //
            throw new System.NotImplementedException();
        }

        public Task<string> ShowFolderDialogAsync(OpenFolderDialog dialog, 
            IWindowImpl parent)
        {
            //
            throw new System.NotImplementedException();
        }
    }
}