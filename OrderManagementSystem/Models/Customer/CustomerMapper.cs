namespace OrderManagementSystem.Models.Customer
{
    /// <summary>
    /// Mapper dla encji klienta
    /// </summary>
    public static class CustomerMapper
    {
        /// <summary>
        /// Mapowanie encji do formularza
        /// </summary>
        /// <param name="customer">Encja klienta</param>
        /// <returns>Formularz</returns>
        public static CustomerForm MapToForm(Domain.User.Customer customer)
        {
            var form = new CustomerForm
            {
                CustomerId = customer.Id,
                Firstname = customer.Firstname,
                Login = customer.AppUser.Login
            };

            //TODO Orders

            return form;
        }
    }
}