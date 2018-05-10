/************************************
* Rudolf Chrispens                  *
* AES: Advanced Encryption Standard *
* DES: Data Encryption Standard     *
*************************************/

#region using
using UnityEngine;
using System.Collections;
using Dev6;
using System.IO;


//crypt
using System;
using System.Text;
using System.Security;
using System.Security.Cryptography;
using System.Xml.Serialization;
#endregion

namespace Dev6 {
    [AddComponentMenu("Dev6/MANAGER/Encrypt")]
    public class Cryptography : MonoBehaviour, IamSingleton
    {
        void IamSingleton.iInitialize()
        {
            //generate som keys and IVs:
            if(DEBUG)
                Debug.Log("AES generate...");
            GenerateKey();
            if (DEBUG)
                Debug.Log("...AES done!");
        }

        public bool DEBUG = false;
        public AesManaged AES = null;

        #region Init Values
        AesManaged CreateAESManaged()
        {
            AES = new AesManaged();
            AES.Mode = CipherMode.ECB;
            AES.Padding = PaddingMode.PKCS7;
            AES.GenerateKey();
            AES.GenerateIV();
            return AES;
        }
        
        /// <summary>
        /// Returns the Key as a string. Note: also generates a new IV!
        /// </summary>
        /// <param name="_Type"></param>
        /// <returns></returns>
        KeyAndIV GenerateKey()
        {
            // Create an instance of Symetric Algorithm. Key and IV is generated automatically.
            AesManaged aesCrypto = CreateAESManaged();
            return new KeyAndIV( aesCrypto.Key, aesCrypto.IV);
        }
        #endregion

        #region Encryption
        /// <summary>
        /// Use this function this is better for bug testing try not to use the static function.
        /// </summary>
        /// <param name="_It"></param>
        /// <returns></returns>
        public string EncryptString(string _It)
        {
            string result = null;

            if (AES == null) //security
            {
                AES = CreateAESManaged();
            }

            result = EncryptString(_It, AES);
            return result;
        }

        /// <summary>
        /// Do not use this directly except special cases!! Use a reference to a non static EncryptString() function.
        /// </summary>
        /// <param name="_It"></param>
        /// <param name="_AES"></param>
        /// <returns></returns>
        public static string EncryptString(string _It, AesManaged _AES)
        {
            //values:
            byte[] OriginalText = System.Text.Encoding.UTF8.GetBytes(_It);
            byte[] EncryptedText;

            ICryptoTransform Encryptor = _AES.CreateEncryptor();
            EncryptedText = Encryptor.TransformFinalBlock(OriginalText, 0, OriginalText.Length);
            Encryptor.Dispose();

            return Convert.ToBase64String(EncryptedText, 0, EncryptedText.Length);
        }
        #endregion

        #region Decryption
        /// <summary>
        /// Use this function this is better for bug testing try not to use the static function.
        /// </summary>
        /// <param name="_It"></param>
        /// <returns></returns>
        public string DecryptString(string _It)
        {
            string result = null;

            if(AES == null) //security
            {
                AES = CreateAESManaged();
            }

            result = DecryptString(_It, AES);
            return result;
        }

        /// <summary>
        /// Do not use this directly except special cases!! Use a reference to a non static DecryptString() function.
        /// </summary>
        /// <param name="_It"></param>
        /// <param name="_AES"></param>
        /// <returns></returns>
        public static string DecryptString(string _It, AesManaged _AES)
        {
            //values:
            byte[] EncryptedTextInBytes = Convert.FromBase64String( _It );
            byte[] result = null;

            ICryptoTransform Decryptor = _AES.CreateDecryptor();

            result = Decryptor.TransformFinalBlock(EncryptedTextInBytes, 0, EncryptedTextInBytes.Length);
            Decryptor.Dispose();
            return Encoding.UTF8.GetString(result);
        }
        #endregion

        public struct KeyAndIV
        {
            public byte[] Key;
            public byte[] IV;

            public KeyAndIV(byte[] _Key, byte[] _IV)
            {
                Key = _Key;
                IV = _IV;
            }
        }
    }
}