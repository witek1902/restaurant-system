namespace OrderManagementSystem.Models.Customer
{
    /// <summary>
    /// Mapper for the client entity
    /// </summary>
    public static class CustomerMapper
    {
        /// <summary>
        /// Entity mapping to the form
        /// </summary>
        /// <param name="customer">Customer's entity</param>
        /// <returns>Form</returns>
        public static CustomerForm MapToForm(Domain.User.Customer customer)
        {
            var form = new CustomerForm
            {
                CustomerId = customer.Id,
                Firstname = customer.Firstname,
                Login = customer.AppUser.Login
            };

            //ALL Orders

            return form;
        }
    }
}