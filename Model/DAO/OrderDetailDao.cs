using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class OrderDetailDao
    {
        BaloShopDbContext db = null;
        public OrderDetailDao()
        {
            db = new BaloShopDbContext();
        }

        public bool Insert(Order_Detail detail)
        {
            try
            {
                db.Order_Detail.Add(detail);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;

            }
        }
    }
}
