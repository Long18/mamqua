using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectMamQua.Dao;

namespace ProjectSEM3.Controllers
{
    public class SearchController : Controller
    {
        [ChildActionOnly]
        public ActionResult SearchForm()
        {
            return PartialView("_SearhFormPartial");
        }

        public ActionResult CategoryList()
        {
            var cat = new ProductCategoryDao();
            var lst = cat.GetAll("").ToList();
            return PartialView("_CategoryListPartial", lst);
        }

        [HttpPost]
        public ActionResult SearchByName(string term)
        {
            var db = new ProductDAO().getProductNew(10000);
            var model = db.Where(x => x.Name.Contains(term));
            var splist = (from p in model orderby p.ID descending select new { p.ID, p.Name, p.Price, p.Image ,p.Sale ,p.Quantity,p.MetaTitle}).Take(5);
            return Json(splist, JsonRequestBehavior.AllowGet);
        }



    }
}