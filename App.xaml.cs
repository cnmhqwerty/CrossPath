using CrossPath.ViewModels;

namespace CrossPath
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            DependencyService.Register<IProfile, UserInfo>();

            MainPage = new NavigationPage(new Views.MainPage());
        }
    }
}
