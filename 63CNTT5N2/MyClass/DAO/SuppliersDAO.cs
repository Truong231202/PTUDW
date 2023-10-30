using MyClass.Model;// MyDBContext
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.DAO
{
    public class SuppliersDAO
    {
        private MyDBContext db = new MyDBContext();
        ////////////////////////////
        //INDEX
        public List<Suppliers> getlist()
        {
            return db.Suppliers.ToList();
        }
        //////////////
        ///Index voi gia tri status 1, 2 -- 0: an khoi trang giao dien
        ////
        public List<Suppliers> getlist(string status = "All")
        {/// tra ve danh  sach cac NCC co status = 1, 2, 0 hoạc tat ca
            List<Suppliers> list = null;
            switch (status)
            {
                case "Index": // status == 1, 2
                    {
                        list = db.Suppliers.Where(m => m.Status != 0).ToList();
                        break;
                    }
                case "Trash": //status == 0
                    {
                        list = db.Suppliers.Where(m => m.Status == 0).ToList();
                        break;
                    }
                default:
                    {
                        list = db.Suppliers.ToList();
                        break;
                    }
            }
            return list;
        }
        /////////////////
        ///DETAILS hien thi 1 dong du lieu 
        public Suppliers getRow(int? id)
        {
            if (id == null)
            {
                return null;
            }
            else
            {
                return db.Suppliers.Find(id);
            }
        }

        /////////////////
        ///CREATE = Insert 1 dong db
        public int Insert(Suppliers row)
        {
            db.Suppliers.Add(row);
            return db.SaveChanges();
        }

        //////////////////
        ///EDIT = UPDATE 1 dong db
        public int Update(Suppliers row)
        {
            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();
        }
        //////////////////
        ///DELETE = xoa 1 dong db
        public int Delete(Suppliers row)
        {
            db.Suppliers.Remove(row);
            return db.SaveChanges();
        }
    }
}
//// supplierDAO 
