using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;


namespace Wedding_Dress.Models
{
    [SessionState(SessionStateBehavior.Default)]
    public class Rent
    {
        DBConnection db = new DBConnection();
        public string u_name { get; set; }
        public int u_id { get; set; }

        public int d_id { get; set; }
        public string r_id { get; set; }
        public DateTime rent_date { get; set; }
        public string r_status { get; set; }

        public DateTime return_date { get; set; }
        public int fine { get; set; }
        public int r_qty { get; set; }
        public string r_period { get; set; }
        public string r_address { get; set; }
        public DateTime request_date { get; set; }
        public int request_count()
        {

            List<Rent> files = new List<Rent>();

            db.con.Open();
            string q = "Select DISTINCT  r.u_id from dressinfo d inner JOIN rent r ON d.d_id=r.d_id where d.u_id=(@uid)";
            SqlCommand cmd = new SqlCommand(q, db.con);
            cmd.Parameters.AddWithValue("@uid", HttpContext.Current.Session["id"]);
            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                files.Add(new Rent
                {            
                    d_id = int.Parse(sdr["u_id"].ToString()),
                   

                });
            }
            db.con.Close();
            sdr.Close();
            int count = files.Count();
            return count;
        }

    }
}
public enum period {One_Days,Two_Days,Three_Days,four_Days,Five_Days,Six_Days,One_Week,Two_Week,Three_Week,One_Month}