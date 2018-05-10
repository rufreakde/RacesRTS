using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


//this is a player singleton for local player as controls

namespace Dev6
{

    [DisallowMultipleComponent]
    [AddComponentMenu("Dev6/UTILITY/Controls Trigger Manager")]
    public class ControlsTriggerManager : MonoBehaviour, IamSingleton
    {
        //TO DO implenent InControl so we dont have to rewrite this script for different games just plug in a Keybindings asset and go :)
        public List<trTarget> Triggers = new List<trTarget>();

        public void iInitialize()
        {
            //TO DO initialize the controls!
        }

        void TriggerTheEvent(trControls _Event)
        {
            //check all and fire the right trigger
            for (int i = 0; i < Triggers.Count; i++)
            {
                if (Triggers[i].Event == _Event)
                {
                    Triggers[i].TriggerInterface.iTrigger(Triggers[i].Trigger);
                    return;
                }
            }
            //Debug.LogError("Event '" + _Event + "' was not found in Trigger list!");
        }

        // Use this for interface retreaving
        void Start()
        {
            //retreave the coresponding interface
            for (int i = 0; i < Triggers.Count; i++)
            {
                Triggers[i].TriggerInterface = (IcanGetTriggered)Triggers[i].Target;

                //CheckTriggerForEventType(Triggers[i].Event);//check if one of the trigggers is stay enter oder exit
                if (Triggers[i].TriggerInterface == null)
                {
                    Debug.LogError("No Trigger-Interface found on: " + Triggers[i].Target.name + " !");
                }
            }
        }

        void Update()
        {
            //TO DO input change!! not unity input!!
            //Handle keyboard / mouse / gamepad / and other imput here:
            if(Input.GetKey(KeyCode.W))
            {
                TriggerTheEvent(trControls.Up);
            }
            else if(Input.GetKey(KeyCode.S))
            {
                TriggerTheEvent(trControls.Down);
            }

            if (Input.GetKey(KeyCode.A))
            {
                TriggerTheEvent(trControls.Left);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                TriggerTheEvent(trControls.Right);
            }

            if (Input.GetKey(KeyCode.Q))
            {
                TriggerTheEvent(trControls.TurnLeft);
            }
            else if (Input.GetKey(KeyCode.E))
            {
                TriggerTheEvent(trControls.TurnRight);
            }
        }

        [System.Serializable]
        public class trTarget
        {
            [Tooltip("Zielobject welches ein script besitzt mit dem IcanGetTriggered Interface. (Grünes Steckdosen Icon).")]
            public MonoBehaviour Target = null;
            protected IcanGetTriggered iTarget = null;
            [Tooltip("Zu welchem event das Ziel getriggered wird.")]
            public trControls Event = trControls.Down;
            [Tooltip("Mögliche Trigger Operatoren! Siehe diesbezüglich in die Target scripte. Dort sollte es immer eine Info Box dazu geben. Falls nicht kann das Feld leer gelassen werden.")]
            public string Trigger = "";

            public IcanGetTriggered TriggerInterface
            {
                get { return iTarget; }
                set { iTarget = value; }
            }
        }

        public enum trControls
        {
            Left,
            Right,
            Up,
            Down,
            TurnLeft,
            TurnRight
        }
    }
}