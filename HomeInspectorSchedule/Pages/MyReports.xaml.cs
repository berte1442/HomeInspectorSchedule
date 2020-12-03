using System;
using System.Collections.Generic;
using System.IO;
using Xamarin.Essentials;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HomeInspectorSchedule.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyReports : ContentPage
    {
        public MyReports()
        {
            InitializeComponent();
        }

        private void SearchReports_SearchButtonPressed(object sender, EventArgs e)
        {

        }

        private async void ReportListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            string displayStr = ReportListView.SelectedItem.ToString();
            int index = displayStr.LastIndexOf(" ");
            int length = displayStr.Length - index;
            string name = displayStr.Substring(index, length);
            name = name.Trim();
            var report = await App.Database.GetReportAsync(name);
            await Launcher.OpenAsync(new OpenFileRequest
            {

                File = new ReadOnlyFile(report.FilePath)

            });
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();

            List<Report> reports = await App.Database.GetReportsAsync();
            List<string> reportsDisplay = new List<string>();
            foreach(var r in reports)
            {
                string display = r.timeStamp.ToString() + " - " + r.FileName;
                reportsDisplay.Add(display);
            }
            ReportListView.ItemsSource = reportsDisplay;
        }

    }
}