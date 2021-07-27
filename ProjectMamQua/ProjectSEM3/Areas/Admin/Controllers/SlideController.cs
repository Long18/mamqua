using System;
using System.Web.Mvc;
using PagedList;
using ProjectMamQua.Dao;
using ProjectMamQua.EF;
using ProjectSEM3.Common;

namespace ProjectSEM3.Areas.Admin.Controllers
{
    [AuthorizeController]
    public class SlideController : Controller
    {
        // GET: Admin/Slide
        public ActionResult Index(int? page)
        {
            var model = new SlideDAO().GetAll();
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(model.ToPagedList(pageNumber,pageSize));
        }

        public ActionResult Create()
        {
            setViewBag();
            return View();
        }

        [HttpPost]
        public ActionResult Create(Slide slide)
        {
            if (ModelState.IsValid)
            {
                setViewBag();
                var model = new SlideDAO().Create(slide);
                if (model)
                {
                    TempData["Success"] = "Thêm thành công";
                    return RedirectToAction("Index","Slide");
                }
                else
                {
                    TempData["Error"] = "Thêm thất bại";
                    setViewBag();
                    return View();
                }
            }
            setViewBag();
            return View();
        }

        public ActionResult Edit(long id)
        {
            setViewBag();
            var model = new SlideDAO().Detail(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(Slide slide)
        {
            if (ModelState.IsValid)
            {
                setViewBag();
                var model = new SlideDAO().Edit(slide);
                if (model)
                {
                    TempData["Success"] = "Cập nhập thành công";
                    return RedirectToAction("Index", "Slide");
                }
                else
                {
                    TempData["Error"] = "Cập nhập thất bại";
                    setViewBag();
                    return View();
                }
            }
            setViewBag();
            return View();
        }

        public void setViewBag(long? selectedID = null)
        {
            var dao = new SlideTypeDAO();
            //để thay giá trị id bằng name , selectId dùng để lấy vị trí đang chọn
            ViewBag.TypeID = new SelectList(dao.GetAll(), "ID", "Name", selectedID);
        }

          /// <summary>
        /// xóa mảng thành phần được chọn trong db
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult DeleteSelectedDb(string ids)
        {
            var model = new SlideDAO();
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
        /// xóa trong db
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Delete(long id)
        {
            var dao = new SlideDAO().Delete(id);//xóa trong db
            return Json(new
            {
                status = dao//trả về giá trị cho ajax true false
            });

        }

        /// <summary>
        /// hàm này dùng để xóa tạm
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult ChangeStatus(long id)
        {
            var dao = new SlideDAO().ChangeStatus(id);
            return Json(new
            {
                status = dao
            });

        }
    }
}