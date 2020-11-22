using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using HomeInspectorSchedule.Pages;
using System.Threading.Tasks;


namespace HomeInspectorSchedule
{
    public partial class App : Application
    {
        static HomeInspectorScheduleDB database;

        public App()
        {
            InitializeComponent();
            DataInsert.DatabaseCheck();

            MainPage = new NavigationPage(new Login());
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
