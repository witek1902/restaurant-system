namespace OrderManagementSystem.Domain.Product
{
    using Restaurant;
    using NHibernate;
    using Infrastructure.Service;
    using Models.Product;

    /// <summary>
    /// Builder for products and product categories
    /// </summary>
    public class ProductBuilder : BusinessService
    {
        /// <summary>
        /// Creates a new service instance, expects to inject an NHibernate session
        /// </summary>
        public ProductBuilder(ISession session) : base(session)
        {
        }

        /// <summary>
        /// Creating a product entity from the submitted form
        /// </summary>
        /// <param name="productForm">The form filled out by the user</param>
        /// <returns>entity</returns>
        public Product ConstructProductEntity(ProductForm productForm)
        {
            var product = new Product
            {
                Name = productForm.ProductName,
                PhotoUrl = productForm.ProductPhotoUrl,
                Description = productForm.ProductDescription,
                Price = productForm.Price,
                PercentDiscount = productForm.PercentDiscount,
                Active = true,
                Menu = productForm.MenuId.HasValue ? new Menu
                {
                    Id = productForm.MenuId.Value
                } : null,
                ProductCategory = productForm.ProductCategoryId.HasValue ? new ProductCategory
                {
                    Id = productForm.ProductCategoryId.Value
                } : null
            };

            if (productForm.ProductDetailsCalories != null || productForm.ProductDetailsCarbohydrates != null
                || productForm.ProductDetailsFat != null || productForm.ProductDetailsProtein != null)
            {
                product.ProductDetails = new ProductDetails
                {
                    Protein = productForm.ProductDetailsProtein,
                    Carbohydrates = productForm.ProductDetailsCarbohydrates,
                    Fat = productForm.ProductDetailsFat,
                    Calories = productForm.ProductDetailsCalories
                };
            }

            return product;
        }

        /// <summary>
        /// Update product data
        /// </summary>
        /// <param name="product"></param>
        /// <param name="productForm"></param>
        public void UpdateProductEntity(Product product, ProductForm productForm)
        {
            product.Active = productForm.Active;
            product.Name = productForm.ProductName;
            product.PhotoUrl = productForm.ProductPhotoUrl;
            product.Description = productForm.ProductDescription;
            product.Price = productForm.Price;
            product.PercentDiscount = productForm.PercentDiscount;
            product.ProductCategory.Id = productForm.ProductCategoryId.Value;
            product.Menu.Id = productForm.MenuId.Value;

            if (productForm.ProductDetailsCalories != null || productForm.ProductDetailsCarbohydrates != null
                || productForm.ProductDetailsFat != null || productForm.ProductDetailsProtein != null)
            {
                if (product.ProductDetails != null)
                {
                    product.ProductDetails.Calories = productForm.ProductDetailsCalories;
                    product.ProductDetails.Carbohydrates = productForm.ProductDetailsCarbohydrates;
                    product.ProductDetails.Fat = productForm.ProductDetailsFat;
                    product.ProductDetails.Protein = productForm.ProductDetailsProtein;
                }
                else
                {
                    product.ProductDetails = new ProductDetails
                    {
                        Protein = productForm.ProductDetailsProtein,
                        Carbohydrates = productForm.ProductDetailsCarbohydrates,
                        Fat = productForm.ProductDetailsFat,
                        Calories = productForm.ProductDetailsCalories
                    };
                }
            }
        }

        /// <summary>
        /// Creating a product category from the form
        /// </summary>
        /// <param name="productCategoryForm">Product category form</param>
        /// <returns></returns>
        public ProductCategory ConstructProductCategoryEntity(ProductCategoryForm productCategoryForm)
        {
            var productCategory = new ProductCategory
            {
                Name = productCategoryForm.ProductCategoryName,
                Code = productCategoryForm.ProductCategoryCode,
                Restaurant = productCategoryForm.RestaurantId.HasValue ? new Restaurant
                {
                    Id = productCategoryForm.RestaurantId.Value
                } : null
            };

            return productCategory;
        }

        /// <summary>
        /// Product category update
        /// </summary>
        /// <param name="productCategory"></param>
        /// <param name="productCategoryForm"></param>
        public void UpdateProductCategoryEntity(ProductCategory productCategory, ProductCategoryForm productCategoryForm)
        {
            productCategory.Name = productCategoryForm.ProductCategoryName;
            productCategory.Code = productCategory.Code;
        }
    }
}