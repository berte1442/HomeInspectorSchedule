using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using HomeInspectorSchedule.Pages;

namespace HomeInspectorSchedule
{
    public class Metrics
    {
        public async Task<List<string>> RealtorMetrics()
        {
            var appointments = await App.Database.GetAppointmentsAsync();
            List<int> realtorIds = new List<int>();

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
            List<string> realtorMetrics = new List<string>();
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


            return realtorMetrics;
        }
    }
}
