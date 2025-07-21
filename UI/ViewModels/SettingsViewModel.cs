using Microsoft.Win32;
using UI.Helpers;

namespace UI.ViewModels
{
    internal class SettingsViewModel : Observable
    {
        private string _filePath;
        private string _selectedFileType;
        private uint _sliderValue;

        private bool _modificationsMade;

        public SettingsViewModel()
        {
            _filePath = Properties.Settings.Default.ModelFilePath;
            _sliderValue = Properties.Settings.Default.NGram;

            AllowedFileTypes = ["JSON"]; // I may have to rethink this if I plan to add "proper" support later.
            _selectedFileType = AllowedFileTypes.First();

            BrowseCommand = new RelayCommand(_ => OpenFileDialog());
            ReloadCommand = new RelayCommand(_ => OnReload());
        }

        public string FilePath
        {
            get => _filePath;
            set
            {
                _filePath = value;
                ModificationsMade = true;
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

        public uint SliderValue
        {
            get => _sliderValue;
            set
            {
                if (_sliderValue != value)
                {
                    _sliderValue = value;
                    ModificationsMade = true;

                    SaveSetting(nameof(Properties.Settings.Default.NGram), _sliderValue);

                    OnPropertyChanged();
                }
            }
        }

        public bool ModificationsMade
        {
            get => _modificationsMade;
            set
            {
                if (_modificationsMade != value)
                {
                    _modificationsMade = value;
                    OnPropertyChanged();
                }
            }
        }

        public RelayCommand BrowseCommand { get; }

        public RelayCommand ReloadCommand { get; }

        public List<string> AllowedFileTypes { get; }

        public LoadingViewModel LoadingVM { get; } = new LoadingViewModel();

        private void OpenFileDialog()
        {
            string filter = SelectedFileType switch
            {
                "JSON" => "JSON files (*.json)|*.json",
                _ => throw new NotImplementedException($"Filter not implemented for type '{SelectedFileType}'")
            };

            var dialog = new OpenFileDialog { Filter = filter };

            if (dialog.ShowDialog() == true)
            {
                FilePath = dialog.FileName;
                SaveSetting(nameof(Properties.Settings.Default.ModelFilePath), FilePath);
            }
        }

        private void OnReload()
        {
            LoadingVM.IsAnimating = true;
            ModificationsMade = false;
        }

        private void SaveSetting(string settingName, object value)
        {
            var settings = Properties.Settings.Default;
            settings[settingName] = value;
            settings.Save();
        }
    }
}
