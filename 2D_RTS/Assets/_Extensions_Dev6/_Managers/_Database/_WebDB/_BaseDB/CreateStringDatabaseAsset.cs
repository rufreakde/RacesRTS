using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Dev6;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class CreateStringDatabaseAsset
{

#if UNITY_EDITOR
    [MenuItem("Assets/Create/StringDataBase")]
    public static void CreateAsset()
    {
        StringDataBase asset = ScriptableObject.CreateInstance<StringDataBase>();

        string path = UnityEditor.AssetDatabase.GetAssetPath(UnityEditor.Selection.activeObject);
        if (path == "")
        {
            path = "Assets";
        }
        else if (Path.GetExtension(path) != "")
        {
            path = path.Replace(Path.GetFileName(UnityEditor.AssetDatabase.GetAssetPath(UnityEditor.Selection.activeObject)), "");
        }

        string assetPathAndName = UnityEditor.AssetDatabase.GenerateUniqueAssetPath(path + "/" + "renameMe_StringDB" + ".asset" );

        UnityEditor.AssetDatabase.CreateAsset(asset, assetPathAndName);

        UnityEditor.AssetDatabase.SaveAssets();
        //UnityEditor.EditorUtility.FocusProjectWindow();
        UnityEditor.Selection.activeObject = asset;
    }

#endif

}
