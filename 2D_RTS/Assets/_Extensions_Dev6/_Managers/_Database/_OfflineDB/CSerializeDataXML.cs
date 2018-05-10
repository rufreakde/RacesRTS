/* ************************************ *
 * 	Copyright @ Dev6 Game Studio		*
 * 	Script creator Rudolf Chrispens		*
 *  AES: Advanced Encryption Standard   *
 *  DES: Data Encryption Standard       *
 * ************************************ */

using UnityEngine;
using System.Collections;
using System;
//serialize
using System.IO;
using System.Security.Cryptography;
using System.Xml.Serialization;



namespace Dev6
{
    [RequireComponent(typeof(Cryptography))]
    [AddComponentMenu("Dev6/MANAGER/XML Serializer")]
    public class CSerializeDataXML : MonoBehaviour, IamSingleton
    {
        void IamSingleton.iInitialize()
        {
            if (DEBUG)
                Debug.Log("Init Singleton XML Serialization...");
            CreateDirectory();
            AES = this.GetSafeComponent<Cryptography>().AES;
            if (DEBUG)
                Debug.Log("...finished");
        }

        void CreateDirectory()
        {
            Directory.CreateDirectory(Application.dataPath + Path);
        }

        #region Serialize
        [InfoBox("[XML Data] Path: '_Extensions_Dev6/SaveData/' + FILENAME.xml")]
        public bool DEBUG = false;
        private AesManaged AES = null;
        public static string Path = "_Extensions_Dev6/SaveData/";
        /// <summary>
        /// Try not to use the static function get a reference and use the public function instead!
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_Filename"></param>
        /// <param name="_SerializeThis"></param>
        public static void SerializeXML<T>(string _Filename, T _SerializeThis)
        {
            string cPath = Application.dataPath + Path + _Filename + ".xml";

            XmlSerializer serializer = new XmlSerializer(typeof(T));

            using (TextWriter writer = new StreamWriter(cPath))
            {
                //Debug.Log("Serialize XML:     " + cPath);
                serializer.Serialize(writer, _SerializeThis);
                Debug.Log("STATIC Save XML :  " + cPath);
            }
        }
        public void Serialize<T>(string _Filename, T _SerializeThis)
        {
            string cPath = Application.dataPath + Path + _Filename + ".xml";

            XmlSerializer serializer = new XmlSerializer(typeof(T));

            using (TextWriter writer = new StreamWriter(cPath))
            {
                //Debug.Log("Serialize XML:     " + cPath);
                serializer.Serialize(writer, _SerializeThis);
                Debug.Log("Save XML :  " + cPath);
            }
        }

        /// <summary>
        /// Try not to use the static function get a reference and use the public function instead!
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_Filename"></param>
        /// <param name="_SerializeThis"></param>
        /// <param name="_AES"></param>
        public static void SerializeCrypted<T>(string _Filename, T _SerializeThis, AesManaged _AES)
        {
            string cPath = Application.dataPath + Path + _Filename + "_s.xml";

            ICryptoTransform desencrypt = _AES.CreateEncryptor();

            using (FileStream fileStream = File.Open(cPath, FileMode.Create))
            {
                using (CryptoStream cryptoStream = new CryptoStream(fileStream, desencrypt, CryptoStreamMode.Write))
                {
                    XmlSerializer xmlser = new XmlSerializer(typeof(T));
                    xmlser.Serialize(cryptoStream, _SerializeThis);
                    Debug.Log("STATIC Save XML :  " + cPath);
                }
            }
        }
        public void SerializeCrypted<T>(string _Filename, T _SerializeThis)
        {
            string cPath = Application.dataPath + Path + _Filename + "_s.xml";

            ICryptoTransform desencrypt = AES.CreateEncryptor();

            using (FileStream fileStream = File.Open(cPath, FileMode.Create))
            {
                using (CryptoStream cryptoStream = new CryptoStream(fileStream, desencrypt, CryptoStreamMode.Write))
                {
                    XmlSerializer xmlser = new XmlSerializer(typeof(T));
                    xmlser.Serialize(cryptoStream, _SerializeThis);
                    Debug.Log("Save XML :  " + cPath);
                }
            }
        }

        /// <summary>
        /// Try not to use the static function get a reference and use the public function instead!
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_Filename"></param>
        /// <returns></returns>
        public static T DeserializeXML<T>(string _Filename)
        {
            string cPath = Application.dataPath + Path + _Filename + ".xml";

            XmlSerializer deSerializer = new XmlSerializer(typeof(T));

            TextReader reader = new StreamReader(cPath);
            object obj = deSerializer.Deserialize(reader);
            //Debug.Log("DeSerialize XML:     " + cPath);
            reader.Close();
            Debug.Log("Load XML :  " + cPath);
            return (T)obj;
        }
        public static T Deserialize<T>(string _Filename)
        {
            string cPath = Application.dataPath + Path + _Filename + ".xml";

            XmlSerializer deSerializer = new XmlSerializer(typeof(T));

            TextReader reader = new StreamReader(cPath);
            object obj = deSerializer.Deserialize(reader);
            //Debug.Log("DeSerialize XML:     " + cPath);
            reader.Close();
            Debug.Log("STATIC Load XML :  " + cPath);
            return (T)obj;
        }

        /// <summary>
        /// Try not to use the static function get a reference and use the public function instead!
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_Filename"></param>
        /// <param name="_AES"></param>
        /// <returns></returns>
        public static T DeserializeCrypted<T>(string _Filename, AesManaged _AES)
        {
            string cPath = Application.dataPath + Path + _Filename + "_s.xml";

            ICryptoTransform desencrypt = _AES.CreateDecryptor();

            using (FileStream fileStream = File.Open(cPath, FileMode.Open))
            {
                using (CryptoStream cryptoStream = new CryptoStream(fileStream, desencrypt, CryptoStreamMode.Read))
                {
                    XmlSerializer xmlser = new XmlSerializer(typeof(T));
                    Debug.Log("STATIC Load XML :  " + cPath);
                    return (T)xmlser.Deserialize(cryptoStream);
                }
            }
        }
        public T DeserializeCrypted<T>(string _Filename)
        {
            string cPath = Application.dataPath + Path + _Filename + "_s.xml";

            ICryptoTransform desencrypt = AES.CreateDecryptor();

            using (FileStream fileStream = File.Open(cPath, FileMode.Open))
            {
                using (CryptoStream cryptoStream = new CryptoStream(fileStream, desencrypt, CryptoStreamMode.Read))
                {
                    XmlSerializer xmlser = new XmlSerializer(typeof(T));
                    Debug.Log("Load XML :  " + cPath);
                    return (T)xmlser.Deserialize(cryptoStream);
                }
            }
        }

        #endregion
    }
}