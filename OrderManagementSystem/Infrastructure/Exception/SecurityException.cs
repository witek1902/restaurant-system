namespace OrderManagementSystem.Infrastructure.Exception
{
    using System;

    /// <summary>
    /// Use this class to throw exceptions related to anuthorized access to system functionalities or data
    /// </summary>
    public class SecurityException : Exception
    {
        public const string SecurityExceptionCode = "SecException";

        public SecurityException() : base("Default message") { }

        public SecurityException(string message) : base(message) { }
    }
}