using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using PagedList;
using ProjectMamQua.Dao;

namespace ProjectSEM3.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index(int page = 1 , int pageSize= 9)
        {
            int totalRecord = 0;
            var model = new ProductDAO().getAllProductClient(ref totalRecord, page, pageSize);
            
            int maxPage = 5;
            int totalPage = 0; //tổng số trang tính ra 

            totalPage =(int)Math.Ceiling((double)(totalRecord / pageSize)) + 1;

            //slide
            ViewBag.slideTop = new SlideDAO().GetSlideView(2);
            ViewBag.slideButtom = new SlideDAO().GetSlideView(3);

            ViewBag.TotalRecord = totalRecord;
            ViewBag.PageIndex = page;
            ViewBag.TotalPage = totalPage;
            ViewBag.MaxPage = maxPage;
            ViewBag.Frist = 1;
            ViewBag.Last = totalPage;
            ViewBag.Next = page + 1;
            ViewBag.Prev = page - 1;

            return View(model);
        }

        public ActionResult ProductcategoryMamQua(int page = 1, int pageSize = 9 ,int id = 0 )
        {
            int totalRecord = 0;
            var model = new ProductDAO().getAllProductProducer(ref totalRecord, page, pageSize, id , 8);

            int maxPage = 5;
            int totalPage = 0; //tổng số trang tính ra 

            totalPage = (int)Math.Ceiling((double)(totalRecord / pageSize)) + 1;

            ViewBag.TotalRecord = totalRecord;
            ViewBag.PageIndex = page;
            ViewBag.TotalPage = totalPage;
            ViewBag.MaxPage = maxPage;
            ViewBag.Frist = 1;
            ViewBag.Last = totalPage;
            ViewBag.Next = page + 1;
            ViewBag.Prev = page - 1;

            //slide
            ViewBag.slideTop = new SlideDAO().GetSlideView(2);
            ViewBag.slideButtom = new SlideDAO().GetSlideView(3);

            return View(model);
        }

       
        public ActionResult ProductcategoryPromote(int page = 1, int pageSize = 9, int id = 0)
        {
            int totalRecord = 0;
            var model = new ProductDAO().getAllProductProducer(ref totalRecord, page, pageSize, id, 9);

            int maxPage = 5;
            int totalPage = 0; //tổng số trang tính ra 

            totalPage = (int)Math.Ceiling((double)(totalRecord / pageSize)) + 1;

            ViewBag.TotalRecord = totalRecord;
            ViewBag.PageIndex = page;
            ViewBag.TotalPage = totalPage;
            ViewBag.MaxPage = maxPage;
            ViewBag.Frist = 1;
            ViewBag.Last = totalPage;
            ViewBag.Next = page + 1;
            ViewBag.Prev = page - 1;

            //slide
            ViewBag.slideTop = new SlideDAO().GetSlideView(2);
            ViewBag.slideButtom = new SlideDAO().GetSlideView(3);

            return View(model);
        }


        [ChildActionOnly]
        public ActionResult NewProduct()
        {
            ProductDAO db = new ProductDAO();
            var model = db.getProductNew(8);
            
            
            return PartialView(model);
        }

        [ChildActionOnly]
        public ActionResult ProductFeature()
        {
            ProductDAO db = new ProductDAO();
            var model = db.getProductFeature();
            return PartialView(model);
        }

        [ChildActionOnly]
        public ActionResult SaleProduct()
        {
            ProductDAO db = new ProductDAO();
            var model = db.getProductSale();
            return PartialView(model);
        }

        [ChildActionOnly]
        public ActionResult ProductsRelated(int id)
        {
            ProductDAO db = new ProductDAO();
            var model = db.ProductsRelated(id);
            return PartialView(model);
        }

        public ActionResult Detail(long id)
        {
            var model = new ProductDAO().producrDetail(id);
            List<string> listImages = new List<string>();
            string images = model.MoreImage;
            if (images != null)
            {
                XElement xImages = XElement.Parse(images);
                foreach (XElement element in xImages.Elements())
                {
                    listImages.Add(element.Value);
                }
            }
            ViewBag.listImages = listImages;
            return View(model);
        }

        public JsonResult loadImages(long id)
        {
            ProductDAO dao = new ProductDAO();
            var product = dao.ViewDetail(id);
            var images = product.MoreImage;
            List<string> listImages = new List<string>();
            if (images != null)
            {
                XElement xImages = XElement.Parse(images);
                foreach (XElement element in xImages.Elements())
                {
                    listImages.Add(element.Value);
                }
            }
            return Json(new
            {
                data = listImages
            }, JsonRequestBehavior.AllowGet);
        }

        [ChildActionOnly]
        public ActionResult _Category()
        {
            ProducerDao db = new ProducerDao();
            var model = db.GetAll("");
            return PartialView(model);
        }
    }
}