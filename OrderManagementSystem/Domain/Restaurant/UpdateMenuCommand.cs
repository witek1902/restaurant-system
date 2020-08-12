namespace OrderManagementSystem.Domain.Restaurant
{
    using Models.Restaurant;
    using Castle.Windsor;
    using NHibernate;
    using Infrastructure.Command;

    /// <summary>
    /// Menu update
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
        /// Invokes the command and returns the specified type
        /// </summary>
        /// <returns>Result</returns>
        public override Menu Execute()
        {
            var menu = Session.Load<Menu>(menuForm.MenuId);

            menuBuilder.UpdateMenuEntity(menu, menuForm);

            Session.Update(menu);

            return menu;
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