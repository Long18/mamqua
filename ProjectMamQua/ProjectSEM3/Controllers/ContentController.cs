using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using ProjectMamQua.Dao;

namespace ProjectSEM3.Controllers
{
    public class ContentController : Controller
    {
        ContentDAO db = new ContentDAO();
        // GET: Content
        /// <summary>
        /// danh sách bài viết
        /// </summary>
        /// <param name="searchString"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public ActionResult Index(string searchString, int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(db.GetAllContents(searchString).ToPagedList(pageNumber ,pageSize));
        }

        /// <summary>
        /// xem bài viết
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Detail(long id)
        {
            var model = db.ViewDetail(id);
            return View(model);
        }

        public ActionResult Category(long id, int? page)
        {
            var contentCategory = new ContentCategoryDao().ViewDetail(id);
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            ViewBag.metaTitle = contentCategory.MetaTitle;
            ViewBag.id = contentCategory.ID;
            ViewBag.name = contentCategory.Name;
            var model = db.GetAllContentCategory(id);
            return View(model.ToPagedList(pageNumber, pageSize));
        }

        /// <summary>
        /// load cây danh mục bài viết
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult CategoryContent()
        {
            var listCategory = new ContentCategoryDao().GetAll("");
            return PartialView(listCategory);
        }
    }
}