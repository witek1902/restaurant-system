namespace OrderManagementSystem
{
    using Castle.MicroKernel.Registration;
    using Castle.Windsor;
    using Castle.Windsor.Installer;
    using Infrastructure.IoC;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;
    using System.Web;

    public class MvcApplication : HttpApplication
    {
        private static readonly IWindsorContainer container = new WindsorContainer();

        public static IWindsorContainer Container => container;

        protected void Application_Start()
        {
            InitWindsor();
            ControllerBuilder.Current.SetControllerFactory(new WindsorControllerFactory(container));

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //Uncomment if you want add NHibernateProfiler
            //HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();
        }

        private void InitWindsor()
        {
            Container.Register(Component.For<IWindsorContainer>().Instance(Container).LifestyleSingleton());
            Container.Install(FromAssembly.This());
        }
    }
}
