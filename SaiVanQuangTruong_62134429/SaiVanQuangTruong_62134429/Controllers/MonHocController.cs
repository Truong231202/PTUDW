using Microsoft.Ajax.Utilities;
using SaiVanQuangTruong_62134429.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SaiVanQuangTruong_62134429.Controllers
{
    public class MonHocController : Controller
    {
        // GET: MonHoc
        public ActionResult Index()
        {
            var list = new List<MonHoc>{
                new MonHoc { id = 1, HoTen = "Quang Trường", image = "duy.jpg" },
                new MonHoc { id =2, HoTen = "Bảo My", image = "my.jpg" },
                new MonHoc { id = 3, HoTen = "Đức Duy", image = "truong.jpg" } 
            };
            return View(list); 
        }
        

    }
}