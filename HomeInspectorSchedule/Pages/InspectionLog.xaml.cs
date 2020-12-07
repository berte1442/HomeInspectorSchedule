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
            searchAppointments = await InspectionLogTools.SearchAppointments(search, currentUser);

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
            report.InspectorID = currentUser.ID; 
            var fileName = await DisplayPromptAsync("Name File", "Name this inspection report file.", "Save", "Cancel", "Report Name", 12, Keyboard.Default);
            fileName += ".txt";
            var filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), fileName);

            string text = null;
            foreach (var s in searchAppointments)
            {
                int index = s.IndexOf(" ");
                string idStr = s.Substring(1, index - 1);
                var appointment = await App.Database.GetAppointmentAsync(Convert.ToInt32(idStr.Trim()));
                text += await InspectionLogTools.BuildReport(appointment);
            }
            string creater = "Report created by " + currentUser.Name +"\n" + "________________________" + "\n\n";
            text = creater + text;
            if (!File.Exists(filePath) && !filePath.Contains(" "))
            {
                File.WriteAllText(filePath, text);
                report.FilePath = filePath;
                report.TimeStamp = File.GetLastWriteTime(filePath);
                report.FileName = fileName;
                await App.Database.SaveReportAsync(report);
                await DisplayAlert("Saved", "Report has been saved.", "OK");
            }
            else
            {
                if(File.Exists(filePath))
                    await DisplayAlert("Conflict", "A file already exist by that name.", "OK");

                else if(filePath.Contains(" "))
                    await DisplayAlert("Naming Error", "File name cannot contain spaces.", "OK");
            }
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            searchAppointments = await InspectionLogTools.SearchAppointments("", currentUser);

            SearchListView.ItemsSource = searchAppointments;
            if (searchAppointments.Count > 0)
            {
                SaveReportBtn.IsVisible = true;
            }
            else
            {
                SaveReportBtn.IsVisible = false;
            }
        }
    }
}