namespace OrderManagementSystem.Infrastructure.Command
{
    using Castle.Windsor;
    using NHibernate;
    using Exception;
    using Query;

    /// <summary>
    /// Interface for handling commands
    /// </summary>
    /// <typeparam name="T">Returned result</typeparam>
    public interface ICommand<T>
    {
        /// <summary>
        /// Invokes the command and returns the specified type
        /// </summary>
        /// <returns>Result</returns>
        T Execute();

        /// <summary>
        /// Checks whether the command can be executed
        /// </summary>
        /// <returns>True if the command can be executed, otherwise false</returns>
        bool CanExecute();
    }

    /// <summary>
    /// Base class for commands
    /// </summary>
    /// <typeparam name="T">Return type</typeparam>
    public abstract class Command<T> : ICommand<T>
    {
        /// <summary>
        /// Invokes the command and returns the specified type
        /// </summary>
        /// <returns>Result</returns>
        public abstract T Execute();

        /// <summary>
        /// Checks whether the command can be executed
        /// </summary>
        /// <returns>True if the command can be executed, otherwise false</returns>
        public virtual bool CanExecute()
        {
            return true;
        }

        /// <summary>
        /// Runs the query.
        /// </summary>
        protected virtual TResult Query<TResult>(Query<TResult> queryToExecute)
        {
            var sessionProvider = this as INeedSession;
            if (sessionProvider == null)
                throw new TechnicalException("");
            return queryToExecute.Execute(sessionProvider.Session);
        }

        /// <summary>
        /// Adding custom dependencies to the command.
        /// </summary>
        /// <param name="container">IoC container</param>
        public abstract void SetupDependencies(IWindsorContainer container);
    }

    /// <summary>
    /// An interface that says you should inject NHibernate to CommandRunner
    /// </summary>
    public interface INeedSession
    {
        /// <summary>
        /// NHibernate session.
        /// </summary>
        ISession Session { get; set; }
    }

    /// <summary>
    /// An interface that says that CommandRanner should be running in a transaction
    /// If an exception occurs during 'Execute', the transaction is rolled back.
    /// </summary>
    public interface INeedAutocommitTransaction { }
}