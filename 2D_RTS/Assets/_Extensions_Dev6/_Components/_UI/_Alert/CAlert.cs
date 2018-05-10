/*********************
*	Rudolf Chrispens
***********************/

#region USE
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
#endregion

/*
You dont need to assign this "ShowAlert" to the button in inspector this will be done automaticaly!
*/

namespace Dev6
{
    //[SelectionBase]
    [RequireComponent(typeof(Button))]
    [AddComponentMenu("Dev6/UI/Alert/Alert Box")]
    public class CAlert : MonoBehaviour
    {
        //event for custom button
        public delegate void CreateDelegate();
        public CreateDelegate createEvent;

        //settings
        public StringDataBase DataBase = null;
        [Tooltip("This is the UI prefab without the buttons! But it can have everything it just needs:\n 'Text' als componente. Der Button Layer wird immer darüber gelegt!\n Und 'ButtonContainer' mit einer LayoutGroup.")]
        public GameObject Prefab = null;
        public AlertSettings StringSettings = new AlertSettings(); //we use a DB for alerts as well because they have to be multilingual too!

        //simple alert holders
        public static GameObject ActiveAlertObject = null;
        public static Transform ButtonContainer = null;

        //Alert data
        private Text TextField = null;
        private GameObject tButtonObject = null;
        [HideInInspector]
        public List<Button> ButtonList = new List<Button>();
        [HideInInspector]
        public List<Text> ButtonTextList = new List<Text>();

        public void Awake()
        {
            AddAlertToMainButton();
        }

        public void AddAlertToMainButton()
        {
            Button tAlertCallButton = this.GetSafeComponent<Button>();
            tAlertCallButton.onClick.AddListener(ShowAlert);
        }

        public void ShowAlert()
        {
            //1. Clear the lists
            ButtonList.Clear();
            ButtonTextList.Clear();
            //2. Spawn the box with two or one button
            CreateAlert();
            //3. Modify the Methods of the buttons! ( so that they can do what we want them to do)
            ModifyButtons();
            //4. change the main text to the chosen one
            ModifyMainText();

            //Fire created event
            if (createEvent != null)
            {
                createEvent(); //tell the custom button script to init
            }
        }

        void CreateAlert()
        {
            if(ActiveAlertObject)
            {
                Destroy(ActiveAlertObject);
            }

            Canvas tCanvas = GameObject.FindGameObjectWithTag(TAGS.MainCanvas).GetSafeComponent<Canvas>();
            ActiveAlertObject = Instantiate(Prefab);
            ActiveAlertObject.transform.SetParent(tCanvas.transform, false);

            ButtonContainer = ActiveAlertObject.transform.FindChildRecursive("ButtonContainer");
            TextField = ActiveAlertObject.transform.FindChildRecursive("Text").GetSafeComponent<Text>();

            if(!ButtonContainer)
            {
                Debug.LogError("Error : Could not find 'ButtonContainer' Transform as child on " + ActiveAlertObject + " please make sure to name the prefab Transforms corectly!");
            }

            if (!TextField)
            {
                Debug.LogError("Error : Could not find 'Text' Transform as child on " + ActiveAlertObject + " please make sure to name the prefab Transforms corectly!");
            }
        }

        void ModifyMainText()
        {
            //get the main text
            Text tMainText = ActiveAlertObject.transform.FindChildRecursive("Text").GetSafeComponent<Text>();

            //Get the coresponding text out of database:
            if (DataBase && DataBase.Data.ContainsKey(StringSettings.MainID.ToString() + "_" + StringSettings.MainDBKey))
            {
                string tString = "";
                DataBase.Data.TryGetValue(StringSettings.MainID.ToString() + "_" + StringSettings.MainDBKey, out tString);
                tMainText.text = tString;
                StringSettings.Text = tString;
            }
            else
            {
                Debug.LogError("Error: AlertMainText " + this.ToString() + "  Could not find a matching key:  " + StringSettings.MainID.ToString() + "_" + StringSettings.MainDBKey + "  in Database:  " + DataBase);
                tMainText.text = "Err" + StringSettings.Text;
            }
        }

        void ModifyButtons()
        {
            // -> giving Action()? or SendMessage()? or Invoke()?
            //create the buttons
            for(int i=0; i< StringSettings.Buttons.Length; i++)
            {
                CreateButton(StringSettings.Buttons[i]);
            }
        }

        void CreateButton(AlertButton _ButtonProperties)
        {
            tButtonObject = new GameObject(_ButtonProperties.Text);

            tButtonObject.transform.SetParent(ButtonContainer, false);
            //tGO.transform.localScale = Vector3.one;
            Image tImage = tButtonObject.AddComponent<Image>();
            tImage.sprite = _ButtonProperties.Background;

            Button tButton = tButtonObject.AddComponent<Button>();

            GameObject tTextGO = new GameObject("Text");
            
            //change button text:
            tTextGO.transform.SetParent(tButtonObject.transform);
            Text tText = tTextGO.AddComponent<Text>();

            //ad layoutelement
            LayoutElement tLayoutElement = tButtonObject.AddComponent<LayoutElement>();

            //change the values
            ModifyButton(tButton, tText, tLayoutElement, _ButtonProperties);

            //add to ready list
            ButtonList.Add(tButton);
            ButtonTextList.Add(tText);
        }

        void ModifyButton(Button _btn, Text _txt, LayoutElement _LE, AlertButton _ButtonProperties)
        {
            //change font and everything
            _txt.font = _ButtonProperties.Font;
            _txt.fontSize = _ButtonProperties.FontSize;
            _txt.color = _ButtonProperties.TextColor;
            _txt.alignment = TextAnchor.MiddleCenter;
            string tButtonDesc = _ButtonProperties.Text;

            //modify the rect of txt to be as big as the button rect
            HorizontalLayoutGroup _HL = _btn.gameObject.AddComponent<HorizontalLayoutGroup>();
            _HL.padding = StringSettings.ButtonPadding;

            //change Buttonproperties string with database string
            //Get the coresponding text out of database:
            if (DataBase && DataBase.Data.ContainsKey(_ButtonProperties.DbID.ToString() + "_" + _ButtonProperties.DbKey))
            {
                DataBase.Data.TryGetValue(_ButtonProperties.DbID.ToString() + "_" + _ButtonProperties.DbKey, out tButtonDesc);
                _ButtonProperties.Text = tButtonDesc;
            }
            else
            {
                Debug.LogError("Error: AlertButton " + this.ToString() + "  Could not find a matching key:  " + _ButtonProperties.DbID.ToString() + "_" + _ButtonProperties.DbKey + "  in Database:  " + DataBase);
            }

            //change UI string with Properties string
            _txt.text = tButtonDesc;

            //add button Actions
            //always add the Close delegate because we want the message box to close when the player chose.
            _btn.onClick.AddListener(CloseAlert);

            //we are ready instaniating. Now there is a second string that requires CAlert 
            //-> CustomAlertButtons
            //this script can cicle through the button scripts of Alert and add custom logic to it.!
        }

        public void CloseAlert()
        {
            Destroy(ActiveAlertObject);
        }

        [System.Serializable]
        public class AlertSettings
        {
            //[Split("CAlert")]
            public int MainID = -1;         // this will be set out of the dictionary so you shoul always let it be 
            public string MainDBKey = "String_MainText";       //this will find the coresponding dictionary entry and fill the "Text"

            [Tooltip("This text will be overritten! It is only visible here so you can look at what is stored here.")]
            public string Text = "Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet.";

            public AlertButton[] Buttons;
            public RectOffset ButtonPadding = new RectOffset();
        }

        [System.Serializable]
        public class AlertButton
        {
            public int DbID = -1;
            public string DbKey = "String_Key";
            public string Text = "ButtonName";
            public int FontSize = 14;
            public Font Font = null;
            public Color TextColor = Color.black;
            public Sprite Background = null;
        }
    }
}
