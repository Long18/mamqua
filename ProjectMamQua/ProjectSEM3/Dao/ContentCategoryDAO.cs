using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjectMamQua.EF;

namespace ProjectMamQua.Dao
{
    public class ContentCategoryDao
    {
        private MamQuaDbContext db = null;
        public ContentCategoryDao()
        {
            db = new MamQuaDbContext();
        }
        /// <summary>
        /// lay danh sach do ra list
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ContentCategory> GetAllContentCategory()
        {
            IQueryable<ContentCategory> lst = db.ContentCategories.Where(x=>x.Status == true);
            return lst;
        }
        /// <summary>
        /// laays danh sach cac danh muc bai viets
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns></returns>
        public IEnumerable<ContentCategory> GetAll(string searchString)
        {
            IQueryable<ContentCategory> model = db.ContentCategories.Where(x => x.Status == true).OrderByDescending(x => x.CreateDate);
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
        public IEnumerable<ContentCategory> GetAllRecycelBin(string searchString)
        {
            IQueryable<ContentCategory> model = db.ContentCategories.Where(x => x.Status == false).OrderByDescending(x => x.CreateDate);
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
        public int Create(ContentCategory entities)
        {
            var check = db.ContentCategories.SingleOrDefault(x => x.Name == entities.Name);
            if (check == null)
            {
                entities.Status = true;
                entities.CreateDate = DateTime.Now;
                db.ContentCategories.Add(entities);
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
        public bool Update(ContentCategory entity)
        {
            try
            {
                var contenCategory = db.ContentCategories.Find(entity.ID);
                contenCategory.Name = entity.Name;
                contenCategory.MetaTitle = entity.MetaTitle;

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
        public ContentCategory ViewDetail(long id)
        {
            var pro = db.ContentCategories.Find(id);
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
                var prod = db.ContentCategories.Find(id);
                db.ContentCategories.Remove(prod);
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