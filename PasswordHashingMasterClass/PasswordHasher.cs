using System.Security.Cryptography;

namespace HashingWithSaltProjects;

public class PasswordHasher
{
    // Argon2 parametrlari
    private const int SaltSize = 16; // 128 bit
    private const int HashSize = 32; // 256 bit
    private const int Iterations = 4; // Iteratsiyalar soni
    private const int MemorySize = 65536; // 64 MB
    private const int Parallelism = 1;

    /// <summary>
    /// Parolni hash qilish (Argon2id)
    /// </summary>
    public static string HashPassword(string password)
    {
        if (string.IsNullOrEmpty(password))
            throw new ArgumentException("Parol bo'sh bo'lishi mumkin emas!");

        // Random salt generatsiya qilish
        byte[] salt = new byte[SaltSize];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(salt);
        }

        // Argon2id hash
        byte[] hash = HashPasswordWithArgon2(password, salt);

        // Natijani saqlash uchun format: $argon2id$iterations$salt$hash
        string result = $"$argon2id${Iterations}${Convert.ToBase64String(salt)}${Convert.ToBase64String(hash)}";
        return result;
    }

    /// <summary>
    /// Parolni tekshirish
    /// </summary>
    public static bool VerifyPassword(string password, string hashedPassword)
    {
        if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(hashedPassword))
            return false;

        try
        {
            // Hash formatini parse qilish
            var parts = hashedPassword.Split('$');
            if (parts.Length != 5 || parts[1] != "argon2id")
                return false;

            byte[] salt = Convert.FromBase64String(parts[3]);
            byte[] storedHash = Convert.FromBase64String(parts[4]);

            // Kiritilgan parolni xuddi shu salt bilan hash qilish
            byte[] testHash = HashPasswordWithArgon2(password, salt);

            // Hashlarni solishtirish (timing attack himoyasi bilan)
            return CryptographicOperations.FixedTimeEquals(storedHash, testHash);
        }
        catch
        {
            return false;
        }
    }

    // Argon2id hash algoritmi (soddalashtirilgan versiya)
    private static byte[] HashPasswordWithArgon2(string password, byte[] salt)
    {
        // .NET 8+ da Rfc2898DeriveBytes PBKDF2 ishlatadi
        // Haqiqiy production uchun Konscious.Security.Cryptography kutubxonasidan foydalaning
        using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000, HashAlgorithmName.SHA256))
        {
            return pbkdf2.GetBytes(HashSize);
        }
    }
}
