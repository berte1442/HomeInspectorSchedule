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
    public partial class AppointmentInfo : ContentPage
    {
        Appointment app = new Appointment();
        Address address = new Address();
        Client client = new Client();
        Inspector inspector = new Inspector();
        Realtor realtor = new Realtor();

        bool canceled = false;
        bool approved = false;

        public AppointmentInfo(Appointment appointment)
        {
            app = appointment;
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            address = await App.Database.GetAddressAsync(app.AddressID);
            client = await App.Database.GetClientAsync(app.ClientID);
            inspector = await App.Database.GetInspectorAsync(app.InspectorID);
            realtor = await App.Database.GetRealtorAsync(app.RealtorID);

            InspectorNameLabel.Text = inspector.Name;

            if (inspector.Admin)
            {
                SelectInspectorLabel.IsVisible = true;
                InspectorPicker.IsVisible = true;
                ApproveBtn.IsVisible = true;
                InspectorLabel.IsVisible = false;
            }
            string color = inspector.InspectorColor;
            if (color == "blue")
            {
                DisplayLayout.BackgroundColor = Color.LightBlue;
            }
            else if (color == "yellow")
            {
                DisplayLayout.BackgroundColor = Color.YellowGreen;
            }
            else if (color == "purple")
            {
                DisplayLayout.BackgroundColor = Color.MediumPurple;
            }
            else if (color == "green")
            {
                DisplayLayout.BackgroundColor = Color.LightGreen;
            }
            else if (color == "orange")
            {
                DisplayLayout.BackgroundColor = Color.Orange;
            }

            if(app.Approved == false)
            {
                DisplayLayout.BackgroundColor = Color.White;
            }
            if (app.Canceled)
            {
                DisplayLayout.BackgroundColor = Color.Red;
            }

            var inspectors = await App.Database.GetInspectorsAsync();
            InspectorPicker.Items.Clear();
            foreach(var i in inspectors)
            {
                InspectorPicker.Items.Add(i.Name);
            }

            InspectorPicker.SelectedItem = inspector.Name;

            ClientNameEntry.Text = client.Name;
            if(client.Phone != null)
            ClientPhoneEntry.Text = client.Phone;
            if(client.Email != null)
            ClientEmailEntry.Text = client.Email;

            StreeAddressEntry.Text = address.StreetAddress;
            CityEntry.Text = address.City;
            ZipEntry.Text = address.Zip;

            var inspectionTypes = await App.Database.GetInspectionTypesAsync();
            foreach(var i in inspectionTypes)
            {
                InspectionTypePicker.Items.Add(i.Name);
            }

            string insIDs = app.InspectionTypeIDs;
            int index = insIDs.LastIndexOf(",");
            int length = insIDs.Length;
            while (index != -1)
            {

                int newLength = length - index;
                //int newLength = length - index;
                string nextUp = insIDs.Substring(0, index);
                string singleID = insIDs.Substring(index, newLength);
                insIDs = nextUp;

                int idLength = singleID.Length;

                string idNum = singleID.Substring(1, idLength - 1);
                idNum = idNum.Trim();
                var inspectionType = await App.Database.GetInspectionTypeAsync(int.Parse(idNum));

                InspectionTypeLabel.Text += inspectionType.Name + "\n";
                                    
                index = nextUp.LastIndexOf(",");
                length = insIDs.Length;

            }
            if(index == -1 && length != 0)
            {
                var inspectionType = await App.Database.GetInspectionTypeAsync(int.Parse(insIDs));
                InspectionTypeLabel.Text += inspectionType.Name;
            }
            PriceTotalLabel.Text = app.PriceTotal.ToString("C2");

            StartTimeLabel.Text = "Inspection Date: " + app.StartTime.ToShortDateString();

            StartDatePicker.Date = app.StartTime;

            StartTimePicker.Time = app.StartTime.TimeOfDay;

            DurationEntry.Text = app.Duration.ToString();

            if (app.Paid)
            {
                PaidCheckBox.IsChecked = true;
            }

            if(realtor != null)
            {
                if (realtor.Name != null)
                {
                    RealtorNameEntry.Text = realtor.Name;
                }
                if (realtor.Phone != null)
                {
                    RealtorPhoneEntry.Text = realtor.Phone;
                }
                if (realtor.Email != null)
                {
                    RealtorEmailEntry.Text = realtor.Email;
                }
            }

            if (app.Notes != null)
            {
                NotesEditor.Text = app.Notes;
            }

            if (app.Approved)
            {
                ApproveBtn.IsVisible = false;
                ApprovedLabel.Text = "Inspection has been approved";
                approved = true;
            }

            if (app.Canceled)
            {
                CancelBtn.IsVisible = false;
                CanceledLabel.Text = "This inspection has been canceled";
                canceled = true;
            }

        }

        private void PaidCheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {

        }

        private async void ApproveBtn_Clicked(object sender, EventArgs e)
        {
            var approve = await DisplayAlert("Canceled", "Are you sure you want to cancel this inspection?", "Yes, Cancel", "No, Don't Cancel");

            if (approve)
            {
                approved = true;
            }
            else
            {
                approved = false;
            }
        }

        private async void CancelBtn_Clicked(object sender, EventArgs e)
        {
            var cancel = await DisplayAlert("Canceled", "Are you sure you want to cancel this inspection?", "Yes, Cancel", "No, Don't Cancel");

            if (cancel) 
            { 
                canceled = true; 
            }
            else
            {
                canceled = false;
            }   
        }

        private void AddServicesBtn_Clicked(object sender, EventArgs e)
        {
            var selectedType = InspectionTypePicker.SelectedItem;


        }

        private void RemoveServiceBtn_Clicked(object sender, EventArgs e)
        {

        }

        private async void SaveBtn_Clicked(object sender, EventArgs e)
        {
            client.Name = ClientNameEntry.Text;
            client.Phone = ClientPhoneEntry.Text;
            client.Email = ClientEmailEntry.Text;

            address.StreetAddress = StreeAddressEntry.Text;
            address.City = CityEntry.Text;
            address.Zip = ZipEntry.Text;

            realtor.Name = RealtorNameEntry.Text;
            realtor.Phone = RealtorPhoneEntry.Text;
            realtor.Email = RealtorEmailEntry.Text;

            var selectedInspector = await App.Database.GetInspectorAsync(InspectorPicker.SelectedItem.ToString());

            //inspection type ids
            var typeNames = InspectionTypeLabel.Text;

            string inspectionTypes = null;

            if (typeNames.ToLower().Contains("residential"))
            {
                var inspectionType = await App.Database.GetInspectionTypeAsync("Residential");
                inspectionTypes += inspectionType.ID.ToString() + " ";
            }
            if (typeNames.ToLower().Contains("commercial"))
            {
                var inspectionType = await App.Database.GetInspectionTypeAsync("Commercial");
                inspectionTypes += inspectionType.ID.ToString() + " ";
            } 
            if (typeNames.ToLower().Contains("radon"))
            {
                var inspectionType = await App.Database.GetInspectionTypeAsync("Radon");
                inspectionTypes += inspectionType.ID.ToString() + " ";
            }    
            if (typeNames.ToLower().Contains("mold"))
            {
                var inspectionType = await App.Database.GetInspectionTypeAsync("Mold");
                inspectionTypes += inspectionType.ID.ToString() + " ";
            }

            app.InspectorID = selectedInspector.ID;
            app.ClientID = client.ID;
            app.RealtorID = realtor.ID;
            app.AddressID = address.ID;
            app.InspectionTypeIDs = inspectionTypes.Trim();
            app.PriceTotal = Convert.ToDouble(PriceTotalLabel.Text);
            app.StartTime = StartDatePicker.Date + StartTimePicker.Time;
            app.Duration = Convert.ToDouble(DurationEntry.Text);
            if (PaidCheckBox.IsChecked)
            {
                app.Paid = true;
            }
            else
            {
                app.Paid = false;
            }
            if(NotesEditor.Text != null)
            {
                app.Notes = NotesEditor.Text;
            }

            app.Canceled = canceled;
            app.Approved = approved;
        }
    }
}