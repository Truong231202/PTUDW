using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.ComTypes;
using System.Web;
using System.Web.Mvc;
using MyClass.DAO;
using MyClass.Model;
using UDW.Library;

namespace _63CNTT5N2.Areas.Admin.Controllers
{
    public class SupplierController : Controller
    {
        SuppliersDAO suppliersDAO = new SuppliersDAO();

        ///////////////////////////////////////////////////////////////////////////
        // GET: Admin/Supplier/Index
        public ActionResult Index()
        {
            return View(suppliersDAO.getlist("Index")); 
        }

        ///////////////////////////////////////////////////////////////////////////
        // GET: Admin/Supplier/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                //hien thong bao loi
                TempData["message"] = new XMessage("danger", "Không tìm thấy mẩu tin");
                return RedirectToAction("Index");
            }
            //tim mot mau tin ung voi id = id
            Suppliers suppliers = suppliersDAO.getRow(id);
            if (suppliers == null)
            {
                //hien thong bao loi
                TempData["message"] = new XMessage("danger", "Không tìm thấy mẩu tin");
                return RedirectToAction("Index");
            }
            return View(suppliers);
        }

        ///////////////////////////////////////////////////////////////////////////
        // GET: Admin/Supplier/Create
        public ActionResult Create()
        {
            ViewBag.OrderList = new SelectList(suppliersDAO.getlist("Index"), "Order", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Suppliers suppliers)
        {
            if (ModelState.IsValid)
            {
                //Xu ly mot so truong tu dong
                //CreateAt
                suppliers.CreateAt = DateTime.Now;
                //UpdateAt
                suppliers.UpdateAt = DateTime.Now;
                //CreateBy
                suppliers.CreateBy = Convert.ToInt32(Session["UserID"]);
                //UpdateBy
                suppliers.UpdateBy = Convert.ToInt32(Session["UserID"]);
                //xu ly cho phan upload hinh anh
                var img = Request.Files["img"];//lay thong tin file
                if (img.ContentLength != 0)
                {
                    string[] FileExtentions = new string[] { ".jpg", ".jpeg", ".png", ".gif" };
                    //kiem tra tap tin co hay khong
                    if (FileExtentions.Contains(img.FileName.Substring(img.FileName.LastIndexOf("."))))//lay phan mo rong cua tap tin
                    {
                        string slug = suppliers.Slug;
                        //ten file = Slug + phan mo rong cua tap tin
                        string imgName = slug + img.FileName.Substring(img.FileName.LastIndexOf("."));
                        suppliers.Image = imgName;
                        //upload hinh
                        string PathDir = "~/Public/img/supplier/";
                        string PathFile = Path.Combine(Server.MapPath(PathDir), imgName);
                        img.SaveAs(PathFile);
                    }
                }//ket thuc phan upload hinh anh
                //Slug
                suppliers.Slug = XString.Str_Slug(suppliers.Name);
                //Order
                if (suppliers.Order == null)
                {
                    suppliers.Order = 1;
                }
                else
                {
                    suppliers.Order += 1;
                }
                //luu tru vao DB
                suppliersDAO.Insert(suppliers);
                //hien thong thanh cong
                TempData["message"] = new XMessage("danger", "Thêm mới nhà cung cấp thành công");
                return RedirectToAction("Index");
            }
            ViewBag.OrderList = new SelectList(suppliersDAO.getlist("Index"), "Order", "Name");
            return View(suppliers);
        }

        ///////////////////////////////////////////////////////////////////////////
        // GET: Admin/Supplier/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                //hien thong bao loi
                TempData["message"] = new XMessage("danger", "Không tìm thấy mẩu tin");
                return RedirectToAction("Index");
            }
            Suppliers suppliers = suppliersDAO.getRow(id);
            if (suppliers == null)
            {
                //hien thong bao loi
                TempData["message"] = new XMessage("danger", "Không tìm thấy mẩu tin");
                return RedirectToAction("Index");
            }
            ViewBag.OrderList = new SelectList(suppliersDAO.getlist("Index"), "Order", "Name");
            return View(suppliers);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Suppliers suppliers)
        {
            if (ModelState.IsValid)
            {
                //Xu ly mot so truong tu dong
                //UpdateAt
                suppliers.UpdateAt = DateTime.Now;
                //CreateBy
                suppliers.CreateBy = Convert.ToInt32(Session["UserID"]);
                //UpdateBy
                suppliers.UpdateBy = Convert.ToInt32(Session["UserID"]);
                //Slug
                suppliers.Slug = XString.Str_Slug(suppliers.Name);
                //Order
                if (suppliers.Order == null)
                {
                    suppliers.Order = 1;
                }
                else
                {
                    suppliers.Order += 1;
                }

                // truoc khi cap nhat lai anh moi thi xoa anh cu
                var img = Request.Files["img"];// lay thong  tin file
                string PathDir = "~/Public/img/supplier/";
                if (suppliers.Image != null && img.ContentLength != 0)
                {
                    //xoa anh cu
                    string DelPath = Path.Combine(Server.MapPath(PathDir), suppliers.Image);
                    System.IO.File.Delete(DelPath);
                }
                // Upload anh moi cua NCC
                // xu li cho phan upload hinh anh

                
                if (img.ContentLength != 0)
                {
                    string[] FileExtentions = new string[] { ".jpg", ".jpeg", ".png", ".gif" };
                    //kiem tra tap tin co hay khong
                    if (FileExtentions.Contains(img.FileName.Substring(img.FileName.LastIndexOf("."))))//lay phan mo rong cua tap tin
                    {
                        string slug = suppliers.Slug;
                        //ten file = Slug + phan mo rong cua tap tin
                        string imgName = slug + img.FileName.Substring(img.FileName.LastIndexOf("."));
                        suppliers.Image = imgName;
                        //upload hinh
                        string PathFile = Path.Combine(Server.MapPath(PathDir), imgName);
                        img.SaveAs(PathFile);
                    }
                }//ket thuc phan upload hinh anh
                //Update DB
                suppliersDAO.Update(suppliers);
                //hien thong thanh cong
                TempData["message"] = new XMessage("danger", "Thêm mới nhà cung cấp thành công");
                return RedirectToAction("Index");
            }
            //ViewBag.OrderList = new SelectList(suppliersDAO.getList("Index"), "Order", "Name");
            return View(suppliers);
        }

        ///////////////////////////////////////////////////////////////////////////
        // GET: Admin/Supplier/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                //hien thong bao loi
                TempData["message"] = new XMessage("danger", "Không tìm thấy mẩu tin");
                return RedirectToAction("Index");
            }
            Suppliers suppliers = suppliersDAO.getRow(id);
            if (suppliers == null)
            {
                //hien thong bao loi
                TempData["message"] = new XMessage("danger", "Không tìm thấy mẩu tin");
                return RedirectToAction("Index");
            }
            return View(suppliers);
        }

        // POST: Admin/Supplier/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Suppliers suppliers = suppliersDAO.getRow(id);
            //Xoa khoi DB
            suppliersDAO.Delete(suppliers);


            //hien thong baothanh cong
            TempData["message"] = new XMessage("danger", "Xóa nhà cung cấp thành công");
            return RedirectToAction("Index");
        }

        /// //////////////////////////////////////////////////////////////////////////////////
        // GET: Admin/Supplier/DelTrash/5
        //Chuyen mau tin dang o trang thai status la 1/2 thanh 0: khong hien thi o trang INDEX
        public ActionResult DelTrash(int? id)
        {
            if (id == null)
            {
                //thong bao that bai
                TempData["message"] = new XMessage("danger", "Không tìm thấy mẩu tin");
                return RedirectToAction("Index");
            }
            Suppliers suppliers = suppliersDAO.getRow(id);
            if (suppliers == null)
            {
                //thong bao thay doi status that bai
                TempData["message"] = new XMessage("danger", "Không tìm thấy mẩu tin");
                return RedirectToAction("Index");
            }
            //cap nhat mot so thong tin cho DB (id==id)
            //UpdateAt
            suppliers.UpdateAt = DateTime.Now;
            //UpdateBy
            suppliers.UpdateBy = Convert.ToInt32(Session["UserID"]);
            //Status
            suppliers.Status = 0;
            //Update DB
            suppliersDAO.Update(suppliers);

            //thong bao thay doi status thanh cong
            TempData["message"] = new XMessage("success", "Xóa mẩu tin thành công");
            return RedirectToAction("Index");
        }

        /// //////////////////////////////////////////////////////////////////////////////////
        // TRASH
        public ActionResult Trash()
        {
            return View(suppliersDAO.getlist("Trash"));//status =0
        }

        /// //////////////////////////////////////////////////////////////////////////////////
        // GET: Admin/Supplier/Undo/5
        //Chuyen mau tin dang o trang thai status la 1/2 thanh 0: khong hien thi o trang INDEX
        public ActionResult Undo(int? id)
        {
            if (id == null)
            {
                //thong bao that bai
                TempData["message"] = new XMessage("danger", "Không tìm thấy mẩu tin");
                return RedirectToAction("Index");
            }
            Suppliers suppliers = suppliersDAO.getRow(id);
            if (suppliers == null)
            {
                //thong bao thay doi status that bai
                TempData["message"] = new XMessage("danger", "Không tìm thấy mẩu tin");
                return RedirectToAction("Index");
            }
            //cap nhat mot so thong tin cho DB (id==id)
            //UpdateAt
            suppliers.UpdateAt = DateTime.Now;
            //UpdateBy
            suppliers.UpdateBy = Convert.ToInt32(Session["UserID"]);
            //Status
            suppliers.Status = 2;
            //Update DB
            suppliersDAO.Update(suppliers);

            //thong bao thay doi status thanh cong
            TempData["message"] = new XMessage("success", "Phục hồi mẩu tin thành công");
            return RedirectToAction("Index");
        }

    }
}
