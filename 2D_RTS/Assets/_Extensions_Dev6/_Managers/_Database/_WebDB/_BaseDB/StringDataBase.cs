/*******************
* Rudolf Chrispens *
*******************/

#region using
using UnityEngine;
using System.Collections;
#endregion

namespace Dev6 {

    [System.Serializable]
    public class StringDataBase : ScriptableObject
    {
        [SerializeField]
        public CustomDict<string, string> Data = new CustomDict<string, string>();
    }
}
