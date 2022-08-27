using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wedding_Dress.Models
{
    public class Dress
    {
        public IEnumerable<Category> List { get; set; }
        public int ID { get; set; }

        public string Name { get; set; }
        public string City { get; set; }

        public string UserName { get; set; }
        public string User_Address { get; set; }
        public string User_Contact { get; set; }
        public int Price { get; set; }
        public string Status { get; set; }
        public byte[] Image { get; set; }
        public string type { get; set; }
        public string Gender { get; set; }
        public string Description { get; set; }

        public int Category { get; set; }
        public int qty { get; set; }
        public string time_period { get; set; }
    }
    public enum period { One_Days, Two_Days, Three_Days, four_Days, Five_Days, Six_Days, One_Week, Two_Week, Three_Week, One_Month }
}