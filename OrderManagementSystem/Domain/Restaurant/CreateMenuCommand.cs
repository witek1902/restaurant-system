namespace OrderManagementSystem.Domain.Restaurant
{
    using System;
    using Castle.Windsor;
    using NHibernate;
    using Infrastructure.Command;
    using Models.Restaurant;

    /// <summary>
    /// Menu creation
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
        /// Invokes the command and returns the specified type
        /// </summary>
        /// <returns>Result</returns>
        public override Guid Execute()
        {
            var menu = menuBuilder.ConstructMenuEntity(menuForm);

            Session.Save(menu);

            return menu.Id;
        }

        /// <summary>
        /// Adding custom dependencies to the command.
        /// </summary>
        /// <param name="container">IoC container</param>
        public override void SetupDependencies(IWindsorContainer container)
        {
            menuBuilder = container.Resolve<MenuBuilder>();
        }

        /// <summary>
        /// NHibernate session.
        /// </summary>
        public ISession Session { get; set; }
    }
}