using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Threading.Tasks;
using System.Linq;

namespace HomeInspectorSchedule
{
    public class ScheduleLayout
    {
        public async Task<List<Inspector>> GetInspectors()
        {
            var inspectors = await App.Database.GetInspectorsAsync();
            return inspectors;
        }
        public async Task<List<Appointment>> GetAppointments(Inspector inspector)
        {
            var appointments = await App.Database.GetAppointmentsAsync();
            List<Appointment> inspectorAppointments = new List<Appointment>();

            if (inspector.Admin == false)
            {
                foreach(var a in appointments)
                {
                    if(a.InspectorID == inspector.ID)
                    {
                        inspectorAppointments.Add(a);
                    }
                }
                return inspectorAppointments;
            }
            else
            {
                return appointments;
            }
        }
        public StackLayout DayView(Inspector inspector)
        {
            StackLayout Dayview = new StackLayout();

            if (inspector.Admin)
            {
                var inspectors = GetInspectors();
                var appointments = GetAppointments(inspector);

                List<Appointment> todaysAppointments = new List<Appointment>();

                //Grid GridView = new Grid();
                Label RobertLabel = new Label
                {
                    Text = "Robert"
                };
                Label TedLabel = new Label
                {
                    Text = "Ted"
                };
                Label TimLabel = new Label
                {
                    Text = "Tim"
                };
                Label BillLabel = new Label
                {
                    Text = "Bill"
                };
                Label JayLabel = new Label
                {
                    Text = "Jay"
                };

                Dayview.Children.Add(RobertLabel);
                Dayview.Children.Add(TedLabel);
                Dayview.Children.Add(TimLabel);
                Dayview.Children.Add(BillLabel);
                Dayview.Children.Add(JayLabel);
                //GridView.Children.Add(RobertLabel, 0, 0);
                //GridView.Children.Add(TedLabel, 1, 0);
                //GridView.Children.Add(TimLabel, 2, 0);
                //GridView.Children.Add(BillLabel, 3, 0);
                //GridView.Children.Add(JayLabel, 4, 0);

            }

            return Dayview;
        }
    }
}
