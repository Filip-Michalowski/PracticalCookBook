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
    public class MainViewModel : IViewModel
    {
        public ICommand ExampleRecipeCommand { get; private set; }

        public MainViewModel()
        {
            this.ExampleRecipeCommand = new ActionCommand()

            Debug.WriteLine("\tinicjalizacja MainViewModel");//TODO delete this
        }
    }
}
