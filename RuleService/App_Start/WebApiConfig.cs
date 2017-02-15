namespace RuleService
{
    using System.Web.Http;
    using System.Web.OData.Builder;
    using System.Web.OData.Extensions;
    using Microsoft.OData.Edm;
    using Models;

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

            var conventionModel = GetConventionModel();
            config.MapODataServiceRoute("ODataRoute", null, conventionModel);
        }

        public static IEdmModel GetConventionModel()
        {
            var builder = new ODataConventionModelBuilder();
            builder.EntitySet<Rule>("Rules");
            builder.EntitySet<RuleVariable>("RuleVariables");
            return builder.GetEdmModel();
        }
    }
}
