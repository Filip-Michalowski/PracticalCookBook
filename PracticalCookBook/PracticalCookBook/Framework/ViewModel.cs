using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PracticalCookBook.Framework
{
    public abstract class ViewModel
    {
        protected INavigator _navigator;

        public ICommand GoToMainMenu { get; set; }

        //TODO implement a warning before leaving a page with unsaved changes

        public ViewModel(INavigator navigator)
        {
            _navigator = navigator;

            GoToMainMenu = new ActionCommand(() => { _navigator.NavigateToMainMenu(); }, () => true);
        }
    }
}
