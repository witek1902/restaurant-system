namespace OrderManagementSystem.Controllers
{
    using Domain.User;
    using Infrastructure.Security;
    using Models.Customer;
    using Domain.Restaurant;
    using System.Web.Mvc;
    using Models.Restaurant;

    /// <summary>
    /// Kontroler odpowiedzialny za obsługę rezerwacji
    /// </summary>
    public class AccountController : Infrastructure.Web.ControllerBase
    {
        /// <summary>
        /// Zwraca widok, w którym wybieramy czy chcemy zarejestrować się jako restauracja czy jako klient
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
        /// Logowanie do systemu
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
                return RedirectToAction("Login", new {message = "Logowanie nie powiodło się, spróbuj ponownie."});
        }

        /// <summary>
        /// Rejestracja - ekran główny
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// Zwraca widok do rejestracji restauracji
        /// </summary>
        /// <returns>Widok do rejestracji</returns>
        [HttpGet]
        [AllowAnonymous]
        public ActionResult RegisterRestaurant()
        {
            return View();
        }

        /// <summary>
        /// Utworzenie nowego konta restauracji
        /// </summary>
        /// <param name="restaurantForm">Formularz restauracji</param>
        /// <returns>Widok do edycji lub ponowny widok tworzenia</returns>
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
                    return RedirectToAction("Login", new { message = "Restauracja zarejestrowana pomyślnie. Zaloguj się na dane Managera." });
            }

            return View(restaurantForm);
        }

        /// <summary>
        /// Rejestracja klienta
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public ActionResult RegisterCustomer()
        {
            return View();
        }

        /// <summary>
        /// Utworzenie nowego konta klienta
        /// </summary>
        /// <param name="customerForm">Formularz klienta</param>
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
                    return RedirectToAction("Login", new {message = "Twoje konto zostało zarejestrowane. Możesz się zalogować."});
            }

            return View(customerForm);
        }

        /// <summary>
        /// Wylogowywanie użytkownika
        /// </summary>
        /// <returns>Powrót do strony logowania</returns>
        [HttpGet]
        public ActionResult Logout()
        {
            Security.Logout();
            return RedirectToAction("Login", "Account");
        }

        /// <summary>
        /// Metoda przekierowaniu użytkownika w zależności od pełnionej roli po zalogowaniu
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