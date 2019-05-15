using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.DAO;
using Nhom13.Areas.Admin.Models;
using Nhom13.Common;
namespace Nhom13.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Admin/Login
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login(LoginModel loginData)
        {
            if (ModelState.IsValid)
            {
                UserDAO dao = new UserDAO();
                var result = dao.Login(loginData.UserName, loginData.Password, true);
                if (result == 1)
                {
                    var user = dao.GetByUserName(loginData.UserName);
                    var userSession = new Common.UserLogin();
                    Session["UserID"] = user.ID;
                    Session["UserName"] = user.UserName;
                    Session["GroupID"] = user.GroupID;
                    Session["Name"] = user.Name;
                    Session.Add(CommonConstants.USER_SESSION, userSession);
                    return RedirectToAction("Index", "User");
                }
                else if (result == 0)
                {
                    ModelState.AddModelError("", "Không có tài khoản");
                }
                else if (result == -1)
                {
                    ModelState.AddModelError("", "Tài khoản không được kích hoạt");
                }
                else if (result == -2)
                {
                    ModelState.AddModelError("", "Sai mật khẩu");
                }
                else if (result == -3)
                {
                    ModelState.AddModelError("", "Không có quyền đăng nhập vào Admin");
                }
                else
                {
                    ModelState.AddModelError("", "Tài khoản không đúng");
                }
            }
            return View("Index");
        }
        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Index", "Login");

        }
    }
}