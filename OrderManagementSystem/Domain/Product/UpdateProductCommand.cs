namespace OrderManagementSystem.Domain.Product
{
    using Castle.Windsor;
    using NHibernate;
    using Infrastructure.Command;
    using Models.Product;

    /// <summary>
    /// Aktualizacja produktu
    /// </summary>
    public class UpdateProductCommand : Command<Product>, INeedSession, INeedAutocommitTransaction
    {
        private readonly ProductForm productForm;
        private ProductBuilder productBuilder;

        public UpdateProductCommand(ProductForm productForm)
        {
            this.productForm = productForm;
        }

        /// <summary>
        /// Wywołuje komendę i zwraca wskazany typ
        /// </summary>
        /// <returns>Rezultat</returns>
        public override Product Execute()
        {
            var product = Session.Load<Product>(productForm.ProductId);
            productBuilder.UpdateProductEntity(product, productForm);
            Session.Update(product);
            return product;
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