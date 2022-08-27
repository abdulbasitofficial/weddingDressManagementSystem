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
    public class CartController : Controller
    {
        DBConnection db = new DBConnection();

        // GET: Cart
        public ActionResult AddCart(int id)
        {

            if (Session["email"] != null)
            {
                    db.con.Open();
                    int qty = 1;
                    string q = "insert into Cart (user_id,dress_id,qty) values(@userid,@dressid,@qty)";
                    SqlCommand cmd = new SqlCommand(q, db.con);
                    cmd.Parameters.AddWithValue("@userid", Session["id"]);
                    cmd.Parameters.AddWithValue("@dressid", id);
                    cmd.Parameters.AddWithValue("@qty", qty);
                    cmd.ExecuteNonQuery();
                    db.con.Close();
                    return RedirectToAction("Cart","Cart");
               
            }
            else
            {
                return RedirectToAction("SignIn", "User");
            }
        }
           
        public ActionResult Cart()
        {
            List<Dress> files = new List<Dress>();
            if (Session["email"] != null)
            {
                Rent rn = new Rent();
                Session["count"] = rn.request_count();
                Cart cat = new Cart();
                Session["Cart_Count"] = cat.Cart_count();
            }

            db.con.Open();
            string q = "SELECT * FROM dressinfo d INNER JOIN Cart c ON d.d_id = c.dress_id  INNER JOIN userInfo u ON c.user_id = u.u_id where u.u_id=(@uid)";

            SqlCommand cmd = new SqlCommand(q, db.con);
            cmd.Parameters.AddWithValue("@uid", Session["id"]);

            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                files.Add(new Dress
                {
                    ID = int.Parse(sdr["d_id"].ToString()),
                    Name = sdr["d_name"].ToString(),
                    Gender = sdr["d_gender"].ToString(),
                    Price = int.Parse(sdr["d_price"].ToString()),
                    Status = sdr["d_status"].ToString(),
                    Description = sdr["d_description"].ToString(),
                    type = sdr["d_type"].ToString(),
                    Image = (byte[])sdr["d_img"],
                    qty=int.Parse(sdr["qty"].ToString()),
                });
            }
            db.con.Close();
            sdr.Close();
            return View(files);
        }
        public ActionResult Increase(int id)        {            db.con.Open();            SqlCommand cmd1 = new SqlCommand("select qty from Cart where user_id=(@uid) and dress_id=(@did)", db.con);            cmd1.Parameters.AddWithValue("@uid", Session["id"]);            cmd1.Parameters.AddWithValue("@did", id);            SqlDataReader sdr = cmd1.ExecuteReader();            sdr.Read();            int qty = 0;            qty = int.Parse(sdr["qty"].ToString());            qty = qty + 1;            sdr.Close();            db.con.Close();            db.con.Open();            SqlCommand cmd2 = new SqlCommand("Update Cart set qty =(@qty) where user_id=(@uid) and dress_id=(@did)", db.con);            cmd2.Parameters.AddWithValue("@uid", Session["id"]);            cmd2.Parameters.AddWithValue("@did", id);            cmd2.Parameters.AddWithValue("@qty", qty);            cmd2.ExecuteNonQuery();            db.con.Close();                        return RedirectToAction("Cart");        }
        public ActionResult Decrease(int id)        {            db.con.Open();            SqlCommand cmd1 = new SqlCommand("select qty from Cart where user_id=(@uid) and dress_id=(@did)", db.con);            cmd1.Parameters.AddWithValue("@uid", Session["id"]);            cmd1.Parameters.AddWithValue("@did", id);            SqlDataReader sdr = cmd1.ExecuteReader();            sdr.Read();            int qty = 0;            qty = int.Parse(sdr["qty"].ToString());            qty = qty - 1;            sdr.Close();            db.con.Close();            db.con.Open();            SqlCommand cmd2 = new SqlCommand("Update Cart set qty =(@qty) where user_id=(@uid) and dress_id=(@did)", db.con);            cmd2.Parameters.AddWithValue("@uid", Session["id"]);            cmd2.Parameters.AddWithValue("@did", id);            cmd2.Parameters.AddWithValue("@qty", qty);            cmd2.ExecuteNonQuery();            db.con.Close();                        return RedirectToAction("Cart");        }
        public ActionResult Remove(int id)        {            db.con.Open();            SqlCommand cmd = new SqlCommand("delete from Cart where user_id=(@uid) and dress_id=(@did)", db.con);            cmd.Parameters.AddWithValue("@uid", Session["id"]);            cmd.Parameters.AddWithValue("@did", id);            cmd.ExecuteNonQuery();            db.con.Close();            return RedirectToAction("Cart");        }


    }
}