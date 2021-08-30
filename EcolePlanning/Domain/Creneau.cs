using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EcolePlanning.Domain.Enums;

namespace EcolePlanning.Domain
{

    public class Creneau
    {
        private static List<bool> UsedCounter = new List<bool>();
        private static object Lock = new object();

        public int ID { get; private set; }

        public Activite Activite { get; set; }
        public Classe Classe { get; set; }
        public List<Classe> ListClass_Custom { get; set; }
        public Days Jour { get; set; }
        public HourModel Hour { get; set; }
        public DureeModel Duree { get; set; }
        public ColumnSpanType Column { get; set; }
        public int ConflitCreneauId { get; set; }
        public string LibelleAffiche { get; set; }

        public Creneau()
        {
           /* lock (Lock)
            {
                int nextIndex = GetAvailableIndex();
                if (nextIndex == -1)
                {
                    nextIndex = UsedCounter.Count;
                    UsedCounter.Add(true);
                }

                ID = nextIndex;
            }*/
        }

        public Creneau(bool createID = false) : base()
        {
            if (createID)
            {
                lock (Lock)
                {
                    int nextIndex = GetAvailableIndex();
                    if (nextIndex == -1)
                    {
                        nextIndex = UsedCounter.Count;
                        UsedCounter.Add(true);
                    }

                    ID = nextIndex;
                }
            }
        }

        public TimeSpan StartHour {
            get
            {
                return Hour.Time;
            }
        }

        public TimeSpan EndHour
        {
            get
            {
                return Hour.Time.Add(new TimeSpan(0, Duree.Minutes, 0));
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
    }

    public class DureeModel
    {
        public int Minutes { get; set; }
        public string Libelle { get; set; }
        public int RowSpan { get; set; }
    }

    public class HourModel
    {
        public TimeSpan Time { get; set; }
        public string Libelle { get; set; }
    }
}
