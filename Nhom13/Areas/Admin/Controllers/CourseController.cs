using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nhom13.Areas.Admin.Controllers
{
    public class CourseController : Controller
    {
        // GET: Admin/Course
        public ActionResult Index()
        {
            return View();
        }
    }
}