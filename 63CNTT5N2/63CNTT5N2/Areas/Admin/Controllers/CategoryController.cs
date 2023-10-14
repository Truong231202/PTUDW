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
        

        // GET: Admin/Category
        public ActionResult Index()
        {
           return View(categoriesDAO.getlist("Index")); // chỉ hiển thị các dòng có status = 1, 2
        }
        ////////////////////////////////// DETAILS ///////////////////////////

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categories categories = categoriesDAO.getRow(id);
            if (categories == null)
            {
                return HttpNotFound();
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
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //tim dong db can chinh sua
            Categories categories = categoriesDAO.getRow(id);
            if (categories == null)
            {
                return HttpNotFound();
            }
            return View(categories);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Categories categories)
        {
            if (ModelState.IsValid)
            {
                categoriesDAO.Update(categories);
                return RedirectToAction("Index");
            }
            return View(categories);
        }

        ////////////////////////////////// DELETE///////////////////////////
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //// tim 1 dong db de xoa 
            Categories categories = categoriesDAO.getRow(id);
            if (categories == null)
            {
                return HttpNotFound();
            }
            return View(categories);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //tim dong db can chinh sua
            Categories categories = categoriesDAO.getRow(id);
            categoriesDAO.Delete(categories);
            return RedirectToAction("Index");
        }

        ////////////////////////////////// Status///////////////////////////
        public ActionResult Status(int? id)
        {
          
            //tim dong co id = id cua loai san pham can thay doi status
            Categories categories = categoriesDAO.getRow(id);
           //kiem tra trang thai status, neu hien tai la 1 -> 2, neu 2 -> 1
            categories.Status = (categories.Status == 1) ? 2 : 1;
            // cap nhat gia tri cho UpdateAt
            categories.UpdateAt = DateTime.Now;
            //cap nhat lai db
            categoriesDAO.Update(categories);

            
            // tra ket qua ve Index
            return RedirectToAction("Index");
        }

    }
}
