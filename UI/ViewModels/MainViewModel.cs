using Domain.DomainServices;
using Domain.Interfaces;
using Domain.ValueObjects;
using Domain.ValueObjects.Trees;
using Infrastructure.Factories;
using Infrastructure.FileAccess;
using Infrastructure.Repositories;
using System.ComponentModel;
using System.IO;
using UI.Helpers;

namespace UI.ViewModels
{
    internal class MainViewModel : Observable
    {
        private object _currentView;
        private NGram _model;

        private readonly ModelRepositoryFactory _modelRepositoryFactory;
        private IModelRepository _modelRepository;

        public MainViewModel()
        {
            // TODO: Improve architectural design... Too Much Responsibility in MainViewModel
            SettingsRepository settingsRepository = new();
            _modelRepositoryFactory = new ModelRepositoryFactory();

            _modelRepository = _modelRepositoryFactory.Create(settingsRepository.GetModelFileType(), 
                                                              settingsRepository.GetModelFilePath());
            _model = _modelRepository.Load();

            SettingsVM = new SettingsViewModel(settingsRepository, ReloadModel);
            SettingsVM.PropertyChanged += OnSettingsPropertyChanged;
            _currentView = SettingsVM;
        }

        private void ReloadModel()
        {
            string path = Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.Parent.FullName, "Infrastruktur/Example.txt");

            _model = new NGram(new ModelData());
            ModelTrainer trainer = new(new FileReader(path), _model);
            trainer.TrainModel(SettingsVM.SliderValue);

            _modelRepository.Save(_model);
        }

        private void OnSettingsPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(SettingsViewModel.ModificationsMade))
                _modelRepository = _modelRepositoryFactory.Create(SettingsVM.SelectedFileType, SettingsVM.FilePath);
        }

        public NGram Model
        {
            get => _model;
            set
            {
                _model = value;
                OnPropertyChanged();
            }
        }

        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public SettingsViewModel SettingsVM { get; set; }
    }
}
