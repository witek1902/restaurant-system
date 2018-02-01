namespace OrderManagementSystem.Domain.Product
{
    using Castle.Windsor;
    using NHibernate;
    using Infrastructure.Command;
    using Models.Product;

    /// <summary>
    /// Aktualizacja kategorii produktu
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
        /// Wywołuje komendę i zwraca wskazany typ
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