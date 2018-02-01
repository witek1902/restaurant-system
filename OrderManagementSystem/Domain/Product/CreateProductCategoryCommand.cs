namespace OrderManagementSystem.Domain.Product
{
    using System;
    using Castle.Windsor;
    using NHibernate;
    using Infrastructure.Command;
    using Models.Product;

    /// <summary>
    /// Tworzenie kategorii produktu
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
        /// Wywołuje komendę i zwraca wskazany typ
        /// </summary>
        /// <returns>Rezultat</returns>
        public override Guid Execute()
        {
            var productCategory = productBuilder.ConstructProductCategoryEntity(productCategoryForm);

            Session.Save(productCategory);

            return productCategory.Id;
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