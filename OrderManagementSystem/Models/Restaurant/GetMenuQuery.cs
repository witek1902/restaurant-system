namespace OrderManagementSystem.Models.Restaurant
{
    using System.Linq;
    using Domain.Restaurant;
    using System;
    using NHibernate;
    using Infrastructure.Exception;
    using Infrastructure.Query;

    /// <summary>
    /// Download menu by ID
    /// </summary>
    public class GetMenuQuery : Query<MenuForm>
    {
        private readonly Guid menuId;

        public GetMenuQuery(Guid menuId)
        {
            this.menuId = menuId;
        }

        /// <summary>
        /// A method for constructing and calling a query using the NHibernate session
        /// </summary>
        /// <param name="session">NHibernate session</param>
        public override MenuForm Execute(ISession session)
        {
            var menu = session
                .CreateQuery("select m from Menu m where m.Id = :menuId")
                .SetGuid("menuId", menuId)
                .List<Menu>()
                .Single();

            if (menu == null)
                throw new TechnicalException(String.Format("The menu with the given id can not be found: {0}", menuId));

            return MenuMapper.MapToForm(menu);
        }
    }
}