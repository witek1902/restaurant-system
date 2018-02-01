namespace OrderManagementSystem.Infrastructure.IoC
{
    using System;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;
    using Castle.Windsor;

    public class WindsorControllerFactory : DefaultControllerFactory
    {
        private readonly IWindsorContainer container;

        public WindsorControllerFactory(IWindsorContainer container)
        {
            if (container == null)
                throw new ArgumentNullException(nameof(container));
            
            this.container = container;
        }

        protected override IController GetControllerInstance(RequestContext context, Type controllerType)
        {
            if (controllerType == null)
                throw new HttpException(404, $"The controller for path '{context.HttpContext.Request.Path}' could not be found or it does not implement IController.");

            return (IController)container.Resolve(controllerType);
        }

        public override void ReleaseController(IController controller)
        {
            var disposable = controller as IDisposable;

            disposable?.Dispose();
            container.Release(controller);
        }
    }
}