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
        int id;
        DisplayLayout appointment = new DisplayLayout();
        Address address = new Address();
        Client client = new Client();
        Inspector inspector = new Inspector();
        Realtor realtor = new Realtor();

        public AppointmentInfo(string idStr)
        {
            id = int.Parse(idStr);
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            appointment = await App.Database.GetAppointmentAsync(id);
            address = await App.Database.GetAddressAsync(appointment.AddressID);
            client = await App.Database.GetClientAsync(appointment.ClientID);
            inspector = await App.Database.GetInspectorAsync(appointment.InspectorID);
            realtor = await App.Database.GetRealtorAsync(appointment.RealtorID);

            if (inspector.Admin)
            {
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

            if (appointment.Canceled)
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

            string insIDs = appointment.InspectionTypeIDs;
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

            StartTimeLabel.Text = appointment.StartTime.ToShortDateString();

            StartDatePicker.MinimumDate = DateTime.Today;
            StartDatePicker.Date = appointment.StartTime;

            StartTimePicker.Time = appointment.StartTime.TimeOfDay;

            DurationEntry.Text = appointment.Duration.ToString();

            if (appointment.Paid)
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

            if (appointment.Notes != null)
            {
                NotesEditor.Text = appointment.Notes;
            }

            if (appointment.Approved)
            {
                ApproveBtn.IsVisible = false;
                ApprovedLabel.Text = "Inspection has been approved";
            }

            if (appointment.Canceled)
            {
                CancelBtn.IsVisible = false;
                CanceledLabel.Text = "This inspection has been canceled";
            }

        }

        private void PaidCheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {

        }

        private void ApproveBtn_Clicked(object sender, EventArgs e)
        {

        }

        private void CancelBtn_Clicked(object sender, EventArgs e)
        {

        }

        private void AddServicesBtn_Clicked(object sender, EventArgs e)
        {

        }

        private void RemoveServiceBtn_Clicked(object sender, EventArgs e)
        {

        }

        private void SaveBtn_Clicked(object sender, EventArgs e)
        {

        }
    }
}