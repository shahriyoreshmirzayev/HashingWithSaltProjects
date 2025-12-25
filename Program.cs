using HashingWithSaltProjects;
using HashingWithSaltProjects.HashingWithSalt;
using System.Security.Cryptography;
using System.Text;

SHAHRIYOR:

Console.WriteLine("1. Parolni SHA256 orqali HASH qilish \n2. Encryption Decryption qilish");
int a = int.Parse(Console.ReadLine()!);
if (a == 1)
{
    while (true)
    {
        Console.WriteLine("Quyidagilardan birini kiriting:");
        Console.WriteLine("1 - Parolni HASH qilish.");
        Console.WriteLine("2 - Parol HASH'ini tekshirish.");

        string userInputOptionStr = Console.ReadLine();
        bool isUserInputOptionValid = int.TryParse(userInputOptionStr, out int userInputOption);
        if (!isUserInputOptionValid)
        {
            Console.WriteLine("Noto'g'ri son kiritildi.");
        }
        else
        {
            if (userInputOption == 1)
            {
                Console.WriteLine("Parolni kiriting:");
                string hashStr = Console.ReadLine();

                string hashedPassword = HashingHelper.GetHash(hashStr);
                Console.WriteLine($"Parolning HASH-qiymati: {hashedPassword}");
            }

            if (userInputOption == 2)
            {
                Console.WriteLine("HASH'ni kiriting:");
                string hashStr = Console.ReadLine();

                Console.WriteLine("Solishtirmoqchi bo'lgan parolni kiriting:");
                string input = Console.ReadLine();

                bool isValid = HashingHelper.IsHashValid(input, hashStr);
                string output = isValid ? "HASH to'g'ri!" : "HASH noto'g'ri.";
                Console.WriteLine(output);
                Console.WriteLine();
            }
        }
        Console.WriteLine("Dastur qaytadan ishga tushadi...");
        Console.WriteLine("\n");
    }
}
else if (a == 2)
{

    Console.OutputEncoding = Encoding.UTF8;

    while (true)
    {
        Console.Clear();
        Console.WriteLine("╔═══════════════════════════════════════════════════════╗");
        Console.WriteLine("║        MA'LUMOT SHIFRLASH DASTURI (AES-256)           ║");
        Console.WriteLine("╚═══════════════════════════════════════════════════════╝");
        Console.WriteLine();
        Console.WriteLine("  1 - Ma'lumotni shifrlash (Encryption)");
        Console.WriteLine("  2 - Ma'lumotni ochish (Decryption)");
        Console.WriteLine("  0 - Chiqish");
        Console.WriteLine();
        Console.Write("Tanlang (1/2/0): ");

        string choice = Console.ReadLine();
        Console.WriteLine();

        if (choice == "1")
        {
            EncryptData();
        }
        else if (choice == "2")
        {
            DecryptData();
        }
        else if (choice == "0")
        {
            Console.WriteLine("Dasturdan chiqildi. Xayr!");
            break;
        }
        else
        {
            Console.WriteLine("❌ Noto'g'ri tanlov! 1, 2 yoki 0 ni tanlang.");
            Console.WriteLine("Davom etish uchun Enter bosing...");
            Console.ReadLine();
        }
    }





}
else
{
    Console.WriteLine("Xato son kiritildi.");
}
Console.WriteLine("\n");



static void EncryptData()
{
    Console.WriteLine("═══════════════════════════════════════════════════════");
    Console.WriteLine("           🔒 MA'LUMOTNI SHIFRLASH");
    Console.WriteLine("═══════════════════════════════════════════════════════");
    Console.WriteLine();

    // Ma'lumotni kiritish
    Console.Write("Shifrlash uchun ma'lumotni kiriting: ");
    string plainText = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(plainText))
    {
        Console.WriteLine();
        Console.WriteLine("❌ Xatolik: Ma'lumot bo'sh bo'lishi mumkin emas!");
        Console.WriteLine();
        Console.WriteLine("Davom etish uchun Enter bosing...");
        Console.ReadLine();
        return;
    }

    // Parolni kiritish
    Console.Write("Shifrlash uchun parol kiriting: ");
    string password = ReadPassword();
    Console.WriteLine();

    if (string.IsNullOrWhiteSpace(password))
    {
        Console.WriteLine("❌ Xatolik: Parol bo'sh bo'lishi mumkin emas!");
        Console.WriteLine();
        Console.WriteLine("Davom etish uchun Enter bosing...");
        Console.ReadLine();
        return;
    }

    try
    {
        // Shifrlash
        string encrypted = EncryptionHelper.Encrypt(plainText, password);

        Console.WriteLine();
        Console.WriteLine("✅ Ma'lumot muvaffaqiyatli shifrlandi!");
        Console.WriteLine();
        Console.WriteLine("───────────────────────────────────────────────────────");
        Console.WriteLine("📄 Sizning ma'lumotingiz:");
        Console.WriteLine($"   {plainText}");
        Console.WriteLine();
        Console.WriteLine("🔐 Shifrlangan ko'rinish:");
        Console.WriteLine($"   {encrypted}");
        Console.WriteLine("───────────────────────────────────────────────────────");
        Console.WriteLine();
        Console.WriteLine("💡 Bu shifrlangan ma'lumotni saqlab qo'ying!");
        Console.WriteLine("   Uni ochish uchun xuddi shu parol kerak bo'ladi.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"❌ Xatolik yuz berdi: {ex.Message}");
    }

    Console.WriteLine();
    Console.WriteLine("Davom etish uchun Enter bosing...");
    Console.ReadLine();
}

static void DecryptData()
{
    Console.WriteLine("═══════════════════════════════════════════════════════");
    Console.WriteLine("           🔓 MA'LUMOTNI OCHISH");
    Console.WriteLine("═══════════════════════════════════════════════════════");
    Console.WriteLine();

    // Shifrlangan ma'lumotni kiritish
    Console.Write("Shifrlangan ma'lumotni kiriting: ");
    string encryptedText = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(encryptedText))
    {
        Console.WriteLine();
        Console.WriteLine("❌ Xatolik: Shifrlangan ma'lumot bo'sh bo'lishi mumkin emas!");
        Console.WriteLine();
        Console.WriteLine("Davom etish uchun Enter bosing...");
        Console.ReadLine();
        return;
    }

    // Parolni kiritish
    Console.Write("Parolni kiriting: ");
    string password = ReadPassword();
    Console.WriteLine();

    if (string.IsNullOrWhiteSpace(password))
    {
        Console.WriteLine("❌ Xatolik: Parol bo'sh bo'lishi mumkin emas!");
        Console.WriteLine();
        Console.WriteLine("Davom etish uchun Enter bosing...");
        Console.ReadLine();
        return;
    }

    try
    {
        // Deshifrlash
        string decrypted = EncryptionHelper.Decrypt(encryptedText, password);

        Console.WriteLine();
        Console.WriteLine("✅ Ma'lumot muvaffaqiyatli ochildi!");
        Console.WriteLine();
        Console.WriteLine("───────────────────────────────────────────────────────");
        Console.WriteLine("📄 Ochilgan ma'lumot:");
        Console.WriteLine($"   {decrypted}");
        Console.WriteLine("───────────────────────────────────────────────────────");
    }
    catch (CryptographicException)
    {
        Console.WriteLine();
        Console.WriteLine("❌ XATOLIK: Noto'g'ri parol yoki buzilgan ma'lumot!");
        Console.WriteLine("   Iltimos, to'g'ri parolni kiriting.");
    }
    catch (FormatException)
    {
        Console.WriteLine();
        Console.WriteLine("❌ XATOLIK: Noto'g'ri format!");
        Console.WriteLine("   Shifrlangan ma'lumot noto'g'ri kiritilgan.");
    }
    catch (Exception ex)
    {
        Console.WriteLine();
        Console.WriteLine($"❌ Xatolik: {ex.Message}");
    }

    Console.WriteLine();
    Console.WriteLine("Davom etish uchun Enter bosing...");
    Console.ReadLine();
}

// Parolni yashirin kiritish uchun helper method
static string ReadPassword()
{
    StringBuilder password = new StringBuilder();

    while (true)
    {
        ConsoleKeyInfo key = Console.ReadKey(true);

        if (key.Key == ConsoleKey.Enter)
        {
            break;
        }
        else if (key.Key == ConsoleKey.Backspace)
        {
            if (password.Length > 0)
            {
                password.Remove(password.Length - 1, 1);
                Console.Write("\b \b");
            }
        }
        else if (!char.IsControl(key.KeyChar))
        {
            password.Append(key.KeyChar);
            Console.Write("*");
        }
    }

    return password.ToString();
}
goto SHAHRIYOR;