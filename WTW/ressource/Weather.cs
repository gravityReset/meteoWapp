using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Xml.Linq;
using Windows.Data.Xml.Dom;
using Windows.UI.Popups;

namespace WTW.ressource
{

    public class Weather : INotifyPropertyChanged
    {
        private const string Err404 = "{\"message\":\"\",\"cod\":\"404\"}\n";
        public event PropertyChangedEventHandler PropertyChanged;
        private string location;
        private string city;
        private string pays;


        private ObservableCollection<meteo> meteoP;
        public ObservableCollection<meteo> Meteo { get { return meteoP; } private set { meteoP = value; } }
        public string City
        {
            get { return city; }
            set { city = value; OnPropertyChanged("City"); OnPropertyChanged("PaysFull"); }
        }
        public string PaysFull
        {
            get { return string.Format("{1} {2}", City , Pays); }
        }
        public string Pays
        {
            get { return pays; }
            set { pays = value; OnPropertyChanged("Pays"); OnPropertyChanged("PaysFull"); }
        }

        public Weather()
        {
            this.location = Param.Location;
            City = string.Empty;
            Meteo = new ObservableCollection<meteo>();
            for (int i = 0; i < 7; i++)
                Meteo.Add(new meteo(i));

        }

        /// <summary>
        /// recupere les information météo
        /// </summary>
        /// <returns>renvoie string.Empty si tout a fonctionner sinon renvoie lemessage d'érreur</returns>
        public async void GetCurrentConditions(bool refresher = false)
        {
            if (refresher)
                location = Param.Location;
            try
            {
                var xml = await new System.Net.Http.HttpClient().GetStringAsync(
                    string.Format("http://api.openweathermap.org/data/2.5/forecast/daily?q={0}&mode=xml&units=metric&cnt={1}&lang={2}",
                    location, 7, "fr"));

                xmlMeteo(xml);
            }
            catch (Exception ex)
            {
                MessageDialog msgDialog = new MessageDialog(ex.Message, "Erreur chargement meteo");
                msgDialog.ShowAsync();
            }
        }

        public async void GetCurrentConditions(double longitude, double lattitude)
        {
            try
            {
                string xml = await new System.Net.Http.HttpClient().GetStringAsync(
                    string.Format("http://api.openweathermap.org/data/2.5/forecast/daily?lat={0}&lon={1}&mode=xml&units=metric&cnt={2}&lang={3}",
                     lattitude, longitude, 7, "fr"));

                //read from xmlDocument for your values.
                xmlMeteo(xml);
                location = City;
                Param.Location = location;
            }
            catch (Exception ex)
            {
                MessageDialog msgDialog = new MessageDialog(ex.Message, "Erreur chargement meteo");
                msgDialog.ShowAsync();
            }
        }

        private void xmlMeteo(string xml)
        {
            if (xml != Err404)
            {
                var xmlDocument = XDocument.Parse(xml);
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xmlDocument.ToString());
                City = doc.GetElementsByTagName("name").First().InnerText;
                Pays = doc.GetElementsByTagName("country").First().InnerText;
                foreach (meteo met in Meteo)
                    met.getMeteo(doc);
            }
            else
                throw new NotSupportedException("Ville introuvable!! Essayez de nouveau :)");
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }


}
