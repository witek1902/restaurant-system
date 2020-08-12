namespace OrderManagementSystem.Domain.Product
{
    using System;
    using Castle.Windsor;
    using NHibernate;
    using Infrastructure.Command;
    using Models.Product;

    /// <summary>
    /// Creating a product
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
        /// Invokes the command and returns the specified type
        /// </summary>
        /// <returns>Result</returns>
        public override Guid Execute()
        {
            var product = productBuilder.ConstructProductEntity(productForm);

            Session.Save(product);

            return product.Id;
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