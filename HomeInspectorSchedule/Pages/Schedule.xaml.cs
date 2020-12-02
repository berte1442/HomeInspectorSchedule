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
        DateTime dateTime = DateTime.Today;
        Inspector currentInspector = new Inspector();
        string dateDisplay = null;
        public Schedule()
        {

        }

        public Schedule(Inspector inspector)
        {
            currentInspector = inspector;
            InitializeComponent();
        }
        private async void DayCheckbox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (DayCheckbox.IsChecked)
            {
                dateTime = DateTime.Today;
                await DayCheckboxSelected();
            }
        }

        private async void WeekCheckbox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (WeekCheckbox.IsChecked)
            {
                dateTime = DateTime.Today;
                await WeekCheckboxSelected();
            }
        }

        private async void MonthCheckbox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (MonthCheckbox.IsChecked)
            {
                dateTime = DateTime.Today;
                await MonthCheckboxSelected();
            }
        }

        private async void DateBackButton_Clicked(object sender, EventArgs e)
        {           
            if (DayCheckbox.IsChecked)
            {
                dateTime = dateTime.AddDays(-1);
                await DayCheckboxSelected();
            }
            else if (WeekCheckbox.IsChecked)
            {
                var currentDay = Convert.ToInt32(dateTime.DayOfWeek);
                var daysToCome = 6 - currentDay;
                var daysPast = 6 - daysToCome;

                dateTime = dateTime.AddDays(-daysPast -1);  // may have to add 1 to daysPast here

                await WeekCheckboxSelected();
            }
            else if (MonthCheckbox.IsChecked)
            {
                dateTime = dateTime.AddMonths(-1);
                await MonthCheckboxSelected();
            }            
        }

        private async void DateForwardButton_Clicked(object sender, EventArgs e)
        {
            if (DayCheckbox.IsChecked)
            {
                dateTime = dateTime.AddDays(1);
                await DayCheckboxSelected();
            }
            else if (WeekCheckbox.IsChecked)
            {
                var currentDay = Convert.ToInt32(dateTime.DayOfWeek);
                var daysToCome = 6 - currentDay;
                var daysPast = 6 - daysToCome;

                dateTime = dateTime.AddDays(daysToCome + 1);  // may have to add 1 to daysPast here

                await WeekCheckboxSelected();
            }
            else if (MonthCheckbox.IsChecked)
            {
                dateTime = dateTime.AddMonths(1);
                await MonthCheckboxSelected();
            }
        }

        public async Task DayCheckboxSelected()
        {
            if (DayCheckbox.IsChecked)
            {
                WeekCheckbox.IsChecked = false;
                MonthCheckbox.IsChecked = false;
                dateDisplay = dateTime.DayOfWeek.ToString() + " " + dateTime.ToShortDateString();
                DayWeekMonthLabel.Text = dateDisplay;

                ScheduleLayout scheduleLayout = new ScheduleLayout();
                var DayView = await scheduleLayout.DayView(currentInspector, dateTime);
                ScheduleLayout.Children.Clear();
                ScheduleLayout.Children.Add(DayView);
            }
        }

        public async Task WeekCheckboxSelected()
        {
            if (WeekCheckbox.IsChecked)
            {
                DayCheckbox.IsChecked = false;
                MonthCheckbox.IsChecked = false;
                ScheduleLayout scheduleLayout = new ScheduleLayout();

                dateDisplay = scheduleLayout.WeekDatesLabel(dateTime);
                DayWeekMonthLabel.Text = dateDisplay;

                var WeekView = await scheduleLayout.WeekView(currentInspector, dateTime);
                ScheduleLayout.Children.Clear();
                ScheduleLayout.Children.Add(WeekView);
            }
        }

        public async Task MonthCheckboxSelected()
        {
            if (MonthCheckbox.IsChecked)
            {
                DayCheckbox.IsChecked = false;
                WeekCheckbox.IsChecked = false;
                dateDisplay = dateTime.ToString("MMMM") + " " + dateTime.Year.ToString();
                DayWeekMonthLabel.Text = dateDisplay;

                ScheduleLayout scheduleLayout = new ScheduleLayout();

                var MonthView = await scheduleLayout.MonthView(currentInspector, dateTime);
                ScheduleLayout.Children.Clear();
                ScheduleLayout.Children.Add(MonthView);
            }
        }
    }
}