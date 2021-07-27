using System;
using System.Collections.Generic;
using System.Linq;
using ProjectMamQua.EF;
using ProjectSEM3.Areas.Admin.Models.BusinessModel;

namespace ProjectMamQua.Dao
{
    public class BusinessDao
    {
        private MamQuaDbContext db = null;

        public BusinessDao()
        {
            db = new MamQuaDbContext();
        }

        public IEnumerable<Business> GetAllBusinesses()
        {
            IQueryable<Business> model = db.Businesses;
            return model;
        }


        //lấy tất cả các controler và action insert vào db
        public bool UpdateBusiess(string namespances)
        {
            try
            {
                ReflectionController re = new ReflectionController();
                List<Type> listControllerType = re.GetController(namespances);//truyen vào namepaces mà bạn muốn lấy controller
                                                                              //chứa controller
                List<string> listControllerOld = db.Businesses.Select(p => p.ID).ToList();//lấy ra danh sách tên controller trong bảng
                                                                                          //chứa action
                List<string> listPermisstionOld = db.Permissions.Select(p => p.PermissionName).ToList();//lấy ra danh sach tên trong bảng
                foreach (var c in listControllerType)
                {
                    //gáng giá trị các controller đang được lấy của namespaces cho lớp 
                    if (!listControllerOld.Contains(c.Name))
                    {
                        Business business = new Business()
                        {
                            ID = c.Name,
                            Name = "Chưa có mô tả"
                        };
                        //thêm vào bẳng
                        db.Businesses.Add(business);
                    }


                    List<string> listPermission = re.GetActions(c);//lấy danh sách các action trong controller namespaces truyền vào

                    foreach (var p in listPermission)
                    {
                        if (!listPermisstionOld.Contains(c.Name + '-' + p))//kiểm tra nếu trong db chưa có thì mới thêm vào
                        {
                            Permission permission = new Permission()
                            {
                                PermissionName = c.Name + "-" + p,
                                Desciption = "Chưa có mô tả",
                                BusinessID = c.Name
                            };
                            db.Permissions.Add(permission);
                        }
                    }

                }
                db.SaveChanges();
                return true;


            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
        /// <summary>
        /// cập nhạp
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public bool Edit(Business entities)
        {
            try
            {
                var business = db.Businesses.Find(entities.ID);
                business.Name = entities.Name;
                db.SaveChanges();
                return true;

            }
            catch (Exception)
            {
                return false;
                throw;
            }
            
        }
        /// <summary>
        /// lấy đối tượng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Business ViewDetail(string id)
        {
            var business = db.Businesses.Find(id);
            return business;
        }
        /// <summary>
        /// xóa business
        /// xóa các permission hiện có của business 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(string id)
        {
            try
            {
                var model = db.Businesses.Find(id);
                var delPer = db.Permissions.Where(x => x.BusinessID == id);//lấy danh sách các action của controller
                foreach (var p in delPer)
                {
                    db.Permissions.Remove(p);//xóa các action
                }
                db.Businesses.Remove(model);//xóa controller
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
