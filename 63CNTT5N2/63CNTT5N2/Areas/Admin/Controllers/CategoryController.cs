using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyClass.DAO;
using MyClass.Model;
using UDW.Library;

namespace _63CNTT5N2.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        CategoriesDAO categoriesDAO = new CategoriesDAO();


        ////////////////////////////////// INDEX ///////////////////////////

        public ActionResult Index()
        {
           return View(categoriesDAO.getlist("Index")); // chỉ hiển thị các dòng có status = 1, 2
        }
        ////////////////////////////////// DETAILS ///////////////////////////

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                ////thong bao that bai
                TempData["message"] = new XMessage("danger", "Không tồn tại loại sản phẩm");
                return RedirectToAction("Index");
            }
            Categories categories = categoriesDAO.getRow(id);
            if (categories == null)
            {
                TempData["message"] = new XMessage("danger", "Không tồn tại loại sản phẩm");
                return RedirectToAction("Index");
            }
            
            return View(categories);
        }

        ////////////////////////////////// CREATE ///////////////////////////
        public ActionResult Create()
        {
            ViewBag.ListCat = new SelectList(categoriesDAO.getlist("Index"), "Id", "Name");
            ViewBag.ListOrder = new SelectList(categoriesDAO.getlist("Index"), "Order", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Categories categories)
        {
            if (ModelState.IsValid)
            {
                categories.CreatedAt = DateTime.Now;

                categories.UpdateAt = DateTime.Now;

                if (categories.ParentID == null)
                {
                    categories.Order = 1;
                }
                else
                {
                    categories.Order += 1;
                }

                //xu li tu dong: Slug
                categories.Slug = XString.Str_Slug(categories.Name);

                // thhem dong du lieu cho db
                categoriesDAO.Insert(categories);
                /// thong bao them danh muc san pham thanh cong
                TempData["message"] = new XMessage("success", "Tạo mới loại sản phẩm thành công");
                ///tro ve trang index
                return RedirectToAction("Index");
               
            }
            ViewBag.ListCat = new SelectList(categoriesDAO.getlist("Index"), "Id", "Name");
            ViewBag.ListOrder = new SelectList(categoriesDAO.getlist("Index"), "Order", "Name");
     
            return View(categories);
        }

        ////////////////////////////////// EDIT///////////////////////////
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                // thong bao thât bai
                TempData["message"] = TempData["message"] = new XMessage("danger", "Không  tìm thấy mẫu tin");
                return RedirectToAction("Index");
            }
            //tim dong db can chinh sua
            Categories categories = categoriesDAO.getRow(id);
            if (categories == null)
            {
                // thong bao thât bai
                TempData["message"] = TempData["message"] = new XMessage("danger", "Không  tìm thấy mẫu tin");
                return RedirectToAction("Index");
            }
            ViewBag.ListCat = new SelectList(categoriesDAO.getlist("Index"), "Id", "Name");
            ViewBag.ListOrder = new SelectList(categoriesDAO.getlist("Index"), "Order", "Name");
            return View(categories);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Categories categories)
        {
            if (ModelState.IsValid)
            {
                //xu li tu dong: Slug
                categories.Slug = XString.Str_Slug(categories.Name);
                // xu ly tu dong: ParentID
                if(categories.ParentID == null)
                {
                    categories.ParentID = 0;
                }
                // xu li tu dong: Order
                if(categories.Order == null)
                {
                    categories.Order = 1;
                }
                else
                {
                    categories.Order += 1;
                }
                // xu li tu dong: UpdateAt
                categories.UpdateAt = DateTime.Now;


                //// cap nhat mau tin
                categoriesDAO.Update(categories);

                // thong bao thanh cong
                TempData["message"] = new XMessage("success", "Cập nhật mẫu tin thành công");
                return RedirectToAction("Index");
            }
            ViewBag.ListCat = new SelectList(categoriesDAO.getlist("Index"), "Id", "Name");
            ViewBag.ListOrder = new SelectList(categoriesDAO.getlist("Index"), "Order", "Name");
            return View(categories);
        }

        ////////////////////////////////// DELETE///////////////////////////
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                //thong bao that bai
                TempData["message"] = new XMessage("danger", "Xóa mẩu tin thất bại");
                return RedirectToAction("Index");
            }
            Categories categories = categoriesDAO.getRow(id);
            if (categories == null)
            {
                //thong bao that bai
                TempData["message"] = new XMessage("danger", "Xóa mẩu tin thất bại");
                return RedirectToAction("Index");
            }
            return View(categories);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Categories categories = categoriesDAO.getRow(id);

            categoriesDAO.Delete(categories);

            //thong bao thanh cong
            TempData["message"] = new XMessage("success", "Xóa mẩu tin thành công");
            return RedirectToAction("Trash", "Category");
        }
        ////////////////////////////////// STATUS///////////////////////////

        public ActionResult Status(int? id)
        {
            if (id == null)
            {
                //thong bao that bai
                TempData["message"] = new XMessage("danger", "Cập nhật trạng thái thất bại");
                return RedirectToAction("Index");
            }

            //tim row co id == id cua loai SP can thay doi Status
            Categories categories = categoriesDAO.getRow(id);
            if (categories == null)
            {
                //thong bao that bai
                TempData["message"] = new XMessage("danger", "Cập nhật trạng thái thất bại");
                return RedirectToAction("Index");
            }
            //kiem tra trang thai cua status, neu hien tai la 1 ->2 va nguoc lai
            categories.Status = (categories.Status == 1) ? 2 : 1;
            //cap nhat gia tri cho UpdateAt
            categories.UpdateAt = DateTime.Now;
            //cap nhat lai DB
            categoriesDAO.Update(categories);
            //thong bao thanh cong
            TempData["message"] = new XMessage("success", "Cập nhật trạng thái thành công");
            //tra ket qua ve Index
            return RedirectToAction("Index");
        }

        ////////////////////////////////// DelTrash///////////////////////////
        public ActionResult DelTrash(int? id)
        {
            
            if(id == null)
            {
                ////thong bao that bai
                TempData["message"] = new XMessage("danger", "Không tìm thấy mẩu tin");
                return RedirectToAction("Index");
            }
            /// truy van dong co id = id yeu cau
            Categories categories = categoriesDAO.getRow(id);
            if (categories == null)
            {
                TempData["message"] = new XMessage("danger", "Không tìm thấy mẩu tin");
                return RedirectToAction("Index");
            }
            else
            {
                
                //kiem tra trang thai status, neu hien tai la 1, 2 -> 0: không hiển thị ở Index 
                categories.Status = 0;
                // cap nhat gia tri cho UpdateAt
                categories.UpdateAt = DateTime.Now;
                //cap nhat lai db
                categoriesDAO.Update(categories);

                ///thong bao cap nhat trang thai thanh cong
                TempData["message"] = TempData["message"] = new XMessage("success", "Xóa mẩu tin thành công");

                // tra ket qua ve Index
                return RedirectToAction("Index");
            }
        }
        ////////////////////////////////// TRASH ///////////////////////////

        public ActionResult Trash()
        {
            return View(categoriesDAO.getlist("Trash")); // chỉ hiển thị các dòng có status = 1, 2
        }

        ////////////////////////////////// RECOVER///////////////////////////

        public ActionResult Recover(int? id)
        {
            if (id == null)
            {
                //thong bao that bai
                TempData["message"] = new XMessage("danger", "Phục hồi thất bại");
                return RedirectToAction("Index");
            }

            //tim row co id == id cua loai SP can thay doi Status
            Categories categories = categoriesDAO.getRow(id);
            if (categories == null)
            {
                //thong bao that bai
                TempData["message"] = new XMessage("danger", "Phục hồi thất bại");
                return RedirectToAction("Index");
            }
            //kiem tra trang thai cua status, tu 0 -> 2: khong xuat ban
            categories.Status = 2;

            //cap nhat gia tri cho UpdateAt
            categories.UpdateAt = DateTime.Now;

            //cap nhat lai DB
            categoriesDAO.Update(categories);

            //thong bao phuc hoi  du lieu thanh cong
            TempData["message"] = new XMessage("success", "Phục hồi mẫu tin thành công");

            //tra ket qua ve Index
            return RedirectToAction("Index");
        }
    }
}
