namespace OrderManagementSystem.Infrastructure.Service
{
    using NHibernate;
    using Query;

    /// <summary>
    /// Serwis biznesowy
    /// </summary>
    public abstract class BusinessService
    {
        protected ISession Session { get; }

        /// <summary>
        /// Tworzy nową instancje usługi, oczekuje wstrzyknięcia sesji NHibernate
        /// </summary>
        protected BusinessService(ISession session)
        {
            this.Session = session;
        }

        /// <summary>
        /// Uruchamia zapytanie
        /// </summary>
        protected virtual TResult Query<TResult>(Query<TResult> queryToExecute)
        {
            return queryToExecute.Execute(Session);
        }
    }
}