using Model.EF;
using Model.Encryptor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;

namespace Model.Dao
{
    public class UserDao
    {
        CompanyDbContext db = null;
        public UserDao()
        {
            db = new CompanyDbContext();
        }
        public long Insert(User entity)
        {
            db.Users.Add(entity);
            db.SaveChanges();
            return entity.Id;
        }

        public bool Update(User entity)
        {
            try
            {
                var user = db.Users.Find(entity.Id);
                user.Name = entity.Name;
                user.Phone = entity.Phone;
                user.Address = entity.Address;
                user.Email = entity.Email;
                user.Birthday = entity.Birthday;
                user.Avatar = entity.Avatar;
                user.ModifiedDate = DateTime.Now;
                user.ModifiedBy = entity.ModifiedBy;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public User getUserByUsername(string userName)
        {
            return db.Users.SingleOrDefault(x => x.UserName == userName);
        }

        public User getUserById(long Id)
        {
            return db.Users.SingleOrDefault(x => x.Id == Id);
        }

        public bool Login(string userName, string passWord)
        {
            var result = db.Users.SingleOrDefault(x => x.UserName == userName);
            if (result == null || result.Deleted || result.Status == false)
            {
                return false;
            }
            else
            {
                bool isValidated = BCrypt.CheckPassword(passWord, result.Password);
                if (isValidated)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

        }

        public IEnumerable<User> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<User> model = db.Users.Where(x => x.Deleted == false);
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.UserName.Contains(searchString) || x.Name.Contains(searchString) || x.Email.Contains(searchString));
            }
            model = model.OrderByDescending(x => x.Id);
            return model.ToPagedList(page, pageSize);
            //return model.OrderByDescending(x => x.Id).ToPagedList(page, pageSize);
        }

        public int TotalUser(string searchString)
        {
            if (string.IsNullOrEmpty(searchString)) { 
            return db.Users.Where(x => x.Deleted == false).Count();
            }
            else
            {
                return db.Users.Where(x => x.Deleted == false).Where(x => x.UserName.Contains(searchString) || x.Name.Contains(searchString) || x.Email.Contains(searchString)).Count();
            }
        }

        public bool Delete(long id)
        {
            try
            {
                var user = db.Users.Find(id);
                user.Deleted = true;
                user.ModifiedDate = DateTime.Now;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool ChangeStatus(long id)
        {
            var user = db.Users.Find(id);
            user.Status = !user.Status;
            user.ModifiedDate = DateTime.Now;
            db.SaveChanges();
            return user.Status;
        }

    }
}
