namespace OrderManagementSystem.Domain.Restaurant
{
    using Models.Restaurant;
    using Castle.Windsor;
    using NHibernate;
    using Infrastructure.Command;

    /// <summary>
    /// Aktualizacja Menu
    /// </summary>
    public class UpdateMenuCommand : Command<Menu>, INeedSession, INeedAutocommitTransaction
    {
        private readonly MenuForm menuForm;
        private MenuBuilder menuBuilder;

        public UpdateMenuCommand(MenuForm menuForm)
        {
            this.menuForm = menuForm;
        }
        
        /// <summary>
        /// Wywołuje komendę i zwraca wskazany typ
        /// </summary>
        /// <returns>Rezultat</returns>
        public override Menu Execute()
        {
            var menu = Session.Load<Menu>(menuForm.MenuId);

            menuBuilder.UpdateMenuEntity(menu, menuForm);

            Session.Update(menu);

            return menu;
        }

        /// <summary>
        /// Dodawanie własnych zależności do komendy.
        /// </summary>
        /// <param name="container">Kontener IoC</param>
        public override void SetupDependencies(IWindsorContainer container)
        {
            menuBuilder = container.Resolve<MenuBuilder>();
        }

        /// <summary>
        /// Sesja NHibernate.
        /// </summary>
        public ISession Session { get; set; }
    }
}