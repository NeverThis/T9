using Domain.Interfaces;
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

        private readonly ISettingsRepository _settingsService;

        public SettingsViewModel(ISettingsRepository settingsService)
        {
            _settingsService = settingsService;

            _filePath = _settingsService.GetModelFilePath();
            _sliderValue = _settingsService.GetNGram();
            _selectedFileType = _settingsService.GetModelFileType();

            AllowedFileTypes = ["JSON"];
            SelectedFileType = _selectedFileType;

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
                    ModificationsMade = true;

                    _settingsService.SetModelFileType(value);
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

                    _settingsService.SetNGram(value);
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
                _settingsService.SetModelFilePath(FilePath);
            }
        }

        private void OnReload()
        {
            LoadingVM.IsAnimating = true;
            ModificationsMade = false;
        }

        private void SaveSettings()
        {
            _settingsService.SetModelFilePath(FilePath);
            _settingsService.SetNGram(SliderValue);
            _settingsService.SetModelFileType(SelectedFileType);
        }
    }
}
