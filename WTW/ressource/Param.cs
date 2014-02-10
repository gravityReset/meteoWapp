using Windows.Storage;

namespace WTW.ressource
{
    public static class Param
    {
        private static ApplicationDataContainer roamingSettings;
        private static Weather weather;
        private const string DefaultVille= "Paris";

        public static void Load()
        {
            roamingSettings = ApplicationData.Current.RoamingSettings;
            weather = new Weather();
        }

        public static string Location
        {
            get
            {
                if (roamingSettings.Values["Location"] == null)
                {
                    roamingSettings.Values["Location"] = DefaultVille;
                    return DefaultVille;
                }
                return (string)roamingSettings.Values["Location"];
            }
            set
            {
                roamingSettings.Values["Location"] = value;
            }
        }
        public static Weather Temps
        {
            get
            {
                return weather;
            }
            set
            {
                weather = value;
            }
        }

    }
}
