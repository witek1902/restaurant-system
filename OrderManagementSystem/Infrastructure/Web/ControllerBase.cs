namespace OrderManagementSystem.Infrastructure.Web
{
    using Security;
    using System.Web.Mvc;
    using Castle.Windsor;
    using NHibernate;
    using Command;
    using Query;

    /// <summary>
    /// Base class for controllers
    /// </summary>
    [InitializeSimpleMembership]
    [Authorize]
    public abstract class ControllerBase : Controller
    {
        /// <summary>
        /// Windsor container injecting dependencies
        /// </summary>
        public IWindsorContainer Container { get; set; }

        private CommandRunner commandRunner;
        protected CommandRunner CommandRunner => commandRunner ?? (commandRunner = Container.Resolve<CommandRunner>());

        /// <summary>
        /// Launching CommandRunner and returns a generic type
        /// </summary>
        protected CommandExecutionResult<T> ExecuteCommand<T>(Command<T> cmd)
        {
            return CommandRunner.ExecuteCommand(cmd);
        }

        /// <summary>
        /// Runs Query and returns a generic result
        /// </summary>
        protected TResult Query<TResult>(Query<TResult> queryToRun)
        {
            return ProvideSession().Query(queryToRun);
        }

        /// <summary>
        /// NHibernate session
        /// </summary>
        protected ISession ProvideSession()
        {
            return Container.Resolve<ISession>();
        }
    }
}