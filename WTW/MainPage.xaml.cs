using System;
using Windows.Devices.Geolocation;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using WTW.ressource;

namespace WTW
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        public Weather tps { get { return Param.Temps; } set { Param.Temps = value; } }


        public MainPage()
        {
            Param.Load();
            //tps = new Weather();
            tps.GetCurrentConditions();
            this.InitializeComponent();
            DataContext = this;
            LS_DayTemp.SelectedIndex = 0;
        }


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            appNameAnim.Begin();
            animation_LB.Begin();
            appNameAnim.Completed += appNameAnim_Completed;
        }


        private void appNameAnim_Completed(object sender, object e)
        {
            CityNameAnim.Begin();
            animation_gridGauche.Begin();
        }


        private void AppBarButton_Click_Refresh(object sender, RoutedEventArgs e)
        {
                tps.GetCurrentConditions(true);
                CityNameAnim.Begin();
                animation_LB.Begin();                
        }


        private Geolocator geo = null;

        private async void Position_Click( object sender, RoutedEventArgs e)
        {
            if (geo == null)
                geo = new Geolocator();

            try
            {
                Geoposition pos = await geo.GetGeopositionAsync();
                tps.GetCurrentConditions(pos.Coordinate.Point.Position.Longitude, pos.Coordinate.Point.Position.Latitude);
            }
            catch (Exception ex)
            {
                MessageDialog msgDialog = new MessageDialog(ex.Message, "Erreur chargement localisation");
                msgDialog.ShowAsync();
            }
            CityNameAnim.Begin();
            animation_LB.Begin();
        }


        private void AppBarButton_Click_Parametre(object sender, RoutedEventArgs e)
        {
            Settings sf = new Settings();
            sf.Show();
        }
    }
}
