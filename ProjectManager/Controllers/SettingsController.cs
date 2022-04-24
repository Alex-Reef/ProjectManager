using System;
using System.Windows;

namespace ProjectManager.Controllers
{
    public static class SettingsController
    {
        public static void SetTheme()
        {
            Application.Current.Resources.MergedDictionaries[0].Source = new Uri("/Resources/Themes/DarkTheme.xaml", UriKind.RelativeOrAbsolute);
        }

        public static void SetLanguage()
        {

        }
    }
}
