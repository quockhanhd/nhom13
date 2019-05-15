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

        public JsonResult GetUserList()
        {
            var listUser = db.Users.ToList();
            return Json(new { data = listUser });
        }

        public JsonResult AddUser(string userN, string pass, string name, string email)
        {
            if (ModelState.IsValid)
            {
                User user = new User();
                user.UserName = userN;
                user.Password = pass;
                user.Name = name;
                user.Email = email;
                user.GroupID = "1";
                user.Status = true;
                db.Users.Add(user);
                db.SaveChanges();
                return Json(new { data = true });
            }
            else
            {
                return Json(new { data = true });
            }
        }
        public JsonResult EditUser(int id, string userN, string pass, string name, string email, string group)
        {
            if (ModelState.IsValid)
            {
                User user = db.Users.Single(x => x.ID == id);
                user.UserName = userN;
                user.Password = pass;
                user.Name = name;
                user.Email = email;
                user.GroupID = group;
                user.Status = true;
                try
                {
                    db.SaveChanges();
                    return Json(new { data = true });
                }
                catch (Exception)
                {
                    return Json(new { data = false });
                }
            }
            else
            {
                return Json(new { data = true });
            }
        }
        public JsonResult DeleteUser(int id)
        {
            User hs = db.Users.Single(x => x.ID == id);
            if (hs != null)
            {
                db.Users.Remove(hs);
                db.SaveChanges();
                return Json(new { data = true });
            }
            else
                return Json(new { data = false });
        }
        [HttpGet]
        public JsonResult LoadGroup()
        {
            return Json(new { data = db.UserGroups.ToList() }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AddandEditUserModal(int id)
        {
            var result = new User();
            string mode = "Add";
            result = db.Users.FirstOrDefault(x => x.ID == id);
            if (result == null)
            {
                mode = "Add";
                result = new User();
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