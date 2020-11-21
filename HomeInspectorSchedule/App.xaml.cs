using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HomeInspectorSchedule
{
    public partial class App : Application
    {
        static HomeInspectorScheduleDB database;

        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }
        public static HomeInspectorScheduleDB Database
        {
            get
            {
                if (database == null)
                {
                    database = new HomeInspectorScheduleDB(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "HomeInspectorScheduleDatabase");
                }
                return database;
            }
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
