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
    public partial class MetricsMenu : ContentPage
    {
        Inspector currentUser = new Inspector();
        public MetricsMenu(Inspector user)
        {
            currentUser = user;
            InitializeComponent();
        }

        private async void InspectionsBtn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MetricsReport(false));
        }

        private async void Realtors_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MetricsReport(true));
        }
    }
}