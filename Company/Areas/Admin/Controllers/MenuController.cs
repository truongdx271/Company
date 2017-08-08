using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Company.Areas.Admin.Controllers
{
    public class MenuController : BaseController
    {
        // GET: Admin/Menu
        public ActionResult Index(string searchString, int page = 1, int pageSize = 5)
        {
            var dao = new MenuDao();

            var model = dao.ListAllPaging(searchString, page, pageSize);
            int totalRecord = dao.TotalMenu(searchString);

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
            //var menuDao = new MenuDao();
            //List<Menu> listMenu = menuDao.getListMenu();
            //ViewBag.listMenu = listMenu;
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int Id)
        {
            var dao = new MenuDao();
            Menu menu = dao.getMenuById(Id);
            return View(menu);
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new MenuDao().Delete(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult ChangeStatus(int id)
        {
            var result = new MenuDao().ChangeStatus(id);
            return Json(new
            {
                status = result
            });

        }

    }
}