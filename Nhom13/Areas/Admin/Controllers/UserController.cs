using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.EF;
namespace Nhom13.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        OnlineCourse db = new OnlineCourse();

        public ActionResult Index()
        {

            return View();
        }

    
    }
}