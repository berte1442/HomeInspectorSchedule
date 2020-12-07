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

                var fullReport = metrics.RealtorReportList();

                var count = fullReport.Count;
                string[,,] report = new string[count, count, count];
                for (int i = 0; i < fullReport.Count; i++)
                {
                    double priceTotal = 0;
                    var realtor = await App.Database.GetRealtorAsync(metricsReport[i, 0]);
                    var appointments = await App.Database.GetAppointmentsAsync();
                    appointments = await Metrics.RemoveNonRealtorApps(appointments);
                    foreach (var a in appointments)
                    {
                        if (a.RealtorID == realtor.ID && a.StartTime.Year == Metrics.year)
                        {
                            priceTotal += a.PriceTotal;
                        }
                    }
                    report[i, 0, 0] = metricsReport[i, 0];
                    report[i, 1, 0] = metricsReport[i, 1];
                    report[i, 1, 1] = priceTotal.ToString("C2");
                }

                report = metrics.OrganizeArray(report, count);

                firstRealtorLabel.Text = report[0, 0, 0];
                secondRealtorLabel.Text = report[1, 0, 0];
                thirdRealtorLabel.Text = report[2, 0, 0];

                int first = int.Parse(report[0, 1, 0]);
                int second = int.Parse(report[1, 1, 0]);
                int third = int.Parse(report[2, 1, 0]);

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
                Label insLabel = new Label
                {
                    Text = "Inspector",
                    FontAttributes = FontAttributes.Bold,
                    HorizontalTextAlignment = TextAlignment.Center
                };
                Label countLabel = new Label
                {
                    Text = "Total\nReferrals",
                    FontAttributes = FontAttributes.Bold,
                    HorizontalTextAlignment = TextAlignment.Center
                };
                Label priceLabel = new Label
                {
                    Text = "Generated\nRevenue",
                    FontAttributes = FontAttributes.Bold,
                    HorizontalTextAlignment = TextAlignment.Center
                };

                FullReportGrid.Children.Add(insLabel, 0, 0);
                FullReportGrid.Children.Add(countLabel, 1, 0);
                FullReportGrid.Children.Add(priceLabel, 2, 0);

                BoxView line = new BoxView
                {
                    Color = Color.Black,
                    WidthRequest = 100
                };
                FullReportGrid.Children.Add(line, 0, 1);

                Grid.SetColumnSpan(line, 3);
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
                    label3.HorizontalTextAlignment = TextAlignment.Center;


                    FullReportGrid.Children.Add(label, 0, i + 2);
                    FullReportGrid.Children.Add(label2, 1, i + 2);
                    FullReportGrid.Children.Add(label3, 2, i + 2);
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
                    await metrics.DisplaySet(metricsReport, GraphGrid2, FullReportGrid2, appointments, TopInspectorsLabel);
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
                    await metrics.DisplaySet(metricsReport, GraphGrid2, FullReportGrid2, appointments, TopInspectorsLabel);
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
                    await metrics.DisplaySet(metricsReport, GraphGrid2, FullReportGrid2, appointments, TopInspectorsLabel);
                }
                else
                {
                    await DisplayAlert("No Inspections", "There are no inspections to show for this year.", "OK");
                }
            }
        }
    }
}