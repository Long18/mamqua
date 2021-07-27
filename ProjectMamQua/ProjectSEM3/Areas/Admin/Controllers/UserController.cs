using System;
using System.Web.Mvc;
using PagedList;
using ProjectMamQua.Dao;
using ProjectMamQua.DAO;
using ProjectMamQua.EF;
using ProjectSEM3.Common;

namespace ProjectSEM3.Areas.Admin.Controllers
{
    //[CustomActionFilter]
    [AuthorizeController]
    public class UserController : Controller
    {


        // GET: Admin/User
        public ActionResult Index(string searchString, int? page)
        {
            UserDAO _dao = new UserDAO();
            var list = _dao.GetAllUser(searchString);
            setViewBag();
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            ViewBag.SearchString = searchString;
            return View(list.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Create()
        {
            setViewBag();//hiển thị tên danh mục thay vì hiển thị ID
            return View();

        }
        /// <summary>
        /// xem thông tin tài khoản
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult UserProfile(int id)
        {
            UserDAO dao = new UserDAO();
            return View(dao.ViewDetail(id));
        }

        /// <summary>
        /// thêm tài khoản
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                var encrypt = Encryptor.Encrypt(user.Password);
                user.Password = encrypt;
                var db = new UserDAO();
                var dao = db.Create(user);
    
                if (dao == 1)
                {
                    TempData["Success"] = "Thêm tài khoản thành công";
                    setViewBag();
                    return RedirectToAction("Index", "User");
                }
                else if (dao == 0)
                {
                    TempData["Error"] = "Email này đã tồn tại";
                    setViewBag();
                    return View("Create");
                }
                else if (dao == -1)
                {
                    TempData["Error"] = "Tài khoản này đã tồn tại";
                    setViewBag();
                    return View("Create");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm tài khoản thất bại");
                }

            }

            TempData["Error"] = "Thêm tài khoản thất bại";
            setViewBag();
            return View();

        }

        //get


        public ActionResult RecycelBin(string searchString, int? page)
        {
            var user = new UserDAO();
            var model = user.GetAllRecycelBin(searchString);
            int pageNumber = (page ?? 1);
            int pageSize = 10;
            ViewBag.SearchString = searchString;
            return View(model.ToPagedList(pageNumber, pageSize));
        }

        //GET
        public ActionResult Edit(int id)
        {
            var dao = new UserDAO();
            var model = dao.ViewDetail(id);
            setViewBag();
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDAO();
                var model = dao.Update(user);
                if (model)
                {
                    TempData["Success"] = "Cập nhập tài khoản thành công";
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    TempData["Error"] = "Cập nhập tài khoản thất bại";
                    return View("Edit");
                }

            }
            setViewBag();
            return View("Edit");
        }


        /// <summary>
        /// để hiện thị tên danh mục sản phẩm và danh mục đang chọn
        /// </summary>
        /// <param name="selectedID"></param>
        public void setViewBag(long? selectedID = null)
        {
            var dao = new GroupUserDao();
            ViewBag.GroupUserID = new SelectList(dao.GetAllGroupUsers(""), "ID", "Name", selectedID);

        }




        /// <summary>
        /// cập nhập quyền có thể login
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult ChangeActive(long id)
        {
            var dao = new UserDAO().ChangeActive(id);
            return Json(new
            {
                status = dao
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
            var dao = new UserDAO().ChangeStatus(id);
            return Json(new
            {
                status = dao
            });

        }

        [HttpPost]
        public JsonResult Delete(long id)
        {
            var dao = new UserDAO().Delete(id);
            return Json(new
            {
                status = dao
            });

        }

        /// <summary>
        /// xóa trong db
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult DeleteSelectedDb(string ids)
        {
            var model = new UserDAO();
            var lstID = ids.Split(',');
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
        /// xóa mảng thành phần được chọn trong db
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult DeleteSelected(string ids)
        {
            var model = new UserDAO();
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

        [HttpPost]
        public JsonResult DeleteSelectedRecycelBin(string ids)
        {
            var model = new UserDAO();
            var lstID = ids.Split(',');
            bool res = true;
            foreach (var id in lstID)
            {
                long cv = Convert.ToInt64(id);
                res = model.ChangeStatusTrue(cv);
            }
            return Json(new
            {
                status = res
            });
        }



    }
}