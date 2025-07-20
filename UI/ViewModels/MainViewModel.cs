using UI.Helpers;

namespace UI.ViewModels
{
    internal class MainViewModel : Observable
    {
        private object _currentView;

        public MainViewModel()
        {
            SettingsVM = new SettingsViewModel();
            _currentView = SettingsVM;
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
