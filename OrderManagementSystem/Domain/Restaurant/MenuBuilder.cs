namespace OrderManagementSystem.Domain.Restaurant
{
    using System.Collections.Generic;
    using Product;
    using NHibernate;
    using Infrastructure.Service;
    using Models.Restaurant;

    /// <summary>
    /// Builder dla Menu
    /// </summary>
    public class MenuBuilder : BusinessService
    {
        /// <summary>
        /// Tworzy nową instancje usługi, oczekuje wstrzyknięcia sesji NHibernate
        /// </summary>
        public MenuBuilder(ISession session) : base(session)
        {
        }

        /// <summary>
        /// Tworzenie nowego Menu
        /// </summary>
        /// <param name="menuForm"></param>
        /// <returns></returns>
        public Menu ConstructMenuEntity(MenuForm menuForm)
        {
            var menu = new Menu
            {
                Name = menuForm.MenuName,
                Code = menuForm.MenuCode,
                Active = true,
                Products = new List<Product>(),
                Restaurant = new Restaurant
                {
                    Id = menuForm.RestaurantId.Value
                }
            };

            return menu;
        }

        /// <summary>
        /// Update Menu
        /// </summary>
        /// <param name="menu"></param>
        /// <param name="menuForm"></param>
        public void UpdateMenuEntity(Menu menu, MenuForm menuForm)
        {
            menu.Active = menuForm.Active;
            menu.Name = menuForm.MenuName;
            menu.Code = menuForm.MenuCode;
        }
    }
}