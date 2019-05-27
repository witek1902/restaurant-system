namespace OrderManagementSystem.Infrastructure.Security
{
    using WebMatrix.WebData;
    using System;
    using System.Threading;
    using System.Web.Mvc;
    using System.Web.Security;

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class InitializeSimpleMembershipAttribute : ActionFilterAttribute
    {
        private static SimpleMembershipInitializer initializer;
        private static object initializerLock = new object();
        private static bool isInitialized;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            LazyInitializer.EnsureInitialized(ref initializer, ref isInitialized, ref initializerLock);
        }

        private class SimpleMembershipInitializer
        {
            public SimpleMembershipInitializer()
            {
                try
                {
                    WebSecurity.InitializeDatabaseConnection("OMSDb", "AppUser", "UserId", "Login", true);
                    SeedUsers();
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException(ex.Message);
                }
            }

            private void SeedUsers()
            {
                CreateRoles();
                CreateCustomerUserAccount();
                CreateRestaurantWorkerUserAccount("rw");
                CreateRestaurantWorkerUserAccount("rf");
                CreateRestaurantWorkerUserAccount("rt");
                CreateRestaurantWorkerUserAccount("rs");
            }

            private void CreateRoles()
            {
                if (!Roles.RoleExists("managers"))
                    Roles.CreateRole("managers");

                if (!Roles.RoleExists("customers"))
                    Roles.CreateRole("customers");

                if (!Roles.RoleExists("waiters"))
                    Roles.CreateRole("waiters");

                if (!Roles.RoleExists("cooks"))
                    Roles.CreateRole("cooks");
            }

            private void CreateCustomerUserAccount()
            {
                if (!WebSecurity.UserExists("customer"))
                {
                    WebSecurity.CreateUserAndAccount("customer", "customer");
                    Roles.AddUserToRole("customer", "customers");
                }
            }

            private void CreateRestaurantWorkerUserAccount(string prefix)
            {
                var manager = $"{prefix}-manager";
                var waiter = $"{prefix}-waiter";
                var Cook = $"{prefix}-Cook";

                if (!WebSecurity.UserExists(manager))
                {
                    WebSecurity.CreateUserAndAccount(manager, "manager");
                    Roles.AddUserToRole(manager, "managers");
                }

                if (!WebSecurity.UserExists(waiter))
                {
                    WebSecurity.CreateUserAndAccount(waiter, "waiter");
                    Roles.AddUserToRole(waiter, "waiters");
                }

                if (!WebSecurity.UserExists(Cook))
                {
                    WebSecurity.CreateUserAndAccount(Cook, "Cook");
                    Roles.AddUserToRole(Cook, "cooks");
                }
            }
        }
    }
}