using System;
using System.Web.Mvc;
using PagedList;
using ProjectMamQua.Dao;
using ProjectSEM3.Common;

namespace ProjectSEM3.Areas.Admin.Controllers
{
    [AuthorizeController]
    public class FeedBackController : Controller
    {
        FeedBackDAO db = new FeedBackDAO();
        // GET: Admin/FeedBack
        public ActionResult Index(string searchString ,int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            var model = db.GetAll(searchString).ToPagedList(pageNumber , pageSize);

            return View(model);
        }

        /// <summary>
        /// xóa bằng ajax
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Delete(long id)
        {
            var model = db.Delete(id);
            return Json(new
            {
                status = model
            });
        }


        /// <summary>
        /// xóa mảng thành phần được chọn trong db
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult DeleteSelectedDb(string ids)
        {
            var lstID = ids.Split(',');//chuyển chuỗi thành mảng
            bool res = true;
            foreach (var id in lstID)
            {
                long cv = Convert.ToInt64(id);
                res = db.Delete(cv);
            }
            return Json(new
            {
                status = res
            });
        }

        public JsonResult Detail(long id)
        {
            var model = new FeedBackDAO().ViewDetail(id);
            return Json(new
            {
                status = true,
                model = model
            },JsonRequestBehavior.AllowGet);
        }
    }
}