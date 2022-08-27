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
    public class UserController : Controller
    {
        DBConnection db = new DBConnection();
        
        // GET: User
        
        [HttpGet]
        public ActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SignIn(string email, string password)
        {
            db.con.Open();
            string q = "select * from userInfo where u_email='" + email + "' and u_password='" + password + "'";
            SqlCommand cmd = new SqlCommand(q, db.con);
            SqlDataReader sdr = cmd.ExecuteReader();
            sdr.Read();
            if (sdr.HasRows)
            {
                Session["name"] = sdr["u_name"].ToString();
                Session["id"] = sdr["u_id"].ToString();
                Session["address"] = sdr["u_address"].ToString();
                Session["email"] = sdr["u_email"].ToString();
                sdr.Close();
                db.con.Close();
                return RedirectToAction("Women","Home");
            }
            ViewBag.Result = "Invalid Entry Plz Enter Valid Data";
            return View();
        }
        public ActionResult LogOut()
        {
            Session.RemoveAll(); //Clear all session variables
            return RedirectToAction("Women", "Home");                   //...              
        }
        [HttpGet]
        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
     
        public ActionResult SignUp(User u)
        {
            if (ModelState.IsValid)
            {
               db.con.Open();
                string q = "insert into userInfo (u_name,u_email,u_address,u_contact,u_gender,u_password) values(@name,@email,@address,@contact,@gender,@pass)";
                SqlCommand cmd = new SqlCommand(q, db.con);
                cmd.Parameters.AddWithValue("@name", u.Name);
                cmd.Parameters.AddWithValue("@email", u.Email);
                cmd.Parameters.AddWithValue("@address", u.Address);
                cmd.Parameters.AddWithValue("@contact", u.Contact);
                cmd.Parameters.AddWithValue("@pass", u.Password);
                cmd.Parameters.AddWithValue("@gender", u.Gender);
                cmd.ExecuteNonQuery();                           
                db.con.Close();
                return RedirectToAction("SignIn");
            }
            else
            {
                return View("SignUp");

            }
        }

        [HttpGet]
        public ActionResult AllUser()
        {
            List<User> files = new List<User>();

            db.con.Open();
            string q = "select * from userInfo";
            SqlCommand cmd = new SqlCommand(q, db.con);

            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                files.Add(new User
                {
                    id = int.Parse(sdr["u_id"].ToString()),
                    Name = sdr["u_name"].ToString(),
                    Email = sdr["u_email"].ToString(),
                    Address = sdr["u_address"].ToString(),
                    Contact = sdr["u_contact"].ToString(),
                    Password = sdr["u_password"].ToString(),
                    Gender = sdr["u_gender"].ToString(),
                });
            }
            db.con.Close();
            sdr.Close();
            return View(files);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            db.con.Open();
            string q = "select * from userInfo where u_id='" + id + "'";
            SqlCommand cmd = new SqlCommand(q, db.con);
            SqlDataReader sdr = cmd.ExecuteReader();
            sdr.Read();
            User u = new User();
            u.id = int.Parse(sdr["u_id"].ToString());
            u.Name = sdr["u_name"].ToString();
            u.Email = sdr["u_email"].ToString();
            u.Address = sdr["u_address"].ToString();
            u.Contact = sdr["u_contact"].ToString();
            u.Password = sdr["u_password"].ToString();
            u.Gender = sdr["u_gender"].ToString();
            sdr.Close();
            db.con.Close();
            return View(u);
        }
        [HttpPost]
        public ActionResult Edit(User u)
        {
            db.con.Open();
            string q = "Update userInfo set u_name =(@name),u_email =(@email),u_contact =(@contact),u_password =(@pass),u_gender =(@gender) where u_id='" + u.id + "'";
            SqlCommand cmd = new SqlCommand(q, db.con);
            cmd.Parameters.AddWithValue("@name", u.Name);
            cmd.Parameters.AddWithValue("@email", u.Email);
            cmd.Parameters.AddWithValue("@address", u.Address);
            cmd.Parameters.AddWithValue("@contact", u.Contact);
            cmd.Parameters.AddWithValue("@pass", u.Password);
            cmd.Parameters.AddWithValue("@gender", u.Gender);
            cmd.ExecuteNonQuery();
            db.con.Close();
            return RedirectToAction("AllUser");
        }

        public ActionResult Delete(int id)
        {
            db.con.Open();
            string q = "delete from userInfo where u_id='" + id + "'";
            SqlCommand cmd = new SqlCommand(q, db.con);
            cmd.ExecuteNonQuery();
            db.con.Close();
            return RedirectToAction("AllUser");
        }
    }
}
