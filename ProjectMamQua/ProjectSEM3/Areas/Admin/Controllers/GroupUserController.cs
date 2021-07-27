using System;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using PagedList;
using ProjectMamQua.Dao;
using ProjectMamQua.EF;
using ProjectSEM3.Common;

namespace ProjectSEM3.Areas.Admin.Controllers
{
    [AuthorizeController]
    public class GroupUserController : Controller
    {
        GroupUserDao db = new GroupUserDao();
        // GET: Admin/GroupUser
        public ActionResult Index(string searchString ,  int? page)
        {
            var model = new GroupUserDao();
            int pageSize = 15;
            int pageNumber = 1;
            return View(model.GetAllGroupUsers(searchString).ToPagedList(pageNumber ,pageSize));
        }
        [HttpPost]
        public JsonResult Create(string data)
        {
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            GroupUser model = javaScriptSerializer.Deserialize<GroupUser>(data);

            bool status = false;
            string mess = String.Empty;

            if (model.ID == 0)
            {

                var dao = db.Create(model);
                if (dao == 1)
                {
                    status = true;
                    mess = "Thêm mới thành công";
                }
                else
                {
                    status = false;
                    mess = "Tên đã tồn tại";
                }
           
            }
            else
            {

                status = db.Update(model);
                mess = "Cập nhập thất bại";
            }

            return Json(new
            {
                status = status,
                mess = mess
            });

        }
       

        [HttpPost]
        public JsonResult Delete(long id)
        {
            var model= new GroupUserDao().Delete(id);
            return Json(new
            {
                status = model
            });
        }

        [HttpPost]
        public JsonResult ViewDetail(long id)
        {
            var model = db.ViewDetail(id);
            return Json(new
            {
                status = true,
                id = model.ID
                ,
                name = model.Name
                ,
                des = model.Desciption
            });

        }


    }
}