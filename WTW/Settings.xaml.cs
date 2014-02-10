using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using WTW.ressource;

// Pour en savoir plus sur le modèle d'élément du menu volant des paramètres, consultez la page http://go.microsoft.com/fwlink/?LinkId=273769

namespace WTW
{
    public sealed partial class Settings : SettingsFlyout
    {
        public Settings()
        {
            this.InitializeComponent();
            TB_Ville.Text = Param.Location;
        }

        private void Button_Click_Sauvegarde(object sender, RoutedEventArgs e)
        {
            string tbText = TB_Ville.Text;
            if (!string.IsNullOrWhiteSpace(tbText))
                Param.Location = TB_Ville.Text;
            this.Hide();
            Param.Temps.GetCurrentConditions(true);
        }

    }
}
