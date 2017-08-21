using PracticalCookBook.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PracticalCookBook.ViewModels
{
    public class MainViewModel : ViewModel
    {
        public ICommand GoToExampleRecipe { get; private set; }

        public MainViewModel(INavigator navigator)
            : base(navigator)
        {
            GoToExampleRecipe = new ActionCommand(() => {
                Debug.WriteLine("\tKliknięto na przycisk przykładowego przepisu.");
                _navigator.NavigateToRecipe(1);
            }, () => true);

            Debug.WriteLine("\tinicjalizacja MainViewModel");//TODO delete this
        }

        private void ShowExampleRecipe()
        {

        }
    }
}
