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
        bool loggedInAdmin = false;
        //Grid TimeGridView = new Grid();


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

                RowDefinitions =
                {

                },
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

            newGrid.RowDefinitions = rowDefinitions;
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


            //int i = 0;
            //if (inspector.Admin)
            //{
            //    i++;
            //}

            //foreach(var l in labels)
            //{
            //    TimeGridView.Children.Add(l, i, 0);
            //    i++;
            //}

            //TimeGridView.Children.Add(Eight, 1, 0);
            //    TimeGridView.Children.Add(Nine, 2, 0);
            //    TimeGridView.Children.Add(Ten, 3, 0);
            //    TimeGridView.Children.Add(Eleven, 4, 0);
            //    TimeGridView.Children.Add(Twelve, 5, 0);
            //    TimeGridView.Children.Add(One, 6, 0);
            //    TimeGridView.Children.Add(Two, 7, 0);
            //    TimeGridView.Children.Add(Three, 8, 0);
            //    TimeGridView.Children.Add(Four, 9, 0);
            //    TimeGridView.Children.Add(Five, 10, 0);


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

                //Grid Schedule = new Grid
                //{
                //    RowDefinitions =
                //    {
                //        new RowDefinition {Height = 40}
                //    }
                //};

                //Schedule.Children.Add(TimeGridView, 0, 0);


                //Schedule.Children.Add(GridView, 0, 1);

            

                ScrollView Scroll = new ScrollView
                {
                    HorizontalOptions = LayoutOptions.Fill,
                    Orientation = ScrollOrientation.Horizontal,

                    Content = new StackLayout
                    {
                        //BackgroundColor = Color.Gray,
                        //HorizontalOptions = LayoutOptions.Fill,
                        //Orientation = StackOrientation.Horizontal,
                        //Children = { Schedule }
                        Children = {GridView}
                    }
                };

                Dayview.Children.Add(Scroll);
            return Dayview;

        }
            //else
            //{
            //    AddAppointments(inspector);

            //    Dayview.Children.Add(GridView);
            //}
        
        public async Task AddAppointments(Inspector inspector)
        {
            var clients = await App.Database.GetClientsAsync();
            var addresses = await App.Database.GetAddressesAsync();
            var appointments = await GetTodaysAppointments(inspector);

            int i = 1;
            //int column = 0;
            //if (inspector.Admin)
            //{
            //    column = 1;
            //}

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

                        var time = startTime.TimeOfDay;
                        var hours = time.TotalHours;
                        var marginSize = hours - 8;

                        Button Appointment = new Button
                        {
                            Text = startTime.ToShortTimeString() + "\n" + clientName + "\n" + address
                            //HorizontalOptions = LayoutOptions.Start,
                            //WidthRequest = duration * 105
                        };

                        //Thickness margin = Appointment.Margin;
                        //margin.Left = marginSize * 100;
                        //Appointment.Margin = margin;

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


                        GridView.Children.Add(Appointment, column, row);

                        Grid.SetColumnSpan(Appointment, Convert.ToInt32(duration));
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
