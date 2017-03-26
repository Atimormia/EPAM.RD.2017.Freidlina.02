using System.Web;
using System.Web.Mvc;

namespace EPAM.RD._2017.Freidlina._02
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
