using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace ProjectManager.Controllers
{
    public static class SettingsController
    {
        public static void SetTheme()
        {
            Application.Current.Resources.MergedDictionaries[0].Source = new Uri("/Resources/Styles/Themes/DarkTheme.xaml", UriKind.RelativeOrAbsolute);
        }
    }
}
