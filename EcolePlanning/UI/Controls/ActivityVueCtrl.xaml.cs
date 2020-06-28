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
    /// Logique d'interaction pour ActivityCtrl.xaml
    /// </summary>
    public partial class ActivityVueCtrl : UserControl
    {
        public Activite CurrentActivite { get; set; }

        public ActivityVueCtrl()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = CurrentActivite;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainBorder.BorderBrush = Brushes.Red;
            ActiviteChoosed?.Invoke(this, CurrentActivite);
        }

        public void ResetSelection()
        {
            MainBorder.BorderBrush = Brushes.Transparent;
        }

        public event EventHandler<Activite> ActiviteChoosed;
    }
}
