using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjectMamQua.EF;

namespace ProjectMamQua.Dao
{
    public class ProducerDao
    {
        private MamQuaDbContext db = null;
        public ProducerDao()
        {
            db = new MamQuaDbContext();
        }
        public IEnumerable<Producer> GetAllProductProducers()
        {
            IQueryable<EF.Producer> lst = db.Producers.Where(x=>x.Status == true);
            return lst;
        }

        /// <summary>
        /// laays danh sach cac danh muc bai viets
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns></returns>
        public IEnumerable<Producer> GetAll(string searchString)
        {
            IQueryable<Producer> model = db.Producers.Where(x => x.Status == true).OrderByDescending(x => x.CreateDate);
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString));
            }
            return model;
        }
        /// <summary>
        /// lay danh sach cac muc trong thung rac 
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns></returns>
        public IEnumerable<Producer> GetAllRecycelBin(string searchString)
        {
            IQueryable<Producer> model = db.Producers.Where(x => x.Status == false).OrderByDescending(x => x.CreateDate);
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString));
            }
            return model;
        }

        /// <summary>
        /// them moi danh muc san pham
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public int Create(Producer entities)
        {
            var check = db.Producers.SingleOrDefault(x => x.Name == entities.Name);
            if (check == null)
            {
                entities.Status = true;
                entities.CreateDate = DateTime.Now;
                db.Producers.Add(entities);
                db.SaveChanges();
                return 1;
            }
            else
            {
                return -1;
            }
        }
        /// <summary>
        /// cap nhap danh muc san pham
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Update(Producer entity)
        {
            try
            {
                var producer = db.Producers.Find(entity.ID);
                producer.Name = entity.Name;
                producer.MetaTitle = entity.MetaTitle;

                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// lấy thông tin sản phẩm
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Producer ViewDetail(long id)
        {
            var pro = db.Producers.Find(id);
            return pro;
        }


        /// <summary>
        /// xóa tạm
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool ChangeStatus(long id)
        {
            var pro = ViewDetail(id);
            pro.Status = !pro.Status;
            db.SaveChanges();
            return true;
        }

        /// <summary>
        /// xóa tài khoản
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(long id)
        {
            //xóa sản phẩm trong database 
            //nếu thành công trả về true
            //nếu thất bại trả về false
            try
            {
                var prod = db.Producers.Find(id);
                db.Producers.Remove(prod);
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