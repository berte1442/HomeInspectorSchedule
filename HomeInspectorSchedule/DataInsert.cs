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
            //await App.Database.SaveInspectorAsync(robert);
            await robert.SavePersonAsync(robert);
            
            Inspector ted = new Inspector 
            { 
                Name = "Ted Amberson", 
                UserName = "TedAmberson", 
                Password = "inspector", 
                Admin = false, 
                InspectorColor = "yellow"
            };
            //await App.Database.SaveInspectorAsync(ted);
            await ted.SavePersonAsync(ted);

            Inspector tim = new Inspector
            {
                Name = "Tim Stroud",
                UserName = "TimStroud",
                Password = "inspector",
                Admin = false,
                InspectorColor = "purple"
            };
            //await App.Database.SaveInspectorAsync(tim);
            await tim.SavePersonAsync(tim);

            Inspector bill = new Inspector
            {
                Name = "Bill Hicks",
                UserName = "BillHicks",
                Password = "inspector",
                Admin = false,
                InspectorColor = "green"
            };
            //await App.Database.SaveInspectorAsync(bill);
            await bill.SavePersonAsync(bill);

            Inspector jay = new Inspector
            {
                Name = "Jay Franklin",
                UserName = "JayFranklin",
                Password = "inspector",
                Admin = false,
                InspectorColor = "orange"
            };
            //await App.Database.SaveInspectorAsync(jay);
            jay.SavePersonAsync(jay);

            //realtors

            Realtor susy = new Realtor
            {
                Name = "Susy Stricklin",
                Phone = "(555)555-5555",
                Email = "susystricklinsells@remax.com"
            };
            //await App.Database.SaveRealtorAsync(susy);
            susy.SavePersonAsync(susy);
            
            Realtor chris = new Realtor
            {
                Name = "Chris Harris",
                Phone = "(555)555-5556",
                Email = "chris_top_broker@coldwellbanker.com"
            };
            //await App.Database.SaveRealtorAsync(chris);
            await chris.SavePersonAsync(chris);

            Realtor jan = new Realtor
            {
                Name = "Jan Sparks",
                Phone = "(555)555-5557",
                Email = "JanetSparks@janetsparks.com"
            };
            //await App.Database.SaveRealtorAsync(jan);
            await jan.SavePersonAsync(jan);

            Realtor brandon = new Realtor
            {
                Name = "Brandon Wright",
                Phone = "(555)555-5558",
                Email = "BWrightBuyRight@yahoo.com"
            };
            //await App.Database.SaveRealtorAsync(brandon);
            await brandon.SavePersonAsync(brandon);

            Realtor sam = new Realtor
            {
                Name = "Sam Burks",
                Phone = "(555)555-5559",
                Email = "samanthaburks@kellerwilliams.com"
            };
            //await App.Database.SaveRealtorAsync(sam);
            await sam.SavePersonAsync(sam);

            // clients

            Client jim = new Client
            {
                Name = "Jim Russell",
                Phone = "(555)666-6666",
                Email = "Jim_Russell@NASA.com"
            };
            //await App.Database.SaveClientAsync(jim);
            await jim.SavePersonAsync(jim);

            Client liz = new Client
            {
                Name = "Liz Scott",
                Phone = "(555)666-6667",
                Email = "actingupwithliz@livestudio.com"
            };
            //await App.Database.SaveClientAsync(liz);
            await liz.SavePersonAsync(liz);
            Client mary = new Client
            {
                Name = "Mary Blanch",
                Phone = "(555)666-6668",
                Email = "BlanchMS@gmail.com"
            };
            //await App.Database.SaveClientAsync(mary);
            await mary.SavePersonAsync(mary);
            Client tom = new Client
            {
                Name = "Tom Hays",
                Phone = "(555)666-6669",
                Email = "tomhays1956@hotmail.com"
            };
            //await App.Database.SaveClientAsync(tom);
            await tom.SavePersonAsync(tom);
            Client lynn = new Client
            {
                Name = "Lynn Larkin",
                Phone = "(555)666-6670",
                Email = "lynnlarkin@gmail.com"
            };
            //await App.Database.SaveClientAsync(lynn);
            await lynn.SavePersonAsync(lynn);

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

            Appointment robMonMorn = new Appointment
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
                Notes = "Client will pay by check on site",
                Approved = true
            };
            await App.Database.SaveAppointmentAsync(robMonMorn);
            
            Appointment robMonAft = new Appointment
            {
                InspectorID = 1,
                ClientID = 2,
                RealtorID = 2,
                InspectionTypeIDs = "1, 3",
                PriceTotal = residential.Price + radon.Price,
                StartTime = DateTime.Parse("12/29/2020 1:00:00 PM"),
                Duration = residential.DurationHours + radon.DurationHours,
                Paid = true,
                AddressID = 2,
                Notes = "Client paid via credit card",
                Approved = true
            };
            await App.Database.SaveAppointmentAsync(robMonAft);

            Appointment tedMonAft = new Appointment
            {
                InspectorID = 2,
                ClientID = 3,
                RealtorID = 3,
                InspectionTypeIDs = "2",
                PriceTotal = commercial.Price,
                StartTime = DateTime.Parse("01/02/2021 1:00:00 PM"),
                Duration = commercial.DurationHours,
                Paid = false,
                AddressID = 3,
                Notes = "Large warehouse building.  Signs of roof leaks.",
                Approved = true
            };
            await App.Database.SaveAppointmentAsync(tedMonAft);

            Appointment timMonMorn = new Appointment
            {
                InspectorID = 3,
                ClientID = 4,
                RealtorID = 4,
                InspectionTypeIDs = "1",
                PriceTotal = residential.Price,
                StartTime = DateTime.Parse("01/12/2021 8:00:00 AM"),
                Duration = residential.DurationHours,
                Paid = false,
                AddressID = 4,
                Notes = "Client booked inspection before offer was accepted.",
                Canceled = true
                
            };
            await App.Database.SaveAppointmentAsync(timMonMorn);

            Appointment jayMonAft = new Appointment
            {
                InspectorID = 5,
                ClientID = 5,
                RealtorID = 5,
                InspectionTypeIDs = "1, 4",
                PriceTotal = residential.Price + mold.Price,
                StartTime = DateTime.Parse("01/27/2021 1:00:00 PM"),
                Duration = residential.DurationHours + mold.DurationHours,
                Paid = false,
                AddressID = 5,
                Notes = "Client requested mold testing with inspection.",
                Approved = true
            };
            await App.Database.SaveAppointmentAsync(jayMonAft); 
            
            /// begin editing below here

            Appointment new1 = new Appointment
            {
                InspectorID = 1,
                ClientID = 1,
                RealtorID = 1,
                InspectionTypeIDs = "2, 3",
                PriceTotal = commercial.Price + radon.Price,
                StartTime = DateTime.Parse("02/8/2021 8:00:00 AM"),
                Duration = commercial.DurationHours + radon.DurationHours,
                Paid = true,
                AddressID = 1,
                Notes = "Commercial building with radon testing.",
                Approved = true
            };
            await App.Database.SaveAppointmentAsync(new1);

            Appointment new2 = new Appointment
            {
                InspectorID = 2,
                ClientID = 2,
                RealtorID = 2,
                InspectionTypeIDs = "1",
                PriceTotal = residential.Price,
                StartTime = DateTime.Parse("02/8/2021 12:00:00 PM"),
                Duration = residential.DurationHours,
                Paid = false,
                AddressID = 2,
                Notes = "Client will attend at end of inspection.",
                Approved = false
            };
            await App.Database.SaveAppointmentAsync(new2);

            Appointment new3 = new Appointment
            {
                InspectorID = 2,
                ClientID = 3,
                RealtorID = 5,
                InspectionTypeIDs = "1, 3",
                PriceTotal = residential.Price + radon.Price,
                StartTime = DateTime.Parse("02/28/2021 8:00:00 AM"),
                Duration = residential.DurationHours + radon.DurationHours,
                Paid = true,
                AddressID = 3,
                Notes = "Client mentioned stains in the master bedroom ceiling.",
                Approved = true
            };
            await App.Database.SaveAppointmentAsync(new3);

            Appointment new4 = new Appointment
            {
                InspectorID = 3,
                ClientID = 3,
                RealtorID = 4,
                InspectionTypeIDs = "1",
                PriceTotal = residential.Price,
                StartTime = DateTime.Parse("03/07/2021 2:00:00 PM"),
                Duration = residential.DurationHours,
                Paid = false,
                AddressID = 4,
                Notes = "Client may add radon testing.",
                Approved = false
            };
            await App.Database.SaveAppointmentAsync(new4);

            Appointment new5 = new Appointment
            {
                InspectorID = 5,
                ClientID = 4,
                RealtorID = 5,
                InspectionTypeIDs = "1",
                PriceTotal = residential.Price,
                StartTime = DateTime.Parse("03/09/2020 8:00:00 AM"),
                Duration = residential.DurationHours,
                Paid = false,
                AddressID = 5,
                Notes = "Client is moving from out of state and will not attend inspection.",
                Approved = true
            };
            await App.Database.SaveAppointmentAsync(new5);

            Appointment new6 = new Appointment
            {
                InspectorID = 1,
                ClientID = 1,
                RealtorID = 2,
                InspectionTypeIDs = "1",
                PriceTotal = residential.Price,
                StartTime = DateTime.Parse("03/10/2021 8:00:00 AM"),
                Duration = residential.DurationHours,
                Paid = true,
                AddressID = 1,
                Notes = "Client specifically requested Robert as inspector.",
                Approved = true
            };
            await App.Database.SaveAppointmentAsync(new6);

            Appointment new7 = new Appointment
            {
                InspectorID = 2,
                ClientID = 2,
                RealtorID = 3,
                InspectionTypeIDs = "3, 4",
                PriceTotal = radon.Price + mold.Price,
                StartTime = DateTime.Parse("03/10/2021 1:00:00 PM"),
                Duration = radon.DurationHours + mold.DurationHours,
                Paid = true,
                AddressID = 2,
                Notes = "Radon and mold testing only.",
                Canceled = true
            };
            await App.Database.SaveAppointmentAsync(new7);

            Appointment new8 = new Appointment
            {
                InspectorID = 3,
                ClientID = 3,
                RealtorID = 3,
                InspectionTypeIDs = "1, 4",
                PriceTotal = residential.Price + mold.Price,
                StartTime = DateTime.Parse("03/17/2021 1:00:00 PM"),
                Duration = residential.DurationHours + mold.DurationHours,
                Paid = true,
                AddressID = 3,
                Notes = "Client is an out of sate realtor.",
                Approved = false
            };
            await App.Database.SaveAppointmentAsync(new8);

            Appointment new9 = new Appointment
            {
                InspectorID = 4,
                ClientID = 4,
                RealtorID = 4,
                InspectionTypeIDs = "1",
                PriceTotal = residential.Price,
                StartTime = DateTime.Parse("03/19/2021 9:00:00 AM"),
                Duration = residential.DurationHours,
                Paid = false,
                AddressID = 4,
                Notes = "1500 sqft property with detached garage.",
                Approved = true
            };
            await App.Database.SaveAppointmentAsync(new9);

            Appointment new10 = new Appointment
            {
                InspectorID = 4,
                ClientID = 5,
                RealtorID = 1,
                InspectionTypeIDs = "1",
                PriceTotal = residential.Price,
                StartTime = DateTime.Parse("03/21/2021 1:00:00 PM"),
                Duration = residential.DurationHours,
                Paid = false,
                AddressID = 5,
                Notes = "New construction.",
                Approved = false
            };
            await App.Database.SaveAppointmentAsync(new10);

            Appointment new11 = new Appointment
            {
                InspectorID = 5,
                ClientID = 1,
                RealtorID = 5,
                InspectionTypeIDs = "1, 3",
                PriceTotal = residential.Price + radon.Price,
                StartTime = DateTime.Parse("03/22/2021 1:00:00 PM"),
                Duration = residential.DurationHours + radon.DurationHours,
                Paid = false,
                AddressID = 1,
                Notes = "1200 sqft home with radon testing.",
                Approved = true
            };
            await App.Database.SaveAppointmentAsync(new11);

            Appointment new12 = new Appointment
            {
                InspectorID = 1,
                ClientID = 2,
                RealtorID = 2,
                InspectionTypeIDs = "1",
                PriceTotal = residential.Price,
                StartTime = DateTime.Parse("03/23/2021 10:00:00 AM"),
                Duration = residential.DurationHours,
                Paid = true,
                AddressID = 2,
                Notes = "Client is a property investor and only needs structure, electrical, plumbing, HVAC and roof evaluated.",
                Approved = true
            };
            await App.Database.SaveAppointmentAsync(new12);

            Appointment new13 = new Appointment
            {
                InspectorID = 1,
                ClientID = 3,
                RealtorID = 0,
                InspectionTypeIDs = "1",
                PriceTotal = radon.Price,
                StartTime = DateTime.Parse("04/01/2021 2:00:00 PM"),
                Duration = radon.DurationHours,
                Paid = true,
                AddressID = 3,
                Notes = "Stand alone radon test.",
                Approved = true
            };
            await App.Database.SaveAppointmentAsync(new13);

            Appointment new14 = new Appointment
            {
                InspectorID = 2,
                ClientID = 4,
                RealtorID = 0,
                InspectionTypeIDs = "1",
                PriceTotal = residential.Price,
                StartTime = DateTime.Parse("04/03/2021 8:00:00 AM"),
                Duration = residential.DurationHours,
                Paid = false,
                AddressID = 4,
                Notes = "For sale by owner FSBO.",
                Approved = false
            };
            await App.Database.SaveAppointmentAsync(new14);

            Appointment new15 = new Appointment
            {
                InspectorID = 3,
                ClientID = 5,
                RealtorID = 1,
                InspectionTypeIDs = "2",
                PriceTotal = commercial.Price,
                StartTime = DateTime.Parse("05/07/2021 11:00:00 AM"),
                Duration = commercial.DurationHours,
                Paid = true,
                AddressID = 5,
                Notes = "Building is an old dentist office which will be converted into a counceling center.",
                Approved = true
            };
            await App.Database.SaveAppointmentAsync(new15);

            Appointment new16 = new Appointment
            {
                InspectorID = 4,
                ClientID = 1,
                RealtorID = 0,
                InspectionTypeIDs = "1",
                PriceTotal = residential.Price,
                StartTime = DateTime.Parse("05/21/2021 1:00:00 PM"),
                Duration = residential.DurationHours,
                Paid = false,
                AddressID = 1,
                Notes = "FSBO.  Client stated HVAC and Roof appear to be beyond typical life expectancy.",
                Approved = true
            };
            await App.Database.SaveAppointmentAsync(new16);

        }
    }
}
