using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjectMamQua.EF;

namespace ProjectMamQua.Dao
{
    public class SaleDAO
    {
        MamQuaDbContext db = null;

        public SaleDAO()
        {
            db = new MamQuaDbContext();
        }

        public IEnumerable<Sale> GetAll(string searchString)
        {
            IQueryable<Sale> model = db.Sales.OrderByDescending(x=>x.ID);
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Product.Name.Contains(searchString));
            }
            return model;
        }

        public bool Create(Sale price)
        {
            try
            {
                var model = db.Sales.Add(price);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public Sale ViewDetail(long id)
        {
            var model = db.Sales.Find(id);
            return model;
        }

        /// <summary>
        /// lấy ra giá mới nhất để so sánh
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Sale ViewDetailNew(long id)
        {
            IQueryable<Sale> model = db.Sales.OrderByDescending(x => x.ID);
            return model.Where(x => x.ProductID == id).Take(1).FirstOrDefault();
        }

        public bool Edit(Sale price)
        {
            try
            {
                var model = ViewDetail(price.ID);
                model.Price = price.Price;
                model.ProductID = price.ProductID;
                model.BeginDate = price.BeginDate;
                model.EndDate = price.EndDate;
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
                db.Sales.Remove(model);
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