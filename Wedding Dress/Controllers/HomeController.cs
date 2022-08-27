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
    public class HomeController : Controller
    {
        DBConnection db = new DBConnection();
        public ActionResult Women()
        {
            if (Session["email"] != null)
            {
                Rent rn = new Rent();
                Session["count"] = rn.request_count();
                Cart cat = new Cart();
                Session["Cart_Count"] = cat.Cart_count();
            }
 

            string gender = "Female";
            List<Dress> files = new List<Dress>();

            db.con.Open();
            string q = "select * from userInfo u inner join dressinfo d On u.u_id=d.u_id where d.d_gender=(@gen)";

            SqlCommand cmd = new SqlCommand(q, db.con);
            cmd.Parameters.AddWithValue("@gen", gender);

            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                files.Add(new Dress
                {
                    ID = int.Parse(sdr["d_id"].ToString()),
                    Name = sdr["d_name"].ToString(),
                    UserName = sdr["u_name"].ToString(),
                    User_Address = sdr["u_address"].ToString(),
                    User_Contact = sdr["u_contact"].ToString(),
                    Gender = sdr["d_gender"].ToString(),
                    Price = int.Parse(sdr["d_price"].ToString()),
                    Status = sdr["d_status"].ToString(),
                    Description = sdr["d_description"].ToString(),
                    type = sdr["d_type"].ToString(),
                    Image = (byte[])sdr["d_img"]
                });
            }
            db.con.Close();
            sdr.Close();
            ViewBag.category = FillList();
            ViewBag.city = City_List();
            return View(files); ;
        }
        public ActionResult Men()
        {
            if(Session["email"]!=null)
            {
                Rent rn = new Rent();
                Session["count"] = rn.request_count();
                Cart cat = new Cart();
                Session["Cart_Count"] = cat.Cart_count();
            }
           
            List<Dress> files = new List<Dress>();

            db.con.Open();
            string q = "select * from userInfo u inner join dressinfo d On u.u_id=d.u_id where d.d_gender=(@gen)";

            SqlCommand cmd = new SqlCommand(q, db.con);
            cmd.Parameters.AddWithValue("@gen", "Male");

            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                files.Add(new Dress
                {
                    ID = int.Parse(sdr["d_id"].ToString()),
                    Name = sdr["d_name"].ToString(),
                    UserName = sdr["u_name"].ToString(),
                    User_Address = sdr["u_address"].ToString(),
                    User_Contact = sdr["u_contact"].ToString(),
                    Gender = sdr["d_gender"].ToString(),
                    Price = int.Parse(sdr["d_price"].ToString()),
                    Status = sdr["d_status"].ToString(),
                    Description = sdr["d_description"].ToString(),
                    type = sdr["d_type"].ToString(),
                    Image = (byte[])sdr["d_img"]
                });
            }
            db.con.Close();
            sdr.Close();
            ViewBag.category = FillList();
            ViewBag.city = City_List();
            return View(files);
        }
        public ActionResult Search(string search)
        {
            if (Session["email"] != null)
            {
                Rent rn = new Rent();
                Session["count"] = rn.request_count();
                Cart cat = new Cart();
                Session["Cart_Count"] = cat.Cart_count();
            }

            List<Dress> files = new List<Dress>();

            db.con.Open();
            string q = "select * from userInfo u inner join dressinfo d On u.u_id=d.u_id where d.d_name LIKE '%'+(@sr)+'%'";

            SqlCommand cmd = new SqlCommand(q, db.con);
            cmd.Parameters.AddWithValue("@sr", search);

            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                files.Add(new Dress
                {
                    ID = int.Parse(sdr["d_id"].ToString()),
                    Name = sdr["d_name"].ToString(),
                    UserName = sdr["u_name"].ToString(),
                    User_Address = sdr["u_address"].ToString(),
                    User_Contact = sdr["u_contact"].ToString(),
                    Gender = sdr["d_gender"].ToString(),
                    Price = int.Parse(sdr["d_price"].ToString()),
                    Status = sdr["d_status"].ToString(),
                    Description = sdr["d_description"].ToString(),
                    type = sdr["d_type"].ToString(),
                    Image = (byte[])sdr["d_img"]
                });
            }
            db.con.Close();
            sdr.Close();
            return View(files);
        }
        // GET: Home
        public ActionResult Dresses()
        {
            return View();
        }
        public ActionResult Filteration_Women(int id,string city)
        {
            if (Session["email"] != null)
            {
                Rent rn = new Rent();
                Session["count"] = rn.request_count();
                Cart cat = new Cart();
                Session["Cart_Count"] = cat.Cart_count();
            }

            string gender = "Female";
            List<Dress> files = new List<Dress>();

            db.con.Open();
            string q = "select * from userInfo u inner join dressinfo d On u.u_id=d.u_id where d.d_gender=(@gen) and d.c_id=(@cat) and u.u_address LIKE '%'+(@sr)+'%'";

            SqlCommand cmd = new SqlCommand(q, db.con);
            cmd.Parameters.AddWithValue("@gen", gender);
            cmd.Parameters.AddWithValue("@cat", id);
            cmd.Parameters.AddWithValue("@sr", city);
            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                files.Add(new Dress
                {
                    ID = int.Parse(sdr["d_id"].ToString()),
                    Name = sdr["d_name"].ToString(),
                    UserName = sdr["u_name"].ToString(),
                    User_Address = sdr["u_address"].ToString(),
                    User_Contact = sdr["u_contact"].ToString(),
                    Gender = sdr["d_gender"].ToString(),
                    Price = int.Parse(sdr["d_price"].ToString()),
                    Status = sdr["d_status"].ToString(),
                    Description = sdr["d_description"].ToString(),
                    type = sdr["d_type"].ToString(),
                    Image = (byte[])sdr["d_img"]
                });
            }
            db.con.Close();
            sdr.Close();
            ViewBag.category = FillList();
            ViewBag.city = City_List();
            return View("Women",files); ;
        }
        public ActionResult Filteration_Men(int id,string city)
        {
            if (Session["email"] != null)
            {
                Rent rn = new Rent();
                Session["count"] = rn.request_count();
                Cart cat = new Cart();
                Session["Cart_Count"] = cat.Cart_count();
            }


            string gender = "Male";
            List<Dress> files = new List<Dress>();

            db.con.Open();
            string q = "select * from userInfo u inner join dressinfo d On u.u_id=d.u_id where d.d_gender=(@gen) and d.c_id=(@cat) and u.u_address LIKE '%'+(@sr)+'%'";

            SqlCommand cmd = new SqlCommand(q, db.con);
            cmd.Parameters.AddWithValue("@gen", gender);
            cmd.Parameters.AddWithValue("@cat", id);
            cmd.Parameters.AddWithValue("@sr", city);
            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                files.Add(new Dress
                {
                    ID = int.Parse(sdr["d_id"].ToString()),
                    Name = sdr["d_name"].ToString(),
                    UserName = sdr["u_name"].ToString(),
                    User_Address = sdr["u_address"].ToString(),
                    User_Contact = sdr["u_contact"].ToString(),
                    Gender = sdr["d_gender"].ToString(),
                    Price = int.Parse(sdr["d_price"].ToString()),
                    Status = sdr["d_status"].ToString(),
                    Description = sdr["d_description"].ToString(),
                    type = sdr["d_type"].ToString(),
                    Image = (byte[])sdr["d_img"]
                });
            }
            db.con.Close();
            sdr.Close();
            ViewBag.category = FillList();
            ViewBag.city = City_List();
            return View("Men",files); ;
        }
        public List<SelectListItem> FillList()
        {
            db.con.Open();
            SqlCommand cmd = new SqlCommand("Select c_id,c_name From category", db.con);

            SqlDataReader idr = cmd.ExecuteReader();
            List<SelectListItem> category = new List<SelectListItem>();
            if (idr.HasRows)
            {
                while (idr.Read())
                {
                    category.Add(new SelectListItem
                    {

                        Value = idr["c_id"].ToString(),
                        Text = idr["c_name"].ToString()
                    });
                }
            }
            db.con.Close();

            return category;
        }
        public List<SelectListItem> City_List()
        {
            List<SelectListItem> category = new List<SelectListItem>();
            category.Add(new SelectListItem { Value ="Islamabad", Text ="Islamabad"});
            category.Add(new SelectListItem { Value = "Rawalpindi", Text = "Rawalpindi" });
            category.Add(new SelectListItem { Value = "Multan", Text = "Multan" });
            category.Add(new SelectListItem { Value = "Karachi", Text = "Karachi" });
            category.Add(new SelectListItem { Value = "Faisalabad", Text = "Faisalabad" });
            category.Add(new SelectListItem { Value = "Hyderabad", Text = "Hyderabad" });
            category.Add(new SelectListItem { Value = "Muzaffarabad", Text = "Muzaffarabad" });
            category.Add(new SelectListItem { Value = "Quetta", Text = "Quetta" });
            category.Add(new SelectListItem { Value = "Sialkot", Text = "Sialkot" });
            category.Add(new SelectListItem { Value = "Lahore", Text = "Lahore" });
            category.Add(new SelectListItem { Value = "Peshawar", Text = "Peshawar" });

            return category;
        }
    }
}
  
