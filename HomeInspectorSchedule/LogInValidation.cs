using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using HomeInspectorSchedule.Pages;
using System.Threading.Tasks;
namespace HomeInspectorSchedule
{
    public class LogInValidation
    {
        static public async Task<Inspector> ValidateLogin(string username, string password)
        {
            Inspector user = new Inspector();
            var users = await App.Database.GetInspectorsAsync();
            foreach(var u in users)
            {
                if(u.UserName == username && u.Password == password)
                {
                    user = u;
                    break;
                }
            }
            return user;
        }
    }
}
