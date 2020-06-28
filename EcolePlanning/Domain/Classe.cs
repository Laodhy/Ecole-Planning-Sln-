using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace EcolePlanning.Domain
{
    public class Classe : IDisposable
    {
        private static List<bool> UsedCounter = new List<bool>();
        private static object Lock = new object();

        public int Id { get; set; }

        public string Libelle { get; set; }

        public string NomProfesseur { get; set; }

        public string PrenomProfesseur { get; set; }

        public Brush BackgroundColor { get; set; }

        public Classe()
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

        public void Dispose()
        {
            lock (Lock)
            {
                UsedCounter[Id] = false;
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

        public bool Equal(Classe e)
        {
            if (this.Id == e.Id)
                return true;
            else
                return false;
        }
    }
}
