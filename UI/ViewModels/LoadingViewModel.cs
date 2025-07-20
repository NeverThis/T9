using UI.Helpers;

namespace UI.ViewModels
{
    internal class LoadingViewModel : Observable
    {
        private bool _isAnimating;

        public bool IsAnimating
        {
            get => _isAnimating;
            set
            {
                if (_isAnimating != value)
                {
                    _isAnimating = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
