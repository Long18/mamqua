
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using PagedList;
using ProjectMamQua.Dao;
using ProjectMamQua.EF;
using ProjectSEM3.Common;

namespace ProjectSEM3.Areas.Admin.Controllers
{
    [AuthorizeController]
    public class ProductController : Controller
    {


        /// <summary>
        /// để hiện thị tên danh mục sản phẩm và danh mục đang chọn
        /// </summary>
        /// <param name="selectedID"></param>
        public void setViewBag(long? selectedID = null)
        {
            var dao = new ProductCategoryDao();
            //để thay giá trị id bằng name , selectId dùng để lấy vị trí đang chọn
            ViewBag.ProductCategoryID = new SelectList(dao.GetAllProductCategories(), "ID", "Name", selectedID);
        }
        public void setProducer(long? selectedID = null)
        {
            var dao = new ProducerDao();
            ViewBag.ProducerID = new SelectList(dao.GetAllProductProducers(), "ID", "Name", selectedID);
        }
        // GET: Admin/Product
        public ActionResult Index(string searchString, int? page)
        {
            var model = new ProductDAO().GetAllProducts(searchString);
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            ViewBag.SearchString = searchString;
            setViewBag();//để hiện thị name thay vì id
            setProducer();//để hiện thị name thay vì id
            return View(model.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Create()
        {
            setViewBag();
            setProducer();//hiển thị tên danh mục thay vì hiển thị ID
            return View();

        }

        /// <summary>
        /// thêm tài khoản
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(Product product, IEnumerable<HttpPostedFileBase> MoreImage, long price, long? sale, DateTime? beginDate, DateTime? endDate)
        {
            if (ModelState.IsValid)
            {
                //láy hình ảnh thêm vao trong thư mục data
                //mục thêm nhiều hình ảnh
                int i = 0;
                string path = "/Data/images/Product/";
                XElement xElement = new XElement("Images");//khổi tạo xml có node là Images
                if (MoreImage.First() != null)
                {
                    foreach (var file in MoreImage)
                    {
                        if (file.ContentLength > 0)
                        {
                            var fileName = Path.GetFileName(file.FileName); //lấy tên hình ảnh
                            string[] name = fileName.Split('.'); //tách để đổi tên hình ảnh
                            name[0] = product.MetaTitle + "item_" + i++; //đổi tên sản phẩm
                            var rename = name[0] + "." + name[1]; //gáng lại vào chuỗi
                            var filePath = Path.Combine(Server.MapPath(path), rename);
                            file.SaveAs(filePath);
                            xElement.Add(new XElement("Image", path + rename));
                        }
                    }
                }

                //gáng hình vào object product
                product.MoreImage = xElement.ToString();

                var db = new ProductDAO();

                var dao = db.Create(product);

                if (dao > 0)
                {
                    TempData["Success"] = "Thêm sản phẩm thành công";
                    //tạo đối tượng giá
                    var prices = new Price();
                    prices.Price1 = price;
                    prices.ProductID = dao;
                    new PriceDAO().Create(prices, Session["username"].ToString());
                    //them giam giá
                    if (sale != null)
                    {
                        var sales = new Sale();
                        sales.Price = sale;
                        sales.ProductID = dao;
                        if (beginDate == null)
                        {
                            sales.BeginDate = DateTime.Now;
                        }
                        else
                        {
                            sales.BeginDate = (DateTime)beginDate;
                        }
                        if (endDate == null)
                        {
                            sales.EndDate = DateTime.Now.AddMonths(+1);
                        }
                        else
                        {
                            sales.EndDate = (DateTime)endDate;
                        }

                        new SaleDAO().Create(sales);
                    }
                    setViewBag();
                    setProducer();
                    return RedirectToAction("Index", "Product");
                }
                else if (dao == -1)
                {
                    TempData["Error"] = "Sản phẩm đã tồn tại";
                    setViewBag();
                    setProducer();
                    return View("Create");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm sản phẩm thất bại ");
                }

            }

            TempData["Error"] = "Thêm sản phẩm thất bại";
            setViewBag();
            setProducer();
            return View();

        }

        public ActionResult Edit(int id)
        {
            var model = new ProductDAO().ViewDetail(id);
            var sale = new SaleDAO().ViewDetailNew(model.ID);
            ViewBag.price = new PriceDAO().ViewDetailNew(model.ID);
            if (sale != null)
            {
                ViewBag.sale = sale;
            }
            setViewBag();
            setProducer();
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(Product product, IEnumerable<HttpPostedFileBase> MoreImage, long? price, long? sale, DateTime? beginDate, DateTime? endDate)
        {
            if (ModelState.IsValid)
            {
                //láy hình ảnh thêm vao trong thư mục data
                //mục thêm nhiều hình ảnh
                int i = 0;
                string path = "/Data/images/Product/";
                XElement xElement = new XElement("Images");//khổi tạo xml có node là Images
                if (MoreImage.First() != null)
                {
                    foreach (var file in MoreImage)
                    {
                        if (file.ContentLength > 0)
                        {
                            var fileName = Path.GetFileName(file.FileName);//lấy tên hình ảnh
                            string[] name = fileName.Split('.');//tách để đổi tên hình ảnh
                            name[0] = product.MetaTitle + "-item_" + i++;//đổi tên sản phẩm
                            var rename = name[0] + "." + name[1];//gáng lại vào chuỗi
                            var filePath = Path.Combine(Server.MapPath(path), rename);
                            file.SaveAs(filePath);
                            xElement.Add(new XElement("Image", path + rename));
                        }
                    }
                    //gáng hình vào object product
                    product.MoreImage = xElement.ToString();
                }



                var dao = new ProductDAO();
                var model = dao.Update(product);
                if (model)
                {
                    TempData["Success"] = "Cập nhập sản phẩm thành công";
                    //nếu giá mà thay đổi sé thêm mới
                    //cập nhập giá 
                    var prices = new PriceDAO().ViewDetailNew(product.ID);
                    var sales = new SaleDAO().ViewDetailNew(product.ID);
                    if (prices.Price1 != price && price != null)
                    {
                        var priceNew = new Price();
                        priceNew.Price1 = price;
                        priceNew.ProductID = product.ID;
                        new PriceDAO().Create(priceNew, Session["username"].ToString());
                    }
                    if (sales != null)
                    {
                        if (sales.Price != sale && sale != null)
                        {
                            var saleNew = new Sale();
                            saleNew.Price = sale;
                            saleNew.ProductID = product.ID;
                            saleNew.BeginDate = (DateTime)beginDate;
                            saleNew.EndDate = (DateTime)endDate;
                            new SaleDAO().Create(saleNew);
                        }
                    }
                    else
                    {
                        var saleNew = new Sale();
                        saleNew.Price = sale;
                        saleNew.ProductID = product.ID;
                        saleNew.BeginDate = (DateTime)beginDate;
                        saleNew.EndDate = (DateTime)endDate;
                        new SaleDAO().Create(saleNew);
                    }



                    return RedirectToAction("Index", "Product");
                }
                else
                {
                    TempData["Error"] = "Cập nhập sản phẩm thất bại";
                    return View("Edit");
                }
            }
            setViewBag();
            setProducer();
            TempData["Error"] = "Cập nhập sản phẩm thất bại";
            return View("Edit");
        }



        /// <summary>
        /// hàm này dùng để xóa tạm
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult ChangeStatus(long id)
        {
            var dao = new ProductDAO().ChangeStatus(id);
            return Json(new
            {
                status = dao
            });

        }

        /// <summary>
        /// cập nhập lại status theo mảng
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>

        [HttpPost]
        public JsonResult DeleteSelected(string ids)
        {
            var model = new ProductDAO();
            var lstID = ids.Split(',');
            bool res = true;
            foreach (var id in lstID)
            {
                long cv = Convert.ToInt64(id);
                res = model.ChangeStatus(cv);
            }
            return Json(new
            {
                status = res
            });
        }


        //thùng rác
        //GET 
        public ActionResult RecycelBin(string searchString, int? page)
        {
            var prod = new ProductDAO();
            var model = prod.GetAllRecycelBin(searchString);
            int pageNumber = (page ?? 1);
            int pageSize = 10;
            ViewBag.SearchString = searchString;
            return View(model.ToPagedList(pageNumber, pageSize));
        }

        /// <summary>
        /// xóa trong db
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Delete(long id)
        {
            var dao = new ProductDAO().Delete(id);//xóa trong db
            return Json(new
            {
                status = dao//trả về giá trị cho ajax true false
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
            var model = new ProductDAO();
            var lstID = ids.Split(',');//chuyển chuỗi thành mảng
            bool res = true;
            foreach (var id in lstID)
            {
                long cv = Convert.ToInt64(id);
                res = model.Delete(cv);
            }
            return Json(new
            {
                status = res
            });
        }
        /// <summary>
        /// cập nhập mảng status
        /// </summary>
        /// <param name="ids"></param>
        /// <returns>bool</returns>
        [HttpPost]
        public JsonResult DeleteSelectedRecycelBin(string ids)
        {
            var model = new ProductDAO();
            var lstID = ids.Split(',');
            bool res = true;
            foreach (var id in lstID)
            {
                long cv = Convert.ToInt64(id);
                res = model.ChangeStatus(cv);
            }
            return Json(new
            {
                status = res
            });
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

    }


}