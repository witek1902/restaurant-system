namespace OrderManagementSystem.Domain.Product
{
    using System;
    using Castle.Windsor;
    using NHibernate;
    using Infrastructure.Command;

    /// <summary>
    /// Zmienia aktywność produktu (czy jest tylko historyczny czy nie)
    /// </summary>
    public class ChangeActiveProductCommand : Command<bool>, INeedSession, INeedAutocommitTransaction
    {
        private readonly Guid productId;
        private readonly bool active;

        public ChangeActiveProductCommand(Guid productId, bool active)
        {
            this.productId = productId;
            this.active = active;
        }

        /// <summary>
        /// Wywołuje komendę i zwraca wskazany typ
        /// </summary>
        /// <returns>Rezultat</returns>
        public override bool Execute()
        {
            var product = Session.Load<Product>(productId);
            product.Active = active;
            Session.Update(product);

            return true;
        }

        /// <summary>
        /// Dodawanie własnych zależności do komendy.
        /// </summary>
        /// <param name="container">Kontener IoC</param>
        public override void SetupDependencies(IWindsorContainer container)
        {
        }

        /// <summary>
        /// Sesja NHibernate.
        /// </summary>
        public ISession Session { get; set; }
    }
}