using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Threading.Tasks;
using System.Linq;
using HomeInspectorSchedule.Pages;
using Xamarin.Forms.Core;

//using Android.Content.Res;

namespace HomeInspectorSchedule
{
    //[XamlCompilation(XamlCompilationOptions.Compile)]
    public /*partial*/ class ScheduleLayout
    {
        //Button Appointment = new Button();

        Grid GridView = new Grid();
        bool loggedInAdmin = false;

        public async Task<List<Appointment>> GetTodaysAppointments(Inspector inspector)
        {
            var appointments = await App.Database.GetAppointmentsAsync();
            List<Appointment> inspectorAppointments = new List<Appointment>();

            foreach (var a in appointments)
            {
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
            int columnSpan = 60;
            int rowCount = 1;
            RowDefinitionCollection rowDefinitions = new RowDefinitionCollection();
            RowDefinition rowDefinition = new RowDefinition { Height = 80 };

            if (inspector.Admin)
            {
                loggedInAdmin = true;
                columnSpan = 100;
                rowCount = 6;
            }
            for (int x = 0; x < rowCount; x++)
            {
                rowDefinitions.Add(rowDefinition);
            }
            Grid newGrid = new Grid {
                HorizontalOptions = LayoutOptions.Fill,

                RowDefinitions = rowDefinitions,

                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = columnSpan },
                    new ColumnDefinition { Width = 60 },
                    new ColumnDefinition { Width = 60 },
                    new ColumnDefinition { Width = 60 },
                    new ColumnDefinition { Width = 60 },
                    new ColumnDefinition { Width = 60 },
                    new ColumnDefinition { Width = 60 },
                    new ColumnDefinition { Width = 60 },
                    new ColumnDefinition { Width = 60 },
                    new ColumnDefinition { Width = 60 },
                    new ColumnDefinition { Width = 60 },
                    new ColumnDefinition { Width = 60 },
                    new ColumnDefinition { Width = 60 }
                }
            };

            GridView = newGrid;

            StackLayout Dayview = new StackLayout
            {
                BackgroundColor = Color.Gray
            };

            if (inspector.Admin)
            {
                Label InspectorsLabel = new Label
                {
                    Text = "Inspectors",
                    FontSize = 20
                };
                GridView.Children.Add(InspectorsLabel, 0, 0);
            }
            List<Label> labels = new List<Label>();

            Label Eight = new Label
                {
                    Text = "8:00",
                    FontSize = 20
                };
            labels.Add(Eight);
                Label Nine = new Label
                {
                    Text = "9:00",
                    FontSize = 20
                };
            labels.Add(Nine);

            Label Ten = new Label
                {
                    Text = "10:00",
                    FontSize = 20
                };
            labels.Add(Ten);

            Label Eleven = new Label
                {
                    Text = "11:00",
                    FontSize = 20
                };
            labels.Add(Eleven);

            Label Twelve = new Label
                {
                    Text = "12:00",
                    FontSize = 20
                };
            labels.Add(Twelve);

            Label One = new Label
                {
                    Text = "1:00",
                    FontSize = 20
                };
            labels.Add(One);

            Label Two = new Label
                {
                    Text = "2:00",
                    FontSize = 20
                };
            labels.Add(Two);

            Label Three = new Label
                {
                    Text = "3:00",
                    FontSize = 20
                };
            labels.Add(Three);

            Label Four = new Label
                {
                    Text = "4:00",
                    FontSize = 20
                };
            labels.Add(Four);

            Label Five = new Label
                {
                    Text = "5:00",
                    FontSize = 20
                };
            labels.Add(Five);
            
            Label Six = new Label
                {
                    Text = "6:00",
                    FontSize = 20
                };
            labels.Add(Six);

            Label Seven = new Label
                {
                    Text = "7:00",
                    FontSize = 20
                };
            labels.Add(Seven);
                        
            Label EightPm = new Label
                {
                    Text = "8:00",
                    FontSize = 20
                };
            labels.Add(EightPm);

            int i = 0;
            if (inspector.Admin)
            {
                i++;
            }

            foreach (var l in labels)
            {
                GridView.Children.Add(l, i, 0);
                i++;
            }

            if (inspector.Admin)
            {
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

                if (inspector.Admin) 
                { 
                GridView.Children.Add(RobertLabel, 0, 1);
                GridView.Children.Add(TedLabel, 0, 2);
                GridView.Children.Add(TimLabel, 0, 3);
                GridView.Children.Add(BillLabel, 0, 4);
                GridView.Children.Add(JayLabel, 0, 5);
                }
            }

            AddAppointments(inspector);
            
            ScrollView Scroll = new ScrollView
            {
                HorizontalOptions = LayoutOptions.Fill,
                Orientation = ScrollOrientation.Horizontal,

                Content = new StackLayout
                {
                    Children = {GridView}
                }
            };
            Dayview.Children.Add(Scroll);
            return Dayview;
        }
        
        public async Task AddAppointments(Inspector inspector)
        {
            var clients = await App.Database.GetClientsAsync();
            var addresses = await App.Database.GetAddressesAsync();
            var appointments = await GetTodaysAppointments(inspector);

            var inspectors = await App.Database.GetInspectorsAsync();
            int i = 1;
            while(i <= inspectors.Count)
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

                        Button Appointment = new Button
                        {
                            Text = startTime.ToShortTimeString() + "\n" + clientName + "\n" + address,
                            Opacity = .6,
                            ClassId = a.ID.ToString()
                        };
                        Appointment.Clicked += async (sender, args) => await Application.Current.MainPage.Navigation.PushAsync(new AppointmentInfo(Appointment.ClassId));

                        string color = inspector.InspectorColor;
                        if (color == "blue")
                        {
                            Appointment.BackgroundColor = Color.LightBlue;
                        }
                        else if (color == "yellow")
                        {
                            Appointment.BackgroundColor = Color.YellowGreen;
                        }
                        else if (color == "purple")
                        {
                            Appointment.BackgroundColor = Color.MediumPurple;
                        }
                        else if (color == "green")
                        {
                            Appointment.BackgroundColor = Color.LightGreen;
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
                        if (a.Approved == false)
                        {
                            Appointment.BackgroundColor = Color.White;
                            Appointment.Text = "Awaiting Approval from Admin\n" + Appointment.Text;
                        }

                        int row = 1;
                        string type = "null";

                        if(GridView.Children.Count > 0)
                        {
                            var element = GridView.Children.First();
                            if (element != null)
                            {
                                type = element.GetType().ToString();
                            }
                            if (loggedInAdmin && element != null && type == "Xamarin.Forms.Label")
                            {
                                row = inspector.ID;
                            }
                        }
                        int column = startTime.Hour - 7;
                        if(column > 0)
                        {
                            GridView.Children.Add(Appointment, column, row);
                        }

                        Grid.SetColumnSpan(Appointment, Convert.ToInt32(duration));
                    }
                }
                i++;
            }
        }
    }
}
