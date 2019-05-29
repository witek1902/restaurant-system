namespace OrderManagementSystem.Infrastructure.Service
{
    using NHibernate;
    using Query;

    /// <summary>
    /// Business service
    /// </summary>
    public abstract class BusinessService
    {
        protected ISession Session { get; }

        /// <summary>
        /// Creates a new service instance, expects to inject an NHibernate session
        /// </summary>
        protected BusinessService(ISession session)
        {
            this.Session = session;
        }

        /// <summary>
        /// Runs the query
        /// </summary>
        protected virtual TResult Query<TResult>(Query<TResult> queryToExecute)
        {
            return queryToExecute.Execute(Session);
        }
    }
}