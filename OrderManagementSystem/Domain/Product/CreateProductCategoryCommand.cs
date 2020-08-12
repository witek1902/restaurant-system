namespace OrderManagementSystem.Domain.Product
{
    using System;
    using Castle.Windsor;
    using NHibernate;
    using Infrastructure.Command;
    using Models.Product;

    /// <summary>
    /// Creating a product category
    /// </summary>
    public class CreateProductCategoryCommand : Command<Guid>, INeedSession, INeedAutocommitTransaction
    {
        private readonly ProductCategoryForm productCategoryForm;
        private ProductBuilder productBuilder;

        public CreateProductCategoryCommand(ProductCategoryForm productCategoryForm)
        {
            this.productCategoryForm = productCategoryForm;
        }

        /// <summary>
        /// Invokes the command and returns the specified type
        /// </summary>
        /// <returns>Result</returns>
        public override Guid Execute()
        {
            var productCategory = productBuilder.ConstructProductCategoryEntity(productCategoryForm);

            Session.Save(productCategory);

            return productCategory.Id;
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