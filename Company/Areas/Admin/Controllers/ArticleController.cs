using Company.Common;
using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Company.Areas.Admin.Controllers
{
    public class ArticleController : BaseController
    {
        // GET: Admin/Article
        public ActionResult Index(string searchString, int page=1, int pageSize=5)
        {
            var dao = new ArticleDao();

            var model = dao.ListAllPaging(searchString, page, pageSize);
            int totalRecord = dao.TotalArticle(searchString);

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

            if (totalRecord < 5)
            {
                if (totalRecord == 0)
                {
                    startRecord = 0;
                    endRecord = 0;
                }
                else
                {
                    startRecord = 1;
                    endRecord = totalRecord;
                }
            }

            ViewBag.startRecord = startRecord;
            ViewBag.endRecord = endRecord;
            ViewBag.totalRecord = totalRecord;
            ViewBag.searchString = searchString;

            return View(model);
        }
        [HttpGet]
        public ActionResult Create()
        {
            SetDropMenu();
            return View();
        }

        [HttpPost]
        public ActionResult Create(Article entity)
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
                    var dao = new ArticleDao();

                    entity.CreatedDate = DateTime.Now;
                    entity.CreatedBy = currentUsr.UserID;
                    entity.Status = false;
                    entity.Deleted = false;

                    int id = dao.Insert(entity);
                    if (id > 0)
                    {
                        //SetAlert("Thêm người dùng thành công", "success");
                        return RedirectToAction("Index", "Article");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Thêm bài viết thất bại");
                    }
                }
            }
            SetDropMenu();
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int Id)
        {
            var dao = new ArticleDao();
            Article article = dao.getArticleById(Id);
            SetDropMenu(article.MenuId);
            return View(article);
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new ArticleDao().Delete(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult ChangeStatus(int id)
        {
            var result = new ArticleDao().ChangeStatus(id);
            return Json(new
            {
                status = result
            });

        }

        public void SetDropMenu(int? selectedId = null)
        {
            var menuDao = new MenuDao();
            ViewBag.MenuId = new SelectList(menuDao.getAllMenu(), "Id", "Text", selectedId);
        }
    }
}