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
        InspectionServices services = new InspectionServices();
        int inspectorID = 0;
        string inspectionTypeIDs = null;
        bool paid = false;
        string search = null;
        bool realtorSelected = false;
        string clientLastPhoneText;
        string realtorLastPhoneText;

        Inspector currentUser = new Inspector();

        public BookInspection(Inspector user)  
        {
            currentUser = user;
            inspectorID = currentUser.ID;
            InitializeComponent();
            OnStart();
        }

        private async void RealtorNameEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(realtorSelected == false)
            {
                string lastSearch = search;
                search = RealtorNameEntry.Text;
                if (search.Length >= 3)
                {
                    bool backspace = Validate.BackSpace(search, lastSearch);
                    if (backspace == false)
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
            try
            {
                // sets first letter to uppercase for each new word 
                if (RealtorNameEntry.Text.Length >= 2 && search != null && search != "")
                {
                    RealtorNameEntry.Text = Validate.Capitalize_Name(search);
                }
            }
            catch
            {
                //continues as search 
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
            DateTime dateTime = DateTime.Parse("1/1/2020 8:00:00 AM");
            InspectionDatePicker.MinimumDate = DateTime.Today;
            InspectionTimePicker.Time = dateTime.TimeOfDay;
        }


        private async void AddServiceBtn_Clicked(object sender, EventArgs e)
        {
            inspectionTypeIDs = await services.SetServices(ServicesPicker, PriceTotalEntry, RunningTotal, DurationTimeLabel, inspectionTypeIDs);
        }

        private async void UndoServiceBtn_Clicked(object sender, EventArgs e)
        {
            inspectionTypeIDs = await services.UndoServices(ServicesPicker, PriceTotalEntry, RunningTotal, DurationTimeLabel, inspectionTypeIDs);
        }

        private async void ScheduleBtn_Clicked(object sender, EventArgs e)
        {
            try
            {
                var workhours = Validate.CheckTime(InspectionTimePicker.Time);
                var acceptedTime = true;

                if(workhours == false)
                {
                    acceptedTime = await DisplayAlert("Schedule Time", "The time you've selected is outside of normal work hours.  " +
                        "Do you want to continue?", "Yes, book inspection", "No, don't book inspection");
                }
                if (acceptedTime)
                {
                    bool cPhone = true;
                    bool rPhone = true;
                    bool cEmail = true;
                    bool rEmail = true;
                    string phoneC = null;
                    string phoneR = null;
                    if (ClientPhoneEntry.Text != null && ClientPhoneEntry.Text != "")
                    {
                        phoneC = Validate.Phone_Syntax(ClientPhoneEntry.Text);
                        cPhone = Validate.Phone_Validate(phoneC);
                    }
                    if (RealtorPhoneEntry.Text != null && RealtorPhoneEntry.Text != "")
                    {
                        phoneR = Validate.Phone_Syntax(RealtorPhoneEntry.Text);
                        rPhone = Validate.Phone_Validate(phoneR);
                    }
                    if (ClientEmailEntry.Text != null && ClientEmailEntry.Text != "")
                    {
                        cEmail = Validate.Email_Validate(ClientEmailEntry.Text);
                    }
                    if (RealtorEmailEntry.Text != null && RealtorEmailEntry.Text != "")
                    {
                        rEmail = Validate.Email_Validate(RealtorEmailEntry.Text);
                    }
                    var aZip = Validate.Zip_Syntax(ZipEntry.Text);

                    if (cPhone && rPhone && cEmail && rEmail && aZip)
                    {
                        Client client = new Client
                        {
                            Name = ClientNameEntry.Text,
                        };

                        if (ClientEmailEntry.Text != null && ClientEmailEntry.Text != "")
                            client.Email = ClientEmailEntry.Text;

                        if (phoneC != null && phoneC != "")
                        {
                            var clientPhone = Validate.Phone_Syntax(phoneC);
                            client.Phone = clientPhone;
                        }

                        Realtor realtor = new Realtor();
                        if (RealtorNameEntry.Text != null & RealtorNameEntry.Text != "")
                        {
                            realtor.Name = RealtorNameEntry.Text;
                        }

                        if (RealtorEmailEntry.Text != null && RealtorEmailEntry.Text != "")
                            realtor.Email = RealtorEmailEntry.Text;
                        if (phoneR != null && phoneR != "")
                        {
                            var realtorPhone = Validate.Phone_Syntax(phoneR);
                            realtor.Phone = realtorPhone;
                        }
                        Address address = new Address
                        {
                            StreetAddress = StreetEntry.Text,
                            City = CityEntry.Text,
                            Zip = ZipEntry.Text
                        };

                        bool noName = false;
                        if((RealtorNameEntry.Text == null || RealtorNameEntry.Text == "") && 
                            (RealtorPhoneEntry.Text != null || RealtorPhoneEntry.Text != "" ||
                            RealtorEmailEntry.Text != null || RealtorEmailEntry.Text != ""))
                        {
                            noName = true;
                        }

                        if (RealtorNameEntry.Text != null && RealtorNameEntry.Text != "" && noName == false)
                        {
                            if (SaveEditCheckbox.IsChecked)
                            {
                                var realtorUpdate = await App.Database.GetRealtorAsync(RealtorPicker.SelectedItem.ToString());
                                realtorUpdate.Name = RealtorNameEntry.Text;
                                realtorUpdate.Phone = RealtorPhoneEntry.Text;
                                realtorUpdate.Email = RealtorEmailEntry.Text;
                                //await App.Database.SaveRealtorAsync(realtorUpdate);
                                await realtorUpdate.SavePersonAsync(realtorUpdate);
                                realtor = realtorUpdate;                           
                            }
                            else
                            {
                                var realtorCheck = await App.Database.GetRealtorAsync(realtor.Name);

                                if (realtorCheck != null && realtorCheck.Name != realtor.Name && realtorCheck.Phone != realtor.Phone && realtorCheck.Email != realtor.Email)
                                {
                                    await realtor.SavePersonAsync(realtor);
                                }
                                else if (realtorCheck != null && (realtorCheck.Name != realtor.Name || realtorCheck.Phone != realtor.Phone || realtorCheck.Email != realtor.Email))
                                {
                                    var save = await DisplayAlert("Conflict", "There is a conflict in the realtor database, did you intend to update this realtor's" +
                                         " information or create a new realtor?", "Update Existing Realtor", "Create New Realtor");
                                    if (save == false)
                                    {
                                        realtor = await RealtorTools.SameName(realtor);

                                        await realtor.SavePersonAsync(realtor);
                                    }
                                    else
                                    {
                                        var realtorUpdate = await App.Database.GetRealtorAsync(RealtorPicker.SelectedItem.ToString());
                                        realtorUpdate.Name = RealtorNameEntry.Text;
                                        realtorUpdate.Phone = RealtorPhoneEntry.Text;
                                        realtorUpdate.Email = RealtorEmailEntry.Text;
                                        await realtorUpdate.SavePersonAsync(realtorUpdate);
                                    }
                                }
                                else if (realtorCheck == null && realtor.Name != null)
                                {
                                    await realtor.SavePersonAsync(realtor);
                                }
                                else if (realtorCheck != null && realtor.Name != null)
                                {
                                    realtor = realtorCheck;
                                }
                            }
                        }
                        if (noName == false)
                        {
                            await client.SavePersonAsync(client);
                            await App.Database.SaveAddressAsync(address);

                            DateTime startDate = InspectionDatePicker.Date;
                            TimeSpan startTime = InspectionTimePicker.Time;

                            DateTime startDateAndTime = startDate + startTime;

                            Appointment appointment = new Appointment
                            {
                                InspectorID = inspectorID,
                                ClientID = client.ID,
                                //RealtorID = realtor.ID,
                                InspectionTypeIDs = inspectionTypeIDs,
                                PriceTotal = double.Parse(PriceTotalEntry.Text),
                                StartTime = startDateAndTime,
                                Duration = double.Parse(DurationTimeLabel.Text),
                                Paid = paid,
                                AddressID = address.ID,
                            };
                            if (realtor.ID != 0)
                                appointment.RealtorID = realtor.ID;

                            if (currentUser.Admin)
                                appointment.Approved = true;

                            var appointments = await App.Database.GetAppointmentsAsync();
                            bool schedule = true;
                            foreach (var a in appointments)
                            {
                                if ((a.InspectorID == appointment.InspectorID) && ((appointment.StartTime >= a.StartTime && appointment.StartTime <= a.StartTime.AddHours(a.Duration)) ||
                                        (a.StartTime >= appointment.StartTime && a.StartTime <= appointment.StartTime.AddHours(appointment.Duration))))
                                {
                                    var save = await DisplayAlert("Conflict", "There is a schedule conflict. Do you still wish to schedule this appointment?", "Yes, Schedule", "No, Don't Schedule");
                                    if (save)
                                    {
                                        await App.Database.SaveAppointmentAsync(appointment);
                                        schedule = false;
                                        await Application.Current.MainPage.Navigation.PopAsync();
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
                                await Application.Current.MainPage.Navigation.PopAsync();

                            }
                        }
                        else
                        {
                            var useRealtor = await DisplayAlert("No Realtor Name", "You did not provide a realtor name.", "Add Name", "Clear Realtor Input");
                            if (useRealtor == false)
                            {
                                ClearRealtorBtn_Clicked(sender, e);
                            }
                            else
                            {
                                RealtorNameEntry.Focus();
                            }
                        }
                    }
                    else
                    {
                        string alert = "";
                        if (aZip == false)
                            alert = "Invalid zip code entry for address\n";
                        if (cPhone == false)
                            alert += "Invalid Client Phone Number\n";
                        if (rPhone == false)
                            alert += "Invalid Realtor Phone Number\n";
                        if (cEmail == false)
                            alert += "Invalid Client Email Address\n";
                        if (rEmail == false)
                            alert += "Invalid Realtor Email Address";

                        await DisplayAlert("Error", alert, "OK");
                    }
                }       
            }
            catch
            {
                await DisplayAlert("Error", "Missing information. Ensure all required fields are complete", "OK");
                if(InspectorPicker.SelectedIndex == -1)                
                    InspectorPicker.BackgroundColor = Color.Red;
                
                if(ClientNameEntry.Text == null || ClientNameEntry.Text == "")
                    ClientNameEntry.BackgroundColor = Color.Red;
                
                if(StreetEntry.Text == null || StreetEntry.Text == "")
                    StreetEntry.BackgroundColor = Color.Red;
                
                if(CityEntry.Text == null || CityEntry.Text == "")
                    CityEntry.BackgroundColor = Color.Red;
                
                if(ZipEntry.Text == null || ZipEntry.Text == "")
                    ZipEntry.BackgroundColor = Color.Red;

                if (PriceTotalEntry.Text == "0")
                    ServicesPicker.BackgroundColor = Color.Red;

            }
        }

        private async void InspectorPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var inspector = await App.Database.GetInspectorAsync(InspectorPicker.SelectedItem.ToString());
            inspectorID = inspector.ID;
            if(InspectorPicker.SelectedIndex != -1)
            {
                InspectorPicker.BackgroundColor = default;
            }
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
            realtorSelected = true;

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

            SelectRealtorBtn.IsVisible = false;

            realtorSelected = false;
        }

        private void ClearRealtorBtn_Clicked(object sender, EventArgs e)
        {
            RealtorPicker.SelectedIndex = -1;

            RealtorNameEntry.Text = "";
            RealtorPhoneEntry.Text = "";
            RealtorEmailEntry.Text = "";
        }

        private void ClientEmailEntry_TextChanged(object sender, TextChangedEventArgs e)
        {


        }

        private void ClientNameEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            // sets first letter to uppercase for each new word 
            if (ClientNameEntry.Text.Length >= 2)
            {
                var input = ClientNameEntry.Text;
                ClientNameEntry.Text = Validate.Capitalize_Name(input);
            }
            if(ClientNameEntry.Text != null && ClientNameEntry.Text != "")
            {
                ClientNameEntry.BackgroundColor = default;
            }
        }

        private void ClientPhoneEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            clientLastPhoneText = Validate.PhoneInput(ClientPhoneEntry, clientLastPhoneText);
        }

        private void RealtorPhoneEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            realtorLastPhoneText = Validate.PhoneInput(RealtorPhoneEntry, realtorLastPhoneText);
        }

        private void StreetEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (StreetEntry.Text != null & StreetEntry.Text != "")
                StreetEntry.BackgroundColor = default;
        }

        private void CityEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (CityEntry.Text != null & CityEntry.Text != "")
                CityEntry.BackgroundColor = default;
        }

        private void ZipEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ZipEntry.Text != null & ZipEntry.Text != "")
                ZipEntry.BackgroundColor = default;

            ZipEntry.Text = Validate.ZipLength(ZipEntry.Text);
        }

        private void ServicesPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ServicesPicker.SelectedIndex != -1)
                ServicesPicker.BackgroundColor = default;
        }
    }
}