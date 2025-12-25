using HashingWithSaltProjects;
using HashingWithSaltProjects.HashingWithSalt;
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
    Console.WriteLine("╔═══════════════════════════════════════════════════════╗");
    Console.WriteLine("║   PAROL XAVFSIZLIGI - MASTER CLASS DEMO               ║");
    Console.WriteLine("╚═══════════════════════════════════════════════════════╝\n");

    // ============= QISM 1: PAROL HASHING =============
    Console.WriteLine("📌 QISM 1: PAROL HASHING (To'g'ri usul)\n");
    Console.WriteLine("   Parollar HECH QACHON decrypt qilinmaydi!");
    Console.WriteLine("   Faqat hash va tekshirish!\n");

    string userPassword = "MySecureP@ss123";
    Console.WriteLine($"Original parol: {userPassword}");

    // Parolni hash qilish
    string hashedPassword = PasswordHasher.HashPassword(userPassword);
    Console.WriteLine($"\n✅ Hash natija:");
    Console.WriteLine($"   {hashedPassword}");
    Console.WriteLine($"   Uzunlik: {hashedPassword.Length} belgi\n");

    // To'g'ri parolni tekshirish
    bool isValid = PasswordHasher.VerifyPassword(userPassword, hashedPassword);
    Console.WriteLine($"✅ To'g'ri parol: {isValid}");

    // Noto'g'ri parolni tekshirish
    bool isInvalid = PasswordHasher.VerifyPassword("WrongPassword", hashedPassword);
    Console.WriteLine($"❌ Noto'g'ri parol: {isInvalid}\n");

    Console.WriteLine("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n");

    // ============= QISM 2: MA'LUMOT SHIFRLASH =============
    Console.WriteLine("📌 QISM 2: MA'LUMOT SHIFRLASH (AES-256)\n");
    Console.WriteLine("   Bu FAQAT maxfiy ma'lumotlar uchun!");
    Console.WriteLine("   Parollar uchun ISHLATILMAYDI!\n");

    string secretData = "Karta raqami: 1234-5678-9012-3456";
    string encryptionKey = "MyMasterKey2024!";

    Console.WriteLine($"Original ma'lumot: {secretData}");

    // Shifrlash
    string encrypted = DataEncryption.Encrypt(secretData, encryptionKey);
    Console.WriteLine($"\n🔒 Shifrlangan:");
    Console.WriteLine($"   {encrypted}\n");

    // Deshifrlash
    string decrypted = DataEncryption.Decrypt(encrypted, encryptionKey);
    Console.WriteLine($"🔓 Deshifrlangan:");
    Console.WriteLine($"   {decrypted}\n");

    Console.WriteLine("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n");

    // ============= QISM 3: XAVFSIZLIK TAMOYILLARI =============
    Console.WriteLine("📌 QISM 3: XAVFSIZLIK TAMOYILLARI\n");
    Console.WriteLine("✅ TO'G'RI:");
    Console.WriteLine("   • Parollarni hash qiling (Argon2, bcrypt, PBKDF2)");
    Console.WriteLine("   • Har bir parol uchun unique salt");
    Console.WriteLine("   • Hech qachon parolni dekodlamang\n");

    Console.WriteLine("❌ NOTO'G'RI:");
    Console.WriteLine("   • Parollarni plain text saqlash");
    Console.WriteLine("   • Parollarni reversible shifrlash");
    Console.WriteLine("   • MD5 yoki SHA1 ishlatish (zaif!)\n");

    Console.WriteLine("╔═══════════════════════════════════════════════════════╗");
    Console.WriteLine("║   DEMO TUGADI - Savol-javob uchun tayyor!           ║");
    Console.WriteLine("╚═══════════════════════════════════════════════════════╝");

    Console.ReadKey();
}
else
{
    Console.WriteLine("Xato son kiritildi.");
}
Console.WriteLine("\n");

goto SHAHRIYOR;