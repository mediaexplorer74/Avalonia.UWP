using System;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Controls.Platform;
using Avalonia.Platform;
using Windows.Storage.Pickers;

namespace Avalonia.UWP
{
    internal class SystemDialogImpl : ISystemDialogImpl
    {
        public async Task<string[]> ShowFileDialogAsync(FileDialog dialog, IWindowImpl parent)
        {
            // UWP FileOpenPicker/FileSavePicker integration
            try
            {
                var picker = dialog is OpenFileDialog ? (object)new FileOpenPicker() : new FileSavePicker();
                // Configure picker (file types, etc.)
                // TODO: Add file type filters from dialog.Filters
                if (dialog is OpenFileDialog openDialog && openDialog.AllowMultiple)
                {
                    var files = await ((FileOpenPicker)picker).PickMultipleFilesAsync();
                    return files?.Count > 0 ? files.Select(f => f.Path).ToArray() : null;
                }
                else if (dialog is OpenFileDialog)
                {
                    var file = await ((FileOpenPicker)picker).PickSingleFileAsync();
                    return file != null ? new[] { file.Path } : null;
                }
                else
                {
                    var file = await ((FileSavePicker)picker).PickSaveFileAsync();
                    return file != null ? new[] { file.Path } : null;
                }
            }
            catch
            {
                // UWP picker unavailable or not running in UWP context
                return null;
            }
        }

        public async Task<string> ShowFolderDialogAsync(OpenFolderDialog dialog, IWindowImpl parent)
        {
            // UWP FolderPicker integration
            try
            {
                var picker = new FolderPicker();
                // TODO: Add file type filters if needed
                var folder = await picker.PickSingleFolderAsync();
                return folder?.Path;
            }
            catch
            {
                // UWP picker unavailable or not running in UWP context
                return null;
            }
        }
    }
}