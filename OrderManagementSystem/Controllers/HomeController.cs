namespace OrderManagementSystem.Controllers
{
    using System.Web.Mvc;
    
    /// <summary>
    /// Ekran główny aplikacji
    /// </summary>
    [AllowAnonymous]
    public class HomeController : Infrastructure.Web.ControllerBase
    {
        /// <summary>
        /// Strona główna aplikacji
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Help
        /// </summary>
        /// <returns></returns>
        public ActionResult Help()
        {
            return View();
        }
    }
}