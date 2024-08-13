using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApps.Models.Course;
using System.Data.Sql;
using System.Data.SqlClient;

namespace WebApps.Controllers
{
    public class HomeController : Controller
    {
        string connestionString = "";
        public HomeController()
        {

            connestionString = ConfigurationManager.ConnectionStrings["ConnStringDb"].ConnectionString;
        }
        public ActionResult Index()
        {
            var courseList = getAllCourses();
            return View(courseList);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        List<Courses> getAllCourses()
        {
            List<Courses> CoursesList = new List<Courses>();
            try
            {
                string cmdToExecute = "SELECT * FROM Course;";
                using (SqlConnection sql = new SqlConnection(connestionString))
                {
                    sql.Open();
                    var sqlcmd = new SqlCommand(cmdToExecute, sql);
                    using (SqlDataReader reader = sqlcmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Courses courses = new Courses();
                            courses.CourseID = Convert.ToInt32(reader["CourseID"]);
                            courses.CourseName = reader["CourseName"].ToString();
                            courses.Rating = decimal.Parse(reader["Rating"].ToString());
                            CoursesList.Add(courses);
                        }
                    }
                }
                return CoursesList;
            }
            catch (Exception ex)
            {
                if (ex != null)
                {

                }
                return CoursesList;
            }
        }
    }
}