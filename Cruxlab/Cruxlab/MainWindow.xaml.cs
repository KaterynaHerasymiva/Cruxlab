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
            DataContext = new MainWindowViewModel(new PasswordsValidator());
        }
    }
}
