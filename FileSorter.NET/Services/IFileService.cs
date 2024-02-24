using Avalonia.Controls;
using System.IO;
using System.Threading.Tasks;

namespace FileSorter.NET.Services
{
    internal interface IFileService
    {
        Task<string?> GetDirectory(Window window, string path);
        FileInfo[] GetFileInfos(string directory);
    }
}