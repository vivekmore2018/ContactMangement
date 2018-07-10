using CM.Business;
using CM.DataAccess;
using CM.Interface.Business;
using CM.Interface.DataAccess;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using Unity;

namespace CM.WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            var container = new UnityContainer();
            var DatabaseConnectionString = ConfigurationManager.ConnectionStrings["CMconnection"].ConnectionString;
            container.RegisterInstance<IDbConnection>(new SqlConnection(DatabaseConnectionString));

            container.RegisterType<IContactManager, ContactManager>();
            container.RegisterType<IContactRepository, ContactRepository>();
            config.DependencyResolver = new ServiceResolver(container);

            var enableCorsAttribute = new EnableCorsAttribute("*",
                                               "Origin, Content-Type, Accept",
                                               "GET, PUT, POST, DELETE, OPTIONS");
            config.EnableCors(enableCorsAttribute);
        }
    }
}
