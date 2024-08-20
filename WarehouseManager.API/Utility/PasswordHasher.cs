using System.Security.Cryptography;
using System.Text;

namespace WarehouseManager.API.Utility;

public class PasswordHasher
{
    public static string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(bytes);
    }

    public static bool VerifyPassword(string enteredPassword, string storedHash)
    {
        var hashedPassword = HashPassword(enteredPassword);
        return hashedPassword == storedHash;
    }
}
