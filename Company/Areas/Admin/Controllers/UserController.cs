using Company.Common;
using Model.Dao;
using Model.EF;
using Model.Encryptor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Company.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        // GET: Admin/User
        public ActionResult Index(string searchString, int page = 1, int pageSize = 2)
        {
            var dao = new UserDao();
            var model = dao.ListAllPaging(searchString, page, pageSize);
            ViewBag.searchString = searchString;
            return View(model);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                UserLogin currentUsr = (UserLogin)Session[CommonConstants.USER_SESSION];
                if (currentUsr.Role == null || currentUsr.Role != 1)
                {
                    return RedirectToAction("Index", "Login");
                }
                else
                {
                    var dao = new UserDao();

                    string salt = BCrypt.GenerateSalt();
                    string hashedPw = BCrypt.HashPassword(user.Password, salt);
                    DateTime createdDate = DateTime.Now;
                    var date = createdDate.Date;

                    user.Password = hashedPw;
                    user.Role = 2;
                    user.CreatedDate = date;
                    user.CreatedBy = currentUsr.UserID;

                    long id = dao.Insert(user);
                    if (id > 0)
                    {
                        //SetAlert("Thêm người dùng thành công", "success");
                        return RedirectToAction("Index", "User");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Thêm người dùng không thành công");
                    }
                }
                
            }
            return View("Index");
        }
        public DateTime convertDate(DateTime? input)
        {
            DateTime defaultDate = new DateTime(1001, 1, 1);
            if (input == null)
                return defaultDate;
            var date = input.Value.Date;
            return date;
               
        }
    }
}