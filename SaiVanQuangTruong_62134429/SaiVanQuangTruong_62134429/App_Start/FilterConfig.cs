using System.Web;
using System.Web.Mvc;

namespace SaiVanQuangTruong_62134429
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
