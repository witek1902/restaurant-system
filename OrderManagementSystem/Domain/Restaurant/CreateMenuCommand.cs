namespace OrderManagementSystem.Domain.Restaurant
{
    using System;
    using Castle.Windsor;
    using NHibernate;
    using Infrastructure.Command;
    using Models.Restaurant;

    /// <summary>
    /// Tworzenie Menu
    /// </summary>
    public class CreateMenuCommand : Command<Guid>, INeedSession, INeedAutocommitTransaction
    {
        private readonly MenuForm menuForm;
        private MenuBuilder menuBuilder;

        public CreateMenuCommand(MenuForm menuForm)
        {
            this.menuForm = menuForm;
        }

        /// <summary>
        /// Wywołuje komendę i zwraca wskazany typ
        /// </summary>
        /// <returns>Rezultat</returns>
        public override Guid Execute()
        {
            var menu = menuBuilder.ConstructMenuEntity(menuForm);

            Session.Save(menu);

            return menu.Id;
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