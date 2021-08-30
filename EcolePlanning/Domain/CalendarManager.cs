using EcolePlanning.Tools;
using EcolePlanning.UI.Controls;
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

        public CalendarManager()
        {
            ListCreneauChoosed = new List<Creneau>();
        }

        public bool AddCreneau(Creneau cr, out int countConflictCreneau, out List<Creneau> conflitCreneau)
        {
            if (!IsCreneauAlreadyTaken(cr, out countConflictCreneau, out conflitCreneau))
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

        public bool IsCreneauAlreadyTaken(Creneau cr, out int countConflictCreneau, out List<Creneau> conflitCreneau)
        {
            countConflictCreneau = 0;
            bool alreadyTaken = false;
            conflitCreneau = new List<Creneau>();
            foreach (Creneau creneau in ListCreneauChoosed)
            {
                //Si la même activite ou bien la même classe
                //if ( (creneau.Classe == null || cr.Classe == null) || 
                //    creneau.Classe.Equal(cr.Classe) || creneau.Activite.Equal(cr.Activite))

                if ((creneau.Classe != null && cr.Classe != null && (creneau.Classe.Equal(cr.Classe) || creneau.Activite.Equal(cr.Activite)))
                    ||
                    (creneau.Classe != null && cr.Classe == null && (cr.ListClass_Custom.Where(x => x.Id == creneau.Classe.Id).Count() > 0))
                    ||
                    (creneau.Classe == null && cr.Classe == null && (cr.ListClass_Custom.Any(x => creneau.ListClass_Custom.Any(y => y.Id == x.Id))))
                   )
                {
                    //Si le même jour
                    if (creneau.Jour == cr.Jour)

                    {
                        //Si empiète sur l'heure d'un autre creneau
                        if ((cr.StartHour >= creneau.StartHour && cr.StartHour < creneau.EndHour)
                            || (cr.EndHour > creneau.StartHour && cr.EndHour < creneau.EndHour)
                            || (cr.StartHour < creneau.StartHour && cr.EndHour >= creneau.EndHour))
                        {
                            alreadyTaken = true;
                            conflitCreneau.Add(creneau);
                            countConflictCreneau += 1;
                            /*if (creneau.ConflitCreneauId == 0)
                            {
                                canBeSplit = true;
                            }*/
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
                    if (creneau.Classe != null && creneau.Classe.Equal(classe))
                    {
                        listToRemove.Add(creneau);
                    }

                    if (creneau.ListClass_Custom != null && creneau.ListClass_Custom.Contains(classe))
                    {
                        creneau.ListClass_Custom.Remove(classe);

                        if (creneau.ListClass_Custom.Count == 0)
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
