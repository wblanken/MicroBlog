using System.Diagnostics;
using System.Web.Http;
using MicroBlog.Presentation.DependencyResolution;
using Owin;

namespace MicroBlog.Presentation
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var container = IoC.Initialize();
            container.AssertConfigurationIsValid();
            Debug.WriteLine(container.WhatDoIHave());
            var config = new HttpConfiguration
            {
                DependencyResolver = new StructureMapWebApiDependencyResolver(container)
            };

            ConfigureAuth(app);
            
            WebApiConfig.Register(config);
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(config);
        }
    }
}