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

        public BookInspection(Inspector user)  // add DateTime param to set inspection time datepicker from schedule page
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
                bool found = false;
                if(backspace == false)
                {
                    var realtors = await App.Database.GetRealtorsAsync();

                    foreach (var r in realtors)
                    {
                        if (r.Name.Contains(search))
                        {
                            RealtorPicker.SelectedItem = r.Name;
                            found = true;
                            break;
                        }
                        //else
                        //{
                        //    RealtorPicker.SelectedIndex = -1;
                        //}
                    }
                    
                }
                //if (search.Length == 0 || found == false)
                //{
                //    RealtorPicker.SelectedIndex = -1;
                //}
            }
               
        }

        private async void RealtorPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var realtor = await App.Database.GetRealtorAsync(RealtorPicker.SelectedItem.ToString());

            if(realtor != null)
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
        private async void RealtorNameEntry_Completed(object sender, EventArgs e)
        {
            if (RealtorPicker.SelectedIndex != -1)
            {
                var realtor = await App.Database.GetRealtorAsync(RealtorPicker.SelectedItem.ToString());
                RealtorNameEntry.Text = realtor.Name;
                RealtorPhoneEntry.Text = realtor.Phone;
                RealtorEmailEntry.Text = realtor.Email;
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

            // save the client, realtor and address
            // retrieve them from the databse in order to obtain ID #s
            // plug ID numbers into appointment properties
            // save appointment

            // parse database to determine if client is already saved
            await App.Database.SaveClientAsync(client);
            // parse database to determine if realtor is already saved
            var realtors = await App.Database.GetRealtorsAsync();
            if(RealtorPicker.SelectedIndex == -1)
            {
                if (!realtors.Contains(realtor))
                {
                    await App.Database.SaveRealtorAsync(realtor);
                }
                else
                {
                    var realtorUpdate = await App.Database.GetRealtorAsync(realtor.Name);
                    await App.Database.SaveRealtorAsync(realtorUpdate);
                }
            }
            else
            {
                var editRealtor = await App.Database.GetRealtorAsync(RealtorPicker.SelectedItem.ToString());
                await App.Database.DeleteRealtorAsync(editRealtor);
                editRealtor = realtor;
                await App.Database.SaveRealtorAsync(editRealtor);

            }

            // parse database to determine if client is already saved
            await App.Database.SaveAddressAsync(address);
            var client2 = await App.Database.GetClientAsync(client.Name);
            var realtor2 = await App.Database.GetRealtorAsync(realtor.Name);
            var address2= await App.Database.GetAddressAsync(address.StreetAddress, address.City, address.Zip);

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
            await App.Database.SaveAppointmentAsync(appointment);
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
    }
}