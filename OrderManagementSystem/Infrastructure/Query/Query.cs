namespace OrderManagementSystem.Infrastructure.Query
{
    using NHibernate;

    /// <summary>
    /// Base class for Queries
    /// </summary>
    /// <typeparam name="TResult">The type of result returned</typeparam>
    public abstract class Query<TResult>
    {
        /// <summary>
        /// A method for constructing and calling a query using the NHibernate session
        /// </summary>
        /// <param name="session">NHibernate session</param>
        public abstract TResult Execute(ISession session);
    }

    /// <summary>
    /// A method that extends the NHibernate session to the ability to create queries
    /// </summary>
    public static class NHibernateSessionQueryExtension
    {
        /// <summary>
        /// Runs the query for the given NHibernate session
        /// </summary>
        /// <typeparam name="TResult">The type returned by the query </ typeparam>
        /// <param name="session">NHibernate session, which is to be used to run the query</param>
        /// <param name="queryToRun">Query to run</param>
        public static TResult Query<TResult>(this ISession session, Query<TResult> queryToRun)
        {
            return queryToRun.Execute(session);
        }
    }
}