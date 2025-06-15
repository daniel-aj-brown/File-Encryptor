using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Encryption
{
    public static class EncryptionManager
    {
        private static byte[] GenerateKeyFromPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                return sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        public static void EncryptFile(string inputFilePath, string password)
        {
            byte[] key = GenerateKeyFromPassword(password);
            string outputFilePath = inputFilePath + ".enc";

            if(File.Exists(outputFilePath))
            {
                throw new FileExistsException(string.Format(Localisations.FileAlreadyExistsErrorMessageLocalisation, outputFilePath));
            }

            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.GenerateIV();
                byte[] iv = aes.IV;

                using (var inputFileStream = new FileStream(inputFilePath, FileMode.Open, FileAccess.Read))
                using (var outputFileStream = new FileStream(outputFilePath, FileMode.Create, FileAccess.Write))
                {
                    outputFileStream.Write(iv, 0, iv.Length);

                    using (var encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
                    using (var cryptoStream = new CryptoStream(outputFileStream, encryptor, CryptoStreamMode.Write))
                    {
                        inputFileStream.CopyTo(cryptoStream);
                    }
                }
            }
        }

        public static void DecryptFile(string encryptedFilePath, string password)
        {
            byte[] key = GenerateKeyFromPassword(password);
            string outputFilePath = encryptedFilePath.Replace(".enc", "");

            if (File.Exists(outputFilePath))
            {
                throw new FileExistsException(string.Format(Localisations.FileAlreadyExistsErrorMessageLocalisation, outputFilePath));
            }

            using (Aes aes = Aes.Create())
            {
                aes.Key = key;

                using (var inputFileStream = new FileStream(encryptedFilePath, FileMode.Open, FileAccess.Read))
                using (var outputFileStream = new FileStream(outputFilePath, FileMode.Create, FileAccess.Write))
                {
                    byte[] iv = new byte[16];
                    inputFileStream.Read(iv, 0, iv.Length);

                    using (var decryptor = aes.CreateDecryptor(aes.Key, iv))
                    using (var cryptoStream = new CryptoStream(inputFileStream, decryptor, CryptoStreamMode.Read))
                    {
                        cryptoStream.CopyTo(outputFileStream);
                    }
                }
            }
        }

        public static void EncryptDirectory(string directoryPath, string password)
        {
            foreach (var filePath in Directory.GetFiles(directoryPath))
            {
                EncryptFile(filePath, password);
            }
        }

        public static void DecryptDirectory(string directoryPath, string password)
        {
            foreach (var filePath in Directory.GetFiles(directoryPath, "*.enc"))
            {
                DecryptFile(filePath, password);
            }
        }
    }
}
