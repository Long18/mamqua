using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ProjectMamQua.Dao;
using ProjectMamQua.EF;
using ProjectSEM3.Areas.Admin.Models.BusinessModel;
using ProjectSEM3.Common;

namespace ProjectSEM3.Areas.Admin.Controllers
{
    [AuthorizeController]
    public class GrantPermissionController : Controller
    {
        public ActionResult Index(int id)
        {

            MamQuaDbContext db = new MamQuaDbContext();
            //lấy tất cả các controller trog db ra
            var listControl = db.Businesses.AsEnumerable();
            //tạo ra danh sách list dropdown
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var item in listControl)
            {
                //add tất cả vào list item với key và value
                items.Add(new SelectListItem()
                {
                    Text = item.Name,
                    Value = item.ID
                });
            }
            //gáng list items vào viewbag
            ViewBag.items = items;
            //lấy ra tất cả các quyền đã được gáng cho người dùng
            var listGranted = from g in db.GrantPermissions
                              join p in db.Permissions on g.PermissionID equals p.ID
                              where g.GroupUserID == id
                              select new SelectListItem()
                              {
                                  Text = p.Desciption,
                                  Value = p.ID.ToString()
                              };
            ViewBag.listGranted = listGranted;//gáng list hiện có vào view bag
            Session["groupUserID"] = id;
            var userGrant = db.GroupUsers.Find(id);//tìm kiếm quyền đó
            ViewBag.userGrant = userGrant.Desciption;//gáng đối tượng GroupUser đó vào viewbag

            return View();
        }

        /// <summary>
        /// lấy danh sách quyền đưuọc cấp cho người dùng
        /// </summary>
        /// <param name="groupUserID"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetPermission(string id, int groupUserID)
        {
	        MamQuaDbContext db = new MamQuaDbContext();
            //láy danh sách quyền đã được cấp
            var listGranted = (from g in db.GrantPermissions
                               join p in db.Permissions on g.PermissionID equals p.ID//lấy những cái groupid của controller(businessID)
                               where g.GroupUserID == groupUserID && p.BusinessID == id
                               select new PermissionAction
                               {
                                   PermissionID = p.ID,
                                   PermissionName = p.PermissionName,
                                   Desciption = p.Desciption,
                                   IsGranted = true
                               }).ToList();
            //lấy tất cả các permission của controller hiện có db
            var listpermisstion = from p in db.Permissions
                                  where p.BusinessID == id
                                  select new PermissionAction
                                  {
                                      PermissionID = p.ID,
                                      PermissionName = p.PermissionName,
                                      Desciption = p.Desciption,
                                      IsGranted = false
                                  };
            //lấy các id của peerrmission đã được gán cho người dùng
            var listPermissionID = listGranted.Select(x => x.PermissionID);
            //isgranted = false
            foreach (var item in listpermisstion)
            {//view tất cả ra bằng mảng listGranted 
                //điều kiện true false ở đây
                if (!listPermissionID.Contains(item.PermissionID))//nếu người dùng đã có permimssuonid mặc định là true 
                    listGranted.Add(item);//nếu chưa thì add permissioni = false vào trong mảng listGranted
            }
            return Json(listGranted.OrderBy(x => x.Desciption), JsonRequestBehavior.AllowGet);
        }

        //update các permission

        [HttpPost]
        public JsonResult UpdatePermission(int id, int groupUserID)
        {
            var model = new GrantPermissionDAO().UpdatePermission(id, groupUserID);

            return Json(new
            {
                status = model
            });
        }


    }
}