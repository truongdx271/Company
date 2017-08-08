using Company.Areas.Admin.Models;
using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class MenuDao
    {
        CompanyDbContext db = null;
        public MenuDao()
        {
            db = new CompanyDbContext();
        }

        public Menu getMenuById(int id)
        {
            return db.Menus.SingleOrDefault(x => x.Id == id);
        }

        public List<Menu> getAllMenu()
        {
            return db.Menus.Where(x => x.Status == true && x.Deleted == false).ToList();
        }

        public List<Menu> getListMenu()
        {
            return db.Menus.Where(x => x.IsSubMenu == false).ToList();
        }

        public IEnumerable<Menu> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Menu> model = db.Menus.Where(x => x.Deleted == false);
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Text.Contains(searchString) || x.MetaTitle.Contains(searchString));
            }
            model = model.OrderBy(x => x.Id);
            return model.ToPagedList(page, pageSize);
            //return model.OrderByDescending(x => x.Id).ToPagedList(page, pageSize);
        }

        public List<Menu> getListSubMenu(int menuId)
        {
            var model = (from a in db.Menus
                         join b in db.Menu_Sub
                         on a.Id equals b.MenuId
                         where b.MenuId == menuId
                         select new
                         {
                             Id = b.Id,
                             Text = a.Text,
                             MetaTitle = a.MetaTitle,
                             Link = a.Link,
                             DisplayOrder = a.DisplayOrder,
                             IsSubMenu = a.IsSubMenu
                         }).AsEnumerable().Select(x => new Menu()
                         {
                             Id = x.Id,
                             Text = x.Text,
                             MetaTitle = x.MetaTitle,
                             DisplayOrder = x.DisplayOrder,
                             Link = x.Link,
                             IsSubMenu = x.IsSubMenu
                         });
            return model.OrderBy(x=>x.DisplayOrder).ToList();
        }

        public int TotalMenu(string searchString)
        {
            if (string.IsNullOrEmpty(searchString))
            {
                return db.Menus.Where(x => x.Deleted == false).Count();
            }
            else
            {
                return db.Menus.Where(x => x.Deleted == false).Where(x => x.Text.Contains(searchString) || x.MetaTitle.Contains(searchString)).Count();
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var menu = db.Menus.Find(id);
                menu.Deleted = true;
                menu.ModifiedDate = DateTime.Now;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool ChangeStatus(int id)
        {
            var menu = db.Menus.Find(id);
            menu.Status = !menu.Status;
            menu.ModifiedDate = DateTime.Now;
            db.SaveChanges();
            return menu.Status;
        }

    }
}
