using System;
using System.Windows.Input;

namespace Dashboard1.Utils
{
    public class Command : ICommand
    {
        #region ICommand

        public event EventHandler CanExecuteChanged;
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            executeAction?.Invoke(parameter);
        }

        #endregion

        public Command(Action<object> executeAction)
        {
            this.executeAction = executeAction
                ?? throw new ArgumentNullException("Переменная не может быть null.", nameof(executeAction));
        }

        private readonly Action<object> executeAction;
    }
}
