using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace HomeInspectorSchedule    
{
    public class HomeInspectorScheduleDB
    {
        readonly SQLiteAsyncConnection database;

        public HomeInspectorScheduleDB(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTablesAsync<Address, DisplayLayout, Client, InspectionType, Inspector>().Wait();
            database.CreateTableAsync<Realtor>().Wait();
        }

        // Address 
        public Task<List<Address>> GetAddressesAsync()
        {
            return database.Table<Address>().ToListAsync();
        }
        public Task<Address> GetAddressAsync(int id)
        {
            return database.Table<Address>().Where(i => i.ID == id).FirstOrDefaultAsync();
        } 
        public Task<Address> GetAddressAsync(string streetAddress, string city, string zip)
        {
            return database.Table<Address>().Where(i => i.StreetAddress == streetAddress && i.City == city && i.Zip == zip).FirstOrDefaultAsync();
        }
        public Task<int> SaveAddressAsync(Address address)
        {
            if (address.ID != 0)
            {
                return database.UpdateAsync(address);
            }
            else
            {
                return database.InsertAsync(address);
            }
        }
        public Task<int> DeleteAddressAsync(Address address)
        {
            return database.DeleteAsync(address);
        }

        //Appointment
        public Task<List<DisplayLayout>> GetAppointmentsAsync()
        {
            return database.Table<DisplayLayout>().ToListAsync();
        }
        public Task<DisplayLayout> GetAppointmentAsync(int id)
        {
            return database.Table<DisplayLayout>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }
        public Task<int> SaveAppointmentAsync(DisplayLayout appointment)
        {
            if (appointment.ID != 0)
            {
                return database.UpdateAsync(appointment);
            }
            else
            {
                return database.InsertAsync(appointment);
            }
        }
        public Task<int> DeleteAppointmentAsync(DisplayLayout appointment)
        {
            return database.DeleteAsync(appointment);
        }

        //Client
        public Task<List<Client>> GetClientsAsync()
        {
            return database.Table<Client>().ToListAsync();
        }
        public Task<Client> GetClientAsync(int id)
        {
            return database.Table<Client>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }
        public Task<Client> GetClientAsync(string name)
        {
            return database.Table<Client>().Where(i => i.Name == name).FirstOrDefaultAsync();
        }
        public Task<int> SaveClientAsync(Client client)
        {
            if (client.ID != 0)
            {
                return database.UpdateAsync(client);
            }
            else
            {
                return database.InsertAsync(client);
            }
        }
        public Task<int> DeleteClientAsync(Client client)
        {
            return database.DeleteAsync(client);
        }

        //InspectionType
        public Task<List<InspectionType>> GetInspectionTypesAsync()
        {
            return database.Table<InspectionType>().ToListAsync();
        }
        public Task<InspectionType> GetInspectionTypeAsync(int id)
        {
            return database.Table<InspectionType>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }
        public Task<InspectionType> GetInspectionTypeAsync(string name)
        {
            return database.Table<InspectionType>().Where(i => i.Name == name).FirstOrDefaultAsync();
        }
        public Task<int> SaveInspectionTypeAsync(InspectionType inspectionType)
        {
            if (inspectionType.ID != 0)
            {
                return database.UpdateAsync(inspectionType);
            }
            else
            {
                return database.InsertAsync(inspectionType);
            }
        }
        public Task<int> DeleteInspectionTypeAsync(InspectionType inspectionType)
        {
            return database.DeleteAsync(inspectionType);
        } 
        
        //Inspector
        public Task<List<Inspector>> GetInspectorsAsync()
        {
            return database.Table<Inspector>().ToListAsync();
        }
        public Task<Inspector> GetInspectorAsync(int id)
        {
            return database.Table<Inspector>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }
        public Task<Inspector> GetInspectorAsync(string name)
        {
            return database.Table<Inspector>().Where(i => i.Name == name).FirstOrDefaultAsync();
        }
        public Task<int> SaveInspectorAsync(Inspector inspector)
        {
            if (inspector.ID != 0)
            {
                return database.UpdateAsync(inspector);
            }
            else
            {
                return database.InsertAsync(inspector);
            }
        }
        public Task<int> DeleteInspectorAsync(Inspector inspector)
        {
            return database.DeleteAsync(inspector);
        } 
        
        //Realtor
        public Task<List<Realtor>> GetRealtorsAsync()
        {
            return database.Table<Realtor>().ToListAsync();
        }
        public Task<Realtor> GetRealtorAsync(int id)
        {
            return database.Table<Realtor>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }
        public Task<Realtor> GetRealtorAsync(string name)
        {
            return database.Table<Realtor>().Where(i => i.Name == name).FirstOrDefaultAsync();
        }
        public Task<int> SaveRealtorAsync(Realtor realtor)
        {
            if (realtor.ID != 0)
            {
                return database.UpdateAsync(realtor);
            }
            else
            {
                return database.InsertAsync(realtor);
            }
        }
        public Task<int> DeleteRealtorAsync(Realtor realtor)
        {
            return database.DeleteAsync(realtor);
        }
    }
}
