using UnityEngine;
using System.Collections;

public struct SplitscreenPreset
{
    //All 4 Cams
    //Width and Height of Rect
    public Vector2 pos1, pos2, pos3, pos4;
    //Startposition of Rect
    public Vector2 c1, c2, c3, c4;

    //Pos = X and Y // c = W and H
    public SplitscreenPreset(   Vector2 _Pos1, Vector2 _c1, 
                                Vector2 _Pos2, Vector2 _c2, 
                                Vector2 _Pos3, Vector2 _c3, 
                                Vector2 _Pos4, Vector2 _c4)
    {
        c1 = _c1;
        c2 = _c2;
        c3 = _c3;
        c4 = _c4;

        pos1 = _Pos1;
        pos2 = _Pos2;
        pos3 = _Pos3;
        pos4 = _Pos4;
    }
}

public enum SplitType
{ 
    p1_fullscreen = 0,
    p2_horizontal = 1,
    p2_vertical = 2,
    p3_top = 3,
    p3_bot = 4,
    p3_left = 5,
    p3_right = 6,
    p4_screen = 7
};

[AddComponentMenu("Dev6/CAMERA/Splitscreen")]
public class Splitscreen : MonoBehaviour {

    private SplitscreenPreset[] SplitValues = new SplitscreenPreset[8];
    public SplitType Type = SplitType.p1_fullscreen;

    private SplitType type = SplitType.p1_fullscreen;
    private Camera[] Cameras = new Camera[4];
    private GameObject[] CanvasPL = new GameObject[4];

    //important to be in start because the camera manager needs the camera scripts before they are deactivated
    void Start()
    {
        //store all presets into array
        SetCameraValuesAfterEnum();
        //init
        SetSplitTypeValues();
        type = Type;
    }
	
	// Update is called once per frame
	void Update () 
    {
        //set in update for testing!!!! ONLY !!!!
        if (type != Type)
        {
            SetSplitTypeValues();
            type = Type;
        }
	}

    public void SetSplitTypeValues()
    {
        OverrideCameraValues(SplitValues[(int)Type]);
    }

    //set for each setup the values so they are stored in the array for the chosen enum
    private void SetCameraValuesAfterEnum()
    {
        Cameras[0] = transform.Find("p1_cam").GetSafeComponent<Camera>();
        Cameras[1] = transform.Find("p2_cam").GetSafeComponent<Camera>();
        Cameras[2] = transform.Find("p3_cam").GetSafeComponent<Camera>();
        Cameras[3] = transform.Find("p4_cam").GetSafeComponent<Camera>();

        CanvasPL[0] = transform.Find("p1_cam").Find("p1_Canvas").gameObject;
        CanvasPL[1] = transform.Find("p2_cam").Find("p2_Canvas").gameObject;
        CanvasPL[2] = transform.Find("p3_cam").Find("p3_Canvas").gameObject;
        CanvasPL[3] = transform.Find("p4_cam").Find("p4_Canvas").gameObject;

        for (int i = 0; i < SplitValues.Length; i++)
        {
            switch (i)
            {
                default: SplitValues[i] = new SplitscreenPreset(); break;

                case 0: SplitValues[i] = new SplitscreenPreset(
                    new Vector2(0f, 0f), new Vector2(1f, 1f), 
                    new Vector2(0f, 0f), new Vector2(0f, 0f), 
                    new Vector2(0f, 0f), new Vector2(0f, 0f), 
                    new Vector2(0f, 0f), new Vector2(0f, 0f)); break;
                case 1: SplitValues[i] = new SplitscreenPreset(
                    new Vector2(0f, 0.5f), new Vector2(1f, 0.5f), 
                    new Vector2(0f, 0f), new Vector2(1f, 0.5f), 
                    new Vector2(0f, 0f), new Vector2(0f, 0f), 
                    new Vector2(0f, 0f), new Vector2(0f, 0f)); break;
                case 2: SplitValues[i] = new SplitscreenPreset(
                    new Vector2(0f, 0f), new Vector2(0.5f, 1f), 
                    new Vector2(0.5f, 0f), new Vector2(0.5f, 1f), 
                    new Vector2(0f, 0f), new Vector2(0f, 0f), 
                    new Vector2(0f, 0f), new Vector2(0f, 0f)); break;


                case 3: SplitValues[i] = new SplitscreenPreset(
                    new Vector2(0f, 0.5f), new Vector2(1f, 0.5f),         
                    new Vector2(0.5f, 0f), new Vector2(0.5f, 0.5f),       
                    new Vector2(0f, 0f), new Vector2(0.5f, 0.5f),       
                    new Vector2(0.5f, 0f), new Vector2(0.5f, 0.5f)); break;
                case 4: SplitValues[i] = new SplitscreenPreset(
                    new Vector2(0f, 0.5f), new Vector2(0.5f, 0.5f),      
                    new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f),       
                    new Vector2(0f, 0f), new Vector2(1f, 0.5f),         
                    new Vector2(0.5f, 0f), new Vector2(0.5f, 0.5f)); break;
              
  
                case 5: SplitValues[i] = new SplitscreenPreset(
                    new Vector2(0f, 0f), new Vector2(0.5f, 1f), 
                    new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), 
                    new Vector2(0.5f, 0f), new Vector2(0.5f, 0.5f), 
                    new Vector2(0.5f, 0f), new Vector2(0.5f, 0.5f)); break;
                case 6: SplitValues[i] = new SplitscreenPreset(
                    new Vector2(0f, 0.5f), new Vector2(0.5f, 0.5f), 
                    new Vector2(0f, 0f), new Vector2(0.5f, 0.5f), 
                    new Vector2(0.5f, 0f), new Vector2(0.5f, 1f), 
                    new Vector2(0.5f, 0f), new Vector2(0.5f, 1f)); break;

                case 7: SplitValues[i] = new SplitscreenPreset(
                    new Vector2(0f, 0.5f), new Vector2(0.5f, 0.5f), 
                    new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), 
                    new Vector2(0f, 0f), new Vector2(0.5f, 0.5f), 
                    new Vector2(0.5f, 0f), new Vector2(0.5f, 0.5f)); break;
            }
        }
    }

    //enable and disable camer ascripts on the object to safe resources. Also changes the rects of the cameras according to chosen playernumber
    private void OverrideCameraValues(SplitscreenPreset _SetValues)
    {
        Cameras[0].rect = new Rect(_SetValues.pos1.x, _SetValues.pos1.y ,_SetValues.c1.x, _SetValues.c1.y);
        Cameras[1].rect = new Rect(_SetValues.pos2.x, _SetValues.pos2.y, _SetValues.c2.x, _SetValues.c2.y);
        Cameras[2].rect = new Rect(_SetValues.pos3.x, _SetValues.pos3.y, _SetValues.c3.x, _SetValues.c3.y);
        Cameras[3].rect = new Rect(_SetValues.pos4.x, _SetValues.pos4.y, _SetValues.c4.x, _SetValues.c4.y);

        switch (Type)
        {
            default: Cameras[0].enabled = true; Cameras[1].enabled = true; Cameras[2].enabled = true; Cameras[3].enabled = true; break;
            
            case SplitType.p1_fullscreen :  Cameras[0].enabled = true;  Cameras[1].enabled = false;     Cameras[2].enabled = false;     Cameras[3].enabled = false; 
                                            CanvasPL[0].SetActive(true);  CanvasPL[1].SetActive(false);     CanvasPL[2].SetActive(false);     CanvasPL[3].SetActive(false); break;

            case SplitType.p2_horizontal :  Cameras[0].enabled = true;  Cameras[1].enabled = true;     Cameras[2].enabled = false;     Cameras[3].enabled = false; 
                                            CanvasPL[0].SetActive(true);  CanvasPL[1].SetActive(true);     CanvasPL[2].SetActive(false);     CanvasPL[3].SetActive(false); break;


            case SplitType.p2_vertical :    Cameras[0].enabled = true;  Cameras[1].enabled = true;     Cameras[2].enabled = false;     Cameras[3].enabled = false; 
                                            CanvasPL[0].SetActive(true);  CanvasPL[1].SetActive(true);     CanvasPL[2].SetActive(true);     CanvasPL[3].SetActive(false); break;

            case SplitType.p3_bot :         Cameras[0].enabled = true;  Cameras[1].enabled = true;     Cameras[2].enabled = true;     Cameras[3].enabled = false; 
                                            CanvasPL[0].SetActive(true);  CanvasPL[1].SetActive(true);     CanvasPL[2].SetActive(true);     CanvasPL[3].SetActive(false); break;

            case SplitType.p3_left :        Cameras[0].enabled = true;  Cameras[1].enabled = true;     Cameras[2].enabled = true;     Cameras[3].enabled = false; 
                                            CanvasPL[0].SetActive(true);  CanvasPL[1].SetActive(true);     CanvasPL[2].SetActive(true);     CanvasPL[3].SetActive(false); break;

            case SplitType.p3_right :       Cameras[0].enabled = true;  Cameras[1].enabled = true;     Cameras[2].enabled = true;     Cameras[3].enabled = false; 
                                            CanvasPL[0].SetActive(true);  CanvasPL[1].SetActive(true);     CanvasPL[2].SetActive(true);     CanvasPL[3].SetActive(false); break;

            case SplitType.p3_top :         Cameras[0].enabled = true;  Cameras[1].enabled = true;     Cameras[2].enabled = true;     Cameras[3].enabled = false; 
                                            CanvasPL[0].SetActive(true);  CanvasPL[1].SetActive(true);     CanvasPL[2].SetActive(true);     CanvasPL[3].SetActive(false); break;

            case SplitType.p4_screen :      Cameras[0].enabled = true;  Cameras[1].enabled = true;     Cameras[2].enabled = true;     Cameras[3].enabled = true; 
                                            CanvasPL[0].SetActive(true);  CanvasPL[1].SetActive(true);     CanvasPL[2].SetActive(true);     CanvasPL[3].SetActive(true); break;

        }
    }
}
