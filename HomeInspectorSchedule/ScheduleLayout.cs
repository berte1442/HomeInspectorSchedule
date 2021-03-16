using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Threading.Tasks;
using System.Linq;
using HomeInspectorSchedule.Pages;
using Xamarin.Forms.Core;

namespace HomeInspectorSchedule
{
    public class ScheduleLayout
    {
        Grid GridView = new Grid
        {
            Padding = 10
        };

        bool loggedInAdmin = false;

        public async Task<List<Appointment>> GetTodaysAppointments(Inspector inspector, DateTime dateTime)
        {
            var appointments = await App.Database.GetAppointmentsAsync();
            List<Appointment> inspectorAppointments = new List<Appointment>();

            foreach (var a in appointments)
            {
                if (inspector.Admin == false && a.InspectorID == inspector.ID && a.StartTime.Date.ToShortDateString() == dateTime.Date.ToShortDateString())
                {
                    inspectorAppointments.Add(a);
                }
                else if (inspector.Admin == true && a.StartTime.Date.ToShortDateString() == dateTime.Date.ToShortDateString())
                {
                    inspectorAppointments.Add(a);
                }
            }
            return inspectorAppointments;
        }

        public async Task<List<Appointment>> GetWeekAppointments(Inspector inspector)
        {
            var appointments = await App.Database.GetAppointmentsAsync();
            List<Appointment> inspectorAppointments = new List<Appointment>();

            foreach (var a in appointments)
            {
                if (inspector.Admin == false && a.InspectorID == inspector.ID && (a.StartTime >= DateTime.Today && a.StartTime < DateTime.Today.AddDays(6)))
                {
                    inspectorAppointments.Add(a);
                }
                else if (inspector.Admin == true && (a.StartTime >= DateTime.Today && a.StartTime < DateTime.Today.AddDays(6)))
                {
                    inspectorAppointments.Add(a);
                }
            }
            return inspectorAppointments;
        }

        public async Task<StackLayout> DayView(Inspector inspector, DateTime dateTime)
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
                Padding = 10,
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

            await AddDayAppointments(inspector, dateTime);
            
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

        public async Task<StackLayout> WeekView(Inspector inspector, DateTime dateTime)
        {
            Grid weekGrid = new Grid
            {
                VerticalOptions = LayoutOptions.FillAndExpand,

                ColumnDefinitions = {
                    new ColumnDefinition{ Width = 100 },
                    new ColumnDefinition{ Width = 100 },
                    new ColumnDefinition{ Width = 100 },
                    new ColumnDefinition{ Width = 100 },
                    new ColumnDefinition{ Width = 100 },
                    new ColumnDefinition{ Width = 100 },
                    new ColumnDefinition{ Width = 100 }
                },
                RowDefinitions =
                {
                    new RowDefinition{ Height = GridLength.Auto }
                }
            };
            Label sunday = new Label
            {
                Text = "Sun",
                FontSize = 20
            };
            Label monday = new Label
            {
                Text = "Mon",
                FontSize = 20
            };
            Label tuesday = new Label
            {
                Text = "Tue",
                FontSize = 20
            };
            Label wednesday = new Label
            {
                Text = "Wed",
                FontSize = 20
            };
            Label thursday = new Label
            {
                Text = "Thu",
                FontSize = 20
            };
            Label friday = new Label
            {
                Text = "Fri",
                FontSize = 20
            };
            Label saturday = new Label
            {
                Text = "Sat",
                FontSize = 20
            };
            weekGrid.Children.Add(sunday, 0, 0);
            weekGrid.Children.Add(monday, 1, 0);
            weekGrid.Children.Add(tuesday, 2, 0);
            weekGrid.Children.Add(wednesday, 3, 0);
            weekGrid.Children.Add(thursday, 4, 0);
            weekGrid.Children.Add(friday, 5, 0);
            weekGrid.Children.Add(saturday, 6, 0);

            var appointments = await AddWeekAppointments(inspector, dateTime);

            for(int i = 0; i < appointments.Count; i++)
            {
                if (i == 0 || appointments[i].StartTime.Day != appointments[i - 1].StartTime.Day)
                {
                    var dayOfWeek = Convert.ToInt32(appointments[i].StartTime.DayOfWeek);
                    var client = await App.Database.GetClientAsync(appointments[i].ClientID);
                    var clientName = client.Name;

                    StackLayout stack = new StackLayout
                    {
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        StyleId = dayOfWeek.ToString()
                    };

                    Frame frame = new Frame
                    {
                        BorderColor = Color.Black,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        Padding = 0
                    };
                    frame.Content = stack;
                    weekGrid.Children.Add(frame, dayOfWeek, 1);
                    List<Button> appointments1 = new List<Button>();
                    for (int n = 0; n < appointments.Count; n++)
                    {
                        var address = await App.Database.GetAddressAsync(appointments[n].AddressID);
                        var appAddress = address.StreetAddress + " " + address.City + ", " + address.Zip;
                        var startTime = appointments[n].StartTime.ToShortTimeString();
                        if (Convert.ToInt32(appointments[n].StartTime.DayOfWeek) == Convert.ToInt32(stack.StyleId))
                        {
                            Button app = new Button
                            {
                                Text = startTime + "\n" + clientName + "\n" + appAddress,
                                Opacity = .6,
                                ClassId = appointments[n].ID.ToString()
                            };
                            var appointment = await App.Database.GetAppointmentAsync(appointments[n].ID);
                            app.Clicked += async (sender, args) => await Application.Current.MainPage.Navigation.PushAsync(new AppointmentInfo(appointment, inspector));

                            var appInspector = await App.Database.GetInspectorAsync(appointments[n].InspectorID);

                            app.BackgroundColor = Metrics.GetInspectorColor(appInspector);

                            if (appointments[n].Approved == false)
                            {
                                app.BackgroundColor = Color.White;
                                app.Text = "Awaiting Approval from Admin\n" + app.Text;
                            }
                            if (appointments[n].Canceled)
                            {
                                app.BackgroundColor = Color.Red;
                                app.Text = "Canceled \n" + app.Text;
                            }
                            stack.Children.Add(app);
                            //appointments1.Add(app);
                            //stack.Children.Add(Appointment);
                        }
                    }
                    //appointments1 = await OrganizeScheduleDisplay(appointments1);
                    //foreach(var a in appointments1)
                    //{
                    //    stack.Children.Add(a);
                    //}
                }
            }

            GridView = weekGrid;

            ScrollView Scroll = new ScrollView
            {
                HorizontalOptions = LayoutOptions.Fill,
                Orientation = ScrollOrientation.Horizontal,

                Content = new StackLayout
                {
                    Children = { GridView }
                }
            };
            StackLayout Weekview = new StackLayout
            {
                Padding = new Thickness(10, 0, 10, 0),
                BackgroundColor = Color.Gray
            };
            Weekview.Children.Add(Scroll);

            return Weekview;
        }

        public async Task<StackLayout> MonthView(Inspector inspector, DateTime dateTime)
        {
            Grid monthGrid = new Grid
            {
               
                ColumnDefinitions = {
                    new ColumnDefinition{ Width = 100 },
                    new ColumnDefinition{ Width = 100 },
                    new ColumnDefinition{ Width = 100 },
                    new ColumnDefinition{ Width = 100 },
                    new ColumnDefinition{ Width = 100 },
                    new ColumnDefinition{ Width = 100 },
                    new ColumnDefinition{ Width = 100 }
                },
                RowDefinitions =
                {                   
                    new RowDefinition{ Height = GridLength.Auto },
                    new RowDefinition{ Height = GridLength.Auto },
                    new RowDefinition{ Height = GridLength.Auto },
                    new RowDefinition{ Height = GridLength.Auto },
                    new RowDefinition{ Height = GridLength.Auto },
                    new RowDefinition{ Height = GridLength.Auto }
                }
                
            };

            Label sunday = new Label
            {
                Text = "Sun",
                FontSize = 20
            };
            Label monday = new Label
            {
                Text = "Mon",
                FontSize = 20
            };
            Label tuesday = new Label
            {
                Text = "Tue",
                FontSize = 20
            };
            Label wednesday = new Label
            {
                Text = "Wed",
                FontSize = 20
            };
            Label thursday = new Label
            {
                Text = "Thu",
                FontSize = 20
            };
            Label friday = new Label
            {
                Text = "Fri",
                FontSize = 20
            };
            Label saturday = new Label
            {
                Text = "Sat",
                FontSize = 20
            };
            monthGrid.Children.Add(sunday, 0, 0);
            monthGrid.Children.Add(monday, 1, 0);
            monthGrid.Children.Add(tuesday, 2, 0);
            monthGrid.Children.Add(wednesday, 3, 0);
            monthGrid.Children.Add(thursday, 4, 0);
            monthGrid.Children.Add(friday, 5, 0);
            monthGrid.Children.Add(saturday, 6, 0);

            //var month = DateTime.Today.Month;
            //var year = DateTime.Today.Year;
            var month = dateTime.Month;
            var year = dateTime.Year;
            DateTime firstOfMonth = DateTime.Parse(month + "/" + "1" + "/" + year);
            int dayOfWeek = Convert.ToInt32(firstOfMonth.DayOfWeek);
            int days = 0;
            var testAddDays = firstOfMonth.AddDays(30);
            var testMonth = testAddDays.Month;
            if(month == 2 && year % 4 != 0)
            {
                days = 28;
            }
            else if(month == 2 && (year % 4 == 0 || (year % 100 != 0 || year % 400 == 0)))
            {
                days = 29;
            }
            else if(month == testMonth)
            {
                days = 31;
            }
            else
            {
                days = 30;
            }

            var row = 1;
            var column = dayOfWeek;
            var appointments = await AddMonthAppointments(inspector, dateTime);
            int n = 1;
            for (int i = dayOfWeek; i < days + dayOfWeek; i++)
            {

                if(column != 0 && column % 7 == 0)
                {
                    row++;
                    column = 0;
                }

                StackLayout stack = new StackLayout
                {
                    VerticalOptions = LayoutOptions.Fill,
                    HorizontalOptions = LayoutOptions.FillAndExpand,

                    //ClassId = (1 + i).ToString(),
                    ClassId = n.ToString(),
                    StyleId = column.ToString() + " " + row.ToString()
                };
                Frame frame = new Frame
                {
                    BorderColor = Color.Black,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    Padding = 0
                    //MinimumHeightRequest = 50
                };
                Label date = new Label
                {
                    Text = (n).ToString(),
                    Padding = new Thickness(3, 0, 0, 0),

                };
                stack.Children.Add(date);
                frame.Content = stack;

                List<Button> buttons = new List<Button>();
                foreach (var a in appointments)
                {
                    Button app = new Button
                    {
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        Padding = new Thickness(3, 0, 0, 0),
                        ClassId = a.ID.ToString(),
                        StyleId = a.StartTime.Day.ToString()
                    };
                    app.Clicked += async (sender, args) => await Application.Current.MainPage.Navigation.PushAsync(new AppointmentInfo(a, inspector));
                    var ins = await App.Database.GetInspectorAsync(a.InspectorID);
                    var address = await App.Database.GetAddressAsync(a.AddressID);
                    if (inspector.Admin)
                    {
                        if(Convert.ToInt32(a.StartTime.Day) == Convert.ToInt32(stack.ClassId))
                        {
                            app.Text = ins.Name + "\n" + a.StartTime.ToShortTimeString();
                            if (a.Canceled == false)
                            {
                                app.BackgroundColor = Metrics.GetInspectorColor(await App.Database.GetInspectorAsync(a.InspectorID));
                                if(a.Approved == false)
                                {
                                    app.BackgroundColor = Color.White;
                                    app.Text = "Awaiting Approval from Admin\n" + app.Text;
                                }
                            }
                            else
                            {
                                app.BackgroundColor = Color.Red;
                            }
                            buttons.Add(app);
                            //stack.Children.Add(app);
                        }
                    }
                    else if((Convert.ToInt32(a.StartTime.Day) == Convert.ToInt32(stack.ClassId) && inspector.ID == a.InspectorID))
                    {
                        app.Text = address.StreetAddress + " " + address.City + ", " + address.Zip + "\n" + a.StartTime.ToShortTimeString();
                        if (a.Canceled == false)
                        {
                            app.BackgroundColor = Metrics.GetInspectorColor(inspector);
                            if (a.Approved == false)
                            {
                                app.BackgroundColor = Color.White;
                                app.Text = "Awaiting Approval from Admin\n" + app.Text;
                            }
                        }
                        else
                        {
                            app.BackgroundColor = Color.Red;
                        }
                        buttons.Add(app);
                        //stack.Children.Add(app);
                    }
                }

                buttons = await OrganizeScheduleDisplay(buttons);
                foreach(var b in buttons)
                {
                    stack.Children.Add(b);
                }
                //monthGrid.Children.Add(stack, column, row);
                monthGrid.Children.Add(frame, column, row);
                column++;
                n++;
            }

            GridView = monthGrid;
            ScrollView Scroll = new ScrollView
            {
                HorizontalOptions = LayoutOptions.Fill,
                Orientation = ScrollOrientation.Horizontal,

                Content = new StackLayout
                {
                    Children = { GridView }
                }
            };
            StackLayout Monthview = new StackLayout
            {
                BackgroundColor = Color.Gray,
                Padding = new Thickness(10,0,10,0)
            };
            Monthview.Children.Add(Scroll);
            return Monthview;
        }

        public async Task<List<Appointment>> AddWeekAppointments(Inspector inspector, DateTime dateTime)
        {
            var appointments = await App.Database.GetAppointmentsAsync();
            List<Appointment> weekAppoinments = new List<Appointment>();

            //var currentDay = Convert.ToInt32(DateTime.Today.DayOfWeek);
            var currentDay = Convert.ToInt32(dateTime.DayOfWeek);
            var daysToCome = 6 - currentDay;
            var daysPast = 6 - daysToCome;

            //var currentDate = dateTime;

            var sunday = dateTime.AddDays(-daysPast);

            var saturday = dateTime.AddDays(daysToCome);
            foreach (var a in appointments)
            {
                var appDate = a.StartTime.Date;

                if (appDate >= sunday && appDate <= saturday)
                {
                    if (inspector.Admin || inspector.ID == a.InspectorID)
                    {
                        weekAppoinments.Add(a);
                    }
                }
            }
            return weekAppoinments;
        }

        public async Task<List<Appointment>> AddMonthAppointments(Inspector inspector, DateTime dateTime)
        {
            var appointments = await App.Database.GetAppointmentsAsync();
            List<Appointment> monthAppointments = new List<Appointment>();

            foreach(var a in appointments)
            {
                if(inspector.Admin && a.StartTime.Month == dateTime.Month && a.StartTime.Year == dateTime.Year)
                {
                    monthAppointments.Add(a);
                }
                else if(a.StartTime.Month == dateTime.Month && a.StartTime.Year == dateTime.Year && a.InspectorID == inspector.ID)
                {
                    monthAppointments.Add(a);
                }
            }
            return monthAppointments;
            }
        public async Task AddDayAppointments(Inspector inspector, DateTime dateTime)
        {
            var clients = await App.Database.GetClientsAsync();
            var addresses = await App.Database.GetAddressesAsync();
            var appointments = await GetTodaysAppointments(inspector, dateTime);

            var inspectors = await App.Database.GetInspectorsAsync();
            int i = 1;
            Inspector appInspector = new Inspector();
            while(i <= inspectors.Count)
            {

                foreach (var n in inspectors)
                {
                    if (i == n.ID)
                    {
                        appInspector = n;
                    }
                }

                foreach (var a in appointments)
                {
                    string clientName = null;
                    string address = null;
                    DateTime startTime;
                    double duration = 0;
                    if (a.InspectorID == appInspector.ID)
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
                        Appointment.Clicked += async (sender, args) => await Application.Current.MainPage.Navigation.PushAsync(new AppointmentInfo(a, inspector));

                        Appointment.BackgroundColor = Metrics.GetInspectorColor(appInspector);

                        if (a.Approved == false)
                        {
                            Appointment.BackgroundColor = Color.White;
                            Appointment.Text = "Awaiting Approval from Admin\n" + Appointment.Text;
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
                                row = appInspector.ID;  // verify this is correct inspector.ID
                            }
                        }

                        int column = startTime.Hour - 7;
                        
                        if(column <= 0)
                        {
                            Appointment.Text = "EARLY START\n" + Appointment.Text;
                            Appointment.TextColor = Color.Red;
                            int variance = column - 1;
                            duration += variance;
                            column = 1;
                        }
                        else if(column >= 13)
                        {
                            column = 13;
                        }
                        if (column > 10)
                        {
                            Appointment.Text = "Late Start\n" + Appointment.Text;
                        }

                        int columnSpan = Convert.ToInt32(column + duration);
                        if(columnSpan > 13)
                        {
                            var difference = 13 - columnSpan;
                            duration += difference + 1;
                        }
                        if (duration <= 0)
                        {
                            duration = 1;
                        }
                        GridView.Children.Add(Appointment, column, row);

                        Grid.SetColumnSpan(Appointment, Convert.ToInt32(duration));
                    }
                }
                i++;
            }
        }

        public async Task<List<Button>> OrganizeScheduleDisplay(List<Button> list)
        {
            //first organizes by inspector ID
            for(int i = 0; i < list.Count; i++)
            {
                var appointment = await App.Database.GetAppointmentAsync(Convert.ToInt32(list[i].ClassId));

                for(int n = 0; n < list.Count; n++)
                {
                    var appointment2 = await App.Database.GetAppointmentAsync(Convert.ToInt32(list[n].ClassId));

                    if(appointment.InspectorID < appointment2.InspectorID)
                    {
                        var temp = list[i];
                        list[i] = list[n];
                        list[n] = temp;
                    }
                }
            }
            
            // second organizes by appointment time
            for(int i = 0; i < list.Count; i++)
            {
                var appointment = await App.Database.GetAppointmentAsync(Convert.ToInt32(list[i].ClassId));

                for(int n = 0; n < list.Count; n++)
                {
                    var appointment2 = await App.Database.GetAppointmentAsync(Convert.ToInt32(list[n].ClassId));

                    if(appointment.InspectorID == appointment2.InspectorID && appointment.StartTime < appointment2.StartTime)
                    {
                        var temp = list[i];
                        list[i] = list[n];
                        list[n] = temp;
                    }
                }
            }
            return list.ToList();
        }
        public string WeekDatesLabel(DateTime dateTime)
        {
            var currentDay = Convert.ToInt32(dateTime.DayOfWeek);
            var daysToCome = 6 - currentDay;
            var daysPast = 6 - daysToCome;
            var sunday = dateTime.AddDays(-daysPast);
            var saturday = dateTime.AddDays(daysToCome);

            var dateStr = sunday.ToShortDateString() + " - " + saturday.ToShortDateString();

            return dateStr;
        }
    }
}
