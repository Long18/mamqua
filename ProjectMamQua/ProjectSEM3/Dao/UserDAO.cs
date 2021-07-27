using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.WebPages;
using Microsoft.Ajax.Utilities;
using ProjectMamQua.EF;


namespace ProjectMamQua.DAO
{
    public class UserDAO
    {

        private MamQuaDbContext _entities = null;

        public UserDAO()
        {
            _entities = new MamQuaDbContext();
        }
        /// <summary>
        /// Them user co kiem tra user ton tại
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public long Create(User user)
        {
            var check = _entities.Users.SingleOrDefault(x => x.Username == user.Username);
            var checkEmail = find(user.Email);
            if (check == null)
            {
                if (checkEmail == null)
                {

                    if (user.GroupUserID == null)
                    {
                        user.GroupUserID = 1;
                    }
                    if (user.Avatar == null)
                    {
                        user.Avatar = "/Data/images/Avatar/avatar_default.png";
                    }

                    user.Status = true;
                    user.CreateDate = DateTime.Now;
                    user.Active = true;
                  //  code kierm tra loi EntityValidate lỗi ràng buỗjc
                    try
                    {
                        _entities.Users.Add(user);
                        _entities.SaveChanges();
                        // code của bạn
                    }
                    catch (DbEntityValidationException ex)
                    {
                        // Retrieve the error messages as a list of strings.
                        var errorMessages = ex.EntityValidationErrors
                                .SelectMany(x => x.ValidationErrors)
                                .Select(x => x.ErrorMessage);

                        // Join the list to a single string.
                        var fullErrorMessage = string.Join("; ", errorMessages);

                        // Combine the original exception message with the new one.
                        var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                        // Throw a new DbEntityValidationException with the improved exception message.
                        throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
                    }

                
                    return 1;
                    
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return -1;
            }

        }

        //tìm user
        public User find(string email)
        {
            var user = _entities.Users.SingleOrDefault(x => x.Email == email);
            return user;
        }

        //tìm user
        public User findByUsername(string username)
        {
            var user = _entities.Users.SingleOrDefault(x => x.Username == username);
            return user;
        }


        /// <summary>
        /// xóa tài khoản
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(long id)
        {
            try
            {
                var prod = _entities.Users.Find(id);
                _entities.Users.Remove(prod);
                _entities.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        /// <summary>
        /// lấy danh sách tài khoản
        /// </summary>
        /// <returns></returns>
        public IEnumerable<User> GetAllUser(string searchString)
        {
            IQueryable<User> model = _entities.Users.Where(x => x.Status == true).OrderByDescending(x => x.CreateDate);
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Username.Contains(searchString) || x.Name.Contains(searchString) || x.Email.Contains(searchString));
            }
            return model;
        }
        public List<User> GetAllUser()
        {
            List<User> model = _entities.Users.Where(x => x.Status == true).OrderByDescending(x => x.CreateDate).ToList();
            return model;
        }

        public IEnumerable<User> GetAllRecycelBin(string searchString)
        {
            IQueryable<User> model = _entities.Users.Where(x => x.Status == false).OrderByDescending(x=>x.CreateDate);
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString));
            }
            return model;
        }

        /// <summary>
        /// cập nhập tài khoản
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Update(User entity)
        {
            try
            {
                var user = _entities.Users.Find(entity.ID);
                user.Name = entity.Name;
                user.Phone = entity.Phone;
                user.GroupUser = entity.GroupUser;
                user.Address = entity.Address;
                user.Email = entity.Email;
                user.BirthDay = entity.BirthDay;
                user.ModyfiedDate = DateTime.Now;
                user.Genre = entity.Genre;
                user.Avatar = entity.Avatar;
                user.Active = entity.Active;
                user.GroupUserID = entity.GroupUserID;
                _entities.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdatePass( string email ,string pass)
        {
            try
            {
                var user = find(email);
                user.Password = pass;
                _entities.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// lấy thông tin sản phẩm
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public User ViewDetail(int id)
        {
            var user = _entities.Users.Find(id);
            return user;
        }

        /// <summary>
        /// cập nhập trạng thái quyền đăng nhập
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool ChangeActive(long id)
        {
            var user = _entities.Users.Find(id);
            user.Active = !user.Active;
            _entities.SaveChanges();
            return user.Active;
        }

        /// <summary>
        /// xóa tạm
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool ChangeStatus(long id)
        {
            

            try
            {
                var user = _entities.Users.Find(id);
                user.Status = !user.Status;
                _entities.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
            return true;
        }
        /// <summary>
        /// set tat ca status ve true
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool ChangeStatusTrue(long id)
        {
            try
            {
                var user = _entities.Users.Find(id);
                user.Status = true;
                _entities.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
            
        }
        /// <summary>
        /// tạo password random
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public string CreatePassword(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }

        public int Login(string username, string password)
        {
            //trả về null nếu user không tồn tại
            var result = _entities.Users.SingleOrDefault(x => x.Username == username);
            if (result == null)
            {
                return -1; //tài khoản không tồn tại
            }
            else
            {
                if (result.Status == false || result.Active == false)
                {
                    return 0; //không có quyền truy cập hoặc bị xóa tạm
                }
                else
                {
                    if (result.GroupUserID == 2 || result.GroupUserID == 3 || result.GroupUserID == 4)
                    {
                        if (result.Password == password)
                        {
                            return 1; //đăng nhập thành công
                        }
                        else
                        {
                            return 2;//mật khẩu tài khoản sai
                        }
                    }
                    else
                    {
                        return 3;
                    }
                }
            }
        }

        /// <summary>
        /// lay user
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public User GetUserString(string userName)
        {
            return _entities.Users.SingleOrDefault(x => x.Username == userName);
        }

        ///
        /// 
        /// 
        /// 
        /// 
        /// ----------------------------CLIENT--------------------------
        /// 
        /// 
        /// 
        /// 
        /// 
        /// 

        public int LoginClient(string username, string password)
        {
            //trả về null nếu user không tồn tại
            var result = _entities.Users.SingleOrDefault(x => x.Username == username);
            if (result == null)
            {
                return -1; //tài khoản không tồn tại
            }
            else
            {
                if (result.Status == false || result.Active == false)
                {
                    return 0; //không có quyền truy cập hoặc bị xóa tạm
                }
                else
                {
                    if (result.GroupUserID == 1)
                    {
                        if (result.Password == password)
                        {
                            return 1; //đăng nhập thành công
                        }
                        else
                        {
                            return 2;//mật khẩu tài khoản sai
                        }
                    }
                    else
                    {
                        return 3;//không được đăng nhập ở đây
                    }
                }
            }
        }
    }
}