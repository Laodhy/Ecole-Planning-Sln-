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
    /// Logique d'interaction pour CalendrierCtrl.xaml
    /// </summary>
    public partial class CalendrierCtrl : UserControl
    {
        public CalendrierCtrl()
        {
            InitializeComponent();
        }

        public void AddCtrlClass(CalendarCtrlClasse classe)
        {
            CalendarGridContent.Children.Add(classe);
        }

        public void RemoveCtrlClasse(CalendarCtrlClasse classe)
        {
            CalendarGridContent.Children.Remove(classe);
        }

        public void ClearCalendar()
        {
            CalendarGridContent.Children.Clear();
        }
    }
}
