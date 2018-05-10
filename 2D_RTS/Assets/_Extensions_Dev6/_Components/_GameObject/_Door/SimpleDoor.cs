using UnityEngine;
using System.Collections;
using System;

//object needs an animator
[AddComponentMenu("Dev6/GAMGE OBJECTS/Simple Door")]
public class SimpleDoor : MonoBehaviour , IcanGetTriggered{

    [InfoBox("[Trigger]   UNLOCK | LOCK | OPEN | CLOSE \n[ImAnimator]   Braucht im Animator Controller einen Bool mit 'open'!")]
    [SerializeField] bool Locked = false;
    Animator myAnimator = null;

    void Awake()
    {
        myAnimator = this.transform.GetComponentInChildren<Animator>();

        if(!myAnimator)
        {
            Debug.LogError("No animator found for: " + this.gameObject.name + "  script:" + this.ToString());
        }
    }

    public void DoorLockState(bool _Locked)
    {
        Locked = _Locked;
    }

    public void iTrigger(string _Trigger)
    {
        switch (_Trigger)
        {
            default: Debug.LogError("Something went wrong trigger: '" + _Trigger + "' does not exist!"); return;
            case "UNLOCK": { DoorLockState(false); } break;
            case "LOCK": { DoorLockState(false); } break;
            case "OPEN": { if (!Locked) myAnimator.SetBool("open", true); } break;
            case "CLOSE": { myAnimator.SetBool("open", false); } break;
        }
    }
}
