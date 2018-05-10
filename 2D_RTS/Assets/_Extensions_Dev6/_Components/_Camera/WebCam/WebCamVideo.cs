/********************************************
*	Rudolf Chrispens

THIS SCRIPT HAS TO BE USED WITH A RAW IMAGE
********************************************/

#region USE
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
#endregion

[RequireComponent(typeof(RawImage))]
[AddComponentMenu("Dev6/CAMERA/Webcam")]
public class WebCamVideo : MonoBehaviour
{
    public bool DEBUG = true;
    public RawImage rawimage;
    WebCamTexture webcamTexture;

    void Start()
    {
        rawimage = GetComponent<RawImage>();
        webcamTexture = new WebCamTexture();
        //Debug.Log("1 Texture: " + rawimage.texture + "  webcamtexture: " + webcamTexture);
        rawimage.texture = webcamTexture;
        //Debug.Log("2 Texture: " + rawimage.texture + "  webcamtexture: " + webcamTexture);
        rawimage.material.mainTexture = webcamTexture;
        webcamTexture.Play();
    }

    void OnGUI()
    {
        if (DEBUG)
        {
            if (webcamTexture.isPlaying)
            {
                if (GUILayout.Button("Stop"))
                {
                    webcamTexture.Stop();
                }
            }
            else
            {
                if (GUILayout.Button("Play"))
                {
                    webcamTexture.Play();
                }
            }
        }
    }
}