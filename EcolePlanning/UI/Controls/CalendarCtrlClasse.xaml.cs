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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EcolePlanning.UI.Controls
{
    /// <summary>
    /// Logique d'interaction pour CalendarCtrlClassexaml.xaml
    /// </summary>
    public partial class CalendarCtrlClasse : UserControl
    {
        public Classe CurrentClasse { get; set; }
        public Creneau CurrentCreneau { get; set; }

        public CalendarCtrlClasse()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = CurrentClasse;
            backgroundImg.ImageSource = CurrentCreneau.Activite.BackgroundImage;
            activiteName.Text = CurrentCreneau.Activite.Libelle;
            LibelleCreneau.Text = CurrentCreneau.LibelleAffiche;
        }
        
        private void ButtonSupprimer_Click(object sender, RoutedEventArgs e)
        {
            RemoveCreneauEvent?.Invoke(this, CurrentCreneau);
        }

        private void ButtonAnnuler_Click(object sender, RoutedEventArgs e)
        {
            GridSupprimerBtn.Visibility = Visibility.Collapsed;
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (GridSupprimerBtn.Visibility == Visibility.Collapsed)
                GridSupprimerBtn.Visibility = Visibility.Visible;
            else
                GridSupprimerBtn.Visibility = Visibility.Collapsed;
        }

        public event EventHandler<Creneau> RemoveCreneauEvent;
    }
}
