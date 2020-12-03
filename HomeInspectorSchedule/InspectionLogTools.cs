using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HomeInspectorSchedule
{
    static public class InspectionLogTools
    {
        static public async Task<List<string>> SearchAppointments(string search)
        {
            search = search.ToLower();

            var appointments = await App.Database.GetAppointmentsAsync();
            var clients = await App.Database.GetClientsAsync();
            var inspectors = await App.Database.GetInspectorsAsync();
            var inspectionTypes = await App.Database.GetInspectionTypesAsync();
            var addresses = await App.Database.GetAddressesAsync();
            var realtors = await App.Database.GetRealtorsAsync();

            List<int> clientIds = new List<int>();
            List<int> inspectorIds = new List<int>();
            List<int> typeIds = new List<int>();
            List<int> addressIds = new List<int>();
            List<int> realtorIds = new List<int>();

            List<Appointment> searchApps = new List<Appointment>();
            List<string> searchDisplay = new List<string>();


            foreach (var c in clients)
            {
                if (c.Name.ToLower().Contains(search))
                {
                    clientIds.Add(c.ID);
                }
            }
            foreach(var i in inspectors)
            {
                if (i.Name.ToLower().Contains(search))
                {
                    inspectorIds.Add(i.ID);
                }
            } 
            foreach(var x in inspectionTypes)
            {
                if (x.Name.ToLower().Contains(search))
                {
                    typeIds.Add(x.ID);
                }
            }
            foreach(var a in addresses)
            {
                if(a.StreetAddress.ToLower().Contains(search) || a.City.ToLower().Contains(search) || a.Zip.ToLower().Contains(search))
                {
                    addressIds.Add(a.ID);
                }
            }
            foreach(var r in realtors)
            {
                if (r.Name.ToLower().Contains(search))
                {
                    realtorIds.Add(r.ID);
                }
            }

            ///////////////////////
            foreach(var app in appointments)
            {
                foreach(var c in clientIds)
                {
                    if (app.ClientID == c && !searchApps.Contains(app))
                    {
                        searchApps.Add(app);
                    }
                }

                foreach (var i in inspectorIds)
                {
                    if (app.InspectorID == i && !searchApps.Contains(app))
                    {
                        searchApps.Add(app);
                    }
                }

                foreach(var t in typeIds)
                {
                    if(app.InspectionTypeIDs.Contains(t.ToString()) && !searchApps.Contains(app))
                    {
                        searchApps.Add(app);
                    }
                }

                foreach(var a in addressIds)
                {
                    if(app.AddressID == a && !searchApps.Contains(app))
                    {
                        searchApps.Add(app);
                    }
                }

                foreach(var r in realtorIds)
                {
                    if(app.RealtorID == r && !searchApps.Contains(app))
                    {
                        searchApps.Add(app);
                    }
                }
            }
            foreach(var s in searchApps)
            {
                var ins = await App.Database.GetInspectorAsync(s.InspectorID);
                var client = await App.Database.GetClientAsync(s.ClientID);
                string item = "#" + s.ID.ToString() + " - " + ins.Name + " - " + s.StartTime.ToShortDateString() + " - " + client.Name;
                searchDisplay.Add(item);
            }
            return searchDisplay;
        }
    }
}
