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
using static EcolePlanning.Domain.Enums;

namespace EcolePlanning.UI.Controls
{
    /// <summary>
    /// Logique d'interaction pour ClasseCalendarCtrl.xaml
    /// </summary>
    public partial class CustomCreneau_Ctrl : UserControl
    {

        public CustomCreneau_Ctrl()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //this.DataContext = CurrentClasse;


            ComboDays.ItemsSource = new List<string>() { "Lundi", "Mardi", "Jeudi", "Vendredi" };
            ComboHours.ItemsSource = DataManager.Instance.HoursAvailables;
            ComboDuree.ItemsSource = DataManager.Instance.DureeAvailable;
            LstClasses.ItemsSource = DataManager.Instance.ListClasses;
        }

        public void HideInfos()
        {
            stckSetInfos.Visibility = Visibility.Collapsed;
        }

        private void Classe_MouseDown(object sender, MouseButtonEventArgs e)
        {

            if (stckSetInfos.Visibility == Visibility.Visible)
                stckSetInfos.Visibility = Visibility.Collapsed;
            else
            {
                Clicked?.Invoke(this, e);
                stckSetInfos.Visibility = Visibility.Visible;
            }
        }

        private void ClasseInfos_MouseDown(object sender, MouseButtonEventArgs e)
        {
            HideInfos();
        }
        private void Valider_Click(object sender, RoutedEventArgs e)
        {
            if (ComboDays.SelectedItem == null ||
                ComboHours.SelectedItem == null ||
                ComboDuree.SelectedItem == null ||
                string.IsNullOrEmpty(TxtBoxLibelle.Text) ||
                LstClasses.SelectedItems.Count == 0)
                return;

            string day = ComboDays.SelectedItem as string;
            Days dayChoosed = GetDayByStr(day);
            HourModel hour = ComboHours.SelectedItem as HourModel;
            DureeModel duree = ComboDuree.SelectedItem as DureeModel;
            string libelleAffiche = TxtBoxLibelle.Text;
            List<Classe> lstClasse = new List<Classe>();
            lstClasse.AddRange(LstClasses.SelectedItems.Cast<Classe>());

            Creneau creneauChoosed = new Creneau()
            {
                Activite = DataManager.Instance.ActiviteChoosed,
                //Classe = CurrentClasse,
                ListClass_Custom = lstClasse,
                Jour = dayChoosed,
                Duree = duree,
                Hour = hour,
                Column = ColumnSpanType.Full,
                LibelleAffiche = libelleAffiche,
                ConflitCreneauId = 0
            };

            LstClasses.SelectedItem = null;

            CreneauChoosed?.Invoke(this, creneauChoosed);

        }

        private Days GetDayByStr(string day)
        {
            switch (day)
            {
                case "Lundi":
                    return Days.Lundi;
                case "Mardi":
                    return Days.Mardi;
                case "Jeudi":
                    return Days.Jeudi;
                default:
                    return Days.Vendredi;
            }
        }

        public event EventHandler<Creneau> CreneauChoosed;

        public event EventHandler Clicked;
    }
}
