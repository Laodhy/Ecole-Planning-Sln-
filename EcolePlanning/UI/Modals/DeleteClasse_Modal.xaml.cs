using EcolePlanning.Domain;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace EcolePlanning.UI.Modals
{
    /// <summary>
    /// Logique d'interaction pour DeleteClasse_Modal.xaml
    /// </summary>
    public partial class DeleteClasse_Modal : Window
    {
        public DeleteClasse_Modal()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ComboBxClasses.ItemsSource = DataManager.Instance.ListClasses;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Classe classeChoosed = ComboBxClasses.SelectedItem as Classe;

                CalendarManager.Instance.RemoveCreneauFromClasse(classeChoosed);

                DataManager.Instance.ListClasses.Remove(classeChoosed);

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problème lors de la suppression d'une classe : \n\n " + ex.Message);
            }
        }
    }
}
