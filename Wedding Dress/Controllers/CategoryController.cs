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
    public class CategoryController : Controller
    {
        DBConnection db = new DBConnection();        
        // GET: Category
        [HttpGet]      
        public ActionResult AddCategory()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddCategory(Category c)
        {
            db.con.Open();
            string q = "insert into category (c_name) values(@name)";
            SqlCommand cmd = new SqlCommand(q, db.con);
            cmd.Parameters.AddWithValue("@name", c.Name);
            cmd.ExecuteNonQuery();
            db.con.Close();
            return RedirectToAction("Category");
        }
        public ActionResult Category()
        {
            List<Category> files = new List<Category>();

            db.con.Open();
            string q = "select * from category";
            SqlCommand cmd = new SqlCommand(q, db.con);

            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                files.Add(new Category
                {
                    ID = int.Parse(sdr["c_id"].ToString()),
                    Name = sdr["c_name"].ToString(),
                   
                });
            }
            db.con.Close();
            sdr.Close();
            return View(files);
        }
        [HttpGet]
        public ActionResult Edit( int id)
        {
            db.con.Open();
            string q = "select * from category where c_id='" + id + "'";
            SqlCommand cmd = new SqlCommand(q, db.con);
            SqlDataReader sdr = cmd.ExecuteReader();
            sdr.Read();
            Category c = new Category();
            c.ID = int.Parse(sdr["c_id"].ToString());
            c.Name = sdr["c_name"].ToString();
            sdr.Close();
            db.con.Close();
            return View(c);
        }
        [HttpPost]
        public ActionResult Edit(Category c)
        {
            db.con.Open();
            string q = "Update category set c_name =(@name) where c_id='" + c.ID + "'";
            SqlCommand cmd = new SqlCommand(q, db.con);
            cmd.Parameters.AddWithValue("@name", c.Name);
            cmd.ExecuteNonQuery();
            db.con.Close();
            return RedirectToAction("Category");
        }
        public ActionResult Delete(int id)
        {
            db.con.Open();
            string q = "delete from category where c_id='" + id + "'";
            SqlCommand cmd = new SqlCommand(q, db.con);
            cmd.ExecuteNonQuery();
            db.con.Close();
            return RedirectToAction("Category");
        }
    }
}