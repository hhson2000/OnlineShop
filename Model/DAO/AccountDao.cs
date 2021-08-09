using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;
using PagedList;

namespace Model.DAO
{
    
    public class AccountDao
    {
        BaloShopDbContext db = null;

        public string Encryptor { get; private set; }

        public AccountDao()
        {
            db = new BaloShopDbContext();
        }
        public long Insert(Account entity)
        {
            db.Accounts.Add(entity);
            db.SaveChanges();
            return entity.Id;
        }

        public long InsertForFacebook(Account entity)
        {
            var user = db.Accounts.SingleOrDefault(x => x.Email == entity.Email);
            if (user == null)
            {
                db.Accounts.Add(entity);
                db.SaveChanges();
                return entity.Id;
            }
            else
            {
                return user.Id;
            }

        }

        public int Login(string email,string password)
        {
            var result = db.Accounts.SingleOrDefault(x => x.Email == email);
            if(result == null)
            {
                return 0;
            } else
            {
                if(result.Status == 0)
                {
                    return -1;
                } else
                {
                    if(result.Password == password)
                    {
                        return 1;
                    } else
                    {
                        return -2;
                    }
                }
            }
        }

        public Account GetById(string email)
        {
            return db.Accounts.SingleOrDefault(x => x.Email == email);
        }

        public IEnumerable<Account> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Account> model = db.Accounts;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Email.Contains(searchString));
            }

            return model.OrderByDescending(x => x.Create_Date).ToPagedList(page, pageSize);
        }

        public bool Delete(int id)
        {
            try
            {
                var user = db.Accounts.Find(id);
                db.Accounts.Remove(user);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public bool CheckEmail(string email)
        {
            return db.Accounts.Count(x => x.Email == email) > 0;
        }
    }
}
