using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectMamQua.EF;

namespace ProjectSEM3.Common
{
    public class AuthorizeController : ActionFilterAttribute
    {


        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (HttpContext.Current.Session[CommonConstants.USER_SESSION] == null)
            {
                filterContext.Result = new RedirectResult("/Admin/Login/Index");
                return;
                
            }

            UserLogin userLogin = (UserLogin) HttpContext.Current.Session[CommonConstants.USER_SESSION];
            var groupId = userLogin.GroupUserID;
            //UserControler-Index
            string actionname = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName + "Controller-" +
                                filterContext.ActionDescriptor.ActionName;

            //kiểm tra có phải là admin
            MamQuaDbContext db =new MamQuaDbContext();
            var admin = db.Users.SingleOrDefault(x => x.GroupUserID== 2 && x.ID == userLogin.UserID);
            //nếu là admin thì không cần kiểm tra
            if (admin != null)
            {
                return;
            }

            //lấy tên các permission(Usercontroler-index) được gáng cho tài khoản
            var listPermission = from p in db.Permissions
                join g in db.GrantPermissions on p.ID equals g.PermissionID
                where g.GroupUserID == groupId
                select p.PermissionName;
            //nếu tài khoản ko có quyền trong danh sách được cấp thì failed
            if (!listPermission.Contains(actionname))
            {
                filterContext.Result = new RedirectResult("/Admin/Home/NotificationAuthorize");
                return;
            }
        }

       
    }
}