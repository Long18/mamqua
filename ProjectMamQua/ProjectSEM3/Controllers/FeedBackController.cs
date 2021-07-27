using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectMamQua.Dao;
using ProjectMamQua.EF;

namespace ProjectSEM3.Controllers
{
    public class FeedBackController : Controller
    {
        // GET: FeedBack
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create(FeedBack feedBack)
        {
            if (ModelState.IsValid) 
            {
                feedBack.CreateDate = DateTime.Now;
                var model = new FeedBackDAO().Insert(feedBack);
                if (model)
                {
                    TempData["Success"] = "Gửi thành công";
                    return View("Index");
                }
                else
                {
                    TempData["Error"] = "Gửi không thành công";
                    return View("Index");
                }
            }
            TempData["Error"] = "Gửi không thành công";
            return View("Index");
        }

    }
}