using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using static System.Net.Mime.MediaTypeNames;

namespace EcolePlanning.Domain
{
    public class Activite
    {
        private static List<bool> UsedCounter = new List<bool>();
        private static object Lock = new object();

        public int Id { get; set; }        

        public string Libelle { get; set; }

        public string NomIntervenant { get; set; }

        public string PrenomIntervenant { get; set; }
        
        public string PathBackground { get; set; }

        public ImageSource BackgroundImage
        {
            get
            {
                return new BitmapImage(new Uri(PathBackground, UriKind.Relative));
            }
        }

        public int Id_LinkedActivity { get; set; }

        public Activite()
        {
            lock (Lock)
            {
                int nextIndex = GetAvailableIndex();
                if (nextIndex == -1)
                {
                    nextIndex = UsedCounter.Count;
                    UsedCounter.Add(true);
                }

                Id = nextIndex;
            }
        }

        private int GetAvailableIndex()
        {
            for (int i = 0; i < UsedCounter.Count; i++)
            {
                if (UsedCounter[i] == false)
                {
                    return i;
                }
            }

            // Nothing available.
            return -1;
        }

        public bool Equal(Activite e)
        {
            if (this.Id == e.Id)
                return true;
            else
                return false;
        }
    }
}
