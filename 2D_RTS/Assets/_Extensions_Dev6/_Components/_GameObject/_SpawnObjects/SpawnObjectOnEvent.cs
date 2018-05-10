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
    public class SpawnObjectOnEvent : MonoBehaviour , IcanGetTriggered
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

    public CSpawnableObject3D[] Container = new CSpawnableObject3D[1];

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
                if (Container[i].RandomZmax < Container[i].RandomZmin)
                    Debug.LogError("RandomZmax is smaller than RandomXmin! : " + this.ToString());
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
    void SpawnObject(CSpawnableObject3D _Object)
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

    Vector3 SetPosition(CSpawnableObject3D _Object)
    {
            if (_Object.RandomSpawn)
            {
                float RandomX = UnityEngine.Random.Range(_Object.RandomXmin, _Object.RandomXmax);
                float RandomY = UnityEngine.Random.Range(_Object.RandomYmin, _Object.RandomYmax);
                float RandomZ = UnityEngine.Random.Range(_Object.RandomZmin, _Object.RandomZmax);
                return new Vector3(RandomX, RandomY, RandomZ);
            }
            else
            {
                return _Object.RelativeSpawnPos;
            }
    }

    Vector3 SetRotation(CSpawnableObject3D _Object)
    {
        Vector3 tRotation = Vector3.zero;

        if (_Object.RotationRandom)
        {
                tRotation = new Vector3(UnityEngine.Random.Range(_Object.RotationMin, _Object.RotationMax), UnityEngine.Random.Range(_Object.RotationMin, _Object.RotationMax), UnityEngine.Random.Range(_Object.RotationMin, _Object.RotationMax));
        }
        else
        {
                tRotation = _Object.RotationDegree;
        }

        //Debug.Log("Rotation 2D " + _RotationAngle);
        return tRotation;
    }

        //Spawn with set properties:
        GameObject SpawnAfterPropertiesAreSet(CSpawnableObject3D _SpawnableObject, Vector3 _Position, Quaternion _Rotation,  float _Scale)
    {
        GameObject go_temp = Instantiate(_SpawnableObject.Prefab, this.transform.position + _Position, _Rotation) as GameObject;
        go_temp.transform.localScale = go_temp.transform.localScale * _Scale;
        return go_temp;
    }

    float SetScale(CSpawnableObject3D _Object)
    {
        if (_Object.RandomScale)
        {
            return UnityEngine.Random.Range(_Object.ScaleMin, _Object.ScaleMax);
        }
        else
            return 1.00f;
    }

    //misc properties Layer Tag ObjectName SpriteRenderer
    void SetOtherProperties(CSpawnableObject3D _Obj, GameObject _GO) //_Obj info and GO already spawned
    {
        if (_Obj.ObjectName != "")
            _GO.name = _Obj.ObjectName;
    }
        
    //NO GAME OBJECT ERROR HANDLING:
    void EnableAllDisableAll(bool _Set)
    {
         //this function receaves a bool of a checker and if something is wrong it will disable all spawners!
        if(_Set) //nonGameObject so error and we dont spawn anything!
        {
            _OnStart = false; _OnDisable = false; _OnDestroy = false; _OnAwake = false; //no handling for timed spawn!
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
    public class CSpawnableObject3D
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
        public Vector3 RotationDegree = Vector3.zero;           //If zero no rotation
        //[HideIfNot("RotationRandom", true, "Container", "RotationMin")]
        public float RotationMin = -180f;       //Rotates from the RotationDegree rotation! not From the start Rotation
        //[HideIfNot("RotationRandom", true, "Container", "RotationMax")]
        public float RotationMax = 180f;            //Rotates from the RotationDegree rotation! not From the start Rotation

        //Random Position
        //[Header("#Position")]
        public bool RandomSpawn = true;         //if true random position relative to RelativeSpawnPos else at RelativeSpawnPos
        //[HideIfNot("RandomSpawn", false, "Container", "RelativeSpawnPos")]
        [Tooltip("Wir benutzen immer 0 für Z! Ausser in speziellen Sonderfällen!!!")]
        public Vector3 RelativeSpawnPos = Vector3.zero; //relative to transform.position of destroyed object
        //[HideIfNot("RandomSpawn", true, "Container", "RandomXmin")]
        public float RandomXmin = -2.5f;
        //[HideIfNot("RandomSpawn", true, "Container", "RandomXmax")]
        public float RandomXmax = 2.5f;
        //[HideIfNot("RandomSpawn", true, "Container", "RandomYmin")]
        public float RandomYmin = 0f;
        //[HideIfNot("RandomSpawn", true, "Container", "RandomYmax")]
        public float RandomYmax = 2.5f;
        //[HideIfNot("RandomSpawn", true, "Container", "RandomZmin")]
        public float RandomZmin = -2.5f;
        //[HideIfNot("RandomSpawn", true, "Container", "RandomZmax")]
        public float RandomZmax = 2.5f;

        //Random Scale
        //[Header("#Scale")]
        public bool RandomScale = false;            //if true random scale else defaul scale
        //[HideIfNot("RandomScale", true, "Container", "ScaleMin")]
        public float ScaleMin = 0.8f;
        //[HideIfNot("RandomScale", true, "Container", "ScaleMax")]
        public float ScaleMax = 1.2f;



        ////Physics
        ////[Header("#Physics")]
        ////[Tooltip("Deaktiviert die komplette #Physics Kategorie auch wenn sie nicht komplett versteckt ist im inspector!")]
        //public bool HasRigidbody = false;  //if true checks if there is a rigidbody and adds one
        

        ////[HideIfNot("HasRigidbody", true, "Container", "Gravity")]
        //public bool Gravity = false;

        ////[HideIfNot("HasRigidbody", true, "Container", "IsKinematic")]
        //public bool IsKinematic = false;
        ////[HideIfNot("HasRigidbody", true, "Container", "RandomForceValue")]
        //public bool RandomForceValue = false;        //if true makes a random force power betwen min and max else ForceValue
        ////[HideIfNot("RandomForceValue", false, "Container", "NotRandomForce")]
        //public float NotRandomForce = 2f;
        ////[HideIfNot("RandomForceValue", true, "Container", "ForceValueRndMin")]
        //public float ForceValueRndMin = 2f;
        ////[HideIfNot("RandomForceValue", true, "Container", "ForceValueRndMax")]
        //public float ForceValueRndMax = 2f;
        ////[HideIfNot("HasRigidbody", true, "Container", "RndForceDirection")]
        //public bool RndForceDirection = true;        //if true Random Direction 360 degrees

        ////[HideIfNot("RndForceDirection", false, "Container", "PhysicsForceDirection")]
        //public Vector3 PhysicsForceDirection = Vector3.zero;
    }

    #endregion
}
}
