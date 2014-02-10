using System;
using System.ComponentModel;
using Windows.Data.Xml.Dom;

namespace WTW.ressource
{
    public class meteo : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private decimal temp;
        private decimal tempMax;
        private decimal tempMin;
        private string humidity;
        private decimal isRaining;
        private int clouds;
        private int day;
        private string dayOfTheWeek;
        private string strMeteo;
        private string date;

        private Vetements vet;

        public Vetements Vetement
        {
            get { return vet; }
            set { vet = value; }
        }

        public string StrMeteo
        {
            get { return strMeteo; }
            set { strMeteo = value; OnPropertyChanged("StrMeteo"); }
        }
        public int Clouds { get { return clouds; } set { clouds = value; OnPropertyChanged("Clouds"); } }
        public decimal Temp { get { return temp; } set { temp = value; OnPropertyChanged("Temp"); } }
        public decimal TempMax { get { return tempMax; } set { tempMax = value; OnPropertyChanged("TempMax"); } }
        public decimal TempMin { get { return tempMin; } set { tempMin = value; OnPropertyChanged("TempMin"); } }
        public string Humidity { get { return humidity; } set { humidity = value; OnPropertyChanged("Humidity"); } }
        public decimal IsRaining { get { return isRaining; } set { isRaining = value; OnPropertyChanged("IsRaining"); } }
        public string Day { get { return dayOfTheWeek; } set { dayOfTheWeek = value; OnPropertyChanged("Day"); } }
        public string Date { get { return date; } set { date = value; OnPropertyChanged("Date"); } }

        public meteo(int day)
        {
            TempMax = 0.0M;
            TempMin = 0.0M;
            Humidity = string.Empty;
            IsRaining = 0.0M;
            var date = DateTime.Today.AddDays(day);
            Day = date.ToString("dddd", new System.Globalization.CultureInfo("fr-FR"));
            Date = date.ToString("dd/MM/yyyy");
            this.day = day;
            Vetement = new Vetements(this);

        }

        public void getMeteo(XmlDocument doc)
        {
            StrMeteo = doc.GetElementsByTagName("symbol")[day].Attributes.GetNamedItem("name").InnerText;
            var tempr = doc.GetElementsByTagName("temperature")[day];
            Temp = decimal.Parse(tempr.Attributes.GetNamedItem("day").InnerText.Replace(".", ","));
            TempMin = decimal.Parse(tempr.Attributes.GetNamedItem("min").InnerText.Replace(".", ","));
            TempMax = decimal.Parse(tempr.Attributes.GetNamedItem("max").InnerText.Replace(".", ","));

            Humidity = doc.GetElementsByTagName("humidity")[day].Attributes.GetNamedItem("value").InnerText;
            var rain = doc.GetElementsByTagName("time")[day].ChildNodes[3].Attributes;
            if (rain.Count > 0)
                IsRaining = decimal.Parse(rain.GetNamedItem("value").InnerText.Replace(".", ","));

            Clouds = int.Parse(doc.GetElementsByTagName("clouds")[day].Attributes.GetNamedItem("all").InnerText);
            Vetement.Update(this);
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
