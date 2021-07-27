using System.Web.Mvc;
using System.Web.Script.Serialization;
using ProjectMamQua.Dao;
using ProjectMamQua.EF;
using ProjectSEM3.Common;

namespace ProjectSEM3.Areas.Admin.Controllers
{
    [AuthorizeController]
    public class PermissionController : Controller
    {
        // GET: Admin/Permission
        public ActionResult Index(string id)
        {
            var model = new PermissionDao().GetByID(id);
            return View(model);
        }


        [HttpPost]
        public JsonResult Edit(string data)
        {
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            var model = javaScriptSerializer.Deserialize<Permission>(data);
            var result = new PermissionDao().Edit(model);
            return Json(new
            {
                status = result
            });
        }

        [HttpPost]
        public JsonResult Delete(long id)
        {
            var model = new PermissionDao().Delete(id);
            return Json(new
            {
                status = model
            });
        }

        [HttpPost]
        public JsonResult ViewDetail(long id)
        {
            var model = new PermissionDao().ViewDetail(id);
            return Json(new
            {
                status = true,
                id = model.ID,
                name = model.PermissionName,
                des = model.Desciption
            });
        }
      


    }
}