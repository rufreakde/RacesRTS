/*******************
* Rudolf Chrispens *
*******************/

/*
DOES ADD CUSTOM LOGIC TO THE BUTTON YOU CHOOSE
*/

#region using
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
#endregion

namespace Dev6
{
    [AddComponentMenu("Dev6/UI/Alert/Alert Custombutton")]
    [RequireComponent(typeof(CAlert))]
    public class CCustomAlertButtons : MonoBehaviour
    {
        [Split("CCustomAlertButtons")]
        public Logic[] ButtonLogic = new CCustomAlertButtons.Logic[0];
        private CAlert AlertScript = null;

        void Awake()
        {
            AlertScript = this.GetSafeComponent<CAlert>();
        }

        void OnEnable()
        {
            //subscribe to the CreateAlert event!
            AlertScript.createEvent += InitCustomButtons;
        }

        void OnDisable()
        {
            //unsibscribe to the CreateAlert event!
            AlertScript.createEvent -= InitCustomButtons;
        }

        public void InitCustomButtons()
        {
            //go through the Buttonlogic and assign the logic to the specified buttons
            for (int i = 0; i < ButtonLogic.Length; i++)
            {
                if (ButtonLogic[i].MethodHolder == null) //if no other go is defined we will use the go the script is on ( also itself)
                {
                    ButtonLogic[i].MethodHolder = this.gameObject;
                }
                //find the button and add the Listener function to the button
                FindButton(ButtonLogic[i].ButtonName, ButtonLogic[i].MethodName, ButtonLogic[i].MethodHolder);
            }
        }

        Button FindButton(string _ButtonName, string _FunctionName, GameObject _GO)
        {
            for(int i=0; i<AlertScript.ButtonList.Count; i++)
            {
                //Debug.Log("Names: " + AlertScript.ButtonList[i].name);
                if(AlertScript.ButtonList[i].name == _ButtonName) // found one
                {
                    //add the function
                    AlertScript.ButtonList[i].onClick.AddListener( () => _GO.SendMessage(_FunctionName) );
                    //Debug.Log("Adding function:" + _FunctionName + " to " + AlertScript.ButtonList[i] + " from: " + _GO.name);
                    return AlertScript.ButtonList[i];
                }
            }

            Debug.LogError("There is no such Button (" + _ButtonName + ") in : " + AlertScript.ButtonList );
            return null; //return null if you dont find a button
        }

        public void Function()
        {
            Debug.Log("#######################");
            Debug.Log("Test finaly its working!");
        }

        [System.Serializable]
        public class Logic
        {
            [Tooltip("Das GameObject auf dem die Funktion vorhanden ist die der Button ausführen soll.")]
            public GameObject MethodHolder = null;
            [Tooltip("String der benutzt wird um den Button zu finden in CAlert.")]
            public string ButtonName = "Button";
            [Tooltip("SendMessage interna.")]
            public string MethodName = "Function";
        }
    }
}