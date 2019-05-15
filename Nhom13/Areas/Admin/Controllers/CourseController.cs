using Model.EF;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace Nhom13.Areas.Admin.Controllers
{
    public class CourseController : Controller
    {
        OnlineCourse db = new OnlineCourse();
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetCourseList()
        {
            return Json(new { data = db.Courses.ToList() });
        }

        [HttpPost, ValidateInput(false)]
        public JsonResult AddCourse()
        {
            var countform = Request.Form.Count;
            var countfile = Request.Files.Count;
            var name = Request.Form["Ten"].ToString();
            var gia = Request.Form["Gia"].ToString();
            var anh = Request.Files["Anh"];
            var motaRaw = Request.Form["Mota"];
            var mota = HttpUtility.UrlDecode(motaRaw);
            var path = Path.Combine(Server.MapPath("~/Image"), anh.FileName);

            anh.SaveAs(path);

            Course course = new Course();
            course.Name = name;
            course.Image = anh.FileName;
            course.Price = decimal.Parse(gia);

            course.Description = mota;
            db.Courses.Add(course);
            db.SaveChanges();
            return Json(new { data = true });
        }

        [HttpPost, ValidateInput(false)]
        public JsonResult EditCourse()
        {
            var countform = Request.Form.Count;
            var countfile = Request.Files.Count;
            var name = Request.Form["Ten"].ToString();
            var gia = Request.Form["Gia"].ToString();
            var anh = Request.Files["Anh"];
            var motaRaw = Request.Form["Mota"];
            var mota = HttpUtility.UrlDecode(motaRaw);
            var path = Path.Combine(Server.MapPath("~/Image"), anh.FileName);
            var id = int.Parse(Request.Form["ID"]);
            anh.SaveAs(path);

            Course course = db.Courses.Single(x => x.ID == id);
            course.Name = name;
            course.Image = anh.FileName;
            course.Price = decimal.Parse(gia);

            course.Description = mota;
            db.SaveChanges();
            return Json(new { data = true });
        }
        public JsonResult DeleteCourse(int id)
        {
            if (ModelState.IsValid)
            {
                var course = db.Courses.Single(x => x.ID == id);
                db.Courses.Remove(course);
                db.SaveChanges();
                return Json(new { data = true });
            }
            else
                return Json(new { data = false });
        }
        [HttpGet]
        public JsonResult LoadCategory()
        {
            return Json(new { data = db.Course_Category.ToList() }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddandEditCourseModal(int id)
        {
            var result = new Course();
            string mode = "Add";
            result = db.Courses.FirstOrDefault(x => x.ID == id);
            if (result == null)
            {
                mode = "Add";
                result = new Course();
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