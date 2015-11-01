using CSGProHackathonAPI.Shared.Data;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace CSGProHackathonAPI
{
    public static class ContainerConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Create the container as usual.
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebApiRequestLifestyle();

            // Register your types, for instance using the scoped lifestyle:
            container.Register<IContext, Context>(Lifestyle.Scoped);
            container.Register<IRepository, Repository>(Lifestyle.Scoped);

            // This is an extension method from the integration package.
            container.RegisterWebApiControllers(config);

            container.Verify();

            config.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
        }
    }
}