namespace OrderManagementSystem.Domain.User
{
    using System;
    using Castle.Windsor;
    using NHibernate;
    using Infrastructure.Command;
    using Models.Restaurant;

    /// <summary>
    /// Tworzenie pracownika restauracji
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
        /// Wywołuje komendę i zwraca wskazany typ
        /// </summary>
        /// <returns>Rezultat</returns>
        public override Guid Execute()
        {
            var worker = workerBuilder.ConstructRestaurantWorkerEntity(workerForm);
            Session.Save(worker);

            return worker.Id;
        }

        /// <summary>
        /// Dodawanie własnych zależności do komendy.
        /// </summary>
        /// <param name="container">Kontener IoC</param>
        public override void SetupDependencies(IWindsorContainer container)
        {
            this.workerBuilder = container.Resolve<RestaurantWorkerBuilder>();
        }

        /// <summary>
        /// Sesja NHibernate.
        /// </summary>
        public ISession Session { get; set; }
    }
}