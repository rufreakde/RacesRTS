using UnityEngine;
using System.Collections;

using System.Text.RegularExpressions;
using System;

namespace Dev6
{
    public class DBRetrieve : MonoBehaviour, IamSingleton
    {
        //this script just gets the data the php script sends as a string
        [Split("Connect on DB and store into Asset")]
        [SimpleButton("ConnectDB", typeof(DBRetrieve))]
        public bool DEBUG_printWWW = false;
        [Space(10f)]
        protected string url = URL.ToolTipDB;
        private const string url_TWO = "?language=";

        protected string FINISHED_URL = "";

        
        public eLanguage Language = eLanguage.German;

        public StringDataBase DataBase;

        public ArrayList strings = new ArrayList();
        public ArrayList SubStrings = new ArrayList();

        public void ConnectDB()
        {
            if(DEBUG_printWWW)
                Debug.Log("Connecting to Database...  " + url);

            //URL TO GET DATA FROM:
            FINISHED_URL = url + url_TWO + Language.ToString();

            //INIT THE WWW class and retrieve the data
            WWW www = new WWW(FINISHED_URL);

            if (DEBUG_printWWW)
                Debug.Log("WWW: " + www);

            ///start coroutine to handle our data
            StartCoroutine(RegisterFunc(www));
        }

        protected IEnumerator RegisterFunc(WWW www)
        {
            while (!www.isDone)
            {
                if(DEBUG_printWWW)
                    Debug.Log("Not Ready...");
                yield return www;
            }

            if(DEBUG_printWWW)
                Debug.Log("... www LOADED!");

            if (www.error != null) // check if there was an error if so dont make changes!
            {
                Debug.LogWarning("DB Connect ERROR: " + www.error);
                DataBaseReady(); // still the data is ready because we ship with a ready data we only failed to update!
            }
            else
            {
                if (DEBUG_printWWW)
                    Debug.Log("Printing www content: \n" + www.text + "\n\n");

                //can be overridden by custom logic and handling
                ManageParseData(www);

                //TELL THE OTHER CLASES THAT THE DATA IS READY:
                DataBaseReady();
            }
        }

        #region Override
        protected virtual void DataBaseReady()
        {
            //Fill in the required database:
            //Example
            //CTTRetrieve.DATABASE_TT_READY = true;

        }

        protected virtual void ManageParseData(WWW www)
        {
            strings.AddRange(Regex.Split(www.text, "#END#"));
            strings.RemoveAt(strings.Count - 1);

            for (int i = 0; i < strings.Count; i++)
            {
                Debug.Log("STRING: " + i + "   content : " + strings[i]);
            }
            //Debug.Log("Downloading Tooltip Language parts from: " + FINISHED_URL);

            //reset database:
            DataBase.Data = new Dev6.CustomDict<string, string>();

            for (int i = 0; i < strings.Count; i++)
            {
                SubStrings.AddRange(Regex.Split(strings[i].ToString(), "#KEY#"));
                DataBase.Data.Add(SubStrings[SubStrings.Count - 2].ToString(), SubStrings[SubStrings.Count - 1].ToString());
            }
        }

        public void iInitialize()
        {
            ConnectDB();
        }
        #endregion
    }
}
