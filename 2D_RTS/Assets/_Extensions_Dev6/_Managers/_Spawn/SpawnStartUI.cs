/*******************
* Rudolf Chrispens *
********************/


#region using
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
#endregion

namespace Dev6
{
    [System.Serializable]
    [AddComponentMenu("Dev6/MANAGER/Init UI")]
    public class SpawnStartUI : MonoBehaviour, IamSingleton
    {
        public UIPrefab[] StartPrefabs = new UIPrefab[0];
        private GameObject lastInstantiated = null;

        public void iInitialize()
        {
            //Debug.Log("Init...: " + this.ToString() + "  Time: " + Time.frameCount);
            InstantiateStartObjects();
            //Debug.Log("...Finished " + this.ToString() + "  Time: " + Time.frameCount);
        }

        void InstantiateStartObjects()
        {
            for(int i=0; i<StartPrefabs.Length; i++)
            {
                if(StartPrefabs[i].Prefab == null)
                {
                    Debug.LogError("Spawning failed Prefab of " + StartPrefabs[i] + " == null ! ");
                    continue;
                }

                if (!StartPrefabs[i].KeepPrefabTransform)
                    lastInstantiated = Instantiate(StartPrefabs[i].Prefab, StartPrefabs[i].Position, Quaternion.Euler(StartPrefabs[i].Rotation)) as GameObject;
                else
                    lastInstantiated = Instantiate(StartPrefabs[i].Prefab);

                if (StartPrefabs[i].Parent != "")
                {
                    GameObject tGO = GameObject.Find(StartPrefabs[i].Parent);
                    if (!tGO)
                    {
                        tGO = new GameObject();
                        tGO.name = StartPrefabs[i].Parent;
                        tGO.transform.ResetTransform();
                    }
                    lastInstantiated.transform.SetParent(tGO.transform, false);
                    lastInstantiated.transform.localScale = Vector3.one;
                }

                lastInstantiated.name = lastInstantiated.name.Split('(')[0];
            }
        }

        [System.Serializable]
        public class UIPrefab 
        {
            [Tooltip("Let this be null if you dont need a Parent")]
            public string Parent = "";
            public GameObject Prefab = null;
            [Tooltip("Exclude Position and Rotation use the Prefab Settings instead! Does not exclude the Parent!!!")]
            public bool KeepPrefabTransform = true;
            [HideIfNot("KeepPrefabTransform", false, "StartPrefabs", "Position")]
            public Vector3 Position = Vector3.zero;
            [HideIfNot("KeepPrefabTransform", false, "StartPrefabs", "Rotation")]
            public Vector3 Rotation = Vector3.zero;
        }
    }
}
