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

            MainPage = new MainPage();
        }

        //connect to local db
        public App(string databaseLocation)
        {
            InitializeComponent();
            DatabaseLocation = databaseLocation;
            MainPage = new MainPage();
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
    }
}
