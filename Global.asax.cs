using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace Library
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
        protected void Application_Error()
        {
            //intercept application errors and run code to save to an error log file or email to dev team
        }
    }
}
