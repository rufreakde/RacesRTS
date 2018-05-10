/*********************
*	Rudolf Chrispens
***********************/

#region USE
using UnityEngine;
using System.Collections;
#endregion

//[SelectionBase]
public class URL : MonoBehaviour 
{
    [Split("DatabaseURLs")]
    public static string ButtonDB = "http://localhost/ButtonStringsDatabase/RetrieveTT.php";
    public static string ToolTipDB = "http://localhost/ToolTipDatabase/RetrieveTT.php";
    public static string AlertDB = "http://localhost/ToolTipDatabase/RetrieveTT.php";
}
