using BitSchool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using BitSchool.ViewModels;

namespace BitSchool.Controllers
{

    public class HomeController : Controller
    {
        private ApplicationDbContext _dBContext;
        public HomeController() {
            _dBContext = new ApplicationDbContext();
        }
        public ActionResult Index()
        {
            var upcc = _dBContext.Courses
                .Include(c => c.Lecturer)
                .Include(c => c.Categories)
                .Where(c => c.DateTime > DateTime.Now);
            var viewModel = new CourseViewModel
            {
                UpcommingCoures = upcc,
                ShowAction = User.Identity.IsAuthenticated
            };
            return View(viewModel);
        }
 

        //public ActionResult About()
        //{
        //    ViewBag.Message = "Your application description page.";

        //    return View();
        //}

        //public ActionResult Contact()
        //{
        //    ViewBag.Message = "Your contact page.";

        //    return View();
        //}
    }
}