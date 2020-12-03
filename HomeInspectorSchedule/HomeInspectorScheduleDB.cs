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
            database.CreateTablesAsync<Address, Appointment, Client, InspectionType, Inspector>().Wait();
            database.CreateTablesAsync<Realtor, Report>().Wait();
        }

        //Address
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
        public Task<List<Appointment>> GetAppointmentsAsync()
        {
            return database.Table<Appointment>().ToListAsync();
        }
        public Task<Appointment> GetAppointmentAsync(int id)
        {
            return database.Table<Appointment>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }
        public Task<int> SaveAppointmentAsync(Appointment appointment)
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
        public Task<int> DeleteAppointmentAsync(Appointment appointment)
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
        //public Task<int> SaveClientAsync(Client client)
        //{
        //    if (client.ID != 0)
        //    {
        //        return database.UpdateAsync(client);
        //    }
        //    else
        //    {
        //        return database.InsertAsync(client);
        //    }
        //}
        //public Task<int> DeleteClientAsync(Client client)
        //{
        //    return database.DeleteAsync(client);
        //}

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

        
        //Person - inspector / realtor / client
        public Task<int> SavePersonAsync(Person person)
        {
            if (person.ID != 0)
            {
                return database.UpdateAsync(person);
            }
            else
            {
                return database.InsertAsync(person);
            }
        }
        public Task<int> DeletePersonAsync(Person person)
        {
            return database.DeleteAsync(person);
        }

        //reports
        public Task<int> SaveReportAsync(Report report)
        {
            if (report.ID != 0)
            {
                return database.UpdateAsync(report);
            }
            else
            {
                return database.InsertAsync(report);
            }
        }
        public Task<int> DeleteReportAsync(Report report)
        {
            return database.DeleteAsync(report);
        }
        public Task<List<Report>> GetReportsAsync()
        {
            return database.Table<Report>().ToListAsync();
        }
        public Task<Report> GetReportAsync(int id)
        {
            return database.Table<Report>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }
        public Task<Report> GetReportAsync(string fileName)
        {
            return database.Table<Report>().Where(i => i.FileName == fileName).FirstOrDefaultAsync();
        }
    }
}
