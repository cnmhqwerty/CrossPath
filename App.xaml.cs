using CrossPath.ViewModels;
using System.Reflection;

namespace CrossPath
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            DependencyService.Register<IProfile, UserInfo>();

            MainPage = new NavigationPage(new Views.MainPage());

            AppTheme currentTheme = Application.Current.RequestedTheme;

            var Lsource = new Uri("Resources/Styles/LightThemeColours.xaml", UriKind.RelativeOrAbsolute);
            var LresourceDictionary = new ResourceDictionary();
            LresourceDictionary.SetAndLoadSource(Lsource, "Resources/Styles/LightThemeColours.xaml", this.GetType().GetTypeInfo().Assembly, null);

            var Dsource = new Uri("Resources/Styles/DarkThemeColours.xaml", UriKind.RelativeOrAbsolute);
            var DresourceDictionary = new ResourceDictionary();
            DresourceDictionary.SetAndLoadSource(Dsource, "Resources/Styles/DarkThemeColours.xaml", this.GetType().GetTypeInfo().Assembly, null);


            ThemeDictionary.MergedDictionaries.Add(currentTheme.Equals(AppTheme.Light) ? LresourceDictionary : DresourceDictionary);
        }
    }

}
