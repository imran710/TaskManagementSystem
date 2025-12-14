using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using System.Text;

namespace TaskManagement.Domain.Helper;

public class SecurityHelper(IOptions<DomainOption.SecurityOption> options)
{
    private const int KeySize = 256;
    private const int BlockSize = 128;
    private const int NumberOfIterations = 10000;
    private static readonly RandomNumberGenerator DefaultRng = RandomNumberGenerator.Create();
    private const int IterationCount = 100_000;

    public string Encrypt(string plainText)
    {
        byte[] salt = new byte[16];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(salt);
        }

        using var rfc2898DeriveBytes = new Rfc2898DeriveBytes(options.Value.AesSecretKey, salt, NumberOfIterations, HashAlgorithmName.SHA256);
        byte[] key = rfc2898DeriveBytes.GetBytes(KeySize / 8);
        byte[] iv = rfc2898DeriveBytes.GetBytes(BlockSize / 8);

        using var aes = Aes.Create();
        aes.KeySize = KeySize;
        aes.BlockSize = BlockSize;
        aes.Mode = CipherMode.CBC;
        aes.Padding = PaddingMode.PKCS7;

        using var encryptor = aes.CreateEncryptor(key, iv);
        using var memoryStream = new MemoryStream();
        memoryStream.Write(salt, 0, salt.Length);
        using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
        {
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
            cryptoStream.FlushFinalBlock();
        }
        return Convert.ToBase64String(memoryStream.ToArray());
    }

    public string Decrypt(string encryptedText)
    {
        byte[] cipherBytes = Convert.FromBase64String(encryptedText);
        byte[] salt = new byte[16];
        Array.Copy(cipherBytes, 0, salt, 0, 16);

        using var rfc2898DeriveBytes = new Rfc2898DeriveBytes(options.Value.AesSecretKey, salt, NumberOfIterations, HashAlgorithmName.SHA256);
        byte[] key = rfc2898DeriveBytes.GetBytes(KeySize / 8);
        byte[] iv = rfc2898DeriveBytes.GetBytes(BlockSize / 8);

        using var aes = Aes.Create();
        aes.KeySize = KeySize;
        aes.BlockSize = BlockSize;
        aes.Mode = CipherMode.CBC;
        aes.Padding = PaddingMode.PKCS7;

        using var decryptor = aes.CreateDecryptor(key, iv);
        using var memoryStream = new MemoryStream(cipherBytes, 16, cipherBytes.Length - 16);
        using var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
        using var streamReader = new StreamReader(cryptoStream);
        return streamReader.ReadToEnd();
    }

   

}

