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
    public partial class BookInspection : ContentPage
    {
        Inspector currentUser = new Inspector();
        public BookInspection(Inspector user)
        {
            currentUser = user;
            InitializeComponent();
            WelcomeLabel.Text += user.Name;
            if(user.ID != 1)
            {
                MainLayout.Children.Remove(SelectInspector);
            }
        }

        private async void RealtorNameEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            string search = RealtorNameEntry.Text;
            var realtors = await App.Database.GetRealtorsAsync();
            foreach (var r in realtors)
            {
                if (r.Name.Contains(search))
                {
                    RealtorNameEntry.Text = r.Name;
                    RealtorPhoneEntry.Text = r.Phone;
                    RealtorEmailEntry.Text = r.Email;
                    break;
                }
            }
        }
    }
}