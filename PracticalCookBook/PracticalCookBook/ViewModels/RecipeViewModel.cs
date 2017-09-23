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
        public ObservableCollection<IngredientTemplateItem> Ingredients { get; private set; }
        public string Preparation { get; private set; }
        public ObservableCollection<TagTemplateItem> Tags { get; private set; }

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

            Ingredients = new ObservableCollection<IngredientTemplateItem>();
            Tags = new ObservableCollection<TagTemplateItem>();

            {
                Ingredients.Add(new IngredientTemplateItem(1, "squash"));
                Ingredients.Add(new IngredientTemplateItem(2, "half a squash"));
                Ingredients.Add(new IngredientTemplateItem(3, "pinch of a squash"));

                Tags.Add(new TagTemplateItem(1, "squash-things"));
                Tags.Add(new TagTemplateItem(2, "edible"));
                Tags.Add(new TagTemplateItem(3, "verisimilitude"));
                Tags.Add(new TagTemplateItem(4, "the-meaning-of-life-universe-and-everything"));
                Tags.Add(new TagTemplateItem(5, "miscellaneous"));
                Tags.Add(new TagTemplateItem(6, "assorted"));
                Tags.Add(new TagTemplateItem(7, "padder-test-debug-assorted-thingie"));
                Tags.Add(new TagTemplateItem(8, "and-one-more-thing"));
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
        public class IngredientTemplateItem
        {
            public int Id { get; set; }
            public string Body { get; set; }

            public IngredientTemplateItem(int id, string body)
            {
                Id = id;
                Body = body;
            }
        }

        public class TagTemplateItem
        {
            public int Id { get; set; }
            public string Name { get; set; }

            public TagTemplateItem(int id, string name)
            {
                Id = id;
                Name = name;
            }
        }
        #endregion
    }
}
