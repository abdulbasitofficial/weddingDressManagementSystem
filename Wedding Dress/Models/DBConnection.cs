using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Wedding_Dress.Models
{
    public class DBConnection
    {
        public static string constr = @"Data Source=DESKTOP-HCNT8E4;Initial Catalog=WeddingDresses;Integrated Security=true";
        public SqlConnection con = new SqlConnection(constr);
    }
}