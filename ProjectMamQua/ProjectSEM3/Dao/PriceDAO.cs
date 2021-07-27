using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjectMamQua.EF;

namespace ProjectMamQua.Dao
{
    public class PriceDAO
    {
        MamQuaDbContext db = null;

        public PriceDAO()
        {
            db = new MamQuaDbContext();
        }

        public IEnumerable<Price> GetAll(string searchString)
        {
            IQueryable<Price> model = db.Prices.OrderByDescending(x=>x.CreateDate);
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Product.Name.Contains(searchString));
            }
            return model;
        }

        public bool Create(Price price ,string createBy)
        {
            try
            {
                price.CreateBy = createBy;
                price.CreateDate = DateTime.Now;
                var model = db.Prices.Add(price);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public Price ViewDetail(long id)
        {
            var model = db.Prices.Find(id);
            return model;
        }

        /// <summary>
        /// lấy ra giá mới nhất để so sánh
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Price ViewDetailNew(long id)
        {
            IQueryable<Price> model = db.Prices.OrderByDescending(x => x.CreateDate);
            return model.Where(x => x.ProductID == id).Take(1).FirstOrDefault();
        }



        public bool Edit(Price price)
        {
            try
            {
                var model = ViewDetail(price.Id);
                model.Price1 = price.Price1;
                model.ProductID = price.ProductID;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public bool Delete(long id)
        {
            try
            {
                var model = ViewDetail(id);
                db.Prices.Remove(model);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
    }
}