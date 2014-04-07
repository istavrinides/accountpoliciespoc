using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;
using System.IO;

/// <summary>
/// Summary description for EncyptionHelper
/// </summary>
public class EncyptionHelper
{
    #region "Properties"

    // This could be encrypted in the web.config file for security
    public static String SecretKey
    {
        get
        {
            return ConfigurationManager.AppSettings["SecretKey"];
        }
    }
    
    // This could be encrypted in the web.config file for security
    public static String SecretSalt
    {
        get
        {
            return ConfigurationManager.AppSettings["SecretSalt"];
        }
    }

    #endregion
    
    public static String EncryptString(String value)
    {
        if (String.IsNullOrEmpty(value))
            throw new ArgumentNullException("No value given to encypt.");

        var saltBytes = Encoding.ASCII.GetBytes(SecretSalt);
        var key = new Rfc2898DeriveBytes(SecretKey, saltBytes);

        using (var rm = new RijndaelManaged())
        {
            rm.Key = key.GetBytes(rm.KeySize / 8);
            rm.IV = key.GetBytes(rm.BlockSize / 8);

            // Create a decrytor to perform the stream transform.
            ICryptoTransform encryptor = rm.CreateEncryptor(rm.Key, rm.IV);

            // Create the streams used for encryption. 
            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {

                        //Write all data to the stream.
                        swEncrypt.Write(value);
                    }

                    return Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
        }
    }

    public static String EncryptStringWithKey(String value, String secretKey)
    {
        if (String.IsNullOrEmpty(value))
            throw new ArgumentNullException("No value given to encypt.");
        if (String.IsNullOrEmpty(secretKey))
            throw new ArgumentNullException("No key given to encypt with.");

        var saltBytes = Encoding.ASCII.GetBytes(SecretSalt);
        var key = new Rfc2898DeriveBytes(secretKey, saltBytes);

        using (var rm = new RijndaelManaged())
        {
            rm.Key = key.GetBytes(rm.KeySize / 8);
            rm.IV = key.GetBytes(rm.BlockSize / 8);

            // Create a decrytor to perform the stream transform.
            ICryptoTransform encryptor = rm.CreateEncryptor(rm.Key, rm.IV);

            // Create the streams used for encryption. 
            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {

                        //Write all data to the stream.
                        swEncrypt.Write(value);
                    }

                    return Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
        }
    }

    public static String DecryptString(String encryptedValue)
    {
        if (String.IsNullOrEmpty(encryptedValue))
            throw new ArgumentNullException("No value given to decrypt.");

        var saltBytes = Encoding.ASCII.GetBytes(SecretSalt);
        var key = new Rfc2898DeriveBytes(SecretKey, saltBytes);

        using (var rm = new RijndaelManaged())
        {
            rm.Key = key.GetBytes(rm.KeySize / 8);
            rm.IV = key.GetBytes(rm.BlockSize / 8);

            // Create a decrytor to perform the stream transform.
            ICryptoTransform decryptor = rm.CreateDecryptor(rm.Key, rm.IV);

            // Create the streams used for decryption. 
            using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(encryptedValue.Replace(" ", "+"))))
            {
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                    {
                        // Read the decrypted bytes from the decrypting stream 
                        // and place them in a string.
                        return srDecrypt.ReadToEnd();
                    }
                }
            }
        }
    }

    public static String DecryptStringWithKey(String encryptedValue, String secretKey)
    {
        if (String.IsNullOrEmpty(encryptedValue))
            throw new ArgumentNullException("No value given to decrypt.");
        if (String.IsNullOrEmpty(secretKey))
            throw new ArgumentNullException("No key given to decrypt with.");

        var saltBytes = Encoding.ASCII.GetBytes(SecretSalt);
        var key = new Rfc2898DeriveBytes(secretKey, saltBytes);

        using (var rm = new RijndaelManaged())
        {
            rm.Key = key.GetBytes(rm.KeySize / 8);
            rm.IV = key.GetBytes(rm.BlockSize / 8);

            // Create a decrytor to perform the stream transform.
            ICryptoTransform decryptor = rm.CreateDecryptor(rm.Key, rm.IV);

            // Create the streams used for decryption. 
            using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(encryptedValue)))
            {
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                    {
                        // Read the decrypted bytes from the decrypting stream 
                        // and place them in a string.
                        return srDecrypt.ReadToEnd();
                    }
                }
            }
        }
    }
}