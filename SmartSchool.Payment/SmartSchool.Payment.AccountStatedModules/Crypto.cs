using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace SmartSchool.Payment.AccountStatedModules
{
    internal static class Crypto
    {
        internal static byte[] CryptoKey = Encoding.UTF8.GetBytes("IntelliSchool SmartSchool Cryptography Key");

        public static byte[] EncryptLicense(byte[] data, string pin)
        {
            //cipher
            SHA256Managed hasher = new SHA256Managed();
            TripleDES des = TripleDES.Create();
            byte[] pinHash = hasher.ComputeHash(Encoding.UTF8.GetBytes(pin));
            byte[] pinIV = new byte[8];
            byte[] pinKey = new byte[24];
            Array.Copy(pinHash, 0, pinIV, 0, 8);
            Array.Copy(pinHash, 8, pinKey, 0, 24);

            des.KeySize = 192;
            des.IV = pinIV;
            des.Key = pinKey;

            ICryptoTransform encryptor = des.CreateEncryptor();

            return encryptor.TransformFinalBlock(data, 0, data.Length);
        }

        public static byte[] DecryptLicense(byte[] cipherData, string pin)
        {
            //cipher
            SHA256Managed hasher = new SHA256Managed();
            TripleDES des = TripleDES.Create();
            byte[] pinHash = hasher.ComputeHash(Encoding.UTF8.GetBytes(pin));
            byte[] pinIV = new byte[8];
            byte[] pinKey = new byte[24];
            Array.Copy(pinHash, 0, pinIV, 0, 8);
            Array.Copy(pinHash, 8, pinKey, 0, 24);

            des.KeySize = 192;
            des.IV = pinIV;
            des.Key = pinKey;

            ICryptoTransform encryptor = des.CreateDecryptor();

            return encryptor.TransformFinalBlock(cipherData, 0, cipherData.Length);
        }

    }
}
