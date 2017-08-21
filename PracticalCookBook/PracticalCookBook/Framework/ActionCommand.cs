using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PracticalCookBook.Framework
{
    //Run-of-themill ICommand implementation based on popular tutorials.
    public class ActionCommand : ICommand
    {
        private readonly Action _execute;

        private readonly Action<object> _executeWithParam;

        private readonly Func<bool> _canExecute;

        public ActionCommand(Action executeAction, Func<bool> canExecuteFunc)
        {
            if (executeAction == null)
            {
                throw new ArgumentNullException("executeAction parameter cannot be null!");
            }
            if (canExecuteFunc == null)
            {
                throw new ArgumentNullException("canExecuteFunc parameter cannot be null!");
            }

            _execute = executeAction;
            _canExecute = canExecuteFunc;
        }

        public ActionCommand(Action<object> executeAction, Func<bool> canExecuteFunc)
        {
            if (executeAction == null)
            {
                throw new ArgumentNullException("executeAction parameter cannot be null!");
            }
            if (canExecuteFunc == null)
            {
                throw new ArgumentNullException("canExecuteFunc parameter cannot be null!");
            }

            _executeWithParam = executeAction;
            _canExecute = canExecuteFunc;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute();
        }

        public void Execute(object parameter)
        {
            if (_execute != null)
            {
                _execute();
            }
            else
            {
                _executeWithParam(parameter);
            }
        }
    }
}
