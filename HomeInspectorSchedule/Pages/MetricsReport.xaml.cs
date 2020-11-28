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
    public partial class MetricsReport : ContentPage
    {
        bool realtorReport;
        Metrics metrics = new Metrics();
        List<Appointment> appointments = new List<Appointment>();

        public MetricsReport(bool realtor)
        {
            realtorReport = realtor;
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            if (realtorReport)
            {
                RealtorLayout.IsVisible = true;
                RealtorOrInsLabel.Text = "Realtor Referral Report for 2020";
                var metricsReport = await metrics.RealtorMetrics();

                firstRealtorLabel.Text = metricsReport[0, 0];
                secondRealtorLabel.Text = metricsReport[1, 0];
                thirdRealtorLabel.Text = metricsReport[2, 0];

                int first = int.Parse(metricsReport[0, 1]);
                int second = int.Parse(metricsReport[1, 1]);
                int third = int.Parse(metricsReport[2, 1]);

                ColumnDefinitionCollection columnDefinitions = new ColumnDefinitionCollection();
                for(int i = 0; i < first; i++)
                {
                    var def = new ColumnDefinition();
                    columnDefinitions.Add(def);
                }

                GraphGrid.ColumnDefinitions = columnDefinitions;

                Grid.SetColumnSpan(firstBarLabel, first);
                Grid.SetColumnSpan(secondBarLabel, second);
                Grid.SetColumnSpan(thirdBarLabel, third);

                firstBarLabel.Text = first.ToString();
                firstBarLabel.IsVisible = true;
                secondBarLabel.Text = second.ToString();
                secondBarLabel.IsVisible = true;
                thirdBarLabel.Text = third.ToString();
                thirdBarLabel.IsVisible = true;

                var fullReport = metrics.RealtorReportList();
                

                for (int i = 0; i < fullReport.Count; i++)
                {
                    double priceTotal = 0;
                    var realtor = await App.Database.GetRealtorAsync(metricsReport[i, 0]);
                    var appointments = await App.Database.GetAppointmentsAsync();
                    foreach(var a in appointments)
                    {
                        if(a.RealtorID == realtor.ID && a.StartTime.Year == Metrics.year)
                        {
                            priceTotal += a.PriceTotal;
                        }
                    }
                    Label label = new Label();
                    label.Text = metricsReport[i, 0];
                    label.HorizontalTextAlignment = TextAlignment.Center;
                    Label label2 = new Label();
                    label2.Text = metricsReport[i, 1];
                    label2.HorizontalTextAlignment = TextAlignment.Center;
                    Label label3 = new Label();
                    label3.Text = priceTotal.ToString("C2");
                    FullReportGrid.Children.Add(label, 0, i);
                    FullReportGrid.Children.Add(label2, 1, i);
                    FullReportGrid.Children.Add(label3, 2, i);
                }                
            }
            else
            {
                InspectionsLayout.IsVisible = true;
            }
        }

        private async void WeekCheckbox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (WeekCheckbox.IsChecked)
            {
                MetricDisplay.Text = "Past Week";
                MonthCheckbox.IsChecked = false;
                YearCheckbox.IsChecked = false;
                appointments = await metrics.WeekAppointments();
                if(appointments.Count > 0)
                {
                    GraphGrid2.IsVisible = true;
                    var metricsReport = await metrics.InspectorMetrics(appointments);
                    await DisplaySet(metricsReport);
                }
                else
                {
                    await DisplayAlert("No Inspections", "There are no inspections to show for the past week.", "OK");
                }

            }
        }

        private async void MonthCheckbox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (MonthCheckbox.IsChecked)
            {
                MetricDisplay.Text = DateTime.Today.ToString("MMMM");
                WeekCheckbox.IsChecked = false;
                YearCheckbox.IsChecked = false;
                appointments = await metrics.MonthAppointments();
                if (appointments.Count > 0)
                {
                    GraphGrid2.IsVisible = true;
                    var metricsReport = await metrics.InspectorMetrics(appointments);
                    await DisplaySet(metricsReport);
                }
                else
                {
                    await DisplayAlert("No Inspections", "There are no inspections to show for this month.", "OK");
                }
            }
        }

        private async void YearCheckbox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (YearCheckbox.IsChecked)
            {
                MetricDisplay.Text = DateTime.Today.Year.ToString();
                WeekCheckbox.IsChecked = false;
                MonthCheckbox.IsChecked = false;
                appointments = await metrics.YearAppointments();
                if (appointments.Count > 0)
                {
                    GraphGrid2.IsVisible = true;
                    var metricsReport = await metrics.InspectorMetrics(appointments);
                    await DisplaySet(metricsReport);
                }
                else
                {
                    await DisplayAlert("No Inspections", "There are no inspections to show for this year.", "OK");
                }
            }
        }

        public async Task DisplaySet(string[,] metricsReport)
        {
            GraphGrid2.Children.Clear();
            TopInspectorsLabel.IsVisible = true;
            Label firstInspectorLabel = new Label();
            GraphGrid2.Children.Add(firstInspectorLabel, 0, 0);
            Label secondInspectorLabel = new Label();
            GraphGrid2.Children.Add(secondInspectorLabel, 0, 1);
            Label thirdInspectorLabel = new Label();
            GraphGrid2.Children.Add(thirdInspectorLabel, 0, 2);

            Label firstBarLabel2 = new Label
            {
                BackgroundColor = Color.Green,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
                IsVisible = false
            };
            GraphGrid2.Children.Add(firstBarLabel2, 1, 0);
            Label secondBarLabel2 = new Label
            {
                BackgroundColor = Color.Green,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
                IsVisible = false
            };
            GraphGrid2.Children.Add(secondBarLabel2, 1, 1);
            Label thirdBarLabel2 = new Label
            {
                BackgroundColor = Color.Green,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
                IsVisible = false
            };
            GraphGrid2.Children.Add(thirdBarLabel2, 1, 2);

            var length = metricsReport.Length;

            if(length > 0 && metricsReport[0, 0] != null)
            {
                firstInspectorLabel.Text = metricsReport[0, 0];
            }
            if (length > 2 && metricsReport[1, 0] != null)
            {
                secondInspectorLabel.Text = metricsReport[1, 0];
            }
            if (length > 4 && metricsReport[2, 0] != null)
            {
                thirdInspectorLabel.Text = metricsReport[2, 0];
            }

            int first = 0;
            int second = 0;
            int third = 0;
            if (length > 0 && metricsReport[0, 1] != null)
            {
                first = int.Parse(metricsReport[0, 1]);
            }
            if (length > 2 && metricsReport[1, 1] != null)
            {
                second = int.Parse(metricsReport[1, 1]);
            }
            if (length > 4 && metricsReport[2, 1] != null)
            {
                third = int.Parse(metricsReport[2, 1]);
            }

            ColumnDefinitionCollection columnDefinitions = new ColumnDefinitionCollection();
            for (int i = 0; i < first; i++)
            {
                var def = new ColumnDefinition();
                columnDefinitions.Add(def);
            }

            GraphGrid2.ColumnDefinitions = columnDefinitions;

            if(first > 0)
            {
                Grid.SetColumnSpan(firstBarLabel2, first);
                firstBarLabel2.Text = first.ToString();
                firstBarLabel2.IsVisible = true;
            }
            if (second > 0)
            {
                Grid.SetColumnSpan(secondBarLabel2, second);
                secondBarLabel2.Text = second.ToString();
                secondBarLabel2.IsVisible = true;
            }
            if (third > 0)
            {
                Grid.SetColumnSpan(thirdBarLabel2, third);
                thirdBarLabel2.Text = third.ToString();
                thirdBarLabel2.IsVisible = true;
            }

            var fullReport = metrics.InspectorReportList();
            FullReportGrid2.Children.Clear();
            var count = fullReport.Count;
            string[,,] report = new string[count,count,count];
            for (int i = 0; i < count; i++)
            {
                double priceTotal = 0;
                var inspector = await App.Database.GetInspectorAsync(metricsReport[i, 0]);
                var appointments = await App.Database.GetAppointmentsAsync();
                foreach (var a in appointments)
                {
                    if (a.InspectorID == inspector.ID && a.StartTime.Year == Metrics.year)
                    {
                        priceTotal += a.PriceTotal;
                    }
                }
                report[i, 0, 0] = metricsReport[i, 0];
                report[i, 1, 0] = metricsReport[i, 1];
                report[i, 1, 1] = priceTotal.ToString("C2");

            }

            report = metrics.OrganizeArray(report, count);

            for (int i = 0; i < count; i++)
            {
                Label label = new Label();
                label.Text = report[i, 0, 0];
                label.HorizontalTextAlignment = TextAlignment.Center;
                Label label2 = new Label();
                label2.Text = report[i, 1, 0];
                label2.HorizontalTextAlignment = TextAlignment.Center;
                Label label3 = new Label();
                label3.Text = report[i, 1, 1];


                FullReportGrid2.Children.Add(label, 0, i);
                FullReportGrid2.Children.Add(label2, 1, i);
                FullReportGrid2.Children.Add(label3, 2, i);
            }
        }
    }
}