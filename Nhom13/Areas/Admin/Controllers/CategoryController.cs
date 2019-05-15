using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.EF;
namespace Nhom13.Areas.Admin.Controllers
{
    public class CartegoryController : Controller
    {
        // GET: Admin/Category
        OnlineCourse db = new OnlineCourse();
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetCartegoryList()
        {
            return Json(new { data = db.Course_Category.ToList() });
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
        [HttpPost, ValidateInput(false)]
        public JsonResult EditCartegory(int id, string name, string des)
        {
            if (ModelState.IsValid)
            {
                Course_Category cartegory = db.Course_Category.Single(x => x.ID == id);
                cartegory.Name = name;
                cartegory.Description = des;
                cartegory.ModifiedDate = DateTime.Now;
                db.SaveChanges();
                return Json(new { data = true });
            }
            else
                return Json(new { data = false });
        }
        public JsonResult DeleteCartegory(int id)
        {
            if (ModelState.IsValid)
            {
                var cartegory = db.Course_Category.Single(x => x.ID == id);
                db.Course_Category.Remove(cartegory);
                db.SaveChanges();
                return Json(new { data = true });
            }
            else
                return Json(new { data = false });
        }
        public ActionResult AddandEditCartegoryModal(int id)
        {
            var result = new Course_Category();
            string mode = "Add";
            result = db.Course_Category.FirstOrDefault(x => x.ID == id);
            if (result == null)
            {
                mode = "Add";
                result = new Course_Category();
            }
            else
            {
                mode = "Edit";
            }
            ViewData["Mode"] = mode;
            ViewData["Obj"] = result;
            return View();
        }
    }
}