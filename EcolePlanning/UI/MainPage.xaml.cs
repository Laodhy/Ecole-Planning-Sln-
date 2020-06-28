using EcolePlanning.Domain;
using EcolePlanning.UI.Controls;
using EcolePlanning.UI.Modals;
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

namespace EcolePlanning.UI
{
    /// <summary>
    /// Logique d'interaction pour MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        private List<ClasseCtrl> _lstCalendarCtrl { get; set; }
        public bool IsActivityVues { get; set; }

        public MainPage()
        {
            InitializeComponent();
            _lstCalendarCtrl = new List<ClasseCtrl>();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ClearAllLists();

            IsActivityVues = true;
            FillVuesActivites();
        }

        // *************************************************************************************
        #region privates
        private int GetRowFromHour(string hour)
        {
            int row = 0;

            switch (hour)
            {
                case "08H30":
                    row = 1;
                    break;
                case "08H45":
                    row = 2;
                    break;
                case "09H00":
                    row = 3;
                    break;
                case "09H15":
                    row = 4;
                    break;
                case "09H30":
                    row = 5;
                    break;
                case "09H45":
                    row = 6;
                    break;
                case "10H00":
                    row = 7;
                    break;
                case "10H15":
                    row = 8;
                    break;
                case "10H30":
                    row = 9;
                    break;
                case "10H45":
                    row = 10;
                    break;
                case "11H00":
                    row = 11;
                    break;
                case "11H15":
                    row = 12;
                    break;
                case "11H30":
                    row = 13;
                    break;
                case "11H45":
                    row = 14;
                    break;
                case "13H30":
                    row = 16;
                    break;
                case "13H45":
                    row = 17;
                    break;
                case "14H00":
                    row = 18;
                    break;
                case "14H15":
                    row = 19;
                    break;
                case "14H30":
                    row = 20;
                    break;
                case "14H45":
                    row = 21;
                    break;
                case "15H00":
                    row = 22;
                    break;
                case "15H15":
                    row = 23;
                    break;
                case "15H30":
                    row = 24;
                    break;
                case "15H45":
                    row = 25;
                    break;
                case "16H00":
                    row = 26;
                    break;
                case "16H15":
                    row = 27;
                    break;
                case "16H30":
                    row = 28;
                    break;

            }

            return row;
        }

        private void FillVuesActivites()
        {
            ClearAllLists();

            foreach (Activite ac in DataManager.Instance.ListActivites)
            {
                ActivityVueCtrl control = new ActivityVueCtrl
                {
                    CurrentActivite = ac,
                    Margin = new Thickness(20, 10, 20, 10)
                };
                control.ActiviteChoosed += ActivityVueCtrl_ActiviteChoosed;
                StckVuesList.Children.Add(control);
            }

            // Fill classes
            foreach (Classe cl in DataManager.Instance.ListClasses)
            {
                ClasseCtrl control = new ClasseCtrl
                {
                    CurrentClasse = cl,
                    Margin = new Thickness(20, 8, 20, 8)
                };
                control.ClasseClicked += ClasseCtrl_ClasseClicked;
                control.CreneauChoosed += ClasseCtrl_CreneauChoosed;
                _lstCalendarCtrl.Add(control);
                StckClassesLst.Children.Add(control);
            }

            ActivityVueCtrl_ActiviteChoosed(this, DataManager.Instance.ListActivites[0]);
        }

        private void FillVuesClasses()
        {
            ClearAllLists();

            foreach (Classe cl in DataManager.Instance.ListClasses)
            {
                ClasseVueCtrl control = new ClasseVueCtrl
                {
                    CurrentClasse = cl,
                    Margin = new Thickness(20, 8, 20, 8)
                };
                control.ClasseChoosed += ClasseVueCtrl_ActiviteChoosed;
                StckVuesList.Children.Add(control);
            }

            ClasseVueCtrl_ActiviteChoosed(this, DataManager.Instance.ListClasses[0]);
        }

        private void ClearAllLists()
        {
            StckVuesList.Children.Clear();
            StckClassesLst.Children.Clear();
            _lstCalendarCtrl.Clear();
        }
        #endregion
        // *************************************************************************************
        #region Calendar methods
        private void FillCalendar()
        {
            CalendrierCtrl.ClearCalendar();

            foreach (Creneau creneau in CalendarManager.Instance.ListCreneauChoosed)
            {
                if (DataManager.Instance.ActiviteChoosed != null &&
                    (creneau.Activite.Equal(DataManager.Instance.ActiviteChoosed) ||
                     creneau.Activite.Id.Equals(DataManager.Instance.ActiviteChoosed.Id_LinkedActivity)))
                {
                    CalendarCtrlClasse classe = CreateCalendarClasseCtrlByCreneau(creneau);

                    CalendrierCtrl.AddCtrlClass(classe);
                }

                if (DataManager.Instance.ClasseChoosed != null &&
                    creneau.Classe.Equal(DataManager.Instance.ClasseChoosed))
                {
                    CalendarCtrlClasse classe = CreateCalendarClasseCtrlByCreneau(creneau);

                    CalendrierCtrl.AddCtrlClass(classe);
                }
            }

        }

        private CalendarCtrlClasse CreateCalendarClasseCtrlByCreneau(Creneau creneau)
        {
            CalendarCtrlClasse classe = new CalendarCtrlClasse();
            classe.CurrentClasse = creneau.Classe;
            classe.CurrentCreneau = creneau;
            classe.RemoveCreneauEvent += CalendarCtrlClasse_RemoveCreneauEvent;

            int jourChoosed = 0;
            switch (creneau.Jour)
            {
                case Enums.Days.Lundi:
                    jourChoosed = 1;
                    break;
                case Enums.Days.Mardi:
                    jourChoosed = 3;
                    break;
                case Enums.Days.Jeudi:
                    jourChoosed = 5;
                    break;
                case Enums.Days.Vendredi:
                    jourChoosed = 7;
                    break;
            }

            Grid.SetRow(classe, GetRowFromHour(creneau.Hour.Libelle));
            Grid.SetRowSpan(classe, creneau.Duree.RowSpan);
            if (creneau.Column != Enums.ColumnSpanType.Full)
            {
                Grid.SetColumnSpan(classe, 1);
                Grid.SetColumn(classe, jourChoosed + (int)creneau.Column);
            }
            else
            {
                Grid.SetColumnSpan(classe, 2);
                Grid.SetColumn(classe, jourChoosed);
            }

            return classe;
        }

        #endregion
        // *************************************************************************************
        #region Controls events
        private void ClasseVueCtrl_ActiviteChoosed(object sender, Classe e)
        {
            DataManager.Instance.ActiviteChoosed = null;
            DataManager.Instance.ClasseChoosed = e;
            //Reset visual selection of previous classes
            foreach (ClasseVueCtrl ctrl in StckVuesList.Children)
            {
                if (ctrl.CurrentClasse != e)
                    ctrl.ResetSelection();
            }

            UpdateNameCalendar();
            FillCalendar();

        }

        private void ActivityVueCtrl_ActiviteChoosed(object sender, Activite e)
        {
            DataManager.Instance.ActiviteChoosed = e;
            DataManager.Instance.ClasseChoosed = null;

            //Reset visual selection of previous activity
            foreach (ActivityVueCtrl ctrl in StckVuesList.Children)
            {
                if (ctrl.CurrentActivite != e)
                    ctrl.ResetSelection();
            }

            UpdateNameCalendar();
            FillCalendar();
        }

        private void ClasseCtrl_CreneauChoosed(object sender, Creneau e)
        {
            CalendarCtrlClasse classe = CreateCalendarClasseCtrlByCreneau(e);

            bool IsAdded = CalendarManager.Instance.AddCreneau(e, out bool canBeSplit, out Creneau conflitCreneau);
            if (IsAdded)
                CalendrierCtrl.AddCtrlClass(classe);
            else if (!IsAdded && canBeSplit)
            {

                string message = "Ce créneau est déjà pris ! \n\n" +
                    conflitCreneau.Classe.Libelle + " | " + conflitCreneau.Activite.Libelle + "\n" +
                    conflitCreneau.StartHour.ToString("hh\\:mm") + " à " + conflitCreneau.EndHour.ToString("hh\\:mm") +
                    "\n\n Voulez-vous le partagez ?";
                if (MessageBox.Show(message, "Attention", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                {
                    //No
                }
                else //Yes
                {
                    //On ajoute le creneau
                    CalendarManager.Instance.ListCreneauChoosed.Add(e);

                    //On sépare les creneaux
                    conflitCreneau.Column = Enums.ColumnSpanType.Column1;
                    conflitCreneau.ConflitCreneauId = e.ID;

                    e.ConflitCreneauId = conflitCreneau.ID;
                    e.Column = Enums.ColumnSpanType.Column2;
                    FillCalendar();
                }

            }
            else
            {
                string message2 = "Ce créneau est déjà pris ! \n\n" +
                    conflitCreneau.Classe.Libelle + " | " + conflitCreneau.Activite.Libelle + "\n" +
                    conflitCreneau.StartHour.ToString("hh\\:mm") + " à " + conflitCreneau.EndHour.ToString("hh\\:mm");

                MessageBox.Show(message2);
            }
        }

        private void CalendarCtrlClasse_RemoveCreneauEvent(object sender, Creneau e)
        {
            try
            {
                CalendarCtrlClasse classe = sender as CalendarCtrlClasse;
                CalendrierCtrl.RemoveCtrlClasse(classe);
                CalendarManager.Instance.RemoveCreneau(e);

                if (e.Column != Enums.ColumnSpanType.Full)
                {
                    Creneau conflictCreneau = CalendarManager.Instance.ListCreneauChoosed.Single(x => x.ID == e.ConflitCreneauId);
                    conflictCreneau.Column = Enums.ColumnSpanType.Full;
                    conflictCreneau.ConflitCreneauId = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problème lors de la suppression du créneau " +
                    e.Classe.Libelle + "-" + e.Activite.Libelle + " : \n\n" + ex.Message);
            }
            finally
            {
                FillCalendar();
            }
        }

        private void ClasseCtrl_ClasseClicked(object sender, Classe e)
        {
            foreach (ClasseCtrl ctrl in _lstCalendarCtrl)
            {
                ctrl.HideInfos();
            }
            CalendarManager.Instance.SelectedClass = e;
        }

        private void UpdateNameCalendar()
        {
            if (DataManager.Instance.ActiviteChoosed != null)
            {
                TitleNameCalendar.Text = DataManager.Instance.ActiviteChoosed.Libelle;
                //TitleNameCalendar.Foreground = Brushes.Black;
            }
            else if (DataManager.Instance.ClasseChoosed != null)
            {
                TitleNameCalendar.Text = DataManager.Instance.ClasseChoosed.Libelle;
                //TitleNameCalendar.Foreground = DataManager.Instance.ClasseChoosed.BackgroundColor;

            }
            else
            {
                TitleNameCalendar.Text = "";
            }
        }
        #endregion

        // *************************************************************************************
        #region Events
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            DataManager.Instance.SaveData();
        }

        private void PdfButton_Click(object sender, RoutedEventArgs e)
        {
            DataManager.Instance.PrintPdf(CalendrierCtrl);
        }

        private void IntervenantsBtn_Click(object sender, RoutedEventArgs e)
        {
            IsActivityVues = true;
            FillVuesActivites();
        }

        private void ClassesBtn_Click(object sender, RoutedEventArgs e)
        {
            IsActivityVues = false;
            FillVuesClasses();
        }
        #endregion
        // *************************************************************************************
        #region Menu Modals
        private void AddActivity_Click(object sender, RoutedEventArgs e)
        {
            AddIntervenant_Modal modal = new AddIntervenant_Modal();
            modal.ShowDialog();

            if (IsActivityVues)
                FillVuesActivites();
            else
                FillVuesClasses();
        }

        private void DeleteActivity_Click(object sender, RoutedEventArgs e)
        {
            DeleteActivity_Modal modal = new DeleteActivity_Modal();
            modal.ShowDialog();

            if (IsActivityVues)
                FillVuesActivites();
            else
                FillVuesClasses();
        }

        private void AddClasse_Click(object sender, RoutedEventArgs e)
        {
            AddClasse_Modal modal = new AddClasse_Modal();
            modal.ShowDialog();

            if (IsActivityVues)
                FillVuesActivites();
            else
                FillVuesClasses();
        }
        private void DeleteClasse_Click(object sender, RoutedEventArgs e)
        {
            DeleteClasse_Modal modal = new DeleteClasse_Modal();
            modal.ShowDialog();

            if (IsActivityVues)
                FillVuesActivites();
            else
                FillVuesClasses();
        }

        #endregion

        // *************************************************************************************
        #region Cacher/Montrer menus
        bool isActivityShow = true;
        bool isClassesShow = true;
        private void HideActivityBtn_Click(object sender, RoutedEventArgs e)
        {
            if (isActivityShow)
            {
                //Cacher menu
                ActivityMenuStack.Visibility = Visibility.Collapsed;
                hideActivityBtn.Content = ">";

                isActivityShow = false;
            }
            else
            {
                //Montrer menu
                ActivityMenuStack.Visibility = Visibility.Visible;
                hideActivityBtn.Content = "<";
                isActivityShow = true;
            }

        }

        private void HideClassBtn_Click(object sender, RoutedEventArgs e)
        {
            if (isClassesShow)
            {
                //Cacher menu
                ClassesMenuStack.Visibility = Visibility.Collapsed;
                hideClassBtn.Content = "<";

                isClassesShow = false;
            }
            else
            {
                //Montrer menu
                ClassesMenuStack.Visibility = Visibility.Visible;
                hideClassBtn.Content = ">";
                isClassesShow = true;
            }

        }
        #endregion

    }
}
