

using System;
using System.Web.Mvc;
using PagedList;
using ProjectMamQua.Dao;
using ProjectMamQua.EF;
using ProjectSEM3.Common;

namespace ProjectSEM3.Areas.Admin.Controllers
{
    [AuthorizeController]
    public class ContentController : Controller
    {

        private ContentDAO db = new ContentDAO();

        // GET: Admin/Content
        public ActionResult Index(string searchString, int? page)
        {
            var model = db.GetAllContents(searchString);
            ViewBag.SearchString = searchString;
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(model.ToPagedList(pageNumber, pageSize));
        }

        /// <summary>
        /// để hiện thị tên danh mục sản phẩm và danh mục đang chọn
        /// </summary>
        /// <param name="selectedID"></param>
        public void setViewBag(long? selectedID = null)
        {
            var dao = new ContentCategoryDao();
            //để thay giá trị id bằng name , selectId dùng để lấy vị trí đang chọn
            ViewBag.ContentCategoryID = new SelectList(dao.GetAllContentCategory(), "ID", "Name", selectedID);
        }


        [HttpGet]
        [AllowAnonymous]
        public ActionResult Create()
        {
            setViewBag();
            return View();
        }
        /// <summary>
        /// thêm tài khoản
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(Content entities)
        {
            if (ModelState.IsValid)
            {
                var db = new ContentDAO();

                var dao = db.Create(entities , Session["username"].ToString());

                if (dao > 0)
                {
                    TempData["Success"] = "Thêm thành công";
                    setViewBag();
                    return RedirectToAction("Index", "Content");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm thất bại ");
                }
            }
            TempData["Error"] = "Thêm thất bại";
            setViewBag();
            return View();

        }

        //cap nhap 
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = db.ViewDetail(id);
            setViewBag();
            return View(model);
        }
        //cap nhap
        [HttpPost]
        public ActionResult Edit(Content entities)
        {
            if (ModelState.IsValid)
            {
                var dao = new ContentDAO();
                var model = dao.Update(entities);
                if (model)
                {
                    TempData["Success"] = "Cập nhập thành công";
                    return RedirectToAction("Index", "Content");
                }
                else
                {
                    TempData["Error"] = "Cập nhập thất bại";
                    return View("Edit");
                }
            }
            setViewBag();
            return View();
        }



        /// <summary>
        /// hàm này dùng để xóa tạm
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult ChangeStatus(long id)
        {
            var dao = new ContentDAO().ChangeStatus(id);
            return Json(new
            {
                status = dao
            });

        }

        /// <summary>
        /// cập nhập quyền có thể login
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult ChangeActive(long id)
        {
            var dao = new ContentDAO().ChangeActive(id);
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
            var model = new ContentDAO();
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
            var prod = new ContentDAO();
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
            var dao = new ContentDAO().Delete(id);//xóa trong db
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
            var model = new ContentDAO();
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
            var model = new ContentDAO();
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


    }
}