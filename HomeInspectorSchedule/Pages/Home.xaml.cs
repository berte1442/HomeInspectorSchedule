using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HomeInspectorSchedule.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Home : ContentPage
    {
        Inspector currentUser = new Inspector();
        public Home(Inspector user)
        {
            currentUser = user;
            InitializeComponent();
            WelcomeLabel.Text += user.Name;
            
            if(user.Admin == false)
            {
                MainLayout.Children.Remove(Metrics);
            }
        }

        private async void ViewSchedule_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Schedule(currentUser));
        }

        private async void Metrics_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MetricsMenu(currentUser));
        }

        private async void BookInspection_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new BookInspection(currentUser));
        }

        private async void SearchInspections_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new InspectionLog(currentUser));
        }

        private async void MyReports_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MyReports());
        }
    }
}