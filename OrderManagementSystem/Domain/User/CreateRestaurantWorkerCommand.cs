namespace OrderManagementSystem.Domain.User
{
    using System;
    using Castle.Windsor;
    using NHibernate;
    using Infrastructure.Command;
    using Models.Restaurant;

    /// <summary>
    /// Creating a restaurant employee
    /// </summary>
    public class CreateRestaurantWorkerCommand : Command<Guid>, INeedSession, INeedAutocommitTransaction
    {
        private readonly RestaurantWorkerForm workerForm;
        private RestaurantWorkerBuilder workerBuilder;

        public CreateRestaurantWorkerCommand(RestaurantWorkerForm workerForm)
        {
            this.workerForm = workerForm;
        }

        /// <summary>
        /// Invokes the command and returns the specified type
        /// </summary>
        /// <returns>Result</returns>
        public override Guid Execute()
        {
            var worker = workerBuilder.ConstructRestaurantWorkerEntity(workerForm);
            Session.Save(worker);

            return worker.Id;
        }

        /// <summary>
        /// Adding custom dependencies to the command.
        /// </summary>
        /// <param name="container">IoC container</param>
        public override void SetupDependencies(IWindsorContainer container)
        {
            this.workerBuilder = container.Resolve<RestaurantWorkerBuilder>();
        }

        /// <summary>
        /// NHibernate session.
        /// </summary>
        public ISession Session { get; set; }
    }
}