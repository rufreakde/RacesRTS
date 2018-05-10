/*********************
*	Rudolf Chrispens
***********************/

#region USE
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
#endregion

namespace Dev6
{

    //[SelectionBase]
    [RequireComponent(typeof(Button))]
    [AddComponentMenu("Dev6/UI/Button Connect to DB")]
    public class CButtonString : MonoBehaviour
    {
        //[Split("CButtonString")]
        public StringDataBase DataBase = null;
        public ButtonStringSettings StringSettings = new ButtonStringSettings();

        //private GameObject ButtonGO;
        private Text ButtonText;
        //private Button Button;

        void Awake()
        {
            //this.ButtonGO = this.gameObject;
            this.ButtonText = this.transform.GetComponentInChildren<Text>();
            //this.Button = this.GetSafeComponent<Button>();

            ButtonText.font = StringSettings.TextFont;
            ButtonText.color = StringSettings.TextColor;

            //Get the coresponding text out of database:
            if (DataBase && DataBase.Data.ContainsKey(StringSettings.UniqueID.ToString() + "_" + StringSettings.DictKey))
            {
                string tString = "";
                DataBase.Data.TryGetValue(StringSettings.UniqueID.ToString() + "_" + StringSettings.DictKey, out tString);
                ButtonText.text = tString;
            }
            else
            {
                Debug.LogError(this.ToString() + "  Could not find a matching key:  " + StringSettings.UniqueID.ToString() + "_" + StringSettings.DictKey + "  in Database:  " + DataBase);
            }
        }


        [System.Serializable]
        public class ButtonStringSettings
        {
            [Header("#Unique DataBaseIdentifier:")]
            public int UniqueID = -1;         // this will be set out of the dictionary so you shoul always let it be 
            public string DictKey = "String_Button_1";       //this will find the coresponding dictionary entry and fill the "Text"


            [Header("#Set:")]
            public Font TextFont = null;
            [Tooltip("Die Farbe des Textes.")]
            public Color TextColor = Color.black;

            [Header("#Get text out of Dict")]
            [Tooltip("This text will be overritten! It is only visible here so you can look at what is stored here.")]
            [TextArea]
            public string Text = "Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet.";
        }
    }

}