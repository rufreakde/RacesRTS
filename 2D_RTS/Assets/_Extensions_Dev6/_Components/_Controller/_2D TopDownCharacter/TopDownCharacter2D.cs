using UnityEngine;
using System.Collections;
using System;


[AddComponentMenu("Dev6/2D/Top Down Character2D")]
public class TopDownCharacter2D : MonoBehaviour, IcanGetTriggered
{
    [InfoBox("[Trigger]   UP | DOWN | LEFT | RIGHT | CLOCKWISE | CCLOCKWISE | MOUSELOCK | NOTMOUSELOCK\n")]
    public bool AlwaysOnMouse = true;
    [HideIfNot("AlwaysOnMouse",false)]
    [Range(1f, 360f)]
    [SerializeField]private float turnSpeed = 10f;
    //[ShowOnly][SerializeField]private float rotation = 0f;
    [SerializeField]private float horizontal = 10f;
    [SerializeField]private float vertical = 10f;
    

    private Transform myTransform;
    private Vector3 RotateAxsis = new Vector3(0f,0f,1f);

    void Awake()
    {
        this.myTransform = this.GetSafeComponent<Transform>();
    }

    public void iTrigger(string _Trigger)
    {
        //like turn left turn right move left move right and so on ^^
        switch (_Trigger)
        {
            default: Debug.LogError("Something went wrong trigger: '" + _Trigger + "' does not exist!"); return;
            case "UP": { MoveVerticaly(Direction.Up); return; }
            case "DOWN": { MoveVerticaly(Direction.Down); return; }

            case "LEFT": { MoveHorizontaly(Direction.Left); return; }
            case "RIGHT": { MoveHorizontaly(Direction.Right); return; }

            case "CLOCKWISE": { TurnIntoDirection(Direction.Clockwise); return; }
            case "CCLOCKWISE": { TurnIntoDirection(Direction.CounterClockwise); return; }
            case "MOUSELOCK": { AlwaysOnMouse = true; return; }
            case "NOTMOUSELOCK": { AlwaysOnMouse = false; return; }
        }
    }

    void Update()
    {
        //set the variable to Z axis rotation
        //rotation = this.transform.rotation.eulerAngles.z;
        //Lookat mouse
        if (AlwaysOnMouse)
            this.transform.LookAt(Input.mousePosition); //TO DO real rotate to mouse pos and CAMERA.main.world mouse matrix
    }

    public void MoveHorizontaly(Direction _Direction)
    {
        if (_Direction == Direction.Left)
            myTransform.position = (Vector2)myTransform.position + Vector2.left * ( vertical * Time.deltaTime);
        else
            myTransform.position = (Vector2)myTransform.position + Vector2.right * (vertical * Time.deltaTime);
    }

    public void MoveVerticaly(Direction _Direction)
    {
        if (_Direction == Direction.Up)
            myTransform.position = (Vector2)myTransform.position + Vector2.up * (horizontal * Time.deltaTime);
        else
            myTransform.position = (Vector2)myTransform.position + Vector2.down * (horizontal * Time.deltaTime);
    }

    public void TurnIntoDirection(Direction _Direction)
    {
        if(_Direction == Direction.Clockwise)
            myTransform.rotation = myTransform.rotation.RotateInstantlyAroundAxis(turnSpeed * Time.deltaTime, RotateAxsis);
        else
            myTransform.rotation = myTransform.rotation.RotateInstantlyAroundAxis(-turnSpeed * Time.deltaTime, RotateAxsis);
    }

    public enum Direction
    {
        Clockwise = 0,
        CounterClockwise,
        Up,
        Down,
        Left,
        Right
    }
}
