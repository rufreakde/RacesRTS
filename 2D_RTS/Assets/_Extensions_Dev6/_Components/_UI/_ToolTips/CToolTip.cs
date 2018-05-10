using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Dev6
{
    [AddComponentMenu("Dev6/UI/ToolTipp")]
    public class CToolTip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler //,ISelectHandler
    {
        public const float Delay = 0.8f;
        protected float Timer = 0f;

        [Tooltip("Indicator showing if the Coroutine is running ATM.")]
        [Split("CAlert")]
        [SerializeField]
        bool ShowingToolTip = false;
        public StringDataBase DataBase = null;
        public ToolTipSettings ToolTip = new ToolTipSettings(); // loaded from database

        //these are needed for mouse positioning
        private Canvas MainCanvas;
        public static Transform ToolTipContainer;
        
        //made them all public static so we dont have to create new ones we reuse the tooltips and change the txt bevore showing!
        public static GameObject ToolTipHolder;
        public static Image Background;
        public static Text TTText;
        public static RectTransform ToolTipRect;
        public static LayoutElement LeayoutElementOfTT;
        public static HorizontalLayoutGroup LayoutGroup;

        //alloc memory
        private float tAnchorPositionY = 1f;

        void Awake()
        {
            if (DataBase != null && ToolTip != null)
            {
                InitTheToolTip();
            }
        }

        public void InitTheToolTip()
        {
            //warnings
            if (!ToolTip.bgSprite)
            {
                Debug.LogWarning("There is no sprite attached to: " + this.ToString());
            }

            if (!ToolTip.ttFont)
            {
                Debug.LogWarning("There is no font attached to: " + this.ToString());
            }

            if (!DataBase)
            {
                Debug.LogWarning("There is no Database for ToolTips attached to: " + this.ToString());
            }

            //get Canvas
            MainCanvas = GameObject.FindGameObjectWithTag(TAGS.MainCanvas).GetSafeComponent<Canvas>();
            //MainScaler = MainCanvas.transform.GetSafeComponent<CanvasScaler>();

            if(!ToolTipContainer) //init the TT container because if you parent to the object itself it will clip to other obj
            {
                GameObject tEmpty = new GameObject("ToolTips Container");
                ToolTipContainer = tEmpty.transform;
                ToolTipContainer.parent = MainCanvas.transform;
                ToolTipContainer.position = Vector3.zero;
                ToolTipContainer.localScale = Vector3.one;
            }

            if (!TTText) // if the TT object is null create it
            {
                //create child
                GameObject tImgHolder = new GameObject();
                tImgHolder.name = "Image";
                ToolTipHolder = tImgHolder;
                //ToolTipHolder.transform.parent = this.transform.FindChild("Text");
                ToolTipHolder.transform.parent = ToolTipContainer;  //fixing cliping behind an object

                //add Image component
                Background = tImgHolder.AddComponent<Image>();
                Background.sprite = ToolTip.bgSprite;
                Background.rectTransform.localScale = Vector3.one;
                Background.type = Image.Type.Sliced;
                Background.raycastTarget = false;

                //store the rect for mouse positioning
                ToolTipRect = Background.rectTransform;

                //add fitter for text size of background box
                ContentSizeFitter tCSF = tImgHolder.AddComponent<ContentSizeFitter>();
                tCSF.horizontalFit = ContentSizeFitter.FitMode.MinSize;
                tCSF.verticalFit = ContentSizeFitter.FitMode.PreferredSize;

                //add a layout element
                LayoutGroup = tImgHolder.AddComponent<HorizontalLayoutGroup>();  //needed for automatic size fitt
                LayoutGroup.padding = ToolTip.Padding; //new RectOffset(ToolTip.Padding.Left, ToolTip.Padding.Right, ToolTip.Padding.Top, ToolTip.Padding.Bottom);

                //create child after that child
                GameObject tTextHolder = new GameObject();
                tTextHolder.name = "Text";
                tTextHolder.transform.parent = tImgHolder.transform;

                //Add text component fill with thext that is stored
                TTText = tTextHolder.AddComponent<Text>();
                TTText.font = ToolTip.ttFont;
                TTText.rectTransform.localScale = Vector3.one;
                TTText.fontSize = ToolTip.FontSize;
                TTText.color = ToolTip.TextColor;
                TTText.raycastTarget = false;

                LeayoutElementOfTT = tTextHolder.AddComponent<LayoutElement>();
                LeayoutElementOfTT.minWidth = ToolTip.Width;
            }

            //Get the coresponding text out of database:
            if (DataBase && DataBase.Data.ContainsKey(ToolTip.UniqueID.ToString() + "_" + ToolTip.DictKey))
            {
                string tString = "";
                DataBase.Data.TryGetValue(ToolTip.UniqueID.ToString() + "_" + ToolTip.DictKey, out tString);
                TTText.text = tString;
                ToolTip.Text = tString;
            }
            else
            {
                Debug.LogError("Error: ToolTip " + this.ToString() + "  Could not find a matching key:  " + ToolTip.UniqueID.ToString() + "_" + ToolTip.DictKey + "  in Database:  " + DataBase);
                TTText.text = "Err" + ToolTip.Text;
            }

            //after init disable it so no one can see it until it is needed
            DisableToolTip();
        }

        //public void OnSelect(BaseEventData eventData)
        //{
        //    StopCoroutine("ShowToolTip");
        //    DisableToolTipp();
        //}

        public void OnPointerEnter(PointerEventData eventData)
        {
            //Debug.Log("Enter");
            StopCoroutine("ShowToolTip");
            DisableToolTip();
            StartCoroutine("ShowToolTip");
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            //Debug.Log("Exit");
            StopCoroutine("ShowToolTip");
            DisableToolTip();
        }

        IEnumerator ShowToolTip()
        {
            Timer = 0f;

            while (Timer < Delay)
            {
                Timer += Time.deltaTime;
                yield return null;
            }

            EnableToolTip();

            while (ShowingToolTip)
            {
                UpdateToolTipPosition();
                yield return new WaitForEndOfFrame();
            }
        }

        void UpdateToolTipPosition()
        {
            if (ToolTipRect)
            {
                ToolTipRect.position = MainCanvas.CalculatePositionFromMouseToRectTransform(Camera.main);
                //change tooltip anchoring relative to screen

                if (ToolTipRect.position.y > Screen.height * 0.5f)
                {
                    tAnchorPositionY = 1f;
                }
                else
                {
                    tAnchorPositionY = 0f;
                }

                if (ToolTipRect.position.x > Screen.width * 0.5f)
                {
                    ToolTipRect.anchorMin = new Vector2(1f, tAnchorPositionY);
                    ToolTipRect.anchorMax = new Vector2(1f, tAnchorPositionY);
                    ToolTipRect.pivot = new Vector2(1f, tAnchorPositionY);
                }
                else
                {
                    ToolTipRect.anchorMin = new Vector2(0f, tAnchorPositionY);
                    ToolTipRect.anchorMax = new Vector2(0f, tAnchorPositionY);
                    ToolTipRect.pivot = new Vector2(0f, tAnchorPositionY);
                }
            }
        }

        void EnableToolTip()
        {
            ShowingToolTip = true;
            UpdateToolTipPosition(); //refresh position so there wont appear some artifacts

            //text
            TTText.text = ToolTip.Text;
            //fontsize
            TTText.fontSize = ToolTip.FontSize;
            //font
            TTText.font = ToolTip.ttFont;
            //width
            LeayoutElementOfTT.minWidth = ToolTip.Width;
            //padding
            LayoutGroup.padding = ToolTip.Padding; //new RectOffset(ToolTip.Padding.Left, ToolTip.Padding.Right, ToolTip.Padding.Top, ToolTip.Padding.Bottom);
            //textcolor
            TTText.color = ToolTip.TextColor;
            //bgSprite
            Background.sprite = ToolTip.bgSprite;

            ToolTipHolder.SetActive(true);
        }

        void DisableToolTip()
        {
            ShowingToolTip = false;
            ToolTipHolder.SetActive(false);
        }

        [System.Serializable]
        public class ToolTipSettings
        {
            //[Header("#Unique DataBaseIdentifier:")]
            public int UniqueID = -1;         // this will be set out of the dictionary so you shoul always let it be 
            public string DictKey = "ToolTipp_Help_Button_1";       //this will find the coresponding dictionary entry and fill the "Text"


            //[Header("#Set:")]
            public Sprite bgSprite = null;
            public Font ttFont = null;
            public int FontSize = 14;
            [Tooltip("The width of the text box.")]
            public float Width = 400f;
            public RectOffset Padding = new RectOffset();
            [Tooltip("Die Farbe des Textes.")]
            public Color TextColor = Color.black;

            //[Header("#Get text out of Dict")]
            //[Tooltip("This text will be overritten! It is only visible here so you can look at what is stored here.")]
            //[TextArea]
            //[HideInInspector]
            public string Text = "Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet.";
        }
    }

}