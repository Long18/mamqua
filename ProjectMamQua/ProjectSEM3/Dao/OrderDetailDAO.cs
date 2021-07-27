using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Web;
using ProjectMamQua.EF;

namespace ProjectMamQua.Dao
{
    public class OrderDetailDAO
    {
        private MamQuaDbContext db = null;

        public OrderDetailDAO()
        {
            db = new MamQuaDbContext();
        }

        //Lấy danh sách các sản phẩn của đơn hàng
        public IEnumerable<OrderDetail> GetDetailOrder(long orderID)
        {
            IQueryable<OrderDetail> orders = db.OrderDetails.Where(x => x.OrderID == orderID);
            return orders;
        }

        //Insert order
        public bool Create(OrderDetail orderDetail)
        {
            try
            {
                //giảm số lượng khi đặt sản phẩm
                var product = db.Products.Find(orderDetail.ProductID);
                product.Quantity = product.Quantity - orderDetail.Quantity;
                db.OrderDetails.Add(orderDetail);
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