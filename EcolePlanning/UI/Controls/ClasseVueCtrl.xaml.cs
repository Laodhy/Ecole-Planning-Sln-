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
    /// Logique d'interaction pour ClasseVueCtrl.xaml
    /// </summary>
    public partial class ClasseVueCtrl : UserControl
    {
        public Classe CurrentClasse { get; set; }

        public ClasseVueCtrl()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = CurrentClasse;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainBorder.BorderBrush = Brushes.Red;
            ClasseChoosed?.Invoke(this, CurrentClasse);
        }

        public void ResetSelection()
        {
            MainBorder.BorderBrush = Brushes.Transparent;
        }

        public event EventHandler<Classe> ClasseChoosed;
    }
}
