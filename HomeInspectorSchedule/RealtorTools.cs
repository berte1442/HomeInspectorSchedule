using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Threading.Tasks;
using HomeInspectorSchedule.Pages;


namespace HomeInspectorSchedule
{
    public class RealtorTools
    {
        static public async Task<Realtor> SameName(Realtor realtor)
        {
            var realtors = await App.Database.GetRealtorsAsync();
            int nameCount = 1;

            bool result = realtor.Name.Any(x => char.IsNumber(x));
            while (result)
            {
                realtor.Name = realtor.Name.Substring(1, realtor.Name.Length - 1);
                result = realtor.Name.Any(x => char.IsNumber(x));
            }
            foreach (var r in realtors)
            {
                if (r.Name.Contains(realtor.Name.TrimStart()))
                {
                    nameCount++;
                }
            }

            realtor.Name = nameCount + " " + realtor.Name.TrimStart();
            
            return realtor;
        }
    }
}
