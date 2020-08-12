namespace OrderManagementSystem.Controllers
{
    using Models.Order;
    using Domain.User;
    using Infrastructure.Security;
    using System;
    using Domain.Product;
    using Domain.Restaurant;
    using Models.Product;
    using Models.Restaurant;
    using System.Web.Mvc;

    /// <summary>
    /// Restaurant management by the Manager
    /// </summary>
    [Authorize(Roles = "managers")]
    public class ManagerController : Infrastructure.Web.ControllerBase
    {
        private Guid? restaurantId;

        /// <summary>
        /// A general view of the restaurant management panel
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var restaurantForm = Query(new GetRestaurantByUserIdQuery(Security.CurrentUserId));
            ViewBag.RestaurantName = restaurantForm.RestaurantName;
            restaurantId = restaurantForm.RestaurantId;

            return View(restaurantForm);
        }

        /// <summary>
        /// Returns the form for editing restaurant data
        /// </summary>
        /// <returns>A completed form of editing</returns>
        [HttpGet]
        public ActionResult EditRestaurant()
        {
            InitializeInfo();
            var restaurantForm = Query(new GetRestaurantQuery(restaurantId.Value));

            return View(restaurantForm);
        }

        /// <summary>
        /// We save changes in the restaurant
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveRestaurant(RestaurantForm restaurantForm)
        {
            if (!ModelState.IsValid)
                return View("EditRestaurant", restaurantForm);

            var cmdResult = ExecuteCommand(new UpdateRestaurantCommand(restaurantForm));

            if(cmdResult.Success)
                return RedirectToAction("Index", "Manager");
            else
                return View("EditRestaurant", restaurantForm);
        }

        /// <summary>
        /// Adding a restaurant employee
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult AddRestaurantWorker()
        {
            return View();
        }

        /// <summary>
        /// Saving a new employee
        /// </summary>
        /// <param name="workerForm">Employee form</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddRestaurantWorker(RestaurantWorkerForm workerForm)
        {
            InitializeInfo();
            var userCmdResult = ExecuteCommand(new CreateAppUserCommand(workerForm.Login, workerForm.Password, 
                RestaurantWorkerMapper.MapPositionEnumToRole(workerForm.Position)));

            if (userCmdResult.Success)
            {
                workerForm.RestaurantId = restaurantId;
                workerForm.AppUserId = userCmdResult.Result;
                var cmdResult = ExecuteCommand(new CreateRestaurantWorkerCommand(workerForm));

                if (cmdResult.Success)
                    return RedirectToAction("Index");
            }

            return View(workerForm);
        }

        /// <summary>
        /// Editing a restaurant employee
        /// </summary>
        /// <param name="userId">UserId</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult EditRestaurantWorker(int userId)
        {
            var workerForm = Query(new GetRestaurantWorkerByUserIdQuery(userId));

            return View(workerForm);
        }

        /// <summary>
        /// Save restaurant information
        /// </summary>
        /// <param name="workerForm">Form of a restaurant employee</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveRestaurantWorker(RestaurantWorkerForm workerForm)
        {
            if (!ModelState.IsValid)
                return View("EditRestaurantWorker", workerForm);

            var cmdResult = ExecuteCommand(new UpdateRestaurantWorkerCommand(workerForm));

            if (cmdResult.Success)
                return RedirectToAction("Index");
            else
                return View("EditRestaurantWorker", workerForm);

        }

        /// <summary>
        /// A view of adding a menu in a restaurant
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult AddMenu()
        {
            return View();
        }

        /// <summary>
        /// Adding a new menu
        /// </summary>
        /// <param name="menuForm"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddMenu(MenuForm menuForm)
        {
            InitializeInfo();
            menuForm.RestaurantId = restaurantId;
            var cmdResult = ExecuteCommand(new CreateMenuCommand(menuForm));

            if (cmdResult.Success)
                return RedirectToAction("EditMenu", new { menuId = cmdResult.Result });

            return View(menuForm);
        }

        /// <summary>
        /// Edit menu view
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult EditMenu(Guid menuId)
        {
            var menuForm = Query(new GetMenuQuery(menuId));

            return View(menuForm);
        }

        /// <summary>
        /// Save changes in the menu
        /// </summary>
        /// <param name="menuForm"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveMenu(MenuForm menuForm)
        {
            if (!ModelState.IsValid)
                return View("EditMenu", menuForm);

            ExecuteCommand(new UpdateMenuCommand(menuForm));
            return RedirectToAction("EditMenu", new { menuId = menuForm.MenuId });
        }

        /// <summary>
        /// View of adding a new product
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult AddProduct()
        {
            InitializeInfo();
            var categories = Query(new GetProductCategoriesByRestaurantIdQuery(restaurantId.Value));
            var menus = Query(new GetMenusByRestaurantIdQuery(restaurantId.Value));

            var productForm = new ProductForm(categories, menus);

            return View(productForm);
        }

        /// <summary>
        /// Adding a new product
        /// </summary>
        /// <param name="productForm"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddProduct(ProductForm productForm)
        {
            var cmdResult = ExecuteCommand(new CreateProductCommand(productForm));

            if (cmdResult.Success)
                return RedirectToAction("EditProduct", new { productId = cmdResult.Result });
            else
            {
                InitializeInfo();
                productForm.ProductCategories = Query(new GetProductCategoriesByRestaurantIdQuery(restaurantId.Value));
                productForm.Menus = Query(new GetMenusByRestaurantIdQuery(restaurantId.Value));
                ViewBag.Message = "There was a problem adding a new product. try again.";

                return View(productForm);
            }
        }

        /// <summary>
        ///  A view to edit the product
        /// </summary>
        /// <param name="productId">Product ID</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult EditProduct(Guid productId)
        {
            InitializeInfo();
            var productForm = Query(new GetProductQuery(productId));
            productForm.ProductCategories = Query(new GetProductCategoriesByRestaurantIdQuery(restaurantId.Value));
            productForm.Menus = Query(new GetMenusByRestaurantIdQuery(restaurantId.Value));

            return View(productForm);
        }

        /// <summary>
        /// Saving changes on the product
        /// </summary>
        /// <param name="productForm"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveProduct(ProductForm productForm)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("EditProduct", new { productId = productForm.ProductId });
            }

            var cmdResult = ExecuteCommand(new UpdateProductCommand(productForm));

            if (cmdResult.Success)
                return RedirectToAction("EditMenu", new {menuId = productForm.MenuId});

            return RedirectToAction("EditProduct", new {productId = productForm.ProductId});
        }

        /// <summary>
        /// Deactivating the product
        /// </summary>
        /// <param name="productId">Product ID</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DeactiveProduct(Guid productId)
        {
            var menuId = Query(new GetMenuIdByProductId(productId));
            ExecuteCommand(new ChangeActiveProductCommand(productId, false));

            return RedirectToAction("EditMenu", new {menuId});
        }

        /// <summary>
        /// Re-activating the product
        /// </summary>
        /// <param name="productId">Product ID</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ActiveProduct(Guid productId)
        {
            var menuId = Query(new GetMenuIdByProductId(productId));
            ExecuteCommand(new ChangeActiveProductCommand(productId, true));

            return RedirectToAction("EditMenu", new { menuId });
        }

        /// <summary>
        /// A view of adding a new product category
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult AddProductCategory()
        {
            InitializeInfo();
            return View(new ProductCategoryForm {ProductCategories = Query(new GetProductCategoriesByRestaurantIdQuery(restaurantId.Value))});
        }

        /// <summary>
        /// Adding a new product category
        /// </summary>
        /// <param name="productCategoryForm">Product category form</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddProductCategory(ProductCategoryForm productCategoryForm)
        {
            InitializeInfo();
            productCategoryForm.RestaurantId = restaurantId;
            var cmdResult = ExecuteCommand(new CreateProductCategoryCommand(productCategoryForm));

            if (cmdResult.Success)
                return RedirectToAction("EditProductCategory", new { productCategoryId = cmdResult.Result });

            return View(productCategoryForm);
        }

        /// <summary>
        /// A view to editing the product category
        /// </summary>
        /// <param name="productCategoryId">Product category ID</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult EditProductCategory(Guid productCategoryId)
        {
            var productCategoryForm = Query(new GetProductCategoryQuery(productCategoryId));

            return View(productCategoryForm);
        }

        /// <summary>
        /// Saving changes in the product category
        /// </summary>
        /// <param name="productCategoryForm">Product category form</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveProductCategory(ProductCategoryForm productCategoryForm)
        {
            if (!ModelState.IsValid)
                return View("EditProductCategory", productCategoryForm);

            ExecuteCommand(new UpdateProductCategoryCommand(productCategoryForm));
            return RedirectToAction("AddProduct");
        }

        /// <summary>
        /// The History of Orders in the restaurant
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult HistoryOrders()
        {
            InitializeInfo();
            var orders = Query(new GetRestaurantHistoryOrdersQuery(restaurantId.Value));

            return View(orders);
        }

        private void InitializeInfo()
        {
            if(restaurantId == null)
                restaurantId = Query(new GetRestaurantIdByUserIdQuery(Security.CurrentUserId));
        }
    }
}