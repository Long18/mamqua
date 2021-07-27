using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjectMamQua.EF;

namespace ProjectMamQua.Dao
{
    public class GrantPermissionDAO
    {
        private MamQuaDbContext db = null;

        public GrantPermissionDAO()
        {
            db = new MamQuaDbContext();
        }

        //lay danh sach theo id
        public IEnumerable<Permission> GetByID(string id)
        {
            IQueryable<Permission> permissions = db.Permissions.Where(x => x.BusinessID == id);
            return permissions;
        }

        //lay doi tuong
        public Permission ViewDetail(long id)
        {
            return db.Permissions.Find(id);
        }

        public bool Edit(Permission permission)
        {
            try
            {
                var model = ViewDetail(permission.ID);
                model.Desciption = permission.Desciption;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public bool UpdatePermission(int permisstionId, int groupUserId)
        {

            //id mã quyền hạn , groupUserID mã quyền
            var grand = db.GrantPermissions.Find(permisstionId, groupUserId);//kiểm tra xem quyền đó có tồn tại hay không
            if (grand == null)//nếu không thì sẽ thêm vào db
            {
                GrantPermission grantPermission = new GrantPermission() { PermissionID = permisstionId, GroupUserID = groupUserId, Description = "" };
                db.GrantPermissions.Add(grantPermission);//thêm vào db
                db.SaveChanges();
                return true;
            }
            else//nếu có (hoặc bỏ quyền) thì xóa nó đi
            {
                db.GrantPermissions.Remove(grand);
                db.SaveChanges();
                return false;
            }
          
        }
    }
}