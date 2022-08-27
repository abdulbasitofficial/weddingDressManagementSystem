using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Wedding_Dress.Models
{
    public class Cart
    {
        DBConnection db = new DBConnection();
        public int cart_id { get; set; }
        public int userid_id { get; set; }
        public int dress_id { get; set; }
        public int qty { get; set; }

        public int Cart_count()
        {

            List<Cart> files = new List<Cart>();

            db.con.Open();
            string q = "Select * from Cart where user_id=(@uid)";
            SqlCommand cmd = new SqlCommand(q, db.con);
            cmd.Parameters.AddWithValue("@uid", HttpContext.Current.Session["id"]);
            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                files.Add(new Cart
                {
                    userid_id = int.Parse(sdr["user_id"].ToString()),


                });
            }
            db.con.Close();
            sdr.Close();
            int count = files.Count();
            return count;
        }

    }
}