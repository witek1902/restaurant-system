namespace OrderManagementSystem.Domain.Product
{
    using Castle.Windsor;
    using NHibernate;
    using Infrastructure.Command;
    using Models.Product;

    /// <summary>
    /// Product update
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
        /// Invokes the command and returns the specified type
        /// </summary>
        /// <returns>Result</returns>
        public override Product Execute()
        {
            var product = Session.Load<Product>(productForm.ProductId);
            productBuilder.UpdateProductEntity(product, productForm);
            Session.Update(product);
            return product;
        }

        /// <summary>
        /// Adding custom dependencies to the command.
        /// </summary>
        /// <param name="container">IoC container</param>
        public override void SetupDependencies(IWindsorContainer container)
        {
            productBuilder = container.Resolve<ProductBuilder>();
        }

        /// <summary>
        /// NHibernate session.
        /// </summary>
        public ISession Session { get; set; }
    }
}