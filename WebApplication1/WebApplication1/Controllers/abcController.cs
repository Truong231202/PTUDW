using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models.abc;

namespace WebApplication1.Controllers
{
    public class abcController : Controller
    {
        // GET: abc
        public ActionResult Index()
        {
            var list = new List<abc> {
                new abc { id = 1,name =  "quac quac", image = " "},
                new abc { id = 2,name = "cloz", image= " "},

            }
        
        }
    }
}