using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;
using Wedding_Dress.Models;
namespace Wedding_Dress.Controllers
{
    [SessionState(SessionStateBehavior.Default)]
    public class RentController : Controller
    {
        DBConnection db = new DBConnection();

        // GET: Rent
        public ActionResult Rent(string period)
        {

            db.con.Open();
            List<Rent> files = new List<Rent>();
            SqlCommand cmd = new SqlCommand("select u.u_address,c.user_id,c.dress_id,c.qty from userInfo u inner join Cart c on u.u_id=c.user_id inner join dressinfo d on c.dress_id=d.d_id where u.u_id = (@uid)", db.con);
            cmd.Parameters.AddWithValue("@uid", Session["id"]);
            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                files.Add(new Rent
                {
                    d_id = int.Parse(sdr["dress_id"].ToString()),
                    r_qty = int.Parse(sdr["qty"].ToString()),
                    u_id = int.Parse(sdr["user_id"].ToString()),
                    r_address = sdr["u_address"].ToString()

                });
            }
            sdr.Close();
            db.con.Close();

            db.con.Open();
            foreach (var item in files)
            {
                SqlCommand cmd1 = new SqlCommand("insert into [rent](d_id,u_id,r_qty,request_date,r_address,r_period) values(@did,@uid,@qty,@date,@address,@period)", db.con);
                cmd1.Parameters.AddWithValue("@did", item.d_id);
                cmd1.Parameters.AddWithValue("@uid", item.u_id);
                cmd1.Parameters.AddWithValue("@qty", item.r_qty);
                cmd1.Parameters.AddWithValue("@address", item.r_address);
                cmd1.Parameters.AddWithValue("@period", period);
                cmd1.Parameters.AddWithValue("@date", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd1.ExecuteNonQuery();
            }
            db.con.Close();

            db.con.Open();
            string q = "delete from Cart where user_id=(@uid)";
            SqlCommand cmdd = new SqlCommand(q, db.con);
            cmdd.Parameters.AddWithValue("@uid", Session["id"]);

            cmdd.ExecuteNonQuery();
            db.con.Close();
            return View();


        }

        public ActionResult RentRequest()
        {
            Rent rn = new Rent();
            Session["count"] = rn.request_count();

            List<Rent> files = new List<Rent>();

            db.con.Open();
            string q = "SELECT DISTINCT u.u_id,u.u_address,u.u_name,r.request_date FROM userInfo u inner JOIN rent r ON u.u_id = r.u_id inner JOIN dressinfo d ON r.d_id=d.d_id where d.u_id=(@uid)";

            SqlCommand cmd = new SqlCommand(q, db.con);
            cmd.Parameters.AddWithValue("@uid", Session["id"]);
            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                files.Add(new Rent
                {
                    u_id = int.Parse(sdr["u_id"].ToString()),
                    r_address = sdr["u_address"].ToString(),
                    u_name = sdr["u_name"].ToString(),
                    request_date = Convert.ToDateTime(sdr["request_date"])

                });
            }
            db.con.Close();
            sdr.Close();
            return View(files);
        }

    }

}