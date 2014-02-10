using System.ComponentModel;

namespace WTW.ressource
{
    public class Vetements : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        private bool parapluie;
        private int haut;
        private int bas;
        private bool lunetteDeSoleil;

        public bool IsTopVisible
        {
            get { return LunetteDeSoleil || Parapluie; }
        }
        public bool LunetteDeSoleil
        {
            get { return lunetteDeSoleil; }
            set { lunetteDeSoleil = value; OnPropertyChanged("LunetteDeSoleil"); OnPropertyChanged("IsTopVisible"); }
        }

        //TODO onproperty change Bas et Haut peut etre inutile
        public int Bas
        {
            get { return bas; }
            set { bas = value; OnPropertyChanged("TexteBas"); OnPropertyChanged("VetBas"); }
        }
        public int Haut
        {
            get { return haut; }
            set { haut = value; OnPropertyChanged("TexteHaut"); OnPropertyChanged("VetHaut"); }
        }
        public string VetHaut { get { return @"im\shirt" + Haut.ToString()+".png"; } }
        public string VetBas { get { return @"im\short" + Bas.ToString()+".png"; } }
        public bool Parapluie
        {
            get { return parapluie; }
            set { parapluie = value; OnPropertyChanged("Parapluie"); OnPropertyChanged("IsTopVisible"); }
        }
        public string TexteHaut { get { return getTextHaut(); } }
        public string TexteBas { get { return getTextBas(); } }

        private string getTextHaut()
        {
            switch (Haut)
            {
                case 1:
                    return "Un T-shirt ou un débardeur bien léger, histoire de pas avoir trop chaud.";
                case 2:
                    return "Un T-shirt c'est possible et pour les frileux un à manche longue.";
                case 3:
                    return "Il commence à faire froid il faut se couvrir un petit pull est de mise.";
                case 4:
                    return "Il fait froid il faut se couvrir et mettre un bon gros pull.";
                default:
                    return string.Empty;
            }
        }

        private string getTextBas()
        {
            switch (Bas)
            {
                case 1:
                    return "Un short ou une jupe, idéale quand il fait chaud.";
                case 2:
                    return "Un pantacourt ca peut-etre une bonne idée ni trop chaud ni trop froid.";
                case 3:
                    return "Mets un pantalon sauf si tu es courageux!";
                default:
                    return string.Empty;
            }
        }


        public Vetements(meteo met)
        {
            LunetteDeSoleil = false;

            Parapluie = false;
            Haut = 0;
            Update(met);
        }


        public void Update(meteo met)
        {
            Parapluie = false;
            LunetteDeSoleil = false;
            if (met.IsRaining > 0.2M)
                Parapluie = true;
            if (met.Clouds < 15 && met.TempMax > 14)
                LunetteDeSoleil = true;

            //haut+bas
            if (met.Temp < 8 || met.TempMin < 4)
            {
                Haut = 4;
                Bas = 3;
            }
            else if ((met.IsRaining > 2 && met.TempMin < 23) || met.TempMax < 18)
            {
                Haut = 3;
                Bas = 3;
            }
            else if (met.Temp < 25)
                Haut = 2;
            else
                Haut = 1;

            if (met.IsRaining > 2 && met.Temp < 25)
                Bas = 3;
            else if (met.Temp > 28)
                Bas = 1;
            else if (met.Temp > 23)
                Bas = 2;
            else
                Bas = 3;
            //fin haut+bas
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
