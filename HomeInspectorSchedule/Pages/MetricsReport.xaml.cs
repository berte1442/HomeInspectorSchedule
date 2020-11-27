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
                Metrics metrics = new Metrics();
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
                secondBarLabel.Text = second.ToString();
                thirdBarLabel.Text = third.ToString();

                var fullReport = metrics.RealtorReportList();

                for (int i = 0; i < fullReport.Count; i++)
                {
                    Label label = new Label();
                    label.Text = metricsReport[i, 0];
                    label.HorizontalTextAlignment = TextAlignment.Center;
                    Label label2 = new Label();
                    label2.Text = metricsReport[i, 1];
                    label2.HorizontalTextAlignment = TextAlignment.Center;
                    FullReportGrid.Children.Add(label, 0, i);
                    FullReportGrid.Children.Add(label2, 1, i);
                }                
            }
            else
            {
                InspectionsLayout.IsVisible = true;

            }
        }

        private void WeekCheckbox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {

        }

        private void MonthCheckbox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {

        }

        private void YearCheckbox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {

        }
    }
}