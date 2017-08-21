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
    class RecipeViewModel : ViewModel
    {
        public int RecipeId { get; private set; }

        public RecipeViewModel(INavigator navigator, int recipeId)
            : base(navigator)
        {
            RecipeId = recipeId;

            Debug.WriteLine("\tKonstruktor RecipeViewModel()");
        }
    }
}
