
using System;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using PagedList;
using ProjectMamQua.Dao;
using ProjectMamQua.EF;
using ProjectSEM3.Common;

namespace ProjectSEM3.Areas.Admin.Controllers
{
    [AuthorizeController]
    public class ProductCategoryController : Controller
    {
       
      
            ProductCategoryDao db = new ProductCategoryDao();
            // GET: Admin/ContentCategory
            public ActionResult Index(string searchString, int? page)
            {
                int pageSize = 10;
                int pageNumber = (page ?? 1);
                var model = db.GetAll(searchString).ToPagedList(pageNumber, pageSize);
                return View(model);
            }


            [HttpPost]
            public JsonResult Create(string data)
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                ProductCategory model = javaScriptSerializer.Deserialize<ProductCategory>(data);

                bool status = false;
                string mess = String.Empty;

                if (model.ID == 0)
                {
                    var dao = db.Create(model);
                if (dao == 1)
                {
                    status = true;
                    mess = "Thêm mới thành công";
                }
                else
                {
                    status = false; 
                    mess = "Tên đã tồn tại";
                }
            }
                else
                {

                    status = db.Update(model);
                    mess = "Cập nhập thất bại";
                }

                return Json(new
                {
                    status = status,
                    mess = mess
                });

            }

            /// <summary>
            /// hàm này dùng để xóa tạm
            /// </summary>
            /// <param name="id"></param>
            /// <returns></returns>
            [HttpPost]
            public JsonResult ChangeStatus(long id)
            {
                var dao = db.ChangeStatus(id);
                return Json(new
                {
                    status = dao
                });

            }



            /// <summary>
            /// cập nhập lại status theo mảng
            /// </summary>
            /// <param name="ids"></param>
            /// <returns></returns>

            [HttpPost]
            public JsonResult DeleteSelected(string ids)
            {
                var lstID = ids.Split(',');
                bool res = true;
                foreach (var id in lstID)
                {
                    long cv = Convert.ToInt64(id);
                    res = db.ChangeStatus(cv);
                }
                return Json(new
                {
                    status = res
                });
            }


            //thùng rác
            //GET 
            public ActionResult RecycelBin(string searchString, int? page)
            {
                var model = db.GetAllRecycelBin(searchString);
                int pageNumber = (page ?? 1);
                int pageSize = 10;
                ViewBag.SearchString = searchString;
                return View(model.ToPagedList(pageNumber, pageSize));
            }

            /// <summary>
            /// xóa trong db
            /// </summary>
            /// <param name="id"></param>
            /// <returns></returns>
            [HttpPost]
            public JsonResult Delete(long id)
            {
                var dao = db.Delete(id);//xóa trong db
                return Json(new
                {
                    status = dao//trả về giá trị cho ajax true false
                });

            }


            /// <summary>
            /// xóa mảng thành phần được chọn trong db
            /// </summary>
            /// <param name="ids"></param>
            /// <returns></returns>
            [HttpPost]
            public JsonResult DeleteSelectedDb(string ids)
            {
                var lstID = ids.Split(',');//chuyển chuỗi thành mảng
                bool res = true;
                foreach (var id in lstID)
                {
                    long cv = Convert.ToInt64(id);
                    res = db.Delete(cv);
                }
                return Json(new
                {
                    status = res
                });
            }
            /// <summary>
            /// cập nhập mảng status
            /// </summary>
            /// <param name="ids"></param>
            /// <returns>bool</returns>
            [HttpPost]
            public JsonResult DeleteSelectedRecycelBin(string ids)
            {
                var lstID = ids.Split(',');
                bool res = true;
                foreach (var id in lstID)
                {
                    long cv = Convert.ToInt64(id);
                    res = db.ChangeStatus(cv);
                }
                return Json(new
                {
                    status = res
                });
            }

            [HttpPost]
            public JsonResult ViewDetail(long id)
            {
                var model = db.ViewDetail(id);
                return Json(new
                {
                    status = true
                    ,
                    id = model.ID,
                    name = model.Name,
                    url = model.MetaTitle
                }, JsonRequestBehavior.AllowGet);
            }

        }
    }