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

        [HttpPost, ValidateInput(false)]
        public JsonResult AddCartegory(string name, string des)
        {
            if (ModelState.IsValid)
            {
                Course_Category cartegory = new Course_Category();
                cartegory.Name = name;
                cartegory.Description = des;
                cartegory.CreatedDate = DateTime.Now;
                db.Course_Category.Add(cartegory);
                db.SaveChanges();
                return Json(new { });
            }
            else
                return Json(new { data = false });
        }
    }
}