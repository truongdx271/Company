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

        public User getUserByUsername(string userName)
        {
            return db.Users.SingleOrDefault(x => x.UserName == userName);
        }

        public bool Login(string userName, string passWord)
        {
            var result = db.Users.SingleOrDefault(x => x.UserName == userName);
            if (result == null)
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
            IQueryable<User> model = db.Users;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.UserName.Contains(searchString) || x.Name.Contains(searchString));
            }
            model = model.OrderByDescending(x => x.Id);
            return model.ToPagedList(page, pageSize);
            //return model.OrderByDescending(x => x.Id).ToPagedList(page, pageSize);
        }
    }
}
