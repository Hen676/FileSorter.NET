using Avalonia.Controls;
using Avalonia.Platform.Storage;
using Splat;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FileSorter.NET.Services
{
    internal class FileService : IFileService
    {
        public FileInfo[] GetFileInfos(string directory)
        {
            if (directory == string.Empty)
                return Array.Empty<FileInfo>();

            try
            {
                DirectoryInfo Dir = new(directory);
                return Dir.GetFiles("*", SearchOption.TopDirectoryOnly);
            }
            catch (Exception exception)
            {
                Debug.WriteLine($"Error getting files from '{directory}' directory.", exception);
                return Array.Empty<FileInfo>();
            }
        }

        public async Task<string?> GetDirectory(Window window, string path)
        {
            TopLevel? topLevel = TopLevel.GetTopLevel(window);
            if (topLevel == null || topLevel.StorageProvider == null)
                return null;

            IReadOnlyList<IStorageFolder> responce = await topLevel.StorageProvider.OpenFolderPickerAsync(new FolderPickerOpenOptions
            {
                Title = "TODO:: ADD TITLE",
                SuggestedStartLocation = await topLevel.StorageProvider.TryGetFolderFromPathAsync(path),
                AllowMultiple = false
            });
            if (responce.Any())
            {
                return responce[0].Path.LocalPath;
            }
            return null;
        }
    }
}
