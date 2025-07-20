using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.Windows.Input;
using UI.Helpers;

namespace UI.ViewModels
{
    internal class SettingsViewModel : Observable
    {
        private string _filePath;
        private string _selectedFileType;

        public SettingsViewModel()
        {
            _filePath = Properties.Settings.Default.ModelFilePath;

            AllowedFileTypes = ["JSON"]; // I may have to rethink this if I plan to add "proper" support later.
            _selectedFileType = AllowedFileTypes.First();

            BrowseCommand = new RelayCommand(_ => OpenFileDialog());
        }

        public string FilePath
        {
            get => _filePath;
            set
            {
                _filePath = value;
                OnPropertyChanged();
            }
        }

        public string SelectedFileType
        {
            get => _selectedFileType;
            set
            {
                if (_selectedFileType != value)
                {
                    _selectedFileType = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand BrowseCommand { get; }

        public List<string> AllowedFileTypes { get; }

        private void OpenFileDialog()
        {
            OpenFileDialog dialog = new();

            // Only JSON right now...
            dialog.Filter = SelectedFileType switch
            {
                "JSON" => "JSON files (*.json)|*.json",
                _ => "All files (*.*)|*.*"
            };

            if (dialog.ShowDialog() == true)
            {
                FilePath = dialog.FileName;
                Properties.Settings.Default.ModelFilePath = FilePath;
                Properties.Settings.Default.Save();
            }
        }
    }
}
