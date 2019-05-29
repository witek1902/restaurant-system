namespace OrderManagementSystem.Models.Restaurant
{
    using System.Collections.Generic;
    using System.Linq;
    using NHibernate;
    using NHibernate.Criterion;
    using Infrastructure.Query;

    /// <summary>
    /// Query to search for restaurants
    /// </summary>
    public class SearchRestaurantQuery : Query<List<RestaurantSearchResultItem>>
    {
        private readonly RestaurantSearchForm searchForm;

        public SearchRestaurantQuery(RestaurantSearchForm searchForm)
        {
            this.searchForm = searchForm;
        }

        /// <summary>
        /// A method for constructing and calling a query using the NHibernate session
        /// </summary>
        /// <param name="session">NHibernate session</param>
        public override List<RestaurantSearchResultItem> Execute(ISession session)
        {
            var query = session.QueryOver<Domain.Restaurant.Restaurant>();

            if (!string.IsNullOrWhiteSpace(searchForm.RestaurantNameOrCode))
            {
                var searchText = $"%{searchForm.RestaurantNameOrCode}%";
                query.Where(r => r.Name.IsLike(searchText) || r.UniqueCode.IsLike(searchText));
            }

            var results = query.List();

            return results.Select(RestaurantMapper.MapToSearchResultsItem).ToList();
        }
    }
}