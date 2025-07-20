using System.Windows.Input;

namespace UI.Helpers
{
    internal class RelayCommand : ICommand
    {
        private Action<object?> _execute;
        private Func<object, bool>? _canExecute;

        public RelayCommand(Action<object?> execute, Func<object, bool>? canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object? parameter)
        {
            return parameter == null || (_canExecute?.Invoke(parameter) ?? true);
        }

        public void Execute(object? parameter)
        {
            if (_execute == null)
                throw new InvalidOperationException("Execute action cannot be null.");

            _execute(parameter);
        }

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}
