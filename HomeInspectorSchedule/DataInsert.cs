using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HomeInspectorSchedule
{
    class DataInsert
    {
        public static async Task DatabaseCheck()
        {
            var inspectors = await App.Database.GetInspectorsAsync();
            if (inspectors.Count == 0)
            {
                await Populate();
            }
        }
        public static async Task Populate()
        {
            //inspectors

            Inspector robert = new Inspector 
            { 
                Name = "Robert Haines", 
                UserName = "RobertHaines", 
                Password = "Admin", 
                Admin = true, 
                InspectorColor = "blue"                
            };
            await App.Database.SaveInspectorAsync(robert);
            
            Inspector ted = new Inspector 
            { 
                Name = "Ted Amberson", 
                UserName = "TedAmberson", 
                Password = "inspector", 
                Admin = false, 
                InspectorColor = "yellow"
            };
            await App.Database.SaveInspectorAsync(ted);

            Inspector tim = new Inspector
            {
                Name = "Tim Stroud",
                UserName = "TimStroud",
                Password = "inspector",
                Admin = false,
                InspectorColor = "purple"
            };
            await App.Database.SaveInspectorAsync(tim);

            Inspector bill = new Inspector
            {
                Name = "Bill Hicks",
                UserName = "BillHicks",
                Password = "inspector",
                Admin = false,
                InspectorColor = "green"
            };
            await App.Database.SaveInspectorAsync(bill);

            Inspector jay = new Inspector
            {
                Name = "Jay Franklin",
                UserName = "JayFranklin",
                Password = "inspector",
                Admin = false,
                InspectorColor = "orange"
            };
            await App.Database.SaveInspectorAsync(jay);

            //realtors

            Realtor susy = new Realtor
            {
                Name = "Susy Stricklin",
                Phone = "(555)555-5555",
                Email = "susystricklinsells@remax.com"
            };
            await App.Database.SaveRealtorAsync(susy);
            
            Realtor chris = new Realtor
            {
                Name = "Chris Harris",
                Phone = "(555)555-5556",
                Email = "chris_top_broker@coldwellbanker.com"
            };
            await App.Database.SaveRealtorAsync(chris);

            Realtor jan = new Realtor
            {
                Name = "Jan Sparks",
                Phone = "(555)555-5557",
                Email = "JanetSparks@janetsparks.com"
            };
            await App.Database.SaveRealtorAsync(jan);

            Realtor brandon = new Realtor
            {
                Name = "Brandon Wright",
                Phone = "(555)555-5558",
                Email = "BWrightBuyRight@yahoo.com"
            };
            await App.Database.SaveRealtorAsync(brandon);

            Realtor sam = new Realtor
            {
                Name = "Sam Burks",
                Phone = "(555)555-5559",
                Email = "samanthaburks@kellerwilliams.com"
            };
            await App.Database.SaveRealtorAsync(sam);

            // clients

            Client jim = new Client
            {
                Name = "Jim Russell",
                Phone = "(555)666-6666",
                Email = "Jim_Russell@NASA.com"
            };
            await App.Database.SaveClientAsync(jim);
            
            Client liz = new Client
            {
                Name = "Liz Scott",
                Phone = "(555)666-6667",
                Email = "actingupwithliz@livestudio.com"
            };
            await App.Database.SaveClientAsync(liz);

            Client mary = new Client
            {
                Name = "Mary Blanch",
                Phone = "(555)666-6668",
                Email = "BlanchMS@gmail.com"
            };
            await App.Database.SaveClientAsync(mary);
            
            Client tom = new Client
            {
                Name = "Tom Hays",
                Phone = "(555)666-6669",
                Email = "tomhays1956@hotmail.com"
            };
            await App.Database.SaveClientAsync(tom);
            
            Client lynn = new Client
            {
                Name = "Lynn Larkin",
                Phone = "(555)666-6670",
                Email = "lynnlarkin@gmail.com"
            };
            await App.Database.SaveClientAsync(lynn);

            // inspection types

            InspectionType residential = new InspectionType
            {
                Name = "Residential",
                Price = 389.99,
                Description = "Standard Residential Home Inspection",
                DurationHours = 3
            };
            await App.Database.SaveInspectionTypeAsync(residential);
                
            InspectionType commercial = new InspectionType
            {
                Name = "Commercial",
                Price = 469.99,
                Description = "Commercial Building Inspection",
                DurationHours = 5
            };
            await App.Database.SaveInspectionTypeAsync(commercial);

            InspectionType radon = new InspectionType
            {
                Name = "Radon",
                Price = 125.00,
                Description = "Radon Testing",
                DurationHours = 0
            };
            await App.Database.SaveInspectionTypeAsync(radon);

            InspectionType mold = new InspectionType
            {
                Name = "Mold",
                Price = 395.99,
                Description = "Mold swab and indoor air quality testing",
                DurationHours = 1
            };
            await App.Database.SaveInspectionTypeAsync(mold);

            // addresses

            Address huntsville = new Address
            {
                StreetAddress = "123 Fake St.",
                City = "Huntsville",
                State = "Alabama",
                Zip = "35803"
            };
            await App.Database.SaveAddressAsync(huntsville);
            
            Address madison = new Address
            {
                StreetAddress = "456 Not Real Ave.",
                City = "Madison",
                State = "Alabama",
                Zip = "35758"
            };
            await App.Database.SaveAddressAsync(madison);

            Address arab = new Address
            {
                StreetAddress = "789 Rounddaway Ln.",
                City = "Arab",
                State = "Alabama",
                Zip = "35011"
            };
            await App.Database.SaveAddressAsync(arab);
            
            Address guntersville = new Address
            {
                StreetAddress = "987 Gunthrie Dr.",
                City = "Guntersville",
                State = "Alabama",
                Zip = "35018"
            };
            await App.Database.SaveAddressAsync(guntersville);
            
            Address ocr = new Address
            {
                StreetAddress = "654 Rich Folk Place",
                City = "Owens Cross Roads",
                State = "Alabama",
                Zip = "35609"
            };
            await App.Database.SaveAddressAsync(ocr);

            // appointments

            DisplayLayout robMonMorn = new DisplayLayout
            {
                InspectorID = 1,
                ClientID = 1,
                RealtorID = 1,
                InspectionTypeIDs = "1",
                PriceTotal = residential.Price,
                StartTime = DateTime.Parse("12/14/2020 8:00:00 AM"),
                Duration = residential.DurationHours,
                Paid = false,
                AddressID = 1,
                Notes = "Client will pay by check on site"
            };
            await App.Database.SaveAppointmentAsync(robMonMorn);
            
            DisplayLayout robMonAft = new DisplayLayout
            {
                InspectorID = 1,
                ClientID = 2,
                RealtorID = 2,
                InspectionTypeIDs = "1, 3",
                PriceTotal = residential.Price + radon.Price,
                StartTime = DateTime.Parse("12/14/2020 1:00:00 PM"),
                Duration = residential.DurationHours + radon.DurationHours,
                Paid = true,
                AddressID = 2,
                Notes = "Client paid via credit card"
            };
            await App.Database.SaveAppointmentAsync(robMonAft);

            DisplayLayout tedMonAft = new DisplayLayout
            {
                InspectorID = 2,
                ClientID = 3,
                RealtorID = 3,
                InspectionTypeIDs = "2",
                PriceTotal = commercial.Price,
                StartTime = DateTime.Parse("12/14/2020 1:00:00 PM"),
                Duration = commercial.DurationHours,
                Paid = false,
                AddressID = 3,
                Notes = "Large warehouse building.  Signs of roof leaks."
            };
            await App.Database.SaveAppointmentAsync(tedMonAft);

            DisplayLayout timMonMorn = new DisplayLayout
            {
                InspectorID = 3,
                ClientID = 4,
                RealtorID = 4,
                InspectionTypeIDs = "1",
                PriceTotal = residential.Price,
                StartTime = DateTime.Parse("12/14/2020 8:00:00 AM"),
                Duration = residential.DurationHours,
                Paid = false,
                AddressID = 4,
                Notes = "Client booked inspection before offer was accepted.",
                Canceled = true
                
            };
            await App.Database.SaveAppointmentAsync(timMonMorn);

            DisplayLayout jayMonAft = new DisplayLayout
            {
                InspectorID = 5,
                ClientID = 5,
                RealtorID = 5,
                InspectionTypeIDs = "1, 4",
                PriceTotal = residential.Price,
                StartTime = DateTime.Parse("12/14/2020 1:00:00 PM"),
                Duration = residential.DurationHours + mold.DurationHours,
                Paid = false,
                AddressID = 5,
                Notes = "Client requested mold testing with inspection.",
            };
            await App.Database.SaveAppointmentAsync(jayMonAft);
        }
    }
}
