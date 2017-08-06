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
        public ActionResult Index(string searchString, int page = 1, int pageSize = 5)
        {
            if (isAdmin() > 0)
            {
                var dao = new UserDao();
                var model = dao.ListAllPaging(searchString, page, pageSize);

                int totalRecord = dao.TotalUser(searchString);

                double dblPageCount = (double)((decimal)totalRecord / Convert.ToDecimal(pageSize));
                int totalPage = (int)Math.Ceiling(dblPageCount);

                int startRecord = (page - 1) * pageSize + 1;
                int endRecord = 0;
                if (page == totalPage)
                {
                    endRecord = totalRecord;
                }
                else
                {
                    endRecord = startRecord + pageSize - 1;
                }

                ViewBag.startRecord = startRecord;
                ViewBag.endRecord = endRecord;
                ViewBag.totalRecord = totalRecord;
                ViewBag.searchString = searchString;
                return View(model);
            }
            else
                return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public ActionResult Create()
        {
            if (isAdmin() > 0)
            {
                return View();
            }
            else
                return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public ActionResult Create(User user)
        {
            if (isAdmin() > 0)
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
            else
                return RedirectToAction("Index", "Home");

        }
        [HttpGet]
        public ActionResult Edit(long Id)
        {
            if (isAdmin() > 0)
            {
                var dao = new UserDao();
                User user = dao.getUserById(Id);
                return View(user);
            }
            else
                return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public ActionResult Edit(User user)
        {
            if (isAdmin() > 0)
            {
                if (ModelState.IsValid)
                {
                    //UserLogin currentUsr = (UserLogin)Session[CommonConstants.USER_SESSION];
                    var currentId = isAdmin();
                    if (currentId == 0)
                    {
                        return RedirectToAction("Index", "Login");
                    }
                    else
                    {
                        var dao = new UserDao();
                        DateTime modifiedDate = DateTime.Now;
                        var date = modifiedDate.Date;

                        user.ModifiedDate = date;
                        user.ModifiedBy = currentId;

                        var result = dao.Update(user);
                        if (result)
                        {
                            //SetAlert("Thêm người dùng thành công", "success");
                            return RedirectToAction("Index", "User");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Cập nhật thất bại");
                        }
                    }

                }
                return View("Index");
            }
            else
                return RedirectToAction("Index", "Home");
        }

        [HttpDelete]
        public ActionResult Delete(long id)
        {
            if (isAdmin() > 0)
            {
                new UserDao().Delete(id);
                return RedirectToAction("Index");
            }
            else
                return View("Index");
        }

        [HttpPost]
        public JsonResult ChangeStatus(long id)
        {
            var result = new UserDao().ChangeStatus(id);
            return Json(new
            {
                status = result
            });

        }


        public DateTime convertDate(DateTime? input)
        {
            DateTime defaultDate = new DateTime(1001, 1, 1);
            if (input == null)
                return defaultDate;
            var date = input.Value.Date;
            return date;

        }

        public long isAdmin()
        {
            UserLogin currentUsr = (UserLogin)Session[CommonConstants.USER_SESSION];
            {
                if (currentUsr == null || currentUsr.Role != 1 || currentUsr.Role == null)
                {
                    return 0;
                }
                else
                {
                    return currentUsr.UserID;
                }
            }
        }
    }
}