using MyClass.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.DAO
{
    public class CategoriesDAO
    {
        private MyDBContext db = new MyDBContext();
        ////////////////////////////
        //INDEX
        public List<Categories> getlist()
        {
            return db.Categories.ToList();
        }
        //////////////
        ///Index voi gia tri status 1, 2 -- 0: an khoi trang giao dien
        ////
        public List<Categories> getlist(string status = "All") 
        {
            List<Categories> list = null;
            switch (status)
            {
                case "Index": // status == 1, 2
                    {
                        list = db.Categories.Where(m => m.Status !=0).ToList();
                        break;
                    }
                case "Trash": //status == 0
                    {
                        list = db.Categories.Where(m => m.Status == 0).ToList(); 
                        break;
                    }
                default:
                    {
                        list = db.Categories.ToList();
                        break;
                    }
            }
            return list;
        }
        /////////////////
        ///DETAILS hien thi 1 dong du lieu 
        public Categories getRow(int? id)
        {
            if(id == null)
            {
                return null;
            }
            else
            {
                return db.Categories.Find(id);
            }
        }

        /////////////////
        ///CREATE = Insert 1 dong db
        public int Insert(Categories row)
        {
            db.Categories.Add(row);
            return db.SaveChanges();
        }

        //////////////////
        ///EDIT = UPDATE 1 dong db
        public int Update(Categories row)
        {
            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();
        }
        //////////////////
        ///DELETE = xoa 1 dong db
        public int Delete(Categories row) {
            db.Categories.Remove(row);
            return db.SaveChanges();
        }
    }
}
