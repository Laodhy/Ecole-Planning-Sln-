using EcolePlanning.Tools;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace EcolePlanning.Domain
{
    public class DataManager : Singleton<DataManager>
    {
        private List<Classe> _listClasses;
        public List<Classe> ListClasses
        {
            get
            {
                return _listClasses;
            }
            set
            {
                _listClasses = value;
            }
        }

        private List<Activite> _listActivites;
        public List<Activite> ListActivites
        {
            get
            {
                return _listActivites;
            }
            set
            {
                _listActivites = value;
            }
        }

        private List<AVS> _listAVS;
        public List<AVS> ListAVS
        {
            get
            {
                return _listAVS;
            }
            set
            {
                _listAVS = value;
            }
        }

        public List<DureeModel> DureeAvailable { get; set; }
        public List<HourModel> HoursAvailables { get; set; }

        public Activite ActiviteChoosed { get; set; }
        public Classe ClasseChoosed { get; set; }

        public DataManager()
        {
            ListActivites = new List<Activite>();
            ListClasses = new List<Classe>();
            ListAVS = new List<AVS>();
            DureeAvailable = new List<DureeModel>();
            HoursAvailables = new List<HourModel>();
        }

        public void Init()
        {
#if DEBUG
            CreateData();

#else
            ReadData();
#endif
        }

        public void PrintPdf(UI.Controls.CalendrierCtrl Calendrier)
        {
            try
            {
                string folderImage = ConfigurationManager.AppSettings["FolderSaveImage"].ToString();

                RenderTargetBitmap image = GetImage(Calendrier);

                PngBitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(image));

                using (Stream fileStream = new FileStream(folderImage + "Image.png", FileMode.OpenOrCreate, FileAccess.Write))
                {
                    encoder.Save(fileStream);
                }


            }
            catch (Exception ex)
            {
                string message = "Un problème est survenu lors de l'exportation en PDF ! \n \n" + ex;
                MessageBox.Show(message);
            }
        }

        public void SaveData()
        {

            try
            {
                string folderSave = ConfigurationManager.AppSettings["FolderSaveJSON"].ToString();

#region  Sauvegarde des Activités / Classes / Durée disponibles / Heures : dans un fichier JSON
                File.WriteAllText(folderSave + "listActivites.json", JsonConvert.SerializeObject(ListActivites));
                File.WriteAllText(folderSave + "listClasses.json", JsonConvert.SerializeObject(ListClasses));
                File.WriteAllText(folderSave + "listDuree.json", JsonConvert.SerializeObject(DureeAvailable));
                File.WriteAllText(folderSave + "listHour.json", JsonConvert.SerializeObject(HoursAvailables));
#endregion

                // Sauvegarde des créneaux 
                File.WriteAllText(folderSave + "listCreneauChoosed.json", JsonConvert.SerializeObject(CalendarManager.Instance.ListCreneauChoosed));

                string message = "Données sauvegardées !";
                MessageBox.Show(message);
            }
            catch (Exception ex)
            {
                string message = "Un problème est survenu lors de la sauvegarde ! \n \n" + ex;
                MessageBox.Show(message);
            }

        }

        public void ReadData()
        {
            try
            {
                StreamReader reader;
                string myJson;
                string folderSave = ConfigurationManager.AppSettings["FolderSaveJSON"].ToString();

#region  Chargement des Activités / Classes / Durée disponibles / Heures : depui un fichier JSON
                reader = new StreamReader(folderSave + "listActivites.json");
                myJson = reader.ReadToEnd();
                ListActivites = JsonConvert.DeserializeObject<List<Activite>>(myJson);

                reader = new StreamReader(folderSave + "listClasses.json");
                myJson = reader.ReadToEnd();
                ListClasses = JsonConvert.DeserializeObject<List<Classe>>(myJson);

                reader = new StreamReader(folderSave + "listDuree.json");
                myJson = reader.ReadToEnd();
                DureeAvailable = JsonConvert.DeserializeObject<List<DureeModel>>(myJson);

                reader = new StreamReader(folderSave + "listHour.json");
                myJson = reader.ReadToEnd();
                HoursAvailables = JsonConvert.DeserializeObject<List<HourModel>>(myJson);
#endregion

                // Chargement des créneaux 
                reader = new StreamReader(folderSave + "listCreneauChoosed.json");
                myJson = reader.ReadToEnd();
                CalendarManager.Instance.ListCreneauChoosed = JsonConvert.DeserializeObject<List<Creneau>>(myJson);

            }
            catch (Exception ex)
            {

                string message = "Un problème est survenu lors du chargement des données ! \n \n" + ex;
                MessageBox.Show(message);
            }
        }

        public void CreateData()
        {
            try
            {
#region Activites
                Activite act1 = new Activite()
                {
                    Id = 1,
                    Libelle = "Echecs",
                    NomIntervenant = "",
                    PrenomIntervenant = "",
                    PathBackground = @"Res\echecBg.jpg",
                };
                Activite act2 = new Activite()
                {
                    Id = 2,
                    Libelle = "Musique",
                    NomIntervenant = "",
                    PrenomIntervenant = "",
                    PathBackground = @"Res\musiqueBg.jpg",
                    Id_LinkedActivity = 3,
                };
                Activite act3 = new Activite()
                {
                    Id = 3,
                    Libelle = "Musique Chorale",
                    NomIntervenant = "",
                    PrenomIntervenant = "",
                    PathBackground = @"Res\choraleBg.jpg",
                    Id_LinkedActivity = 2,
                };
                Activite act4 = new Activite()
                {
                    Id = 4,
                    Libelle = "Pastorale",
                    NomIntervenant = " ",
                    PrenomIntervenant = " ",
                    PathBackground = @"Res\pastoraleBg.jpg",
                };
                Activite act5 = new Activite()
                {
                    Id = 5,
                    Libelle = "Bibliothèque",
                    NomIntervenant = " ",
                    PrenomIntervenant = " ",
                    PathBackground = @"Res\biblioBg.jpg",
                    Id_LinkedActivity = 6,
                };
                Activite act6 = new Activite()
                {
                    Id = 6,
                    Libelle = "Informatique",
                    NomIntervenant = " ",
                    PrenomIntervenant = " ",
                    PathBackground = @"Res\infoBg.jpg",
                    Id_LinkedActivity = 5,
                };
                Activite act7 = new Activite()
                {
                    Id = 7,
                    Libelle = "Sport",
                    NomIntervenant = " ",
                    PrenomIntervenant = " ",
                    PathBackground = @"Res\sportBg.jpg",
                };
                Activite act8 = new Activite()
                {
                    Id = 8,
                    Libelle = "Anglais",
                    NomIntervenant = " ",
                    PrenomIntervenant = " ",
                    PathBackground = @"Res\anglaisBg.jpg",
                };
                Activite act9 = new Activite()
                {
                    Id = 9,
                    Libelle = "Histoire",
                    NomIntervenant = " ",
                    PrenomIntervenant = " ",
                    PathBackground = @"Res\histoireBg.jpg",
                };
                Activite act10 = new Activite()
                {
                    Id = 10,
                    Libelle = "Autre",
                    NomIntervenant = " ",
                    PrenomIntervenant = " ",
                    PathBackground = @"Res\autreBg.jpg",
                };



                ListActivites.Add(act1);
                ListActivites.Add(act2);
                ListActivites.Add(act3);
                ListActivites.Add(act4);
                ListActivites.Add(act5);
                ListActivites.Add(act6);
                ListActivites.Add(act7);
                ListActivites.Add(act8);
                ListActivites.Add(act9);
                ListActivites.Add(act10);
                #endregion

                BrushConverter brushConverter = new BrushConverter();
#region Classes
                Classe psms = new Classe()
                {
                    Id = 1,
                    Libelle = "PS/MS",
                    NomProfesseur = " ",
                    PrenomProfesseur = "Axelle & Chantal",
                    BackgroundColor = (SolidColorBrush)brushConverter.ConvertFrom("#ffe680")
                };
                Classe msgs = new Classe()
                {
                    Id = 2,
                    Libelle = "MS/GS",
                    NomProfesseur = " ",
                    PrenomProfesseur = "Virginie",
                    BackgroundColor = (SolidColorBrush)brushConverter.ConvertFrom("#ff8080")
                };
                Classe cpce1 = new Classe()
                {
                    Id = 3,
                    Libelle = "CP",
                    NomProfesseur = " ",
                    PrenomProfesseur = "Caroline",
                    BackgroundColor = (SolidColorBrush)brushConverter.ConvertFrom("#ff80b3")
                };
                Classe ce1ce2 = new Classe()
                {
                    Id = 4,
                    Libelle = "CE1/CE2",
                    NomProfesseur = " ",
                    PrenomProfesseur = "Nathalie",
                    BackgroundColor = (SolidColorBrush)brushConverter.ConvertFrom("#8080ff")
                };
                Classe ce2 = new Classe()
                {
                    Id = 5,
                    Libelle = "CE2/CM1",
                    NomProfesseur = " ",
                    PrenomProfesseur = "Laurence & Chantal",
                    BackgroundColor = (SolidColorBrush)brushConverter.ConvertFrom("#80dfff")
                };
                Classe cm1 = new Classe()
                {
                    Id = 6,
                    Libelle = "CM1",
                    NomProfesseur = " ",
                    PrenomProfesseur = "Cassandra",
                    BackgroundColor = (SolidColorBrush)brushConverter.ConvertFrom("#80ffbf")
                };
                Classe cm2 = new Classe()
                {
                    Id = 7,
                    Libelle = "CM2",
                    NomProfesseur = " ",
                    PrenomProfesseur = "Sophie",
                    BackgroundColor = (SolidColorBrush)brushConverter.ConvertFrom("#d5ff80")
                };

                ListClasses.Add(psms);
                ListClasses.Add(msgs);
                ListClasses.Add(cpce1);
                ListClasses.Add(ce1ce2);
                ListClasses.Add(ce2);
                ListClasses.Add(cm1);
                ListClasses.Add(cm2);
#endregion

#region Duree 
                DureeModel duree1 = new DureeModel() { Libelle = "30min", Minutes = 30, RowSpan = 2 };
                DureeModel duree2 = new DureeModel() { Libelle = "45min", Minutes = 45, RowSpan = 3 };
                DureeModel duree3 = new DureeModel() { Libelle = "1h", Minutes = 60, RowSpan = 4 };
                DureeModel duree4 = new DureeModel() { Libelle = "1h30", Minutes = 90, RowSpan = 6 };
                DureeModel duree5 = new DureeModel() { Libelle = "2h", Minutes = 120, RowSpan = 8 };
                DureeAvailable.Add(duree1);
                DureeAvailable.Add(duree2);
                DureeAvailable.Add(duree3);
                DureeAvailable.Add(duree4);
                DureeAvailable.Add(duree5);
#endregion

#region Heures
                TimeSpan timeStart = new TimeSpan(8, 30, 0);
                TimeSpan timeEnd = new TimeSpan(16, 30, 0);
                for (TimeSpan time = new TimeSpan(8, 30, 0); time <= timeEnd; time = time.Add(new TimeSpan(0, 15, 0)))
                {

                    //Pas les plages horaires entre midi et 14h
                    if (time != new TimeSpan(12, 0, 0) && time != new TimeSpan(12, 15, 0) && time != new TimeSpan(12, 30, 0) &&
                        time != new TimeSpan(12, 45, 0) && time != new TimeSpan(13, 0, 0) && time != new TimeSpan(13, 15, 0))
                    {

                        HoursAvailables.Add(new HourModel() { Time = time, Libelle = time.ToString("hh") + "H" + time.ToString("mm") });
                    }

                }
#endregion

                StreamReader reader;
                string myJson;
                string folderSave = ConfigurationManager.AppSettings["FolderSaveJSON"].ToString();

                // Chargement des créneaux 
                reader = new StreamReader(folderSave + "listCreneauChoosed.json");
                myJson = reader.ReadToEnd();
                CalendarManager.Instance.ListCreneauChoosed = JsonConvert.DeserializeObject<List<Creneau>>(myJson);
            }
            catch (Exception ex)
            {
                string message = "Un problème est survenu lors du chargement des données ! \n \n" + ex;
                MessageBox.Show(message);
            }
        }

        private static RenderTargetBitmap GetImage(UI.Controls.CalendrierCtrl view)
        {
            Size size = new Size(view.ActualWidth, view.ActualHeight);
            if (size.IsEmpty)
                return null;

            RenderTargetBitmap result = new RenderTargetBitmap((int)size.Width, (int)size.Height, 96, 96, PixelFormats.Default);

            DrawingVisual drawingvisual = new DrawingVisual();
            using (DrawingContext context = drawingvisual.RenderOpen())
            {
                context.DrawRectangle(new VisualBrush(view), null, new Rect(new Point(), size));
                context.Close();
            }

            result.Render(drawingvisual);
            return result;
        }
    }
}
