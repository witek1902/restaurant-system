namespace OrderManagementSystem.Controllers
{
    using System.Web.Mvc;

    /// <summary>
    ///  The main screen of the application
    /// </summary>
    [AllowAnonymous]
    public class HomeController : Infrastructure.Web.ControllerBase
    {
        /// <summary>
        /// Application home page
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