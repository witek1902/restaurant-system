namespace OrderManagementSystem.Infrastructure.Query
{
    using NHibernate;

    /// <summary>
    /// Klasa bazowa dla Queries
    /// </summary>
    /// <typeparam name="TResult">Typ zwracanego rezultatu</typeparam>
    public abstract class Query<TResult>
    {
        /// <summary>
        /// Metoda do konstruowania i wywoływania zapytania za pomocą sesji NHibernate
        /// </summary>
        /// <param name="session">Sesja NHibernate</param>
        public abstract TResult Execute(ISession session);
    }

    /// <summary>
    /// Metoda rozszerzająca sesję NHibernate w możliwość tworzenia zapytań
    /// </summary>
    public static class NHibernateSessionQueryExtension
    {
        /// <summary>
        /// Uruchamia zapytanie dla danej sesji NHibernate
        /// </summary>
        /// <typeparam name="TResult">Typ zwracany przez zapytanie</typeparam>
        /// <param name="session">Sesja NHibernate, która ma zostać użyta do uruchomienia zapytania</param>
        /// <param name="queryToRun">Zapytanie do uruchomienia</param>
        public static TResult Query<TResult>(this ISession session, Query<TResult> queryToRun)
        {
            return queryToRun.Execute(session);
        }
    }
}