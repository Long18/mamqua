using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Web;
using System.Web.WebPages;
using ProjectMamQua.EF;

namespace ProjectMamQua.Dao
{

    public class GroupUserDao
    {
        private MamQuaDbContext db = null;

        public GroupUserDao()
        {
            db = new MamQuaDbContext();
        }

        /// <summary>
        /// lấy danh sách sản phẩm
        /// </summary>
        /// <returns></returns>
        public IEnumerable<GroupUser> GetAllGroupUsers(string searchString)
        {
            IQueryable<GroupUser> model = db.GroupUsers.OrderBy(x=>x.ID);
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString));
            }
            return model;

        }

        public int Create(GroupUser user)
        {
            var check = db.GroupUsers.SingleOrDefault(x => x.Name == user.Name);
            if (check == null)
            {
                db.GroupUsers.Add(user);
                db.SaveChanges();
                return 1;
            }
            return -1;
        }

        public GroupUser ViewDetail(long id)
        {
            return db.GroupUsers.Find(id);
        }

        public bool Update(GroupUser user)
        {
            try
            {
                var model = db.GroupUsers.Find(user.ID);
                model.Name = user.Name;
                model.Desciption = user.Desciption;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public bool Delete(long id)
        {
            var gr = ViewDetail(id);
            try
            {
                //nếu là 4 quyền mặc địng không đưuọc xóa
                if (gr.ID == 1 || gr.ID == 2 || gr.ID == 3 || gr.ID == 4)
                {
                    return false;
                }
                //nếu không phải trong 4 quyền đó OKE
                db.GroupUsers.Remove(gr);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }


        }
     
    }
}