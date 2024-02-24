using Avalonia.Controls;
using FileSorter.NET.Services;
using ReactiveUI;
using Splat;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Reactive;
using System.Threading.Tasks;

namespace FileSorter.NET.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        #region Public
        public string DirectoryFrom
        {
            get => _directoryFrom;
            set => this.RaiseAndSetIfChanged(ref _directoryFrom, value);
        }
        public string DirectoryTo
        {
            get => _directoryTo;
            set => this.RaiseAndSetIfChanged(ref _directoryTo, value);
        }
        public int ProgressBarValue
        {
            get => _progressBarValue;
            set => this.RaiseAndSetIfChanged(ref _progressBarValue, value);
        }
        public int ProgressBarMax
        {
            get => _progressBarMax;
            set => this.RaiseAndSetIfChanged(ref _progressBarMax, value);
        }
        public bool UseCustomFolders
        {
            get => _useCustomFolders;
            set => this.RaiseAndSetIfChanged(ref _useCustomFolders, value);
        }

        public ReactiveCommand<Window, Unit> FromDirectoryCommand { get; }
        public ReactiveCommand<Window, Unit> ToDirectoryCommand { get; }
        public ReactiveCommand<Unit, Unit> SortFilesCommand { get; }
        public ReactiveCommand<Unit, Unit> OpenToFolderCommand { get; }
        public ReactiveCommand<Unit, Unit> OpenFromFolderCommand { get; }

        public ObservableCollection<FileInfo> FilesToCopy { get; private set; } = new();
        #endregion

        #region Fields
        private readonly ISettingService _settingsSerivce;
        private readonly IFileService _fileService;
        private readonly IFolderHelperService _folderHelperSerivce;

        private string _directoryFrom = "";
        private string _directoryTo = "";
        private int _progressBarValue = 0;
        private int _progressBarMax = 0;
        private bool _useCustomFolders = false;
        #endregion

        public MainWindowViewModel()
        {
            _settingsSerivce = Locator.Current.GetService<ISettingService>() ?? new SettingService();
            _fileService = Locator.Current.GetService<IFileService>() ?? new FileService();
            _folderHelperSerivce = Locator.Current.GetService<IFolderHelperService>() ?? new FolderHelperSerivce();

            DirectoryFrom = _settingsSerivce.FromDirectory;
            DirectoryTo = _settingsSerivce.ToDirectory;
            UseCustomFolders = false;

            FromDirectoryCommand = ReactiveCommand.Create<Window>(SetFromDirectory);
            ToDirectoryCommand = ReactiveCommand.Create<Window>(SetToDirectory);
            SortFilesCommand = ReactiveCommand.Create(SortToNewDirectory);
            OpenToFolderCommand = ReactiveCommand.Create(OpenToFolder);
            OpenFromFolderCommand = ReactiveCommand.Create(OpenFromFolder);

            AddFilesToListView();
        }

        private void OpenFromFolder() => OpenFolder(DirectoryFrom);
        private void OpenToFolder() => OpenFolder(DirectoryTo);

        private static void OpenFolder(string path)
        {
            if (!Directory.Exists(path))
                return;
            Process.Start("explorer.exe", path);
        }

        private async void SetFromDirectory(Window window)
        {
            FilesToCopy.Clear();
            DirectoryFrom = await _fileService.GetDirectory(window, DirectoryFrom) ?? DirectoryFrom;
            _settingsSerivce.FromDirectory = DirectoryFrom;
            _settingsSerivce.Save();
            AddFilesToListView();
        }

        private async void SetToDirectory(Window window)
        {
            DirectoryTo = await _fileService.GetDirectory(window, DirectoryTo) ?? DirectoryTo;
            _settingsSerivce.ToDirectory = DirectoryTo;
            _settingsSerivce.Save();
        }

        private async void SortToNewDirectory()
        {
            if (FilesToCopy.Count < 1 || !Directory.Exists(DirectoryTo))
                return;
            await Task.Run(() => SortToNewDirectoryTask(UseCustomFolders));
            FilesToCopy.Clear();
            AddFilesToListView();
        }

        private void SortToNewDirectoryTask(bool customFolders)
        {
            int count = FilesToCopy.Count;
            ProgressBarMax = count;
            int index = 0;

            foreach (var fileInfo in FilesToCopy)
            {
                ProgressBarValue = index;
                if (!fileInfo.Exists)
                    continue;

                string folderToPath = DirectoryTo + @"\" + _folderHelperSerivce.GetFolder(customFolders, fileInfo.Extension);

                if (!Directory.Exists(folderToPath))
                    Directory.CreateDirectory(folderToPath);

                if (!File.Exists(folderToPath + @"\" + fileInfo.Name))
                {
                    fileInfo.CopyTo(folderToPath + @"\" + fileInfo.Name);
                    index++;
                }
            }
            ProgressBarValue = count;
        }

        private void AddFilesToListView()
        {
            if (DirectoryFrom == "")
                return;
            foreach (FileInfo file in _fileService.GetFileInfos(DirectoryFrom))
                FilesToCopy.Add(file);
        }
    }
}
