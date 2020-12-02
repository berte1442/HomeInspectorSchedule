using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace HomeInspectorSchedule
{
    public class Address
    {
        public Address()
        {
            State = "Alabama";
        }
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        [NotNull]
        public string StreetAddress { get; set; }
        [NotNull]
        public string City { get; set; }
        [NotNull]
        public string State { get; set; }
        [NotNull]
        public string Zip { get; set; }

    }
}
