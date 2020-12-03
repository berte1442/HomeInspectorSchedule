using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HomeInspectorSchedule.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InspectionLog : ContentPage
    {
        Inspector currentUser = new Inspector();
        List<string> searchAppointments = new List<string>();
        public InspectionLog(Inspector user)
        {
            currentUser = user;
            InitializeComponent();
        }

        private async void searchBar_SearchButtonPressed(object sender, EventArgs e)
        {
            string search = searchBar.Text;
            searchAppointments = await InspectionLogTools.SearchAppointments(search);

            SearchListView.ItemsSource = searchAppointments;
            if(searchAppointments.Count > 0)
            {
                SaveReportBtn.IsVisible = true;
            }
            else
            {
                SaveReportBtn.IsVisible = false;
            }
        }

        private async void SearchListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var selectedItem = SearchListView.SelectedItem.ToString();
            var index = selectedItem.IndexOf(" ");
            var id = selectedItem.Substring(1, index -1);
            var appointment = await App.Database.GetAppointmentAsync(Convert.ToInt32(id));
            var view = await DisplayAlert("View Appoinment", "View appointment details.", "View Now", "Cancel");
            if (view)
            {
                await Navigation.PushAsync(new AppointmentInfo(appointment, currentUser));               
            }
        }

        private async void SaveReportBtn_Clicked(object sender, EventArgs e)
        {
            Report report = new Report();
            var fileName = await DisplayPromptAsync("Name File", "Name this inspection report file.", "Save", "Cancel", "Report Name", 12, Keyboard.Default);
            fileName += ".txt";
            var filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), fileName);

            string text = null;
            foreach (var s in searchAppointments)
            {
                text += s + "\n";
            }

            if (!File.Exists(filePath))
            {
                File.WriteAllText(filePath, text);
                report.FilePath = filePath;
                report.timeStamp = File.GetLastWriteTime(filePath);
                report.FileName = fileName;
                await App.Database.SaveReportAsync(report);
                await DisplayAlert("Saved", "Report has been saved.", "OK");
            }
            else
            {
                await DisplayAlert("Conflict", "A file already exist by that name.", "OK");
            }
        }                    
    }
}