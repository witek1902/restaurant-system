namespace OrderManagementSystem.Domain.User
{
    using Common;
    using Infrastructure.Exception;
    using Castle.Windsor;
    using NHibernate;
    using Infrastructure.Command;
    using Models.Restaurant;

    /// <summary>
    /// Employee update
    /// </summary>
    public class UpdateRestaurantWorkerCommand : Command<RestaurantWorker>, INeedSession, INeedAutocommitTransaction
    {
        private readonly RestaurantWorkerForm workerForm;
        private RestaurantWorkerBuilder workerBuilder;

        public UpdateRestaurantWorkerCommand(RestaurantWorkerForm workerForm)
        {
            this.workerForm = workerForm;
        }

        /// <summary>
        /// Invokes the command and returns the specified type
        /// </summary>
        /// <returns>Result</returns>
        public override RestaurantWorker Execute()
        {
            var worker = Session.Load<RestaurantWorker>(workerForm.RestaurantWorkerId);

            if ((workerForm.Position == Position.Manager && worker.Position != Position.Manager)
                || (workerForm.Position != Position.Manager && worker.Position == Position.Manager))
                throw new BusinessException(BusinessErrorCodes.BusinessRulesViolation, "Nie można zmieniać Managera restauracji!");

            workerBuilder.UpdateRestaurantWorkerEntity(worker, workerForm);

            Session.Update(worker);

            return worker;
        }

        /// <summary>
        /// Adding custom dependencies to the command.
        /// </summary>
        /// <param name="container">IoC container</param>
        public override void SetupDependencies(IWindsorContainer container)
        {
            workerBuilder = container.Resolve<RestaurantWorkerBuilder>();
        }

        /// <summary>
        /// NHibernate session.
        /// </summary>
        public ISession Session { get; set; }
    }
}