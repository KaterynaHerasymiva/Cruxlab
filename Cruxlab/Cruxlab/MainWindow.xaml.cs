using Cruxlab.Services;
using System.Windows;

namespace Cruxlab
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            // Every Control in WPF has DataContext property for assigning a viewmodel to it
            DataContext = new MainWindowViewModel(new PasswordsValidator());

            //A obj = new B();
            //obj.Do1();

        }
    }
}
