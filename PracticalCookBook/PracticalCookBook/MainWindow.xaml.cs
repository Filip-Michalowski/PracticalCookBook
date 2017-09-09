using PracticalCookBook.Framework;
using PracticalCookBook.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PracticalCookBook
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged, INavigator
    {
        private ViewModel _currentPage;

        public event PropertyChangedEventHandler PropertyChanged;

        public MainWindow()
        {
            _currentPage = new MainViewModel(this);

            InitializeComponent();

            {
                Database.DatabaseUpdater.Debug_SelfDestruct();

                using (var foo = new Database.DatabaseUpdater())
                {
                    foo.Update();
                }
            }//TODO remove this
        }

        public ViewModel CurrentPage {
            get
            {
                return _currentPage;
            }

            private set
            {
                _currentPage = value;
                
                PropertyChanged(this, new PropertyChangedEventArgs("CurrentPage"));
            }
        }

        public void NavigateToMainMenu()
        {
            Debug.WriteLine("\tWywołanie MainWindow.NavigateToMainMenu()");
            CurrentPage = new MainViewModel(this);
        }

        public void NavigateToRecipe(int recipeId)
        {
            Debug.WriteLine("\tWywołanie MainWindow.NavigateToRecipe()");
            CurrentPage = new RecipeViewModel(this, recipeId);
        }
    }
}
