using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjectMamQua.EF;

namespace ProjectMamQua.Dao
{
    public class ContentDAO
    {
        private MamQuaDbContext db= null;

        public ContentDAO()
        {
            db = new MamQuaDbContext();
        }

        /// <summary>
        /// lấy danh sách sản phẩm
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Content> GetAllContents(string searchString)
        {
            IQueryable<Content> model = db.Contents.Where(x => x.Status == true).OrderByDescending(x => x.CreateDate);
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString));
            }
            return model;
        }

        public IEnumerable<Content> GetAllRecycelBin(string searchString)
        {
            IQueryable<Content> model = db.Contents.Where(x => x.Status == false).OrderByDescending(x => x.CreateDate);
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString));
            }
            return model;
        }


        public IEnumerable<Content> GetAllContentCategory(long id)
        {
            IQueryable<Content> model = db.Contents.Where(x => x.Status == true && x.ContentCategoryID == id).OrderByDescending(x => x.CreateDate);
            
            return model;
        }

        /// <summary>
        /// Them user co kiem tra user ton tại
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public long Create(Content content ,string username)
        {
            try
            {
                if (content.Image == null)
                {
                    content.Image = "/Data/images/Product/product_default.png";
                }
                content.ViewCount = 1;
                content.Active = true;
                content.CreateBy = username;
                content.Status = true;
                content.CreateDate = DateTime.Now;
                db.Contents.Add(content);
                db.SaveChanges();
                return 1;
            }
            catch (Exception)
            {
                return -1;
                throw;
            }
               

        }

        /// <summary>
        /// cập nhập tài khoản
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Update(Content entity)
        {
            try
            {
                var content = db.Contents.Find(entity.ID);
                content.Name = entity.Name;
                content.ContentCategoryID = entity.ContentCategoryID;
                content.Name = entity.Name;
                content.Image = entity.Image;
                content.Status = entity.Status;
                content.ModyfiedDate = DateTime.Now;
                content.Detail = entity.Detail;
                content.Description = entity.Description;
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
        public Content ViewDetail(long id)
        {
            var model = db.Contents.Find(id);
            model.ViewCount += 1;
            db.SaveChanges();
            return model;
        }


        /// <summary>
        /// xóa tạm
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool ChangeStatus(long id)
        {
            var pro = db.Contents.Find(id);
            pro.Status = !pro.Status;
            db.SaveChanges();
            return true;
        }

        /// <summary>
        /// cập nhập trạng thái quyền đăng nhập
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool ChangeActive(long id)
        {
             //try
            //{
            //    db.Slides.Add(slide);
            //    db.SaveChanges();
            //    // code của bạn
            //}
            //catch (DbEntityValidationException ex)
            //{
            //    // Retrieve the error messages as a list of strings.
            //    var errorMessages = ex.EntityValidationErrors
            //            .SelectMany(x => x.ValidationErrors)
            //            .Select(x => x.ErrorMessage);

            //    // Join the list to a single string.
            //    var fullErrorMessage = string.Join("; ", errorMessages);

            //    // Combine the original exception message with the new one.
            //    var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

            //    // Throw a new DbEntityValidationException with the improved exception message.
            //    throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            //}
            var model = db.Contents.Find(id);
            model.Active = !model.Active;
            db.SaveChanges();
            return model.Active;
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
                var prod = db.Contents.Find(id);
                db.Contents.Remove(prod);
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