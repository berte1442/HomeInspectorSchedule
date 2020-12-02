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
    public partial class Login : ContentPage
    {
        public Login()
        {
            InitializeComponent();
        }

        private async void LoginBtn_Clicked(object sender, EventArgs e)
        {
            Inspector user = await LogInValidation.ValidateLogin(UsernameEntry.Text, Password.Text);

            if (user.UserName != null) // correct if statement - user will never be null
            {
                await Navigation.PushAsync(new Home(user));
            }
            else
            {
                await DisplayAlert("Incorrect", "Incorrect username or password", "OK");
            }
        }
    }
}