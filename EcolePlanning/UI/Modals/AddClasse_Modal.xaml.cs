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
        public AddClasse_Modal()
        {
            InitializeComponent();
        }

        private void AddClasse_Click(object sender, RoutedEventArgs e)
        {
            if (!CustomColorPicker.SelectedColor.HasValue &&
                !string.IsNullOrEmpty(LblTxtBox.Text) &&
                !string.IsNullOrEmpty(PrenomTxtBox.Text))
                return;

            Classe newClasse = new Classe()
            {
                Libelle = LblTxtBox.Text,
                NomProfesseur = NomTxtBox.Text,
                PrenomProfesseur = PrenomTxtBox.Text,
                BackgroundColor = new SolidColorBrush(CustomColorPicker.SelectedColor.Value),
            };

            DataManager.Instance.ListClasses.Add(newClasse);

            this.Close();
        }
    }
}
