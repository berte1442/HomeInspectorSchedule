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
    public partial class Schedule : ContentPage
    {
        Inspector currentInspector = new Inspector();
        public Schedule()
        {

        }

        public Schedule(Inspector inspector)
        {
            currentInspector = inspector;
            InitializeComponent();
        }
        private void DayCheckbox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (DayCheckbox.IsChecked)
            {
                WeekCheckbox.IsChecked = false;
                MonthCheckbox.IsChecked = false;
                DayWeekMonthLabel.Text = DateTime.Now.DayOfWeek.ToString();

                ScheduleLayout scheduleLayout = new ScheduleLayout();
                var DayView = scheduleLayout.DayView(currentInspector);
                ScheduleLayout.Children.Clear();
                ScheduleLayout.Children.Add(DayView);
            }
        }

        private async void WeekCheckbox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (WeekCheckbox.IsChecked)
            {
                DayCheckbox.IsChecked = false;
                MonthCheckbox.IsChecked = false;
                DayWeekMonthLabel.Text = "This Week";

                ScheduleLayout scheduleLayout = new ScheduleLayout();
                var WeekView = await scheduleLayout.WeekView(currentInspector);
                ScheduleLayout.Children.Clear();
                ScheduleLayout.Children.Add(WeekView);
            }
        }

        private async void MonthCheckbox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (MonthCheckbox.IsChecked)
            {
                DayCheckbox.IsChecked = false;
                WeekCheckbox.IsChecked = false;
                DayWeekMonthLabel.Text = DateTime.Now.ToString("MMMM");
  
                ScheduleLayout scheduleLayout = new ScheduleLayout();

                var MonthView = await scheduleLayout.MonthView(currentInspector);
                ScheduleLayout.Children.Clear();
                ScheduleLayout.Children.Add(MonthView);
            }
        }
    }
}