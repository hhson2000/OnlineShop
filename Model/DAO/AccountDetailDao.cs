using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class AccountDetailDao
    {
        BaloShopDbContext db = null;

        public string Encryptor { get; private set; }

        public AccountDetailDao()
        {
            db = new BaloShopDbContext();
        }
        public long Insert(Account_Detail entity)
        {
            db.Account_Detail.Add(entity);
            db.SaveChanges();
            return entity.Id;
        }

        public long InsertForFacebook(Account_Detail entity)
        {
            var user = db.Account_Detail.SingleOrDefault(x => x.Name == entity.Name);
            if (user == null)
            {
                db.Account_Detail.Add(entity);
                db.SaveChanges();
                return entity.Id;
            }
            else
            {
                return user.Id;
            }

        }
    }
}
