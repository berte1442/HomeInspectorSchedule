using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeInspectorSchedule
{
    public class Metrics
    {
        List<string> realtorMetrics = new List<string>();

        public async Task<string[,]> RealtorMetrics()
        {
            var appointments = await App.Database.GetAppointmentsAsync();
            List<int> realtorIds = new List<int>();

            //List<List<string>> nameAndCount = new List<List<string>>();
             
            //List<string> names = new List<string>();
            //List<string> count = new List<string>();

            foreach (var a in appointments)
            {
                if (a.StartTime.Year == 2020)
                {
                    realtorIds.Add(a.RealtorID);
                }
            }

            //organizes list in descending order
            for (int i = 0; i < realtorIds.Count; i++)
            {
                for (int n = 0; n < realtorIds.Count; n++)
                {
                    if (realtorIds[i] > realtorIds[n])
                    {
                        var temp = realtorIds[i];
                        realtorIds[i] = realtorIds[n];
                        realtorIds[n] = temp;
                    }
                }
            }
            //creates a list of strings containing realtor names and referral count
            for(int i = 0; i < realtorIds.Count; i++)
            {
                int counter = 0;
                for (int n = 0; n < realtorIds.Count; n++)
                {
                    if(realtorIds[i] == realtorIds[n])
                    {
                        counter++;
                    }
                    else
                    {
                        if(counter > 0)
                        {
                            var realtor = await App.Database.GetRealtorAsync(realtorIds[i]);
                            string report = realtor.Name + " x " + counter + "\n";
                            if (!realtorMetrics.Contains(report))
                            {
                                realtorMetrics.Add(report);
                            }
                        }
                    }
                }
            }
            // organizes list to put top referrals at front of list
            for(int i = 0; i < realtorMetrics.Count; i++)
            {
                for(int n = 0; n < realtorMetrics.Count; n++)
                {
                    int length = realtorMetrics[i].Length;
                    int index = realtorMetrics[i].LastIndexOf(" ");
                    index++;
                    int index2 = realtorMetrics[i].LastIndexOf("\n");
                    index2 = length - index2;
                    string getCount = realtorMetrics[i].Substring(index, index2);

                    int length2 = realtorMetrics[n].Length;
                    int index3 = realtorMetrics[n].LastIndexOf(" ");
                    index3++;
                    int index4 = realtorMetrics[n].LastIndexOf("\n");
                    index4 = length2 - index4;                   
                    string getCount2 = realtorMetrics[n].Substring(index3, index4);

                    if(int.Parse(getCount) > int.Parse(getCount2))
                    {
                        var temp = realtorMetrics[i];
                        realtorMetrics[i] = realtorMetrics[n];
                        realtorMetrics[n] = temp;
                    }
                }
            }
            // divides names and referral counts into separate list;
            var rows = realtorMetrics.Count;
            string name;
            string count;
            string[,] nameAndCount = new string[rows, 2];
            int x = 0;
            foreach (var r in realtorMetrics)
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
        public List<string> RealtorReportList()
        {
            return realtorMetrics;
        }
    }
}
