﻿using Domain.Interfaces;
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

        private readonly Action _reloadModelCallback;

        public SettingsViewModel(ISettingsRepository settingsService, Action reloadModelCallback)
        {
            _settingsService = settingsService;
            _reloadModelCallback = reloadModelCallback;

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

                _settingsService.SetModelFilePath(FilePath);
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

        private LoadingViewModel LoadingVM { get; } = new LoadingViewModel();

        private void OpenFileDialog()
        {
            string filter = SelectedFileType switch
            {
                "JSON" => "JSON files (*.json)|*.json",
                _ => throw new NotImplementedException($"Filter not implemented for type '{SelectedFileType}'")
            };

            var dialog = new OpenFileDialog { Filter = filter };

            if (dialog.ShowDialog() == true)
                FilePath = dialog.FileName;
        }

        private void OnReload()
        {
            LoadingVM.IsAnimating = true;
            ModificationsMade = false;
            _reloadModelCallback?.Invoke();
        }
    }
}
