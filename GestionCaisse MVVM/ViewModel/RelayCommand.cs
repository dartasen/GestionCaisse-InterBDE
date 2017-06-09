using System;
using System.Windows.Input;

namespace GestionCaisse_MVVM.ViewModel
{
    public class RelayCommand : ICommand
    {
        private readonly Action _actionToExecute;
        private readonly Func<object, bool> _checkCanExecute;

        public RelayCommand(Action actionToExecute, Func<object, bool> actionToCheckExecute)
        {
            _actionToExecute = actionToExecute;
            _checkCanExecute = actionToCheckExecute;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return _checkCanExecute.Invoke(parameter);
        }

        public void Execute(object parameter)
        {
            _actionToExecute.Invoke();
        }
    }
}
