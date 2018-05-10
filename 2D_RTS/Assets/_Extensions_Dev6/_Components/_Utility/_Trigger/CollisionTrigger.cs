using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Dev6
{

    [DisallowMultipleComponent]
    [AddComponentMenu("Dev6/UTILITY/Collision Trigger")]
    public class CollisionTrigger : MonoBehaviour
    {
        public List<trTarget> Triggers = new List<trTarget>();

        private bool hasTriggerEnter = false;
        private bool hasTriggerExit = false;
        private bool hasTriggerStay = false;
        #region Collision
        void OnTriggerEnter()
        {
            if (hasTriggerEnter)
                TriggerTheEvent(trEvent.Enter);

        }

        void OnTriggerExit()
        {
            if (hasTriggerExit)
                TriggerTheEvent(trEvent.Exit);

        }

        void OnTriggerStay()
        {
            if (hasTriggerStay)
                TriggerTheEvent(trEvent.Stay);

        }

        void OnTriggerEnter2D()
        {
            if (hasTriggerEnter)
                TriggerTheEvent(trEvent.Enter);

        }

        void OnTriggerExit2D()
        {
            if (hasTriggerExit)
                TriggerTheEvent(trEvent.Exit);

        }

        void OnTriggerStay2D()
        {
            if (hasTriggerStay)
                TriggerTheEvent(trEvent.Stay);

        }
        #endregion

        void TriggerTheEvent(trEvent _Event)
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
            Debug.LogError("Event '" + _Event + "' was not found in Trigger list!");
        }

        // Use this for initialization
        void OnEnable()
        {
            //retreave the coresponding interface
            for (int i = 0; i < Triggers.Count; i++)
            {
                Triggers[i].TriggerInterface = (IcanGetTriggered)Triggers[i].Target;

                CheckTriggerForEventType(Triggers[i].Event);//check if one of the trigggers is stay enter oder exit
                if (Triggers[i].TriggerInterface == null)
                {
                    Debug.LogError("No Trigger-Interface found on: " + Triggers[i].Target.name + " !");
                }
            }
        }

        void CheckTriggerForEventType(trEvent _EventType)
        {
            if (_EventType == trEvent.Enter)
                hasTriggerEnter = true;

            if (_EventType == trEvent.Exit)
                hasTriggerExit = true;

            if (_EventType == trEvent.Stay)
                hasTriggerStay = true;
        }

        [System.Serializable]
        public class trTarget
        {
            [Tooltip("Zielobject welches ein script besitzt mit dem IcanGetTriggered Interface. (Grünes Steckdosen Icon).")]
            public MonoBehaviour Target = null;
            protected IcanGetTriggered iTarget = null;
            [Tooltip("Zu welchem event das Ziel getriggered wird.")]
            public trEvent Event = trEvent.Enter;
            [Tooltip("Mögliche Trigger Operatoren! Siehe diesbezüglich in die Target scripte. Dort sollte es immer eine Info Box dazu geben. Falls nicht kann das Feld leer gelassen werden.")]
            public string Trigger = "";

            public IcanGetTriggered TriggerInterface
            {
                get { return iTarget; }
                set { iTarget = value; }
            }
        }

        public enum trEvent
        {
            Enter,
            Exit,
            Stay
        }
    }
}