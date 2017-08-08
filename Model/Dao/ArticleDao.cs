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
    public class ArticleDao
    {
        CompanyDbContext db = null;
        public ArticleDao()
        {
            db = new CompanyDbContext();
        }

        public int Insert(Article entity)
        {
            db.Articles.Add(entity);
            db.SaveChanges();
            return entity.Id;
        }

        public Article getArticleById(int Id)
        {
            return db.Articles.SingleOrDefault(x => x.Id == Id);
        }

        public IEnumerable<ArticleModel> ListAllPaging(string searchString, int page, int pageSize)
        {
            //IQueryable<Menu> model = db.Menus.Where(x => x.Deleted == false);

            var model = (from a in db.Articles
                         join b in db.Users
                         on a.CreatedBy equals b.Id
                         join c in db.Users
                         on a.ModifiedBy equals c.Id
                         join d in db.Menus
                         on a.MenuId equals d.Id
                         select new
                         {
                             Id = a.Id,
                             Title = a.Title,
                             MetaTitle = a.MetaTitle,
                             Content = a.Content,
                             menuName = d.Text,
                             usrCreate = b.UserName,
                             CreatedDate = a.CreatedDate,
                             usrEdit = c.UserName,
                             ModifiedDate = a.ModifiedDate,
                             Status = a.Status
                         }).AsEnumerable().Select(x => new ArticleModel()
                         {
                             Id = x.Id,
                             Title = x.Title,
                             MetaTitle = x.MetaTitle,
                             Content = x.Content,
                             menuName = x.menuName,
                             usrCreate = x.usrCreate,
                             CreatedDate = x.CreatedDate,
                             usrEdit = x.usrEdit,
                             ModifiedDate = x.ModifiedDate,
                             Status = x.Status
                         });
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Title.Contains(searchString) || 
                x.MetaTitle.Contains(searchString) || 
                //x.usrCreate.Contains(searchString) || 
                //x.usrEdit.Contains(searchString)||
                x.Content.Contains(searchString));
            }

            model = model.OrderByDescending(x => x.CreatedDate);
            return model.ToPagedList(page, pageSize);

        }

        public int TotalArticle(string searchString)
        {
            if (string.IsNullOrEmpty(searchString))
            {
                return db.Articles.Where(x => x.Deleted == false).Count();
            }
            else
            {
                return db.Articles.Where(x => x.Deleted == false).Where(x => x.Title.Contains(searchString) ||
                x.MetaTitle.Contains(searchString) ||
                x.Content.Contains(searchString)).Count();
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var article = db.Articles.Find(id);
                article.Deleted = true;
                article.ModifiedDate = DateTime.Now;
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
            var article = db.Articles.Find(id);
            article.Status = !article.Status;
            article.ModifiedDate = DateTime.Now;
            db.SaveChanges();
            return article.Status;
        }
    }
}
