using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectMamQua.DAO;

namespace ProjectSEM3.Controllers
{
    public class UserController : Controller
    {
        UserDAO _userDao = new  UserDAO();
        // GET: User
        public ActionResult Detail(int id)
        {
            return View(_userDao.ViewDetail(id));
        }
    }
}