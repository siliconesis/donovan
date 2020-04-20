namespace Donovan.Server.Security
{
    // The namespace must be declared inside class namespace to avoid
    // ambiguity between the BCrypt namespace and its class name.
    using BCrypt.Net;

    public static class PasswordHelper
    {
        public static string HashPassword(string password)
        {
            return BCrypt.HashPassword(password);
        }

        public static bool IsValid(string actual, string expected)
        {
            return BCrypt.Verify(actual, expected);
        }
    }
}
