using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;

namespace TodoAPI.Utils;

public class PasswordHasher
{

    private const int SaltSize = 16;
    private const int HashSize = 32;
    private const int Iterations = 100000;

    private static readonly HashAlgorithmName algorithm = HashAlgorithmName.SHA256;

    public static String Hash(String password)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);
        byte[] Hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, algorithm, HashSize);

        return $"{Convert.ToHexString(Hash)}-{Convert.ToHexString(salt)}";
    }

    public static bool Verify(string password, string passwordHash)
    {
        string[] parts = passwordHash.Split("-");

        byte[] hash = Convert.FromHexString(parts[0]);
        byte[] salt = Convert.FromHexString(parts[1]);

        byte[] inputHash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, algorithm, HashSize);

        return CryptographicOperations.FixedTimeEquals(inputHash, hash);
    }
}
