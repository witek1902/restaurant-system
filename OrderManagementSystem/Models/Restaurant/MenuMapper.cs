namespace OrderManagementSystem.Models.Restaurant
{
    using System.Linq;
    using System.Collections.Generic;
    using Product;
    using Domain.Restaurant;

    public static class MenuMapper
    {
        public static MenuForm MapToForm(Menu menu)
        {
            var form = new MenuForm
            {
                MenuId = menu.Id,
                RestaurantId = menu.Restaurant?.Id,
                MenuCode = menu.Code,
                MenuName = menu.Name,
                Active = menu.Active,
                Products = new List<ProductForm>(),
                ProductCategories = new List<ProductCategoryForm>()
            };

            if(menu.Products.Any())
                foreach (var product in menu.Products)
                {
                    form.Products.Add(ProductMapper.MapProductToForm(product));
                }

            return form;
        }
    }
}