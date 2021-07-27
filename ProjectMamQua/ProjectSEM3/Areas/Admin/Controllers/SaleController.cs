

using System;
using System.Web.Mvc;
using PagedList;
using ProjectMamQua.Dao;
using ProjectMamQua.EF;
using ProjectSEM3.Common;

namespace ProjectSEM3.Areas.Admin.Controllers
{
    [AuthorizeController]
    public class SaleController : Controller
    {
        SaleDAO db = new SaleDAO();
        // GET: Admin/Sale
        public ActionResult Index(string searchString , int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            var model = db.GetAll(searchString);
            return View(model.ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/Price
        public ActionResult Create()
        {
            setViewBag();
            return View();
        }

        [HttpPost]
        public ActionResult Create(Sale sale)
        {
            var model = db.Create(sale);

            if (model)
            {
                TempData["Success"] = "Thêm giảm giá thành công";
                return RedirectToAction("Index", "Sale");
            }
            else
            {
                setViewBag();
                TempData["Error"] = "Sửa giá thất bại";
                return View("Create");
            }

        }
        //GET/Edit/id
        public ActionResult Edit(long id)
        {
            var model = db.ViewDetail(id);
            setViewBag();
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(Sale sale)
        {
            var model = db.Edit(sale);
            if (model)
            {
                TempData["Success"] = "Sửa giá thành công";
                setViewBag();
                return RedirectToAction("Index", "Sale");
            }
            else
            {
                TempData["Error"] = "Sửa giá thất bại";
                setViewBag();
                return View("Edit");
            }
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

        /// <summary>
        /// để hiện thị tên danh mục sản phẩm và danh mục đang chọn
        /// </summary>
        /// <param name="selectedID"></param>
        public void setViewBag(long? selectedID = null)
        {
            var dao = new ProductDAO();
            //để thay giá trị id bằng name , selectId dùng để lấy vị trí đang chọn
            ViewBag.ProductID = new SelectList(dao.GetAllProducts(""), "ID", "Name", selectedID);
        }
    }
}