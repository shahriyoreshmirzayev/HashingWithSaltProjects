using HashingWithSaltProjects;
using System.Security.Cryptography;
using System.Text;

SHAHRIYOR:

Console.WriteLine("1. Parolni SHA256 orqali HASH qilish \n2. Encryption Decryption qilish");
string type = Console.ReadLine()!;
Console.Clear();
if (type == "1")
{
    while (true)
    {
        Console.WriteLine("Quyidagilardan birini kiriting:");
        Console.WriteLine("1 - Parolni HASH qilish.");
        Console.WriteLine("2 - Parol HASH'ini tekshirish.");
        Console.WriteLine("0 - Dasturdan chiqish");
        string userInputOptionStr = Console.ReadLine()!;
        Console.Clear();
        bool isUserInputOptionValid = int.TryParse(userInputOptionStr, out int userInputOption);
        if (!isUserInputOptionValid)
        {
        }
        else if (userInputOptionStr == "1" || userInputOptionStr == "2")
        {
            if (userInputOption == 1)
            {
                Console.WriteLine("Parolni kiriting:");
                string hashStr = Console.ReadLine()!;

                string hashedPassword = HashingHelper.GetHash(hashStr);
                Console.WriteLine($"Parolning HASH-qiymati: {hashedPassword}");
            }

            if (userInputOption == 2)
            {
                Console.WriteLine("HASH'ni kiriting:");
                string hashStr = Console.ReadLine()!;

                Console.WriteLine("Solishtirmoqchi bo'lgan parolni kiriting:");
                string input = Console.ReadLine()!;

                bool isValid = HashingHelper.IsHashValid(input, hashStr);
                string output = isValid ? "HASH to'g'ri!" : "HASH noto'g'ri.";
                Console.WriteLine(output);
                Console.WriteLine();
            }
        }
        else if (userInputOptionStr == "0")
        {
            Console.WriteLine("EXIT");
            break;
        }
        Console.WriteLine("Dastur qaytadan ishga tushadi...");
        Console.WriteLine("\n");
    }
}
else if (type == "2")
{

    Console.OutputEncoding = Encoding.UTF8;

    while (true)
    {
        Console.WriteLine("1 - Ma'lumotni shifrlash (Encryption)\n2 - Ma'lumotni ochish (Decryption)\n0 - Chiqish");
        string choice = Console.ReadLine()!;
        Console.Clear();
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
            Console.WriteLine("EXIT");
            break;
        }
        else
        {
            Console.WriteLine("Noto'g'ri tanlov! 1, 2 yoki 0 ni tanlang.");
        }
    }





}
else if (type != "0" || type != "1" || type != "2")
{
    Console.WriteLine("Xato son kiritildi.");
}
else if (type == "0")
{
    Console.WriteLine("Dasturdan chiqildi. Xayr!");
    //break;
}
Console.WriteLine("\n");



static void EncryptData()
{
    // Ma'lumotni kiritish
    Console.Write("Shifrlash uchun ma'lumotni kiriting: ");
    string plainText = Console.ReadLine()!;

    if (string.IsNullOrWhiteSpace(plainText))
    {
        Console.WriteLine("Ma'lumot bo'sh bo'lishi mumkin emas!");
        return;
    }

    // Parolni kiritish
    Console.Write("Shifrlash uchun parol kiriting: ");
    string password = ReadPassword();

    if (string.IsNullOrWhiteSpace(password))
    {
        Console.WriteLine("Parol bo'sh bo'lishi mumkin emas!");
        return;
    }

    try
    {
        // Shifrlash
        string encrypted = EncryptionHelper.Encrypt(plainText, password);

        Console.WriteLine("Ma'lumot muvaffaqiyatli shifrlandi!");
        Console.WriteLine($"Sizning ma'lumotingiz: {plainText}\nShifrlangan ko'rinish: {encrypted}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Xatolik yuz berdi: {ex.Message}");
    }

    Console.WriteLine("\nDavom etish uchun Enter bosing...");
}

static void DecryptData()
{
    // Shifrlangan ma'lumotni kiritish
    Console.Write("Shifrlangan ma'lumotni kiriting: ");
    string encryptedText = Console.ReadLine()!;

    if (string.IsNullOrWhiteSpace(encryptedText))
    {
        Console.WriteLine("Shifrlangan ma'lumot bo'sh bo'lishi mumkin emas!");
        return;
    }

    // Parolni kiritish
    Console.Write("Parolni kiriting: ");
    string password = ReadPassword();
    Console.WriteLine();

    if (string.IsNullOrWhiteSpace(password))
    {
        Console.WriteLine("Parol bo'sh bo'lishi mumkin emas!");
        return;
    }

    try
    {
        // Deshifrlash
        string decrypted = EncryptionHelper.Decrypt(encryptedText, password);

        Console.WriteLine("\nMa'lumot muvaffaqiyatli ochildi!");
        Console.WriteLine($"Ochilgan ma'lumot: {decrypted}\n");
    }
    catch (CryptographicException)
    {
        Console.WriteLine("Noto'g'ri parol yoki buzilgan ma'lumot!");
    }
    catch (FormatException)
    {
        Console.WriteLine("Shifrlangan ma'lumot noto'g'ri kiritilgan.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Xatolik: {ex.Message}");
    }

    Console.WriteLine("Davom etish uchun Enter bosing...");
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