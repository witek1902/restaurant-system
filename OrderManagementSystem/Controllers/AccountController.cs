namespace OrderManagementSystem.Controllers
{
    using Domain.User;
    using Infrastructure.Security;
    using Models.Customer;
    using Domain.Restaurant;
    using System.Web.Mvc;
    using Models.Restaurant;

    /// <summary>
    /// The controller responsible for the booking service
    /// </summary>
    public class AccountController : Infrastructure.Web.ControllerBase
    {
        /// <summary>
        /// Returns the view in which we choose whether to register as a restaurant or as a Customer
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login(string message)
        {
            ViewBag.Message = message;
            return View();
        }

        /// <summary>
        /// Log in to the system
        /// </summary>
        /// <param name="user">User</param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(AppUser user)
        {
            if (Security.Login(user.Login, user.Password))
                return RedirectByRole();
            else
                return RedirectToAction("Login", new {message = "Login failed, please try again."});
        }

        /// <summary>
        /// Registration - main screen
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// Returns the restaurant registration view
        /// </summary>
        /// <returns>View to registration</returns>
        [HttpGet]
        [AllowAnonymous]
        public ActionResult RegisterRestaurant()
        {
            return View();
        }

        /// <summary>
        /// Creating a new restaurant account
        /// </summary>
        /// <param name="restaurantForm">Restaurant form</param>
        /// <returns>Editing view or re-create view</returns>
        [HttpPost]
        [AllowAnonymous]
        public ActionResult RegisterRestaurant(RestaurantForm restaurantForm)
        {
            var managerCmdResult = ExecuteCommand(new CreateAppUserCommand(restaurantForm.ManagerLogin, restaurantForm.ManagerPassword, "managers"));

            if (managerCmdResult.Success)
            {
                restaurantForm.ManagerAppUserId = managerCmdResult.Result;
                var restaurantCmdResult = ExecuteCommand(new CreateRestaurantCommand(restaurantForm));
                
                if (restaurantCmdResult.Success)
                    return RedirectToAction("Login", new { message = "Restaurant registered successfully. Log in to the Manager data.." });
            }

            return View(restaurantForm);
        }

        /// <summary>
        /// Customer registration
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public ActionResult RegisterCustomer()
        {
            return View();
        }

        /// <summary>
        /// Creating a new customer account
        /// </summary>
        /// <param name="customerForm">Customer form</param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public ActionResult RegisterCustomer(CustomerForm customerForm)
        {
            var managerCmdResult = ExecuteCommand(new CreateAppUserCommand(customerForm.Login, customerForm.Password, "customers"));

            if (managerCmdResult.Success)
            {
                customerForm.AppUserId = managerCmdResult.Result;
                var cmdResult = ExecuteCommand(new CreateCustomerCommand(customerForm));

                if (cmdResult.Success)
                    return RedirectToAction("Login", new {message = "Your account has been registered. You can log in." });
            }

            return View(customerForm);
        }

        /// <summary>
        /// User Logout
        /// </summary>
        /// <returns>Return to the login page</returns>
        [HttpGet]
        public ActionResult Logout()
        {
            Security.Logout();
            return RedirectToAction("Login", "Account");
        }

        /// <summary>
        /// The method of user redirection depending on the role played after logging in
        /// </summary>
        /// <returns></returns>
        private ActionResult RedirectByRole()
        {
            if (Security.IsUserInRole("managers"))
                return RedirectToAction("Index", "Manager");

            if (Security.IsUserInRole("customers"))
                return RedirectToAction("Index", "Customer");

            if (Security.IsUserInRole("waiters") || Security.IsUserInRole("cooks"))
                return RedirectToAction("Index", "RestaurantWorker");

            return RedirectToAction("Index", "Home");
        }
    }
}