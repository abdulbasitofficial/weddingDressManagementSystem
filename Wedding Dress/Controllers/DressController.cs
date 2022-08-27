using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;
using Wedding_Dress.Models;

namespace Wedding_Dress.Controllers
{
    [SessionState(SessionStateBehavior.Default)]
    public class DressController : Controller
    {
        DBConnection db = new DBConnection();
        // GET: Category
        [HttpGet]
        public ActionResult AddDress()
        {
            Dress d = new Dress();
           d.List = FillList();
           return View(d);
        }
        public List<Category> FillList()
        {
            db.con.Open();
            SqlCommand cmd = new SqlCommand("Select c_id,c_name From category", db.con);

            SqlDataReader idr = cmd.ExecuteReader();
            List<Category> category = new List<Category>();
            if (idr.HasRows)
            {
                while (idr.Read())
                {
                    category.Add(new Category
                    {
                        ID = int.Parse(idr["c_id"].ToString()),
                        Name = idr["c_name"].ToString()
                    });
                }
            }
            db.con.Close();

            return category;
        }
        [HttpPost]
        public ActionResult AddDress(Dress d, HttpPostedFileBase PostedFile)
        {
            string status = "Un Rent";
            byte[] bytes;
            BinaryReader br = new BinaryReader(PostedFile.InputStream);
            bytes = br.ReadBytes(PostedFile.ContentLength);
            db.con.Open();
            string q = "insert into dressinfo(d_name,d_price,d_gender,d_status,d_description,d_img,d_type,c_id,u_id) values(@name,@price,@gender,@status,@des,@image,@type,@cid,@uid)";
            SqlCommand cmd = new SqlCommand(q, db.con);
            cmd.Parameters.AddWithValue("@name", d.Name);
            cmd.Parameters.AddWithValue("@price", d.Price);
            cmd.Parameters.AddWithValue("@gender", d.Gender);
            cmd.Parameters.AddWithValue("@uid", Session["id"]);
            cmd.Parameters.AddWithValue("@des", d.Description);
            cmd.Parameters.AddWithValue("@image", bytes);
            cmd.Parameters.AddWithValue("@status", status);
            cmd.Parameters.AddWithValue("@cid", d.Category);
            cmd.Parameters.AddWithValue("@type", PostedFile.ContentType);
            cmd.ExecuteNonQuery();
            db.con.Close();
            return RedirectToAction("Dress");
        }
        public ActionResult Dress()
        {
            List<Dress> files = new List<Dress>();

            db.con.Open();
            string q = "select * from dressinfo where u_id=(@uid)";
           
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
                    type= sdr["d_type"].ToString(),
                    Image = (byte[])sdr["d_img"]
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
            string q = "select * from dressinfo where d_id='" + id + "'";
            SqlCommand cmd = new SqlCommand(q, db.con);
            SqlDataReader sdr = cmd.ExecuteReader();
            sdr.Read();
            Dress d = new Dress();
            d.Name = sdr["d_name"].ToString();
            d.Gender = sdr["d_gender"].ToString();
            d.Price = int.Parse(sdr["d_price"].ToString());
            d.Status = sdr["d_status"].ToString();
            d.Description = sdr["d_description"].ToString();
            
            sdr.Close();
            db.con.Close();
            return View(d);
        }
        [HttpPost]
        public ActionResult Edit(Dress d)
        {
            db.con.Open();
            string q = "Update dressinfo set d_name =(@name),d_price =(@price),d_status =(@status),d_gender =(@gender),d_description =(@des) where d_id='" + d.ID + "'";
            SqlCommand cmd = new SqlCommand(q, db.con);
            cmd.Parameters.AddWithValue("@name", d.Name);
            cmd.Parameters.AddWithValue("@price", d.Price);
            cmd.Parameters.AddWithValue("@gender", d.Gender);
            cmd.Parameters.AddWithValue("@des", d.Description);
       cmd.Parameters.AddWithValue("@status",d.Status);
            
            

            cmd.ExecuteNonQuery();
            db.con.Close();
            return RedirectToAction("Dress");
        }
        public ActionResult Delete(int id)
        {
            db.con.Open();
            string q = "delete from dressinfo where d_id='" + id + "'";
            SqlCommand cmd = new SqlCommand(q, db.con);
            cmd.ExecuteNonQuery();
            db.con.Close();
            return RedirectToAction("Dress");
        }

    }
}