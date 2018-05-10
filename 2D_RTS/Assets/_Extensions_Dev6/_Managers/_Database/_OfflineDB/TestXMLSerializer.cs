/*******************
* Rudolf Chrispens *
*******************/

#region using
using UnityEngine;
using System.Collections;
using Dev6;
#endregion

public class TestXMLSerializer : MonoBehaviour {

    [SimpleButton("SaveXML", typeof(TestXMLSerializer))]
    [SimpleButton("RetrieveXML", typeof(TestXMLSerializer))]
    [TextArea]
    public string TEST = "This is the best test string ever!";

    [SerializeField]
    Cryptography CryptLib = null;

    public TestData Data = null;

    [System.Serializable]
    public class TestData
    {
        public string test = "Start1";
        public int aha = 42;
        public bool achso = true;
        public string[] ArrayString = new string[3];

        TestData()
        {
            
        }
    }

    public System.Security.Cryptography.AesManaged AES = null;

    void Start()
    {
        AES = new System.Security.Cryptography.AesManaged();
    }

    public void EncodeS()
    {
        Debug.Log("Start:    " + TEST);
        TEST = CryptLib.EncryptString(TEST);
        Debug.Log("Encrypted:    " + TEST);
    }

    public void DecodeS()
    {
        TEST = CryptLib.DecryptString(TEST);
        Debug.Log("Decrypted:    " + TEST);
    }

    public void SaveXML()
    {
        //CSerializeDataXML.Serialize<TestData>("TEST", Data);
        MANAGER.GET.GetSingleton<CSerializeDataXML>().SerializeCrypted<TestData>("TEST2", Data);
    }

    public void RetrieveXML()
    {
        //Data = CSerializeDataXML.Deserialize<TestData>("TEST");
        Data = MANAGER.GET.GetSingleton<CSerializeDataXML>().DeserializeCrypted<TestData>("TEST2");
    }

}