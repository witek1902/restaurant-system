namespace OrderManagementSystem.Domain.Product
{
    using System;
    using Castle.Windsor;
    using NHibernate;
    using Infrastructure.Command;
    using Models.Product;

    /// <summary>
    /// Tworzenie produktu
    /// </summary>
    public class CreateProductCommand : Command<Guid>, INeedSession, INeedAutocommitTransaction
    {
        private readonly ProductForm productForm;
        private ProductBuilder productBuilder;

        public CreateProductCommand(ProductForm productForm)
        {
            this.productForm = productForm;
        }

        /// <summary>
        /// Wywołuje komendę i zwraca wskazany typ
        /// </summary>
        /// <returns>Rezultat</returns>
        public override Guid Execute()
        {
            var product = productBuilder.ConstructProductEntity(productForm);

            Session.Save(product);

            return product.Id;
        }

        /// <summary>
        /// Dodawanie własnych zależności do komendy.
        /// </summary>
        /// <param name="container">Kontener IoC</param>
        public override void SetupDependencies(IWindsorContainer container)
        {
            productBuilder = container.Resolve<ProductBuilder>();
        }

        /// <summary>
        /// Sesja NHibernate.
        /// </summary>
        public ISession Session { get; set; }
    }
}