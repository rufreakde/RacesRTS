/*******************
* Rudolf Chrispens *
*******************/

#region using
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
#endregion

namespace Dev6 {

    [AddComponentMenu("Dev6/UI/Alert/Alert Tooltip")]
    [RequireComponent(typeof(CAlert))]
    public class CAlertTooltips : MonoBehaviour
    {
        //[Split("CAlertTooltips")]
        public bool deletethis = true;

        private CAlert AlertScript = null;
        public ToolTipData[] ToolTips = new ToolTipData[0];

        void Awake()
        {
            AlertScript = this.GetSafeComponent<CAlert>();
        }

        void OnEnable()
        {
            //subscribe to the CreateAlert event!
            AlertScript.createEvent += CreateTheTooltips;
        }

        void OnDisable()
        {
            //unsibscribe to the CreateAlert event!
            AlertScript.createEvent -= CreateTheTooltips;
        }

        void CreateTheTooltips()
        {
            //what data to store here? 
            for (int i = 0; i < ToolTips.Length; i++)
            {
                CreateTip(ToolTips[i]);
            }

            //remove script after init of tooltips
            if(deletethis)
                Destroy(this);
        }

        void CreateTip(ToolTipData _Data)
        {
            //find the object on the alert and add the tooltips on top of it
            CToolTip tTT = CAlert.ActiveAlertObject.transform.FindChildRecursive(_Data.CreateOn).gameObject.AddComponent<CToolTip>();

            //reset the data of the Toltip to the data that is stored in .this creation script
            tTT.DataBase = _Data.DataBase; // give string databease
            tTT.ToolTip = _Data.ToolTip; // give settings

            //initialize and finalize the ToolTip
            tTT.InitTheToolTip();
        }

        [System.Serializable]
        public class ToolTipData
        {
            [Tooltip("Das Object welches einen Tooltip bekommen soll.")]
            public string CreateOn = "Object_Name"; //if null dont create!
            public StringDataBase DataBase = null;
            public CToolTip.ToolTipSettings ToolTip = new CToolTip.ToolTipSettings();
        }
    }
}
//namespace end