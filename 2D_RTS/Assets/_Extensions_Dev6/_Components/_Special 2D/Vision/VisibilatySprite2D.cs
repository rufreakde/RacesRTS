using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
[AddComponentMenu("Dev6/2D/Vision Sprite Effect")]
public class VisibilatySprite2D : MonoBehaviour
{
    [InfoBox("Füge dieses Script zu jedem Sprite hinzu das vom Visibility2D System beeinflusst werden soll!")]
    public Material VisibilityMaterial = null;
    private SpriteRenderer myRenderer = null;

    void Awake()
    {
        CreateDefaultMaterial();

        myRenderer = this.GetComponent<SpriteRenderer>();

        myRenderer.material = VisibilityMaterial;

        Destroy(this);
    }

    void CreateDefaultMaterial()
    {
        //care multiple materials
    }
}