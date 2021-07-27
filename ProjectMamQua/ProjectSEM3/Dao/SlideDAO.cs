using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using ProjectMamQua.EF;

namespace ProjectMamQua.Dao
{
    public class SlideDAO
    {
        MamQuaDbContext db =null;

        public SlideDAO()
        {
            db = new MamQuaDbContext();
        }

        public IEnumerable<Slide> GetAll()
        {
            IQueryable<Slide> list = db.Slides.OrderByDescending(x => x.CreateDate);
            return list;
        }
        public IEnumerable<Slide> GetSlideView(long id)
        {
            IQueryable<Slide> list = db.Slides.Where(x=>x.Status == true).OrderByDescending(x => x.CreateDate).Take(4);
            return list;
        }

        public bool Create(Slide slide)
        {
            try
            {
                slide.CreateDate = DateTime.Now;
                slide.Status = true;
                db.Slides.Add(slide);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
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
            //return true;
        }

        public bool Edit(Slide slide)
        {
            try
            {
                var model = Detail(slide.ID);
                model.Image = slide.Image;
                model.Status = slide.Status;
                model.Description = slide.Description;
                model.TypeID = slide.TypeID;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public Slide Detail(long id)
        {
            return db.Slides.SingleOrDefault(x => x.ID == id);
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
                var prod = db.Slides.Find(id);
               
                db.Slides.Remove(prod);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                var message = e.Message;
                return false;
                throw;
            }
        }
        /// <summary>
        /// xóa tạm
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool ChangeStatus(long id)
        {
            var slide = db.Slides.Find(id);
            slide.Status = !slide.Status;
            db.SaveChanges();
            return slide.Status;
        }
    }
}