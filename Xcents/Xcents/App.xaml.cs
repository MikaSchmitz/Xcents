using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Xcents
{
    public partial class App : Application
    {
        public static string DatabaseLocation = string.Empty;

        //continue without db
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new MainPage());
            SetNavbarColor();
        }

        //connect to local db
        public App(string databaseLocation)
        {
            InitializeComponent();
            DatabaseLocation = databaseLocation;
            MainPage = new NavigationPage(new MainPage());
            SetNavbarColor();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        //set color of navbar
        private void SetNavbarColor(Color? color = null)
        {
            color = color.HasValue ? color.Value : (Color)App.Current.Resources["AppBackgroundColor"];
            //set navbar color
            var navigationPage = Application.Current.MainPage as NavigationPage;
            navigationPage.BarBackgroundColor = (Color)color;
            navigationPage.BarTextColor = (Color)App.Current.Resources["ThemeColor"];
        }
    }
}
