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
        Client client = new Client();
        Realtor realtor = new Realtor();
        Appointment appointment = new Appointment();
        int inspectorID = 0;
        string InspectionTypeIDs = null;

        Inspector currentUser = new Inspector();

        public BookInspection(Inspector user)
        {
            currentUser = user;
            InitializeComponent();
            OnStart();
        }

        private async void RealtorNameEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            RealtorNameEntry.Placeholder = "Begin Typing Name to Search";
            string search = RealtorNameEntry.Text;
            if(search.Length >= 3)
            {
                var realtors = await App.Database.GetRealtorsAsync();

                foreach (var r in realtors)
                {
                    if (r.Name.Contains(search))
                    {
                        RealtorPicker.SelectedItem = r.Name;

                        break;
                    }
                    else
                    {
                        RealtorPicker.SelectedIndex = -1;
                    }
                }
            }
            if(search.Length == 0)
            {
                RealtorPicker.SelectedIndex = -1;
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
                if(InspectionTypeIDs == null)
                {
                    InspectionTypeIDs = inspectionType.ID.ToString();
                    RunningTotal.Text = "\n" + inspectionType.Name + " - " + "\t $" + inspectionType.Price.ToString();
                    total = inspectionType.Price;
                    DurationTimeLabel.Text = inspectionType.DurationHours.ToString();
                }
                else
                {
                    double duration = double.Parse(DurationTimeLabel.Text);
                    InspectionTypeIDs += ", " + inspectionType.ID.ToString();
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
                string insIDs = InspectionTypeIDs;
                int index3;
                if(RunningTotal.Text != "")
                {
                    index3 = insIDs.LastIndexOf(",");
                    string removeLast = insIDs.Substring(0, index3);
                    InspectionTypeIDs = removeLast;
                }
                else
                {
                    InspectionTypeIDs = null;
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
    }
}