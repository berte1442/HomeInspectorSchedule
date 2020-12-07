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
        List<string> reportsDisplay = new List<string>();
        Inspector user = new Inspector();
        public MyReports(Inspector inspector)
        {
            user = inspector;
            InitializeComponent();
        }

        private async void SearchReports_SearchButtonPressed(object sender, EventArgs e)
        {
            ReportListView.ItemsSource = await InspectionLogTools.SearchReports(SearchReports.Text, reportsDisplay);
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
            foreach(var r in reports)
            {
                string display = r.TimeStamp.ToString() + " - " + r.FileName;
                if (user.Admin)
                {
                    reportsDisplay.Add(display);
                }
                else if(r.InspectorID == user.ID)
                {
                    reportsDisplay.Add(display);
                }
            }
            ReportListView.ItemsSource = reportsDisplay;
        }

    }
}