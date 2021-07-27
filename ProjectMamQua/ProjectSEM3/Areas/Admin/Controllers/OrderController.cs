using System;
using System.Web.Mvc;
using PagedList;
using ProjectMamQua.Dao;
using ProjectSEM3.Common;

namespace ProjectSEM3.Areas.Admin.Controllers
{
    [AuthorizeController]
    public class OrderController : Controller
    {
        OrderDAO db = new OrderDAO();
        // GET: Admin/Order
        public ActionResult Index(string searchString, int? page)
        {
            var model = db.GetAllOrder(searchString);
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(model.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Detail(long id)
        {
            
            var model = db.Detail(id);

            if (model.Status == 0)//kiểm tra xem nếu là chưa duyệt thì mới cập nhập lại tình trạng là đã duyệt
            {
                //cập nhập lại tình trạng 
                db.ChangeStatus(id, 1);//truyefn id và giá trị muốn cập nhập 2 là đã duyệt
            }
            return View(model);
        }

        [HttpPost]
        public JsonResult UpdateOrder(string ids , string status , string payment)
        {
            OrderDAO dao = new OrderDAO();
            var lstID = ids.Split(',');//chuyển chuỗi thành mảng
            
            bool res = true;
            foreach (var id in lstID)
            {
                long cv = Convert.ToInt64(id);
                var order = dao.Detail(cv);
                if (status=="")//kiểm tra nếu status hoặc payment rỗng thì gáng giá trị mặc định của order đó
                {
                    status = order.Status.ToString();
                }else if (payment == "")
                {
                    payment = order.PaymentID.ToString();
                }
                //tiến hành update order
                res = dao.updateStatusOrder(cv, Int32.Parse(status), Int32.Parse(payment));
            }

            return Json(new
            {
                status = res
            });
        }

        [HttpPost]
        public JsonResult Delete(string ids)
        {
            var model = new OrderDAO();
            var lstID = ids.Split(',');//chuyển chuỗi thành mảng
            bool res = true;
            foreach (var id in lstID)
            {
                long cv = Convert.ToInt64(id);
                res = model.Delete(cv);
            }
            return Json(new
            {
                status = res
            });
        }
    }
}