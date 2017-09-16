using PracticalCookBook.Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PracticalCookBook.ViewModels
{
    class RecipeViewModel : ViewModel
    {
        #region Commnads
        public ICommand ToggleEdit { get; private set; }
        #endregion

        #region BindableProperties
        public int RecipeId { get; private set; }
        public string Title { get; private set; }
        public ObservableCollection<string> Ingredients { get; private set; }

        public bool EditMode {
            get
            {
                return _editMode;
            }
        }
        public bool ReadMode {
            get
            {
                return !_editMode;
            }
        }
        #endregion

        #region Private Properties
        private bool _editMode;
        private bool _newRecipe;
        #endregion

        #region Initializers
        private void initialize()
        {
            ToggleEdit = new ActionCommand(() => { ChangeEditMode(!_editMode); }, () => true);
            {
                Ingredients = new ObservableCollection<string>();
                Ingredients.Add("squash");
                Ingredients.Add("half a squash");
                Ingredients.Add("pinch of a squash");
            }//HACK delete this
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Opens page for a new recipe.
        /// </summary>
        /// <param name="navigator">Argument for dependency injection.</param>
        public RecipeViewModel(INavigator navigator)
            : base(navigator)
        {
            initialize();

            _newRecipe = true;

            ChangeEditMode(true);
        }

        /// <summary>
        /// Opens page for an existing recipe.
        /// </summary>
        /// <param name="navigator">Argument for dependency injection.</param>
        /// <param name="recipeId">Id of an existing recipe.</param>
        /// <param name="editMode">True if recipe should be opened for edits, false if only for viewing.</param>
        public RecipeViewModel(INavigator navigator, int recipeId, bool editMode)
            : base(navigator)
        {
            initialize();

            _newRecipe = false;

            RecipeId = recipeId;

            ChangeEditMode(editMode);

            Debug.WriteLine("\tKonstruktor RecipeViewModel()");
        }
        #endregion

        #region Methods
        public void ChangeEditMode(bool editMode)
        {
            _editMode = editMode;

            //NotifyPropertyChanged("EditMode");
            //NotifyPropertyChanged("ReadMode");
            NotifyPropertyChanged(null);//notifies that ALL properties have been updated

            Debug.WriteLine($"\tChangeEditMode({editMode})");//HACK remove
            //TODO finish UI changes related to EditMode switching
        }

        public void SaveChanges()
        {
            //TODO finish saving changes to recipe
        }
        #endregion

        #region Private classes
        
        #endregion
    }
}
