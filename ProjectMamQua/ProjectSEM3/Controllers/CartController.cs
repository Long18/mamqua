using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using ProjectMamQua.Dao;
using ProjectMamQua.DAO;
using ProjectMamQua.EF;
using ProjectSEM3.Models;

namespace ProjectSEM3.Controllers
{
    public class CartController : Controller
    {
        private const string CartSession = "CartSession";
        // GET: Cart
        public ActionResult Index()
        {
            var cart = Session[CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return View(list);
        }

        public ActionResult Success()
        {
            return View();
        }

        public ActionResult PaypalPaymanet()
        {
            var s = PdtHolder.Success(Request.QueryString.Get("tx"));
            //khởi tạo biến order
            var order = new Order();
            var username = Session["UsernameMember"];
            if (username != null)
            {
                var model = new UserDAO().findByUsername(username.ToString());
                order.UserID = model.ID;
            }
            order.ShipCreateDate = DateTime.Now;
            order.ShipName = s.AddressName;
            order.ShipAddress = s.AddressState + "-" + s.AddressCountry + "-" + s.AddressStreet;
            order.ShipEmail = s.PayerEmail;
            order.Status = 0;
            order.PaymentID = 1;

            //insert order

            var id = new OrderDAO().Create(order);//trả về id của order
            try
            {
                var cart = (List<CartItem>)Session[CartSession];
                var db = new OrderDetailDAO();
                foreach (var item in cart)
                {
                    var orderDetail = new OrderDetail();
                    orderDetail.ProductID = item.ProductModel.ID;
                    orderDetail.OrderID = id;
                    if (item.ProductModel.Sale != null)
                    {
                        orderDetail.Price = item.ProductModel.Sale;
                    }
                    else
                    {
                        orderDetail.Price = item.ProductModel.Price;
                    }
                    orderDetail.Quantity = item.Quantity;
                    db.Create(orderDetail);
                    Session[CartSession] = null;
                }
                EmailTool emailTool = new EmailTool();
                emailTool.SendMail(GetParent(id));
            }
            catch (Exception)
            {
                throw;
            }
            return RedirectToAction("Success", "Cart");

        }
        
        [HttpGet]
        public ActionResult Payment()
        {
            var cart = Session[CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return View(list);
        }

        [HttpPost]
        public ActionResult Payment(string shipName, string shipPhone, string shipAddress, string shipEmail)
        {
            var order = new Order();
            order.ShipCreateDate = DateTime.Now;
            order.ShipName = shipName;
            order.ShipAddress = shipAddress;
            order.ShipEmail = shipEmail;
            order.ShipPhone = shipPhone;
            order.Status = 0;
            order.PaymentID = 2;

            //insert order

            var id = new OrderDAO().Create(order);//trả về id của order
            try
            {
                var cart = (List<CartItem>)Session[CartSession];
                var db = new OrderDetailDAO();
                foreach (var item in cart)
                {
                    var orderDetail = new OrderDetail();
                    orderDetail.ProductID = item.ProductModel.ID;
                    orderDetail.OrderID = id;
                    if (item.ProductModel.Sale != null)
                    {
                        orderDetail.Price = item.ProductModel.Sale;
                    }
                    else
                    {
                        orderDetail.Price = item.ProductModel.Price;
                    }
                    orderDetail.Quantity = item.Quantity;
                    db.Create(orderDetail);

                    Session[CartSession] = null;
                }
                EmailTool emailTool = new EmailTool();
                emailTool.SendMail(GetParent(id));

            }
            catch (Exception)
            {
                throw;
            }
            return RedirectToAction("Success", "Cart");
        }

        [HttpPost]
        public ActionResult PaymentUserLogin()
        {
            var username = Session["UsernameMember"];//lấy session gáng giá trị vào order
            var model = new UserDAO().findByUsername(username.ToString());
            var order = new Order();
            order.ShipCreateDate = DateTime.Now;
            order.ShipName = model.Name;
            order.ShipAddress = model.Address;
            order.ShipEmail = model.Email;
            order.ShipPhone = model.Phone;
            order.Status = 0;
            order.PaymentID = 2;
            order.UserID = model.ID;
            //insert order

            var id = new OrderDAO().Create(order);//trả về id của order
            try
            {
                var cart = (List<CartItem>)Session[CartSession];
                var db = new OrderDetailDAO();
                foreach (var item in cart)
                {
                    var orderDetail = new OrderDetail();
                    orderDetail.ProductID = item.ProductModel.ID;
                    orderDetail.OrderID = id;
                    if (item.ProductModel.Sale != null)
                    {
                        orderDetail.Price = item.ProductModel.Sale;
                    }
                    else
                    {
                        orderDetail.Price = item.ProductModel.Price;
                    }
                    orderDetail.Quantity = item.Quantity;
                    db.Create(orderDetail);
                    Session[CartSession] = null;
                }
                EmailTool emailTool = new EmailTool();
                emailTool.SendMail(GetParent(id));
            }
            catch (Exception)
            {
                throw;
            }
            return RedirectToAction("Success", "Cart");
        }

        
        public ActionResult AddToCart(long productId, int quantity)
        {
            var product = new ProductDAO().producrDetail(productId);
            var cart = Session[CartSession];

            //nếu cart đã tồn tại
            if (cart != null)
            {
                //ép lại giở hàng đã tồn tại thành list
                var list = (List<CartItem>)cart;
                if (list.Exists(x => x.ProductModel.ID == productId))
                {
                    foreach (var item in list)
                    {
                        //nếu sản phẩm trùng tăng số lượng sản phẩm đó lên
                        if (item.ProductModel.ID == productId)
                        {
                            item.Quantity += quantity;
                        }
                    }
                }//nếu không chứa add mới 
                else
                {
                    //thêm mối một giỏ hàng 
                    var item = new CartItem();
                    item.ProductModel = product;
                    item.Quantity = quantity;
                    list.Add(item);
                }
                //gán danh sách vào session
                Session[CartSession] = list;

            }
            else//chưa tồn tại
            {
                //thêm mối một giỏ hàng 
                var item = new CartItem();
                item.ProductModel = product;
                item.Quantity = quantity;
                var list = new List<CartItem>();
                list.Add(item);
                //gán danh sách vào session
                Session[CartSession] = list;
            }
            return RedirectToAction("Index");
        }

        public JsonResult Update(string cartModel)
        {
            var cart = new JavaScriptSerializer().Deserialize<List<CartItem>>(cartModel);
            var sessionCart = (List<CartItem>)Session[CartSession];//lấy danh sách các sản phẩm trong giỏ hàng hiện có

            foreach (var item in sessionCart)
            {//lặp lấy sản phảm update
                var itemCart = cart.SingleOrDefault(x => x.ProductModel.ID == item.ProductModel.ID);
                if (itemCart != null)
                {
                    item.Quantity = itemCart.Quantity;
                }
            }

            return Json(new
            {
                status = true
            });
        }

        public JsonResult DeleteAll()
        {
            Session[CartSession] = null;
            return Json(new
            {
                status = true
            });
        }

        public JsonResult Delete(long id)
        {
            var sessionCart = (List<CartItem>)Session[CartSession];//lấy danh sách các sản phẩm trong giỏ hàng hiện có
            sessionCart.RemoveAll(x => x.ProductModel.ID == id);
            Session[CartSession] = sessionCart;

            return Json(new
            {
                status = true
            });
        }

        public EmailModel GetParent(long id)
        {

            OrderDAO orderDao = new OrderDAO();
            var order = orderDao.Detail(id);
            decimal? total = 0;
            string sub ="[Thông báo] Chúng tôi xét duyệt đơn hàng của bạn";
            string bo = @"";
            bo += "Xin chào " + order.ShipName + ",<br><br><br>";
            bo += "<table style='border-collapse: collapse;border-color: #666666;border-width: 1px;color:#333333;'>";
            bo += "<tr>";
            bo += "<td style='background:#dedede;padding: 8px;border-width: 1px;border-color: #666666;'>Tên sản phẩm</td>";
            bo += "<td style='background:#dedede;padding: 8px;border-width: 1px;border-color: #666666;'>Giá</td>";
            bo += "<td style='background:#dedede;padding: 8px;border-width: 1px;border-color: #666666;'>Số lượng</td>";
            bo += "<td style='background:#dedede;padding: 8px;border-width: 1px;border-color: #666666;'>Tổng</td>";
            bo += "</tr>";

            foreach (var item in order.OrderDetails)
            {
                bo += "<tr >";
                bo += "<td style='padding: 8px;border-style: solid;border-color: #666666;background-color: #ffffff;border-width: 1px;'>" + item.Product.Name + "</td>";
                bo += "<td style='padding: 8px;border-style: solid;border-color: #666666;background-color: #ffffff;border-width: 1px;'>" + item.Price + "</td>";
                bo += "<td style='padding: 8px;border-style: solid;border-color: #666666;background-color: #ffffff;border-width: 1px;'>" + item.Quantity + "</td>";
                bo += "<td style='padding: 8px;border-style: solid;border-color: #666666;background-color: #ffffff;border-width: 1px;'>" + item.Quantity*item.Price + "</td>";
                bo += "</tr>";
                total += item.Price*item.Quantity;
            }
            bo += "<tr>";
            bo += "<td colspan='3' style='padding: 8px;border-style: solid;border-color: #666666;background-color: #ffffff;border-width: 1px;'>Tổng tiền</td>";
            bo += "<td style='padding: 8px;border-style: solid;border-color: #666666;background-color: #ffffff;border-width: 1px;'>" + total + "VNĐ" +"</td>";
            bo += "</tr>";
            bo += "</table><br/>";
            bo += "Cảm ơn bạn đã đặt hàng chúng tôi sẽ liên hệ với bạn trong thời gian sớm nhất ";
            EmailModel email = new EmailModel(order.ShipEmail, sub, bo);
            return email;
        }


        
        [ChildActionOnly]
        public ActionResult CartHeader()
        {
            var cart = Session[CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return PartialView(list);
        }
        
        [ChildActionOnly]
        public ActionResult CartRight()
        {
            var cart = Session[CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return PartialView(list);
        }
    }
}