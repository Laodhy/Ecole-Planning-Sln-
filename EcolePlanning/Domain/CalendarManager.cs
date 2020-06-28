using EcolePlanning.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcolePlanning.Domain
{
    public class CalendarManager : Singleton<CalendarManager>
    {
        public List<Creneau> ListCreneauChoosed { get; set; }
        public Classe SelectedClass { get; set; }

        public CalendarManager() {
            ListCreneauChoosed = new List<Creneau>();
        }

        public bool AddCreneau(Creneau cr, out bool canBeSplit, out Creneau conflitCreneau)
        {
            if (!IsCreneauAlreadyTaken(cr,out canBeSplit, out conflitCreneau))
            {
                ListCreneauChoosed.Add(cr);
                return true;
            }
            else
            {
                return false;
            }
        }

        public void RemoveCreneau(Creneau cr)
        {
            if (ListCreneauChoosed.Contains(cr))
            {
                ListCreneauChoosed.Remove(cr);
            }
        } 

        public bool IsCreneauAlreadyTaken(Creneau cr,out bool canBeSplit, out Creneau conflitCreneau)
        {
            bool alreadyTaken = false;
            canBeSplit = false;
            conflitCreneau = null;
            foreach (Creneau creneau in ListCreneauChoosed)
            {
                //Si la même activite ou bien la même classe
                if (creneau.Classe.Equal(cr.Classe) || creneau.Activite.Equal(cr.Activite))
                {
                    //Si le même jour
                    if (creneau.Jour == cr.Jour)

                    {
                        //Si empiète sur l'heure d'un autre creneau
                        if ( (cr.StartHour >= creneau.StartHour && cr.StartHour < creneau.EndHour) 
                            || (cr.EndHour > creneau.StartHour && cr.EndHour < creneau.EndHour)                            
                            || (cr.StartHour < creneau.StartHour && cr.EndHour >= creneau.EndHour) )
                        {
                            alreadyTaken = true;
                            conflitCreneau = creneau;
                            if (creneau.ConflitCreneauId == 0)
                            {
                                canBeSplit = true;
                            }
                        }
                    }
                }
            }

            return alreadyTaken;
        }

        public void RemoveCreneauFromActivity(Activite act)
        {
            try
            {
                List<Creneau> listToRemove = new List<Creneau>();

                foreach (Creneau creneau in ListCreneauChoosed)
                {
                    //Si la même activite 
                    if (creneau.Activite.Equal(act))
                    {
                        listToRemove.Add(creneau);
                    }
                }

                ListCreneauChoosed = ListCreneauChoosed.Except(listToRemove).ToList();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Problème lors de la suppression des créneaux d'une activité : \n\n" + ex.Message);
            }
        }

        public void RemoveCreneauFromClasse(Classe classe)
        {
            try
            {
                List<Creneau> listToRemove = new List<Creneau>();

                foreach (Creneau creneau in ListCreneauChoosed)
                {
                    //Si la même classe
                    if (creneau.Classe.Equal(classe))
                    {
                        listToRemove.Add(creneau);
                    }
                }

                ListCreneauChoosed = ListCreneauChoosed.Except(listToRemove).ToList();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Problème lors de la suppression des créneaux d'une activité : \n\n" + ex.Message);
            }
        }
    }
}
