
using System;
using System.Web.Mvc;
using BotDetect.Web.Mvc;
using ProjectMamQua.DAO;
using ProjectSEM3.Areas.Admin.Models;
using ProjectSEM3.Common;
using ProjectSEM3.Models;

namespace ProjectSEM3.Areas.Admin.Controllers
{

    public class LoginController : Controller
    {
        // GET: Admin/Login
      
        public ActionResult Index()//lấy giá trị và  gáng giá trị cookie sử dụng request.cookie
        {
            var dao = new UserDAO();
            UserLogin userLogin = new UserLogin();

            //kiem tra co luu dang nhap thi gaf gia ri vao userlogin va chuyen vr trang dang nhap
            if (Request.Cookies["UserNameAdmin"] != null)
            {
                string cookieUsername = Request.Cookies["UserNameAdmin"].Value;
                
                var user = dao.GetUserString(cookieUsername);//lấy user trong db ra và gáng vào cookie
                if (user != null)//nếu cookie đó đúng là tài khoản thì 
                {
                    userLogin.UserID = user.ID;
                    userLogin.UserName =user.Username;
                    userLogin.GroupUserID = user.GroupUserID;
                    Session.Add("avatar", user.Avatar);
                    Session.Add("username", user.Username);
                    Session.Add("userID", user.ID);
                    Session[CommonConstants.USER_SESSION] = userLogin;
                    return RedirectToAction("Index", "Home");
                }
                return RedirectToAction("Index", "Login");

            }

            //nếu user vãn còn đằng nhập thì ko quay lại đưuọc trang login
            if (Session[CommonConstants.USER_SESSION] != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]

        public ActionResult Index(LoginModel model)
        {
            var dao = new UserDAO();
            var result = dao.Login(model.Username, Encryptor.Encrypt(model.Password));
            var userSession = new UserLogin();

            if (ModelState.IsValid)
            {
                //kiem tra co chon luu dang nhap

                //neu khong chon luu dang nhap se dang nhap binh thuong
                if (result == 1)//đăng nhập thành công
                {
                    var user = dao.GetUserString(model.Username);//lấy user trong db ra và gáng vào usersession
                    if (model.RememberMe)
                    {
                        Response.Cookies["UserNameAdmin"].Value = model.Username;//sử dụng respone để gáng giá trị cho null
                        Response.Cookies["UserNameAdmin"].Expires = DateTime.Now.AddDays(5);

                        userSession.UserID = user.ID;
                        userSession.UserName = user.Username;
                        userSession.GroupUserID = user.GroupUserID;
                        Session.Add("avatar" ,user.Avatar);
                        Session.Add("username", user.Username);
                        Session.Add("userID", user.ID);

                        Session.Add(CommonConstants.USER_SESSION, userSession);
                        //add nguyên đối tượng user vaf session
                        return RedirectToAction("Index", "Home");
                        //Response.Redirect(ReturnUrl);
                    }
                    else
                    {
                        userSession.UserID = user.ID;
                        userSession.UserName = user.Username;
                        userSession.GroupUserID = user.GroupUserID;
                        Session.Add("avatar", user.Avatar);
                        Session.Add("username", user.Username);
                        Session.Add("userID", user.ID);


                        Session.Add(CommonConstants.USER_SESSION, userSession);//add nguyên đối tượng user vaf session

                        return RedirectToAction("Index", "Home");
                        //Response.Redirect(ReturnUrl);
                    }


                }
                else if (result == -1)
                {
                    ModelState.AddModelError("", "Tài khoản không tồn tại");
                }
                else if (result == 0)
                {
                    ModelState.AddModelError("", "Tài khoản chưa được chấp nhận đăng nhập");
                }
                else if (result == 2)
                {
                    ModelState.AddModelError("", "Mật khẩu không đúng");
                }
                else if (result == 3)
                {
                    ModelState.AddModelError("", "Tài khoản không được truy cập vào trang quản trị");
                }
                else
                {
                    ModelState.AddModelError("", "Đăng nhập không thàng công");
                }
            }

            return View();
        }


        public ActionResult Logout()
        {
            Session[CommonConstants.USER_SESSION] = null;
            Session["avatar"] = null;
            Session["username"] = null;
            if (Response.Cookies["UserNameAdmin"] != null)
            {
                Response.Cookies["UserNameAdmin"].Expires = DateTime.Now.AddDays(-1);
            }

            return View("Index");
        }

        public ActionResult ForgotPassword()
        {
            return View();
        }
        /// <summary>
        /// Quên mật khẩu 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ForgotPassword(string email)
        {
            if (ModelState.IsValid)
            {
                var check = new UserDAO().find(email);
                if (check != null)
                {
                    var newpass = new UserDAO().CreatePassword(8);
                    var creat = new UserDAO().UpdatePass(email , Encryptor.Encrypt(newpass));
                    if (creat)
                    {
                        EmailTool emailTool = new EmailTool();
                        emailTool.SendMail(GetParent(email, newpass));
                        ModelState.AddModelError("","Đã gửi một email chứa mật khẩu tới tài khoản của bạn!");
                        return View();
                    }
                  
                }
            }
            return View();
        }
        /// <summary>
        /// Đổi mật khẩu
        /// </summary>
        /// <returns></returns>
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [CaptchaValidation("CaptchaCode", "registerCaptcha", "Mã xác nhập không đúng!")]
        public ActionResult ChangePassword(UserModel userModel)
        {
            
            UserDAO db = new UserDAO();

            if (ModelState.IsValid)
            {
                var user = db.GetUserString(userModel.Username);
                if (user != null)
                {
                    if (Decryptor.Decrypt(user.Password) == userModel.Password)//nếu đúng pass của tài khoản mới cập nhập
                    {
                        db.UpdatePass(user.Email, Encryptor.Encrypt(userModel.NewPassword));//Hàm cập nhập mật khẩu
                        TempData["Success"] = "Đổi mật khẩu thành công";
                        return RedirectToAction("Index","Home");
                    }
                    else
                    {
                        TempData["Error"] = "Mật khẩu cũ không tồn tại";
                        return View();
                    }
                }
            }
            TempData["Error"] = "Đổi mật khẩu thất bại";
            return View();
        }

        public EmailModel GetParent(string e , string pass)
        {
            string sub = "[Thông báo] Bạn đã lấy lại mật khẩu cho tài khoản";
            string bo = @"";
            bo += "Mật khẩu mới tài khoản của bạn là : <span style='color:red;'>" + pass + "</p>";
            EmailModel email = new EmailModel(e , sub, bo);
            return email;
        }
    }
}