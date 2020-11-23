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
        Grid GridView = new Grid();

        public async Task<List<Inspector>> GetInspectors()
        {
            var inspectors = await App.Database.GetInspectorsAsync();
            return inspectors;
        }
        public async Task<List<Appointment>> GetTodaysAppointments(Inspector inspector)
        {
            var appointments = await App.Database.GetAppointmentsAsync();
            List<Appointment> inspectorAppointments = new List<Appointment>();

            //if (inspector.Admin == false)
            //{
            foreach (var a in appointments)
            {
                // need to verify the below if statement works correctly - create inspection with today's date for testing
                if (inspector.Admin == false && a.InspectorID == inspector.ID && a.StartTime.Date.ToShortDateString() == DateTime.Today.ToShortDateString())
                {
                    inspectorAppointments.Add(a);
                }
                else if (inspector.Admin == true && a.StartTime.Date.ToShortDateString() == DateTime.Today.ToShortDateString())
                {
                    inspectorAppointments.Add(a);
                }
            }
            return inspectorAppointments;
        }
        public StackLayout DayView(Inspector inspector)
        {
            StackLayout Dayview = new StackLayout
            {
                BackgroundColor = Color.Gray
            };

            if (inspector.Admin)
            {
                //var inspectors = GetInspectors();
                //var appointments = GetTodaysAppointments(inspector);

                //List<Appointment> todaysAppointments = new List<Appointment>();

                //Grid GridView = new Grid();
                Label RobertLabel = new Label
                {
                    Text = "Robert",
                    FontSize = 20
                };
                Label TedLabel = new Label
                {
                    Text = "Ted",
                    FontSize = 20
                };
                Label TimLabel = new Label
                {
                    Text = "Tim",
                    FontSize = 20
                };
                Label BillLabel = new Label
                {
                    Text = "Bill",
                    FontSize = 20
                };
                Label JayLabel = new Label
                {
                    Text = "Jay",
                    FontSize = 20
                };

                GridView.Children.Add(RobertLabel, 0, 0);
                GridView.Children.Add(TedLabel, 0, 1);
                GridView.Children.Add(TimLabel, 0, 2);
                GridView.Children.Add(BillLabel, 0, 3);
                GridView.Children.Add(JayLabel, 0, 4);

                AddAppointments(inspector);

                Dayview.Children.Add(GridView);

            }
            else
            {
                AddAppointments(inspector);

                Dayview.Children.Add(GridView);
            }
            return Dayview;
        }
        public async Task AddAppointments(Inspector inspector)
        {
            //var whileCount = await App.Database.GetAppointmentsAsync();
            var test = await App.Database.GetAppointmentAsync(8);
            var clients = await App.Database.GetClientsAsync();
            var addresses = await App.Database.GetAddressesAsync();
            var appointments = await GetTodaysAppointments(inspector);

            int i = 1;
            int column = 0;
            if (inspector.Admin)
            {
                column = 1;
            }

            var inspectors = await App.Database.GetInspectorsAsync();
            while (i <= inspectors.Count)
            {

                foreach (var n in inspectors)
                {
                    if (i == n.ID)
                    {
                        inspector = n;
                    }
                }

                foreach (var a in appointments)
                {
                    string clientName = null;
                    string address = null;
                    DateTime startTime;
                    double duration = 0;
                    if (a.InspectorID == inspector.ID)
                    {
                        duration = a.Duration;
                        startTime = DateTime.Parse(a.StartTime.ToShortTimeString());
                        foreach (var c in clients)
                        {
                            if (a.ClientID == c.ID)
                            {
                                clientName = c.Name;
                            }
                        }
                        foreach (var x in addresses)
                        {
                            if (x.ID == a.AddressID)
                            {
                                address = x.StreetAddress + " " + x.City + ", AL" + " " + x.Zip;
                            }
                        }
                        Label Appointment = new Label
                        {
                            Text = startTime.ToString() + "\n" + clientName + "\n" + address,
                            HorizontalOptions = LayoutOptions.Start
                        };

                        string color = inspector.InspectorColor;
                        if (color == "blue")
                        {
                            Appointment.BackgroundColor = Color.Blue;
                        }
                        else if (color == "yellow")
                        {
                            Appointment.BackgroundColor = Color.Yellow;
                        }
                        else if (color == "purple")
                        {
                            Appointment.BackgroundColor = Color.Purple;
                        }
                        else if (color == "green")
                        {
                            Appointment.BackgroundColor = Color.Green;
                        }
                        else if (color == "orange")
                        {
                            Appointment.BackgroundColor = Color.Orange;
                        }

                        if (a.Canceled)
                        {
                            Appointment.BackgroundColor = Color.Red;
                            Appointment.Text = "Canceled \n" + Appointment.Text;
                        }
                        // find a way to place label according to inspectorID

                        int row = inspector.ID - 1;
                        GridView.Children.Add(Appointment, column, row);
                    }
                }
                i++;
            }



        }
        public async Task<string> GetInspectorColor(string name)
        {
            var inspector = await App.Database.GetInspectorAsync(name);

            return inspector.InspectorColor;
        }
    }
}
