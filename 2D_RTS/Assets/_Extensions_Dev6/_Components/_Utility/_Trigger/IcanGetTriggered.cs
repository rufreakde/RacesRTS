using UnityEngine;
using System.Collections;

public interface IcanGetTriggered
{

    /// <summary>
    /// This function will be called from the Trigger. Insert custom logic to each solo triggers!
    /// </summary>
    void iTrigger(string _Trigger);
}