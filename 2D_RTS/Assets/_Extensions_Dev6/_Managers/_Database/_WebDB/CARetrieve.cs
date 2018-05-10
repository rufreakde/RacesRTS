using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;

/*retreave the alert box text messages because we have to multi language the messaged too!*/

namespace Dev6
{
    [AddComponentMenu("Dev6/UI/Datenbank/Connect Alertbox")]
    public class CARetrieve : DBRetrieve
    {

        //always start unready but get ready after www clas handling ( even if its failed to update its ready it just may hold old strings.
        public static bool DATABASE_TT_READY = false;

        //this script just gets the data the php script sends as a string
        #region Overridden
        protected override void DataBaseReady()
        {
            CARetrieve.DATABASE_TT_READY = true;
        }

        protected override void ManageParseData(WWW www)
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
        #endregion
    }
}