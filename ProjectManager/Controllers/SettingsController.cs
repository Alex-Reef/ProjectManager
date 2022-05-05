using System;
using System.Windows;

namespace ProjectManager.Controllers
{
    public class SettingsController
    {
        private Model Model { get; set; }

        public SettingsController(Model model)
        {
            this.Model = model;
        }

        public void SetTheme(int theme)
        {
            Model.GetAppSettings().Theme = theme;
            switch(theme)
            {
                case 0:
                    Application.Current.Resources.MergedDictionaries[0].Source = new Uri("/Resources/Themes/DarkTheme.xaml", UriKind.RelativeOrAbsolute);
                    break;

                case 1:
                    Application.Current.Resources.MergedDictionaries[0].Source = new Uri("/Resources/Themes/LightTheme.xaml", UriKind.RelativeOrAbsolute);
                    break;
            }
            Model.SaveAppData();
        }

        public void SetLanguage(int language)
        {
            Model.GetAppSettings().Language = language;
            switch (language)
            {
                case 0:
                    Application.Current.Resources.MergedDictionaries[1].Source = new Uri("/Resources/Localization/en-US.xaml", UriKind.RelativeOrAbsolute);
                    break;

                case 1:
                    Application.Current.Resources.MergedDictionaries[1].Source = new Uri("/Resources/Localization/ua-UA.xaml", UriKind.RelativeOrAbsolute);
                    break;
            }
            Model.SaveAppData();
        }
    }
}
