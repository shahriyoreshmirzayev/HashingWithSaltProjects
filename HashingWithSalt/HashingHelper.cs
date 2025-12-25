using System.Security.Cryptography;

namespace HashingWithSaltProjects;

public class HashingHelper
{
    public static string GetHash(string input)
    {
        var salt = new byte[32];
        RandomNumberGenerator.Create().GetBytes(salt);

        var pbkdf2 = new Rfc2898DeriveBytes(input, salt, 50000, HashAlgorithmName.SHA256);

        byte[] hash = pbkdf2.GetBytes(32);

        byte[] hashBytes = new byte[64];
        Array.Copy(salt, 0, hashBytes, 0, 32);
        Array.Copy(hash, 0, hashBytes, 32, 32);

        string hashedPassword = Convert.ToBase64String(hashBytes);
        return hashedPassword;
    }

    public static bool IsHashValid(string input, string hashedValue)
    {
        try
        {
            byte[] hashBytes = Convert.FromBase64String(hashedValue);

            byte[] salt = new byte[32];
            Array.Copy(hashBytes, 0, salt, 0, 32);

            var pbkdf2 = new Rfc2898DeriveBytes(input, salt, 50000, HashAlgorithmName.SHA256);

            byte[] hash = pbkdf2.GetBytes(32);

            for (int i = 0; i < 32; i++)
            {
                if (hashBytes[i + 32] != hash[i])
                {
                    return false;
                }
            }
        }
        catch
        {
            return false;
        }
        return true;
    }
}
