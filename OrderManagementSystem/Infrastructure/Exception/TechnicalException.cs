namespace OrderManagementSystem.Infrastructure.Exception
{
    public class TechnicalException : System.Exception
    {
        public TechnicalException(string message) : base(message) { }
    }
}