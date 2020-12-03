using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using System.Threading.Tasks;


namespace HomeInspectorSchedule
{
    public class Person
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        [NotNull]
        public string Name { get; set; }

        public async Task<int> SavePersonAsync(Person person)
        {
            return await App.Database.SavePersonAsync(person);
        }

        public async Task<int> DeletePersonAsync(Person person)
        {
            return await App.Database.DeletePersonAsync(person);
        }
    }
}
