using PracticalCookBook.Framework;
using PracticalCookBook.ViewModels;
using System;
using System.Collections.Generic;
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
    public partial class MainWindow : Window
    {
        private IViewModel _currentPage = new MainViewModel();
        
        public MainWindow()
        {
            InitializeComponent();
        }

        public IViewModel CurrentPage {
            get
            {
                return _currentPage;
            }

            private set
            {
                _currentPage = value;
                //OnPropertyChanged("CurrentPage");//TODO doesn't seem to work
            }
        }
    }
}
