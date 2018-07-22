using System.Web.Mvc;

namespace CM.WebApi
{

    /// <summary>
    /// Global filter for application
    /// </summary>
    public class FilterConfig
    {
        /// <summary>
        /// Register custom filter
        /// </summary>
        /// <param name="filters"></param>
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
