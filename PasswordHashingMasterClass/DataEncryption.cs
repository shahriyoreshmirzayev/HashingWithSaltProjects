using System.Security.Cryptography;
using System.Text;

namespace HashingWithSaltProjects;

public class DataEncryption
{
    private const int KeySize = 256;
    private const int IvSize = 16;

    /// <summary>
    /// Ma'lumotni shifrlash (AES-256-CBC)
    /// </summary>
    public static string Encrypt(string plainText, string password)
    {
        if (string.IsNullOrEmpty(plainText))
            throw new ArgumentException("Matn bo'sh bo'lishi mumkin emas!");

        // Paroldan kalit generatsiya qilish
        byte[] key = DeriveKey(password);
        byte[] iv = new byte[IvSize];

        using (var rng = RandomNumberGenerator.Create())
            rng.GetBytes(iv);

        using (Aes aes = Aes.Create())
        {
            aes.Key = key;
            aes.IV = iv;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            using (var encryptor = aes.CreateEncryptor())
            {
                byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
                byte[] encryptedBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);

                // IV va encrypted datani birlashtirish
                byte[] result = new byte[iv.Length + encryptedBytes.Length];
                Buffer.BlockCopy(iv, 0, result, 0, iv.Length);
                Buffer.BlockCopy(encryptedBytes, 0, result, iv.Length, encryptedBytes.Length);

                return Convert.ToBase64String(result);
            }
        }
    }

    /// <summary>
    /// Ma'lumotni deshifrlash
    /// </summary>
    public static string Decrypt(string encryptedText, string password)
    {
        if (string.IsNullOrEmpty(encryptedText))
            throw new ArgumentException("Shifrlangan matn bo'sh bo'lishi mumkin emas!");

        byte[] key = DeriveKey(password);
        byte[] fullData = Convert.FromBase64String(encryptedText);

        // IV va encrypted datani ajratish
        byte[] iv = new byte[IvSize];
        byte[] encryptedBytes = new byte[fullData.Length - IvSize];

        Buffer.BlockCopy(fullData, 0, iv, 0, IvSize);
        Buffer.BlockCopy(fullData, IvSize, encryptedBytes, 0, encryptedBytes.Length);

        using (Aes aes = Aes.Create())
        {
            aes.Key = key;
            aes.IV = iv;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            using (var decryptor = aes.CreateDecryptor())
            {
                byte[] decryptedBytes = decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);
                return Encoding.UTF8.GetString(decryptedBytes);
            }
        }
    }

    private static byte[] DeriveKey(string password)
    {
        using (var sha256 = SHA256.Create())
        {
            return sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        }
    }
}
