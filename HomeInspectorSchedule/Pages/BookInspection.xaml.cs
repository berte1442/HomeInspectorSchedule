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
    public partial class BookInspection : ContentPage
    {
        int inspectorID = 0;
        string inspectionTypeIDs = null;
        bool paid = false;
        string search = null;

        Inspector currentUser = new Inspector();

        public BookInspection(Inspector user)  
        {
            currentUser = user;
            inspectorID = currentUser.ID;
            InitializeComponent();
            OnStart();
        }
        public bool BackSpace(string search, string lastSearch)
        {
            bool backspace = false;
            if (lastSearch != null)
            {
                int length = lastSearch.Length;
                string compare = lastSearch.Substring(0, length - 1);
                if (search == compare)
                {
                    backspace = true;
                }
            }
            return backspace;
        }
        private async void RealtorNameEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            string lastSearch = search;
            search = RealtorNameEntry.Text;
            if(search.Length >= 3)
            {
                bool backspace = BackSpace(search, lastSearch);
                if(backspace == false)
                {
                    var realtors = await App.Database.GetRealtorsAsync();

                    foreach (var r in realtors)
                    {
                        if (r.Name.Contains(search))
                        {
                            RealtorPicker.SelectedItem = r.Name;
                            break;
                        }
                    }
                   
                }
            }
               
        }
        private void RealtorPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectRealtorBtn.IsVisible = true;

            if(RealtorPicker.SelectedIndex != -1)
            {
                SaveEditGrid.IsVisible = true;
            }
            else
            {
                SaveEditCheckbox.IsChecked = false;
                SaveEditGrid.IsVisible = false;
            }
        }
        //private async void RealtorNameEntry_Completed(object sender, EventArgs e)
        //{
        //    if (RealtorPicker.SelectedIndex != -1)
        //    {
        //        var realtor = await App.Database.GetRealtorAsync(RealtorPicker.SelectedItem.ToString());
        //        RealtorNameEntry.Text = realtor.Name;
        //        RealtorPhoneEntry.Text = realtor.Phone;
        //        RealtorEmailEntry.Text = realtor.Email;
        //    }
        //}
        private async void OnStart()
        {

            var realtors = await App.Database.GetRealtorsAsync();
            foreach (var r in realtors)
            {
                RealtorPicker.Items.Add(r.Name);
            }
            var services = await App.Database.GetInspectionTypesAsync();
            foreach(var s in services)
            {
                ServicesPicker.Items.Add(s.Name);
            }

            PriceTotalEntry.Text = "0";

            WelcomeLabel.Text += currentUser.Name;

            if (currentUser.Admin == false)
            {
                MainLayout.Children.Remove(SelectInspector);
            }
            else
            {
                var inspectors = await App.Database.GetInspectorsAsync();
                foreach(var i in inspectors)
                {
                    InspectorPicker.Items.Add(i.Name);
                }
            }
        }

        private async void AddServiceBtn_Clicked(object sender, EventArgs e)
        {
            // align running total text appropriately
            
            if(ServicesPicker.SelectedIndex != -1)
            {
                double total = double.Parse(PriceTotalEntry.Text);
                var inspectionType = await App.Database.GetInspectionTypeAsync(ServicesPicker.SelectedItem.ToString());
                if(inspectionTypeIDs == null)
                {
                    inspectionTypeIDs = inspectionType.ID.ToString();
                    RunningTotal.Text = "\n" + inspectionType.Name + " - " + "\t $" + inspectionType.Price.ToString();
                    total = inspectionType.Price;
                    DurationTimeLabel.Text = inspectionType.DurationHours.ToString();
                }
                else
                {
                    double duration = double.Parse(DurationTimeLabel.Text);
                    inspectionTypeIDs += ", " + inspectionType.ID.ToString();
                    RunningTotal.Text += "\n" + inspectionType.Name + " - " + "\t $" + inspectionType.Price.ToString();
                    total += inspectionType.Price;
                    DurationTimeLabel.Text = (duration + inspectionType.DurationHours).ToString();
                }
                
                ServicesPicker.Items.Remove(inspectionType.Name);
                ServicesPicker.SelectedIndex = -1;
                PriceTotalEntry.Text = total.ToString();
            }
        }

        private async void UndoServiceBtn_Clicked(object sender, EventArgs e)
        {
            if(RunningTotal.Text != "" && RunningTotal.Text != null)
            {
                // remove last selected item price from UI display
                string allAdded = RunningTotal.Text;
                int index = allAdded.LastIndexOf("\n");
                string undoLast = allAdded.Substring(0, index);
                RunningTotal.Text = undoLast;

                // add inspection type back to picker
                int index2 = allAdded.LastIndexOf("-") - 1;
                var length = index2 - index;
                string service = allAdded.Substring(index, length);
                service = service.Trim();
                ServicesPicker.Items.Add(service);
                
                // removes inspection type ID from InspectionTypeIDs
                string insIDs = inspectionTypeIDs;
                int index3;
                if(RunningTotal.Text != "")
                {
                    index3 = insIDs.LastIndexOf(",");
                    string removeLast = insIDs.Substring(0, index3);
                    inspectionTypeIDs = removeLast;
                }
                else
                {
                    inspectionTypeIDs = null;
                }                

                // subtracts removed service price from total price
                double priceTotal = double.Parse(PriceTotalEntry.Text);
                var inspectionType = await App.Database.GetInspectionTypeAsync(service);
                PriceTotalEntry.Text = (priceTotal - inspectionType.Price).ToString();

                // subtracts last service duration hours from total inspection estimated duration time
                double subtractDuration = inspectionType.DurationHours;
                double totalDuration = double.Parse(DurationTimeLabel.Text);
                DurationTimeLabel.Text = (totalDuration - subtractDuration).ToString();
            }
        }

        private async void ScheduleBtn_Clicked(object sender, EventArgs e)
        {
            Client client = new Client
            {
                Name = ClientNameEntry.Text,
                Phone = ClientPhoneEntry.Text,
                Email = ClientEmailEntry.Text
            };
            Realtor realtor = new Realtor
            {
                Name = RealtorNameEntry.Text,
                Phone = RealtorPhoneEntry.Text,
                Email = RealtorEmailEntry.Text
            };
            Address address = new Address
            {
                StreetAddress = StreetEntry.Text,
                City = CityEntry.Text,
                Zip = ZipEntry.Text
            };

            await App.Database.SaveClientAsync(client);

            if (SaveEditCheckbox.IsChecked)
            {
                var realtorUpdate = await App.Database.GetRealtorAsync(RealtorPicker.SelectedItem.ToString());
                realtorUpdate.Name = RealtorNameEntry.Text;
                realtorUpdate.Phone = RealtorPhoneEntry.Text;
                realtorUpdate.Email = RealtorEmailEntry.Text;
                await App.Database.SaveRealtorAsync(realtorUpdate);
            }
            else
            {
                var realtorCheck = await App.Database.GetRealtorAsync(realtor.Name);

                if (realtorCheck != null && realtorCheck.Name != realtor.Name && realtorCheck.Phone != realtor.Phone && realtorCheck.Email != realtor.Email)
                {
                    await App.Database.SaveRealtorAsync(realtor);
                }
                else if(realtorCheck != null && (realtorCheck.Name != realtor.Name || realtorCheck.Phone != realtor.Phone || realtorCheck.Email != realtor.Email))
                {
                    var save = await DisplayAlert("Conflict", "There is a conflict in the realtor database, did you intend to update this realtor's" +
                         " information or create a new realtor?", "Update Existing Realtor", "Create New Realtor");
                    if (save == false)
                    {
                        await App.Database.SaveRealtorAsync(realtor);
                    }
                    else
                    {
                        var realtorUpdate = await App.Database.GetRealtorAsync(RealtorPicker.SelectedItem.ToString());
                        realtorUpdate.Name = RealtorNameEntry.Text;
                        realtorUpdate.Phone = RealtorPhoneEntry.Text;
                        realtorUpdate.Email = RealtorEmailEntry.Text;
                        await App.Database.SaveRealtorAsync(realtorUpdate);
                    }
                }
                else if(realtorCheck == null && realtor.Name != null)
                {
                    await App.Database.SaveRealtorAsync(realtor);
                }
                else if(realtorCheck != null && realtor.Name != null)
                {
                    realtor = realtorCheck;
                }
            }

            await App.Database.SaveAddressAsync(address);

            DateTime startDate = InspectionDatePicker.Date;
            TimeSpan startTime = InspectionTimePicker.Time;

            DateTime startDateAndTime = startDate + startTime;

            Appointment appointment = new Appointment
            {
                InspectorID = inspectorID,
                ClientID = client.ID,
                RealtorID = realtor.ID,
                InspectionTypeIDs = inspectionTypeIDs,
                PriceTotal = double.Parse(PriceTotalEntry.Text),
                StartTime = startDateAndTime,
                Duration = double.Parse(DurationTimeLabel.Text),
                Paid = paid,
                AddressID = address.ID,
            };
            if (currentUser.Admin)
            {
                appointment.Approved = true;
            }

            var appointments = await App.Database.GetAppointmentsAsync();
            bool schedule = true;
            foreach(var a in appointments)
            {
                if ((a.InspectorID == appointment.InspectorID) && ((appointment.StartTime >= a.StartTime && appointment.StartTime <= a.StartTime.AddHours(a.Duration)) ||
                        (a.StartTime >= appointment.StartTime && a.StartTime <= appointment.StartTime.AddHours(appointment.Duration))))
                {
                    var save = await DisplayAlert("Conflict", "There is a schedule conflict. Do you still wish to schedule this appointment?", "Yes, Schedule", "No, Don't Schedule");
                    if (save)
                    {
                        await App.Database.SaveAppointmentAsync(appointment);
                        schedule = false;
                        break;
                    }
                    else
                    {
                        schedule = false;
                    }
                }
            }
            if (schedule)
            {
                await App.Database.SaveAppointmentAsync(appointment);
            }
            Application.Current.MainPage.Navigation.PopAsync();
        }

        private async void InspectorPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var inspector = await App.Database.GetInspectorAsync(InspectorPicker.SelectedItem.ToString());
            inspectorID = inspector.ID;
        }

        private void PaidCheckbox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (PaidCheckbox.IsChecked)
            {
                paid = true;
            }
        }

        private async void SelectRealtorBtn_Clicked(object sender, EventArgs e)
        {
            var realtor = await App.Database.GetRealtorAsync(RealtorPicker.SelectedItem.ToString());

            if (realtor != null)
            {
                RealtorNameEntry.Text = realtor.Name;
                RealtorPhoneEntry.Text = realtor.Phone;
                RealtorEmailEntry.Text = realtor.Email;
            }
            else
            {
                RealtorNameEntry.Text = "";
                RealtorPhoneEntry.Text = "";
                RealtorEmailEntry.Text = "";
            }
        }

        private void ClearRealtorBtn_Clicked(object sender, EventArgs e)
        {
            RealtorPicker.SelectedIndex = -1;

            RealtorNameEntry.Text = "";
            RealtorPhoneEntry.Text = "";
            RealtorEmailEntry.Text = "";
        }
    }
}