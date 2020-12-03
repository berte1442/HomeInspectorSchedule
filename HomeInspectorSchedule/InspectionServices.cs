using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Threading.Tasks;

namespace HomeInspectorSchedule
{
    public class InspectionServices
    {
        public async Task<string> SetServices(Picker servicePicker, Entry totalEntry, Label totalLabel, Label durationLabel, string typeIDs)
        {
            if (servicePicker.SelectedIndex != -1)
            {
                double total = double.Parse(totalEntry.Text);
                var inspectionType = await App.Database.GetInspectionTypeAsync(servicePicker.SelectedItem.ToString());
                if (typeIDs == null)
                {
                    typeIDs = inspectionType.ID.ToString();
                    totalLabel.Text = "\n" + inspectionType.Name + " - " + "\t $" + inspectionType.Price.ToString();
                    total = inspectionType.Price;
                    durationLabel.Text = inspectionType.DurationHours.ToString();
                }
                else
                {
                    double duration = double.Parse(durationLabel.Text);
                    typeIDs += ", " + inspectionType.ID.ToString();
                    totalLabel.Text += "\n" + inspectionType.Name + " - " + "\t $" + inspectionType.Price.ToString();
                    total += inspectionType.Price;
                    durationLabel.Text = (duration + inspectionType.DurationHours).ToString();
                }

                servicePicker.Items.Remove(inspectionType.Name);
                servicePicker.SelectedIndex = -1;
                totalEntry.Text = total.ToString();
            }
            return typeIDs;
        }

        public async Task<string> UndoServices(Picker servicePicker, Entry totalEntry, Label totalLabel, Label durationLabel, string typeIDs)
        {
            if (totalLabel.Text != "" && totalLabel.Text != null)
            {
                // remove last selected item price from UI display
                string allAdded = totalLabel.Text;
                int index = allAdded.LastIndexOf("\n");
                string undoLast = allAdded.Substring(0, index);
                totalLabel.Text = undoLast;

                // add inspection type back to picker
                int index2 = allAdded.LastIndexOf("-") - 1;
                var length = index2 - index;
                string service = allAdded.Substring(index, length);
                service = service.Trim();
                servicePicker.Items.Add(service);

                // removes inspection type ID from InspectionTypeIDs
                string insIDs = typeIDs;
                int index3;
                if (totalLabel.Text != "")
                {
                    index3 = insIDs.LastIndexOf(",");
                    string removeLast = insIDs.Substring(0, index3);
                    typeIDs = removeLast;
                }
                else
                {
                    typeIDs = null;
                }

                // subtracts removed service price from total price
                double priceTotal = double.Parse(totalEntry.Text);
                var inspectionType = await App.Database.GetInspectionTypeAsync(service);
                totalEntry.Text = (priceTotal - inspectionType.Price).ToString();

                // subtracts last service duration hours from total inspection estimated duration time
                double subtractDuration = inspectionType.DurationHours;
                double totalDuration = double.Parse(durationLabel.Text);
                durationLabel.Text = (totalDuration - subtractDuration).ToString();
            }
            return typeIDs;
        }
    }
}
