using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HomeInspectorSchedule.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HomeInspectorSchedule
{
    public class Metrics
    {
        List<string> realtorMetrics = new List<string>();
        List<string> inspectorMetrics = new List<string>();

        // change this to a param and create the ability to return realtorMetrics for any year 
        static public int year = DateTime.Today.Year;

        public async Task<string[,]> RealtorMetrics()
        {
            var appointments = await App.Database.GetAppointmentsAsync();
            List<int> realtorIds = new List<int>();

            foreach (var a in appointments)
            {
                if (a.StartTime.Year == year)
                {
                    realtorIds.Add(a.RealtorID);
                }
            }

            //organizes list in descending order
            realtorIds = OrganizeList(realtorIds);

            //creates a list of strings containing realtor names and referral count
            await NamesAndCount(realtorIds, "realtor");

            // organizes the list by most referrals
            realtorMetrics = await TopProducers(realtorMetrics);

            // divides names and referral counts into separate list;
            var nameAndCount = DivideList(realtorMetrics);

            return nameAndCount;
        }
        public List<string> RealtorReportList()
        {
            return realtorMetrics;
        }

        public async Task<double> PriceTotal(int id, string type, List<Appointment> appointments)
        {
            double priceTotal = 0;
            foreach(var a in appointments)
            {
                if (type == "realtor" && a.RealtorID == id)
                {
                    priceTotal += a.PriceTotal;
                }
                if(type == "inspector" && a.InspectorID == id)
                {
                    priceTotal += a.PriceTotal;
                }
            }

            return priceTotal;
        }

        public async Task<List<Appointment>> WeekAppointments()
        {
            inspectorMetrics.Clear();
            var appointments = await App.Database.GetAppointmentsAsync();
            var today = DateTime.Today;
            List<Appointment> weekAppointments = new List<Appointment>();
            foreach(var a in appointments)
            {
                if(a.StartTime.Date <= today && a.StartTime > today.AddDays(-7))
                {
                    weekAppointments.Add(a);
                }
            }         
            return weekAppointments;
        }

        public async Task<List<Appointment>> MonthAppointments()
        {
            inspectorMetrics.Clear();
            var appointments = await App.Database.GetAppointmentsAsync();
            var thisMonth = DateTime.Today.Month;
            List<Appointment> monthAppointments = new List<Appointment>();
            foreach (var a in appointments)
            {
                if (a.StartTime.Month == thisMonth)
                {
                    monthAppointments.Add(a);
                }
            }
            return monthAppointments;
        } 

        public async Task<List<Appointment>> YearAppointments()
        {
            inspectorMetrics.Clear();
            var appointments = await App.Database.GetAppointmentsAsync();
            var thisYear = DateTime.Today.Year;
            List<Appointment> yearAppointments = new List<Appointment>();
            foreach (var a in appointments)
            {
                if (a.StartTime.Year == thisYear)
                {
                    yearAppointments.Add(a);
                }
            }
            return yearAppointments;
        }

        public async Task<string[,]> InspectorMetrics(List<Appointment> appointments)
        {
            List<int> inspectorIds = new List<int>();
            foreach(var a in appointments)
            {
                inspectorIds.Add(a.InspectorID);
            }

            //organizes list in descending order
            inspectorIds = OrganizeList(inspectorIds);

            //creates a list of strings containing inspector names and inspection count
            await NamesAndCount(inspectorIds, "inspector");

            // organizes list to put top inspectors at front of list
            inspectorMetrics = await TopProducers(inspectorMetrics);

            // divides names and inspection counts into separate list;
            var nameAndCount = DivideList(inspectorMetrics);
            return nameAndCount;
        }

        public List<string> InspectorReportList()
        {
            return inspectorMetrics;
        }

        public List<int> OrganizeList(List<int> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                for (int n = 0; n < list.Count; n++)
                {
                    if (list[i] > list[n])
                    {
                        var temp = list[i];
                        list[i] = list[n];
                        list[n] = temp;
                    }
                }
            }
            return list;
        }

        public string[,] DivideList(List<string> list)
        {
            var rows = list.Count;
            string name;
            string count;
            string[,] nameAndCount = new string[rows, 2];
            int x = 0;
            foreach (var r in list)
            {
                int index = r.IndexOf(" x ");
                name = r.Substring(0, index);

                int length = r.Length;
                int index2 = r.LastIndexOf(" ");
                int index3 = length - index2;
                count = r.Substring(index2 + 1, index3 - 2);

                nameAndCount[x, 0] = name;
                nameAndCount[x, 1] = count;
                x++;
            }
            return nameAndCount;
        }

        public async Task NamesAndCount(List<int> list, string type)
        {
            for (int i = 0; i < list.Count; i++)
            {
                int counter = 0;
                for (int n = 0; n < list.Count; n++)
                {
                    if (list[i] == list[n])
                    {
                        counter++;
                    }
                    else
                    {
                        if (counter > 0)
                        {
                            await MetricsBuilder(type, counter, list[i]);
                        }
                    }

                    if(n == list.Count - 1)
                    {
                        await MetricsBuilder(type, counter, list[i]);
                    }
                }
            }
        }
        public async Task MetricsBuilder(string type, int counter, int index)
        {
            if (type == "realtor")
            {
                var realtor = await App.Database.GetRealtorAsync(index);
                string report = realtor.Name + " x " + counter + "\n";
                if (!realtorMetrics.Contains(report))
                {
                    realtorMetrics.Add(report);
                }
            }
            else if (type == "inspector")
            {
                var inspector = await App.Database.GetInspectorAsync(index);
                string report = inspector.Name + " x " + counter + "\n";
                if (!inspectorMetrics.Contains(report))
                {
                    inspectorMetrics.Add(report);
                }
            }
        }
        public async Task<List<string>> TopProducers(List<string> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                for (int n = 0; n < list.Count; n++)
                {
                    int length = list[i].Length;
                    int index = list[i].LastIndexOf(" ");
                    index++;
                    int index2 = list[i].LastIndexOf("\n");
                    index2 = length - index2;
                    string getCount = list[i].Substring(index, index2);

                    int length2 = list[n].Length;
                    int index3 = list[n].LastIndexOf(" ");
                    index3++;
                    int index4 = list[n].LastIndexOf("\n");
                    index4 = length2 - index4;
                    string getCount2 = list[n].Substring(index3, index4);

                    if (int.Parse(getCount) > int.Parse(getCount2))
                    {
                        var temp = list[i];
                        list[i] = list[n];
                        list[n] = temp;
                    }
                }
            }
            return list;
        }

        public string[,,] OrganizeArray(string[,,] array, int count)
        {
            for(int i = 0; i < count; i++)
            {
                var subStr = array[i, 1, 1].Substring(1, array[i, 1, 1].Length - 1);
                int inspectionCount = Convert.ToInt32(array[i, 1, 0]);
                for (int n = 0; n < count; n++)
                {
                    var subStr2 = array[n, 1, 1].Substring(1, array[n, 1, 1].Length - 1);
                    int inspectionCount2 = Convert.ToInt32(array[n, 1, 0]);
                    if (inspectionCount == inspectionCount2 && Convert.ToDouble(subStr) > Convert.ToDouble(subStr2))
                    {
                        var temp1 = array[i, 0, 0];
                        var temp2 = array[i, 1, 0];
                        var temp3 = array[i, 1, 1];

                        array[i, 0, 0] = array[n, 0, 0];
                        array[i, 1, 0] = array[n, 1, 0];
                        array[i, 1, 1] = array[n, 1, 1]; 
                        
                        array[n, 0, 0] = temp1;
                        array[n, 1, 0] = temp2;
                        array[n, 1, 1] = temp3;
                    }
                }
            }
            return array;
        }
    }
}
