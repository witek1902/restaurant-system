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
    /// Zarządzanie restauracją przez Managera
    /// </summary>
    [Authorize(Roles = "managers")]
    public class ManagerController : Infrastructure.Web.ControllerBase
    {
        private Guid? restaurantId;

        /// <summary>
        /// Ogólny widok panelu do zarządzania restauracją
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
        /// Zwraca formularz do edycji danych restauracji
        /// </summary>
        /// <returns>Uzupełniony formularz edycji</returns>
        [HttpGet]
        public ActionResult EditRestaurant()
        {
            InitializeInfo();
            var restaurantForm = Query(new GetRestaurantQuery(restaurantId.Value));

            return View(restaurantForm);
        }

        /// <summary>
        /// Zapisujemy zmiany w restauracji
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
        /// Dodawanie pracownika restauracji
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult AddRestaurantWorker()
        {
            return View();
        }

        /// <summary>
        /// Zapisywanie nowego pracownika
        /// </summary>
        /// <param name="workerForm">Formularz pracownika</param>
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
        /// Edytowanie pracownika restauracji
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
        /// Zapisanie informacji restauracji
        /// </summary>
        /// <param name="workerForm">Formularz pracownika restauracji</param>
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
        /// Widok dodawania menu w restauracji
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult AddMenu()
        {
            return View();
        }

        /// <summary>
        /// Dodanie nowego menu
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
        /// Widok edycji menu
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
        /// Zapisanie zmian w menu
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
        /// Widok dodania nowego produktu
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
        /// Dodanie nowego produktu
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
                ViewBag.Message = "Wystąpił problem podczas dodawania nowego produktu. Spróbuj ponownie.";

                return View(productForm);
            }
        }

        /// <summary>
        /// Widok do edycji produktu
        /// </summary>
        /// <param name="productId">Id produktu</param>
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
        /// Zapisanie zmian na produkcie
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
        /// Deaktywowanie produktu
        /// </summary>
        /// <param name="productId">Id produktu</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DeactiveProduct(Guid productId)
        {
            var menuId = Query(new GetMenuIdByProductId(productId));
            ExecuteCommand(new ChangeActiveProductCommand(productId, false));

            return RedirectToAction("EditMenu", new {menuId});
        }

        /// <summary>
        /// Ponowne aktywowanie produktu
        /// </summary>
        /// <param name="productId">Id produktu</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ActiveProduct(Guid productId)
        {
            var menuId = Query(new GetMenuIdByProductId(productId));
            ExecuteCommand(new ChangeActiveProductCommand(productId, true));

            return RedirectToAction("EditMenu", new { menuId });
        }

        /// <summary>
        /// Widok dodania nowej kategorii produktu
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult AddProductCategory()
        {
            InitializeInfo();
            return View(new ProductCategoryForm {ProductCategories = Query(new GetProductCategoriesByRestaurantIdQuery(restaurantId.Value))});
        }

        /// <summary>
        /// Dodanie nowej kategorii produktu
        /// </summary>
        /// <param name="productCategoryForm">Formularz kategorii produktu</param>
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
        /// Widok do edycji kategorii produktu
        /// </summary>
        /// <param name="productCategoryId">Id kategorii produktu</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult EditProductCategory(Guid productCategoryId)
        {
            var productCategoryForm = Query(new GetProductCategoryQuery(productCategoryId));

            return View(productCategoryForm);
        }

        /// <summary>
        /// Zapisanie zmian w kategorii produktu
        /// </summary>
        /// <param name="productCategoryForm">Formularz kategorii produktu</param>
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
        /// Historia zamówień w restauracji
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