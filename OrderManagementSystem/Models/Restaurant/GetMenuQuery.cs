namespace OrderManagementSystem.Models.Restaurant
{
    using System.Linq;
    using Domain.Restaurant;
    using System;
    using NHibernate;
    using Infrastructure.Exception;
    using Infrastructure.Query;

    /// <summary>
    /// Pobranie menu po ID
    /// </summary>
    public class GetMenuQuery : Query<MenuForm>
    {
        private readonly Guid menuId;

        public GetMenuQuery(Guid menuId)
        {
            this.menuId = menuId;
        }

        /// <summary>
        /// Metoda do konstruowania i wywoływania zapytania za pomocą sesji NHibernate
        /// </summary>
        /// <param name="session">Sesja NHibernate</param>
        public override MenuForm Execute(ISession session)
        {
            var menu = session
                .CreateQuery("select m from Menu m where m.Id = :menuId")
                .SetGuid("menuId", menuId)
                .List<Menu>()
                .Single();

            if (menu == null)
                throw new TechnicalException(String.Format("Nie można znaleźć menu o podanym id: {0}", menuId));

            return MenuMapper.MapToForm(menu);
        }
    }
}