

using System.Linq;
using System.Web.Mvc;
using ProjectMamQua.EF;

namespace ProjectSEM3.Areas.Admin.Controllers
{
   
	public class HomeController : Controller
	{
		// GET: Admin/Home
 
		public ActionResult Index()
		{
			MamQuaDbContext db = new MamQuaDbContext();
			ViewBag.CountUser = db.Users.Count();
			ViewBag.CountProduct = db.Products.Count();
			ViewBag.CountOrder = db.Orders.Count();
			ViewBag.CountContent = db.Contents.Count();
			ViewBag.CountUserAdmin = db.Users.Count(x => x.GroupUserID==2);
			return View();
		}

		public ActionResult NotificationAuthorize()
		{
			return View();
		}


	   
	}


}