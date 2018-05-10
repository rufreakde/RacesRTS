/*********************
*	Rudolf Chrispens
***********************/

#region USE
using UnityEngine;
using System.Collections;
using Dev6;
using System;
#endregion

namespace Dev6
{
    //[SelectionBase]
    [AddComponentMenu("Dev6/GAME OBJECTS/Spawn Object")]
    public class SpawnObjectOnEvent2D : MonoBehaviour , IcanGetTriggered
{
    #region variables
    [Split("SpawnObj3D")]
    [InfoBox("[Trigger]   SPAWN\nDieses Script instanziiert 3D Prefabs.")]
    public bool _OnDisable = true;
    public bool _OnDestroy = false;
    public bool _OnAwake = false;
    public bool _OnStart = false;
    [Range(0f,100f)]
    public float _OnTimed = 0f;
    private float _onTimed = 0f;

    [Space(20f)]
    [Tooltip("Wenn das auf 'true' gesetzt wird dann werden nicht alle Objekte gespawned sondern nur zufällig ausgewählte ( auch doppelungen)")]
    public bool RandomAOA = false;
    [HideIfNot("RandomAOA", true)]
    [Tooltip("This will be enabled if RandomlyOutOfArray is active")]
    public int RandomCount = 1;

    public CSpawnableObject2D[] Container = new CSpawnableObject2D[1];

    private bool GameIsPlaying = true;	//true as long the game is running false if the game is closed so no spawn when false
    private GameObject tGameObject; //alloc memory

    void OnApplicationQuit()
    {
        GameIsPlaying = false;
    }

    public void Spawn()
    {
        if (GameIsPlaying)
            StartSpawning();
    }

    void Awake()
    {
        EnableAllDisableAll(CheckForNonGameObjects()); //no game object error handling
        CheckValidSettings();

        _onTimed = _OnTimed; // set the reset timer to ´set start time

        if (_OnAwake && GameIsPlaying)
            StartSpawning();
    }

    void OnDisable()
    {
        if (_OnDisable && GameIsPlaying)
            StartSpawning();
    }

    void OnDestroy()
    {
        if (_OnDestroy && GameIsPlaying)
            StartSpawning();
    }

    void Start()
    {
        if (_OnStart && GameIsPlaying)
            StartSpawning();
    }

    void Update()
    {
        if (_onTimed == 0) // if the reset time is zero no spawn
            return;

        _OnTimed -= Time.deltaTime;

        if (_OnTimed < 0f && GameIsPlaying)
        {
            StartSpawning();
                _OnTimed = _onTimed;
        }
    }
    #endregion

        private void CheckValidSettings()
        {
            for (int i = 0; i < Container.Length; i++)
            {
                if (Container[i].RandomXmax < Container[i].RandomXmin)
                    Debug.LogError("RandomXmax is smaller than RandomXmin! : " + this.ToString());
                if (Container[i].RandomYmax < Container[i].RandomYmin)
                    Debug.LogError("RandomYmax is smaller than RandomXmin! : " + this.ToString());
            }
        }

    public void StartSpawning()
    {
        //befehl um zu spawnen
        //how many? check settings and make spawn calls
        //random out of Array?
        //Random Count?
        if (RandomAOA)
        {
            for (int i = 0; i < RandomCount; i++)
            {
                int tRand = UnityEngine.Random.Range(0, Container.Length); //choose target out of array
                    SpawnObject(Container[tRand]);
            }
        }
        else
        {
            for (int i = 0; i < Container.Length; i++)
            {
                SpawnObject(Container[i]);
            }
        }
    }

    //Eigentlicher Spawn
    void SpawnObject(CSpawnableObject2D _Object)
    {
        //SetPos
        Vector3 _Position = SetPosition(_Object);
        //SetRot
        Vector3 _Rotation = SetRotation(_Object);
        //SetScale
        float _Scale = SetScale(_Object);

        tGameObject = SpawnAfterPropertiesAreSet(_Object, _Position, Quaternion.Euler(_Rotation), _Scale);
        SetOtherProperties(_Object, tGameObject);
        //ApplyPhysicsToGO(_Object, tGameObject);
    }

    Vector2 SetPosition(CSpawnableObject2D _Object)
    {
            if (_Object.RandomSpawn)
            {
                float RandomX = UnityEngine.Random.Range(_Object.RandomXmin, _Object.RandomXmax);
                float RandomY = UnityEngine.Random.Range(_Object.RandomYmin, _Object.RandomYmax);
                return new Vector3(RandomX, RandomY);
            }
            else
            {
                return _Object.RelativeSpawnPos;
            }
    }

    Vector3 SetRotation(CSpawnableObject2D _Object)
    {
        Vector3 tRotation = Vector3.zero;

        if (_Object.RotationRandom)
        {
                tRotation = new Vector3(0f, 0f, UnityEngine.Random.Range(_Object.RotationMin, _Object.RotationMax));
        }
        else
        {
                tRotation = new Vector3(0f, 0f, _Object.RotationDegree);
        }

        //Debug.Log("Rotation 2D " + _RotationAngle);
        return tRotation;
    }

        //Spawn with set properties:
        GameObject SpawnAfterPropertiesAreSet(CSpawnableObject2D _SpawnableObject, Vector3 _Position, Quaternion _Rotation,  float _Scale)
    {
        GameObject go_temp = Instantiate(_SpawnableObject.Prefab, this.transform.position + _Position, _Rotation) as GameObject;
        go_temp.transform.localScale = go_temp.transform.localScale * _Scale;
        return go_temp;
    }

    float SetScale(CSpawnableObject2D _Object)
    {
        if (_Object.RandomScale)
        {
            return UnityEngine.Random.Range(_Object.ScaleMin, _Object.ScaleMax);
        }
        else
            return 1.0f;
    }

    //misc properties Layer Tag ObjectName SpriteRenderer
    void SetOtherProperties(CSpawnableObject2D _Obj, GameObject _GO) //_Obj info and GO already spawned
    {
        if (_Obj.ObjectName != "")
            _GO.name = _Obj.ObjectName;
    }

    //NO GAME OBJECT ERROR HANDLING:
    void EnableAllDisableAll(bool _Set)
    {
        if(_Set) //nonGameObject so error and we dont spawn anything!
        {
                _OnStart = false; _OnDisable = false; _OnDestroy = false; _OnAwake = false;
        }
    }

    bool CheckForNonGameObjects()
    {
        for (int i = 0; i < Container.Length; i++)
        {
            if (Container[i].Prefab.GetType() != typeof(GameObject) || Container[i].Prefab == null)
            {
                Debug.LogError("ERROR 01 : Type not Supported please make sure its a GameObject " + "| Element " + i + " | Type:" + Container[i].Prefab.GetType() + " | Objectname: " + Container[i].Prefab.name);
                return true;
            }
        }
        return false;
    }

        public void iTrigger(string _Trigger)
        {
            if (_Trigger == "SPAWN")
                StartSpawning();
        }

        #region Definitions

        [System.Serializable]
    public class CSpawnableObject2D
    {
        //[Header("#SpawnObject")]
        public GameObject Prefab = null;

        //[Header("#GameobjectSettings")]
        public string ObjectName = "";  //wenn string name leer wird der name nicht geendert clone immer abschneiden
        //public string Tag = ""; //If tag empty use untagged
        //public string Layer = "";   // ""


        //Basic
        //[Header("#Rotation")]
        public bool RotationRandom = false;     //Random Rotation
        //[HideIfNot("RotationRandom", false, "Container", "RotationDegree")]
        public float RotationDegree = 0f;           //If zero no rotation
        //[HideIfNot("RotationRandom", true, "Container", "RotationMin")]
        public float RotationMin = -180f;       //Rotates from the RotationDegree rotation! not From the start Rotation
        //[HideIfNot("RotationRandom", true, "Container", "RotationMax")]
        public float RotationMax = 180f;            //Rotates from the RotationDegree rotation! not From the start Rotation

        //Random Position
        //[Header("#Position")]
        public bool RandomSpawn = true;         //if true random position relative to RelativeSpawnPos else at RelativeSpawnPos
        //[HideIfNot("RandomSpawn", false, "Container", "RelativeSpawnPos")]
        public Vector2 RelativeSpawnPos = Vector2.zero; //relative to transform.position of destroyed object
        //[HideIfNot("RandomSpawn", true, "Container", "RandomXmin")]
        public float RandomXmin = -2.5f;
        //[HideIfNot("RandomSpawn", true, "Container", "RandomXmax")]
        public float RandomXmax = 2.5f;
        //[HideIfNot("RandomSpawn", true, "Container", "RandomYmin")]
        public float RandomYmin = 0f;
        //[HideIfNot("RandomSpawn", true, "Container", "RandomYmax")]
        public float RandomYmax = 2.5f;
        //[HideIfNot("RandomSpawn", true, "Container", "RandomZmin")]

        //Random Scale
        //[Header("#Scale")]
        public bool RandomScale = false;            //if true random scale else defaul scale
        //[HideIfNot("RandomScale", true, "Container", "ScaleMin")]
        public float ScaleMin = 0.8f;
        //[HideIfNot("RandomScale", true, "Container", "ScaleMax")]
        public float ScaleMax = 1.2f;
    }

    #endregion
}
}
