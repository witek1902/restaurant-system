namespace OrderManagementSystem.Models.Product
{
    public static class ProductMapper
    {
        public static ProductForm MapProductToForm(Domain.Product.Product product)
        {
            var form = new ProductForm
            {
                ProductId = product.Id,
                ProductName = product.Name,
                MenuId = product.Menu.Id,
                MenuName = product.Menu.Name,
                ProductPhotoUrl = product.PhotoUrl,
                ProductCategoryId = product.ProductCategory.Id,
                ProductCategoryName = product.ProductCategory.Name,
                Price = product.Price,
                PercentDiscount = product.PercentDiscount,
                ProductDescription = product.Description,
                ProductDetailsId = product.ProductDetails?.Id,
                ProductDetailsCalories = product.ProductDetails?.Calories,
                ProductDetailsCarbohydrates = product.ProductDetails?.Carbohydrates,
                ProductDetailsFat = product.ProductDetails?.Fat,
                ProductDetailsProtein = product.ProductDetails?.Protein,
                Active = product.Active
            };

            return form;
        }

        public static ProductCategoryForm MapProductCategoryToForm(Domain.Product.ProductCategory productCategory)
        {
            var form = new ProductCategoryForm
            {
                ProductCategoryId = productCategory.Id,
                ProductCategoryCode = productCategory.Code,
                ProductCategoryName = productCategory.Name,
                RestaurantId = productCategory.Restaurant.Id
            };

            return form;
        }
    }
}