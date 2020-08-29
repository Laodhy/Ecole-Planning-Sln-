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
    /// Logique d'interaction pour AddClasse_Modal.xaml
    /// </summary>
    public partial class AddClasse_Modal : Window
    {
        public bool IsModification { get; set; }
        private Classe currentClasse { get; set; }

        public AddClasse_Modal()
        {
            InitializeComponent();
            IsModification = false;
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            currentClasse = null;

            if (IsModification)
            {
                chooseClassGroup.Visibility = Visibility.Visible;
                ComboBxClasses.ItemsSource = DataManager.Instance.ListClasses;

                if (DataManager.Instance.ListClasses.Count > 0)
                {
                    ComboBxClasses.SelectedIndex = 0;
                }

                BtnAddClasse.Content = "Modifier classe";
            }
            else
            {
                chooseClassGroup.Visibility = Visibility.Collapsed;
            }
        }

        private void ComboBxClasses_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Classe classe = ComboBxClasses.SelectedItem as Classe;
            LblTxtBox.Text = classe.Libelle;
            PrenomTxtBox.Text = classe.PrenomProfesseur;
            NomTxtBox.Text = classe.NomProfesseur;
            CustomColorPicker.SelectedColor = ((SolidColorBrush)classe.BackgroundColor).Color;

            currentClasse = classe;
        }
        // ==================================================================

        private void AddClasse_Click(object sender, RoutedEventArgs e)
        {
            if (!CustomColorPicker.SelectedColor.HasValue &&
                !string.IsNullOrEmpty(LblTxtBox.Text) &&
                !string.IsNullOrEmpty(PrenomTxtBox.Text))
                return;

            if (IsModification)
            {
                Classe c = DataManager.Instance.ListClasses.Single(x => x.Id == currentClasse.Id);
                int index = DataManager.Instance.ListClasses.IndexOf(c);
                c.Libelle = LblTxtBox.Text;
                c.NomProfesseur = NomTxtBox.Text;
                c.PrenomProfesseur = PrenomTxtBox.Text;
                c.BackgroundColor = new SolidColorBrush(CustomColorPicker.SelectedColor.Value);
                DataManager.Instance.ListClasses[index] = c;
            }
            else
            {
                Classe newClasse = new Classe()
                {
                    Libelle = LblTxtBox.Text,
                    NomProfesseur = NomTxtBox.Text,
                    PrenomProfesseur = PrenomTxtBox.Text,
                    BackgroundColor = new SolidColorBrush(CustomColorPicker.SelectedColor.Value),
                };

                DataManager.Instance.ListClasses.Add(newClasse);
            }
            this.Close();
        }

    }
}
