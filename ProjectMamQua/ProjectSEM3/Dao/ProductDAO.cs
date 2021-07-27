using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Web.Hosting;
using System.Xml.Linq;
using ProjectMamQua.EF;
using ProjectSEM3.Models;

namespace ProjectMamQua.Dao
{

    public class ProductDAO
    {
        private MamQuaDbContext db = null;

        public ProductDAO()
        {
            db = new MamQuaDbContext();
        }

        
        /// lấy danh sách sản phẩm
        public IEnumerable<Product> GetAllProducts(string searchString)
        {
            IQueryable<Product> model = db.Products.Where(x => x.Status == true).OrderByDescending(x => x.CreateDate);
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString));
            }
            return model;
        }

        public IEnumerable<Product> GetAllRecycelBin(string searchString)
        {
            IQueryable<Product> model = db.Products.Where(x => x.Status == false).OrderByDescending(x => x.CreateDate);
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString));
            }
            return model;
        }
        /// <summary>
        /// Them user co kiem tra user ton tại
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public long Create(Product product)
        {

            var check = db.Products.SingleOrDefault(x => x.Name == product.Name);
            if (check == null)
            {

                if (product.Image == null)
                {
                    product.Image = "/Data/images/Product/product_default.png";
                }
                product.ViewCount = 1;
                product.Status = true;
                product.CreateDate = DateTime.Now;
                db.Products.Add(product);
                db.SaveChanges();
                return product.ID;
            }
            else
            {
                return -1;
            }

        }

        /// <summary>
        /// cập nhập tài khoản
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Update(Product entity)
        {
            try
            {
                var product = db.Products.Find(entity.ID);
                product.Name = entity.Name;
                product.ProducerID = entity.ProducerID;
                product.Code = entity.Code;
                product.Image = entity.Image;
                product.Status = entity.Status;
                product.TopHot = entity.TopHot;
                product.ModyfiedDate = DateTime.Now;
                product.ProductCategoryID = entity.ProductCategoryID;
                product.Detail = entity.Detail;

                product.Description = entity.Description;
                product.Quantity = entity.Quantity;
                if (entity.MoreImage != null)
                {
                    product.MoreImage = entity.MoreImage;
                }
                db.SaveChanges();
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
        public Product ViewDetail(long id)
        {
            var pro = db.Products.Find(id);
            return pro;
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
                var pro = ViewDetail(id);
                pro.Status = !pro.Status;
                db.SaveChanges();
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
        /// xóa tài khoản
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(long id)
        {
            //xóa sản phẩm trong database 
            //nếu thành công trả về true
            //nếu thất bại trả về false
            try
            {
                var prod = db.Products.Find(id);
                var images = "";
                images = prod.MoreImage;

                var price = new PriceDAO().GetAll("").Where(x => x.ProductID == prod.ID);
                var sale = new SaleDAO().GetAll("").Where(x => x.ProductID == prod.ID);
                //xóa các giá đã đặt cho sản phẩm đó

                foreach (var item in price)
                {
                    new PriceDAO().Delete(item.Id);
                }


                //xóa các giá đã đặt cho sản phẩm đó
                foreach (var item in sale)
                {
                    new SaleDAO().Delete(item.ID);
                }

                if (images != null)
                {
                    XElement xImages = XElement.Parse(images);
                    foreach (XElement element in xImages.Elements())
                    {
                        String url = element.Value;

                        if (System.IO.File.Exists(HostingEnvironment.MapPath(@"~/") + url))
                        {
                            File.Delete(HostingEnvironment.MapPath(@"~/") + url);
                        }
                    }
                }
                prod.MoreImage = null;
                db.Products.Remove(prod);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw;
            }

        }


        ///
        /// 
        /// CLIENT
        /// 
        /// 
        ///
        ///  
        public IEnumerable<ProductModel> getProductNew(int top)
        {
            var products = (
                      from p in db.Products.Where(x => x.Status == true).OrderByDescending(x => x.CreateDate)
                      select new ProductModel()
                      {
                          ID = p.ID,
                          Name = p.Name,
                          Image = p.Image,
                          MetaTitle = p.MetaTitle,
                          Detail = p.Detail,
                          Quantity = p.Quantity,
                          CreateDate = p.CreateDate,
                          MoreImage = p.MoreImage,
                          Price =
                              ((from pr in db.Prices
                                where pr.ProductID == p.ID
                                orderby pr.CreateDate descending
                                select new
                                {
                                    pr.Price1
                                }).Take(1).FirstOrDefault().Price1),
                          Sale =
                              ((from s in db.Sales
                                where s.ProductID == p.ID && s.EndDate >= DateTime.Now
                                orderby s.ID descending
                                select new
                                {
                                    s.Price
                                }).Take(1).FirstOrDefault().Price)
                      }).ToList();

            foreach (var item in products)
            {
                var images = "";
                images = item.MoreImage;
                if (images != null)
                {
                    XElement xImages = XElement.Parse(images);
                    foreach (XElement element in xImages.Elements())
                    {
                        var product = products.First(x => x.ID == item.ID).MoreImage = element.Value;
                    }
                }
            }
            var model = products.Take(top);
            return model;
        }

        /// <summary>
        /// lấy danh sach sản phẩm cùng loại
        /// </summary>
        /// <param name="top"></param>
        /// <returns></returns>
        public IEnumerable<ProductModel> ProductsRelated(int top)
        {
            var product = ViewDetail(top);
            var products = (
                      from p in db.Products.Where(x => x.Status == true).OrderByDescending(x => x.CreateDate)
                      select new ProductModel()
                      {
                          ID = p.ID,
                          Name = p.Name,
                          Image = p.Image,
                          MetaTitle = p.MetaTitle,
                          Detail = p.Detail,
                          Quantity = p.Quantity,
                          MoreImage = p.MoreImage,
                          CreateDate = p.CreateDate,
                          ProductCategory = p.ProductCategoryID.ToString(),
                          Price =
                              ((from pr in db.Prices
                                where pr.ProductID == p.ID
                                orderby pr.CreateDate descending
                                select new
                                {
                                    pr.Price1
                                }).Take(1).FirstOrDefault().Price1),
                          Sale =
                              ((from s in db.Sales
                                where s.ProductID == p.ID && s.EndDate >= DateTime.Now
                                orderby s.ID descending
                                select new
                                {
                                    s.Price
                                }).Take(1).FirstOrDefault().Price)
                      }).ToList();

            foreach (var item in products)
            {
                var images = "";
                images = item.MoreImage;
                if (images != null)
                {
                    XElement xImages = XElement.Parse(images);
                    foreach (XElement element in xImages.Elements())
                    {
                        products.First(x => x.ID == item.ID).MoreImage = element.Value;
                    }
                }
            }

            var model = products.Where(x => x.ID != top && x.ProductCategory == product.ProductCategoryID.ToString()).Take(8);
            return model;
        }

        /// <summary>
        /// lấy danh sach tất cả sản phẩm 
        /// </summary>
        /// <param name="totalRecord"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IEnumerable<ProductModel> getAllProductClient(ref int totalRecord, int pageIndex, int pageSize)
        {
            var products = (
                      from p in db.Products.Where(x => x.Status == true).OrderByDescending(x => x.CreateDate)
                      select new ProductModel()
                      {
                          ID = p.ID,
                          Name = p.Name,
                          Image = p.Image,
                          MetaTitle = p.MetaTitle,
                          Detail = p.Detail,
                          MoreImage = p.MoreImage,
                          Quantity = p.Quantity,
                          CreateDate = p.CreateDate,
                          Description = p.Description,
                          Price =
                              ((from pr in db.Prices
                                where pr.ProductID == p.ID
                                orderby pr.CreateDate descending
                                select new
                                {
                                    pr.Price1
                                }).Take(1).FirstOrDefault().Price1),
                          Sale =
                              ((from s in db.Sales
                                where s.ProductID == p.ID && s.EndDate >= DateTime.Now
                                orderby s.ID descending
                                select new
                                {
                                    s.Price
                                }).Take(1).FirstOrDefault().Price)
                      }).ToList();

            foreach (var item in products)
            {
                var images = "";
                images = item.MoreImage;
                if (images != null)
                {
                    XElement xImages = XElement.Parse(images);
                    foreach (XElement element in xImages.Elements())
                    {
                        var product = products.First(x => x.ID == item.ID).MoreImage = element.Value;
                    }
                }
            }
            totalRecord = products.Count();//lấy tổng số lượng sản phẩm 
            var model = products.Skip((pageIndex - 1) * pageSize).Take(pageSize);
            return model;
        }

        /// <summary>
        /// lấy danh danh sách các sản phẩm là điện thoại
        /// </summary>
        /// <param name="totalRecord"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="id"></param>
        /// <returns></returns>

        public IEnumerable<ProductModel> getAllProductProducer(ref int totalRecord, int pageIndex, int pageSize, int id, int cateId)
        {
            var productsphone = (
                from p in
                    db.Products.Where(x => x.Status == true && x.ProducerID == id && x.ProductCategoryID == cateId)
                        .OrderByDescending(x => x.CreateDate)
                select new ProductModel()
                {
                    ID = p.ID,
                    Name = p.Name,
                    Image = p.Image,
                    MetaTitle = p.MetaTitle,
                    Detail = p.Detail,
                    MoreImage = p.MoreImage,
                    Quantity = p.Quantity,
                    CreateDate = p.CreateDate,
                    Description = p.Description,
                    Price =
                        ((from pr in db.Prices
                          where pr.ProductID == p.ID
                          orderby pr.CreateDate descending
                          select new
                          {
                              pr.Price1
                          }).Take(1).FirstOrDefault().Price1),
                    Sale =
                        ((from s in db.Sales
                          where s.ProductID == p.ID && s.EndDate >= DateTime.Now
                          orderby s.ID descending
                          select new
                          {
                              s.Price
                          }).Take(1).FirstOrDefault().Price)
                }).ToList();

            foreach (var item in productsphone)
            {
                var images = "";
                images = item.MoreImage;
                if (images != null)
                {
                    XElement xImages = XElement.Parse(images);
                    foreach (XElement element in xImages.Elements())
                    {
                        var product = productsphone.First(x => x.ID == item.ID).MoreImage = element.Value;
                    }
                }
            }

            totalRecord = productsphone.Count(); //lấy tổng số lượng sản phẩm 
            var models = productsphone.Skip((pageIndex - 1) * pageSize).Take(pageSize);
            return models;

        }


        /// <summary>
        /// lấy danh sách sản phẩm có ngày hot lớn hơn ngày hiện tại
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ProductModel> getProductFeature()
        {
            var products = (
                      from p in db.Products.Where(x => x.Status == true).OrderByDescending(x => x.CreateDate)
                      where p.TopHot >= DateTime.Now
                      select new ProductModel()
                      {
                          ID = p.ID,
                          Name = p.Name,
                          Image = p.Image,
                          MetaTitle = p.MetaTitle,
                          Detail = p.Detail,
                          Quantity = p.Quantity,
                          MoreImage = p.MoreImage,
                          CreateDate = p.CreateDate,
                          Price =
                              ((from pr in db.Prices
                                where pr.ProductID == p.ID
                                orderby pr.CreateDate descending
                                select new
                                {
                                    pr.Price1
                                }).Take(1).FirstOrDefault().Price1),
                          Sale =
                              ((from s in db.Sales
                                where s.ProductID == p.ID && s.EndDate >= DateTime.Now
                                orderby s.ID descending
                                select new
                                {
                                    s.Price
                                }).Take(1).FirstOrDefault().Price)
                      }).ToList();
            foreach (var item in products)
            {
                var images = "";
                images = item.MoreImage;
                if (images != null)
                {
                    XElement xImages = XElement.Parse(images);
                    foreach (XElement element in xImages.Elements())
                    {
                        var product = products.First(x => x.ID == item.ID).MoreImage = element.Value;
                    }
                }
            }
            var model = products.Take(8);

            return model;
        }

        /// <summary>
        /// lấy danh sách sản phẩm hiện đang được giảm giá 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ProductModel> getProductSale()
        {
            var products = (
                      from p in db.Products.Where(x => x.Status == true).OrderByDescending(x => x.CreateDate)
                      select new ProductModel()
                      {
                          ID = p.ID,
                          Name = p.Name,
                          Image = p.Image,
                          MetaTitle = p.MetaTitle,
                          MoreImage = p.MoreImage,
                          Detail = p.Detail,
                          CreateDate = p.CreateDate,
                          Quantity = p.Quantity,
                          Price =
                              ((from pr in db.Prices
                                where pr.ProductID == p.ID
                                orderby pr.CreateDate descending
                                select new
                                {
                                    pr.Price1
                                }).Take(1).FirstOrDefault().Price1),
                          Sale =
                              ((from s in db.Sales
                                where s.ProductID == p.ID && s.EndDate >= DateTime.Now
                                orderby s.ID descending
                                select new
                                {
                                    s.Price
                                }).Take(1).FirstOrDefault().Price)
                      }).ToList();
            var model = products.Where(x => x.Sale != null).Take(8);
            //lấy ra nhiều hình ảnh của sản phẩm
            foreach (var item in products)
            {
                var images = "";
                images = item.MoreImage;
                if (images != null)
                {
                    XElement xImages = XElement.Parse(images);
                    foreach (XElement element in xImages.Elements())
                    {
                        var product = products.First(x => x.ID == item.ID).MoreImage = element.Value;
                    }
                }
            }
            return model;
        }


        public ProductModel producrDetail(long id)
        {
            var products =
                      from p in db.Products
                      select new ProductModel()
                      {
                          ID = p.ID,
                          Name = p.Name,
                          Image = p.Image,
                          MetaTitle = p.MetaTitle,
                          Description = p.Description,
                          Detail = p.Detail,
                          Quantity = p.Quantity,
                          MoreImage = p.MoreImage,
                          Producer = p.Producer.Name,
                          ProductCategory = p.ProductCategory.Name,
                          Price =
                              ((from pr in db.Prices
                                where pr.ProductID == p.ID
                                orderby pr.CreateDate descending
                                select new
                                {
                                    pr.Price1
                                }).Take(1).FirstOrDefault().Price1),
                          Sale =
                              ((from s in db.Sales
                                where s.ProductID == p.ID && s.EndDate >= DateTime.Now
                                orderby s.ID descending
                                select new
                                {
                                    s.Price
                                }).Take(1).FirstOrDefault().Price)
                      };

            var model = products.SingleOrDefault(x => x.ID == id);
            return model;
        }
    }
}