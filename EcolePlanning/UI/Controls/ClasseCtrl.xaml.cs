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
    public partial class ClasseCtrl : UserControl
    {
        public Classe CurrentClasse { get; set; }

        public ClasseCtrl()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = CurrentClasse;

            
            ComboDays.ItemsSource = new List<string>() { "Lundi", "Mardi", "Jeudi", "Vendredi" };
            ComboHours.ItemsSource = DataManager.Instance.HoursAvailables;
            ComboDuree.ItemsSource = DataManager.Instance.DureeAvailable;
        }

        public void HideInfos()
        {
            stckSetInfos.Visibility = Visibility.Collapsed;
        }

        private void Classe_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ClasseClicked?.Invoke(this, CurrentClasse);

            if (stckSetInfos.Visibility == Visibility.Visible)
                stckSetInfos.Visibility = Visibility.Collapsed;
            else
                stckSetInfos.Visibility = Visibility.Visible;
        }

        private void ClasseInfos_MouseDown(object sender, MouseButtonEventArgs e)
        {
            HideInfos();
        }
        private void Valider_Click(object sender, RoutedEventArgs e)
        {
            if (ComboDays.SelectedItem == null || ComboHours.SelectedItem == null || ComboDuree.SelectedItem == null)
                return;

            string day = ComboDays.SelectedItem as string;
            Days dayChoosed = GetDayByStr(day);
            HourModel hour = ComboHours.SelectedItem as HourModel;
            DureeModel duree = ComboDuree.SelectedItem as DureeModel;
            string libelleAffiche = TxtBoxLibelle.Text;

            Creneau creneauChoosed = new Creneau()
            {
                Activite = DataManager.Instance.ActiviteChoosed,
                Classe = CurrentClasse,
                Jour = dayChoosed,
                Duree = duree,
                Hour = hour,
                Column = ColumnSpanType.Full,
                LibelleAffiche = string.IsNullOrEmpty(libelleAffiche) ? CurrentClasse.Libelle : libelleAffiche,
                ConflitCreneauId = 0
            };

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

        public event EventHandler<Classe> ClasseClicked;
    }
}
