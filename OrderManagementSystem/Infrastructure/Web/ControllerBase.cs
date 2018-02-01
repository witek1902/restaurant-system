namespace OrderManagementSystem.Infrastructure.Web
{
    using Security;
    using System.Web.Mvc;
    using Castle.Windsor;
    using NHibernate;
    using Command;
    using Query;

    /// <summary>
    /// Klasa bazowa dla kontrolerów
    /// </summary>
    [InitializeSimpleMembership]
    [Authorize]
    public abstract class ControllerBase : Controller
    {
        /// <summary>
        /// Kontener Windsor wstrzykujacy zaleznosci
        /// </summary>
        public IWindsorContainer Container { get; set; }

        private CommandRunner commandRunner;
        protected CommandRunner CommandRunner => commandRunner ?? (commandRunner = Container.Resolve<CommandRunner>());

        /// <summary>
        /// Uruchamiania CommandRunnera i zwraca typ generyczny
        /// </summary>
        protected CommandExecutionResult<T> ExecuteCommand<T>(Command<T> cmd)
        {
            return CommandRunner.ExecuteCommand(cmd);
        }

        /// <summary>
        /// Uruchamia Query i zwraca generyczny rezultat
        /// </summary>
        protected TResult Query<TResult>(Query<TResult> queryToRun)
        {
            return ProvideSession().Query(queryToRun);
        }

        /// <summary>
        /// Sesja NHibernate
        /// </summary>
        protected ISession ProvideSession()
        {
            return Container.Resolve<ISession>();
        }
    }
}