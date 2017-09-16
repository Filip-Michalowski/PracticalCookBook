using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticalCookBook.Framework
{
    /// <summary>
    /// Interface for the single point of navigation.
    /// </summary>
    public interface INavigator
    {
        void NavigateToMainMenu();
        void NavigateToRecipe();
        void NavigateToRecipe(int recipeId);
    }
}
