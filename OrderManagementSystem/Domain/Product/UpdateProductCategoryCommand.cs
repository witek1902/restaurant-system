namespace OrderManagementSystem.Domain.Product
{
    using Castle.Windsor;
    using NHibernate;
    using Infrastructure.Command;
    using Models.Product;

    /// <summary>
    /// Product category update
    /// </summary>
    public class UpdateProductCategoryCommand : Command<ProductCategory>, INeedSession, INeedAutocommitTransaction
    {
        private readonly ProductCategoryForm productCategoryForm;
        private ProductBuilder productBuilder;

        public UpdateProductCategoryCommand(ProductCategoryForm productCategoryForm)
        {
            this.productCategoryForm = productCategoryForm;
        }

        /// <summary>
        /// Invokes the command and returns the specified type
        /// </summary>
        /// <returns>Rezultat</returns>
        public override ProductCategory Execute()
        {
            var productCategory = Session.Load<ProductCategory>(productCategoryForm.ProductCategoryId);

            productBuilder.UpdateProductCategoryEntity(productCategory, productCategoryForm);

            Session.Update(productCategory);

            return productCategory;
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