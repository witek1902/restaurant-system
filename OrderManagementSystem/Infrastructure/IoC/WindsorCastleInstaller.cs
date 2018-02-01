namespace OrderManagementSystem.Infrastructure.IoC
{
    using Service;
    using System.Reflection;
    using NHibernate.Cfg;
    using Command;
    using Security;
    using System.Web.Mvc;
    using Castle.MicroKernel.Registration;
    using Castle.MicroKernel.SubSystems.Configuration;
    using Castle.Windsor;

    public class WindsorCastleInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromThisAssembly().BasedOn<ControllerBase>().WithServiceSelf().WithServices(typeof(IController)).LifestyleTransient());

            container.Register(Classes.FromThisAssembly().BasedOn<BusinessService>().WithServiceSelf().WithServiceAllInterfaces().LifestyleTransient());

            container.Register(Component.For<ISecurityProvider>().ImplementedBy<SimpleMembershipSecurityProvider>().LifeStyle.Is(Castle.Core.LifestyleType.Singleton));

            container.Register(Component.For<CommandRunner>().ImplementedBy<CommandRunner>().LifestyleTransient());

            var cfg = ConfigureNHibernate("OMSDb", new[] { GetType().Assembly });

            container.Register(
                Component.For<NHibernate.ISessionFactory>().UsingFactoryMethod(kernel => cfg.BuildSessionFactory()).LifestyleSingleton(),
                Component.For<NHibernate.ISession>().UsingFactoryMethod(kernel =>
                {
                    var session = kernel.Resolve<NHibernate.ISessionFactory>().OpenSession();
                    session.FlushMode = NHibernate.FlushMode.Commit;
                    return session;
                })
                .LifestylePerWebRequest());
        }

        protected virtual Configuration ConfigureNHibernate(string connectionStringName, Assembly[] assembliesWithMappings)
        {
            var cfg = new Configuration();

            cfg.DataBaseIntegration(
                db =>
                {
                    db.Dialect<NHibernate.Dialect.MsSql2012Dialect>();
                    db.Driver<NHibernate.Driver.SqlClientDriver>();
                    db.ConnectionProvider<NHibernate.Connection.DriverConnectionProvider>();
                    db.BatchSize = 500;
                    db.IsolationLevel = System.Data.IsolationLevel.ReadCommitted;
                    db.LogSqlInConsole = true;
                    db.ConnectionStringName = connectionStringName;
                    db.Timeout = 30;
                });

            cfg.Proxy(p => p.ProxyFactoryFactory<NHibernate.Bytecode.DefaultProxyFactoryFactory>());

            cfg.Cache(c => c.UseQueryCache = false);

            foreach (var assembly in assembliesWithMappings)
                cfg.AddAssembly(assembly);

            return cfg;
        }
    }
}