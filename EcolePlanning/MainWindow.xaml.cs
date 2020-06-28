
using System.Windows;
using EcolePlanning.UI;

namespace EcolePlanning
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Set the first page
            this.mainFrame.Navigate(new MainPage());
        }

    }
}
