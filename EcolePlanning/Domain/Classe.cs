using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace EcolePlanning.Domain
{
    public class Classe
    {
        public int Id { get; set; }

        public string Libelle { get; set; }

        public string NomProfesseur { get; set; }

        public string PrenomProfesseur { get; set; }

        public Brush BackgroundColor { get; set; }

        public bool Equal(Classe e)
        {
            if (this.Id == e.Id)
                return true;
            else
                return false;
        }
    }
}
