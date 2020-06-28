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
    /// Logique d'interaction pour DeleteActivity_Modal.xaml
    /// </summary>
    public partial class DeleteActivity_Modal : Window
    {
        public DeleteActivity_Modal()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ComboBxActivity.ItemsSource = DataManager.Instance.ListActivites;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Activite activityChoosed = ComboBxActivity.SelectedItem as Activite;

                CalendarManager.Instance.RemoveCreneauFromActivity(activityChoosed);

                DataManager.Instance.ListActivites.Remove(activityChoosed);

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problème lors de la suppression d'une activité : \n\n " + ex.Message);
            }
        }
    }
}
