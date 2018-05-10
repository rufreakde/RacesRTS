/*
using UnityEngine;
using Dev6;

public class DEV6_Gguidelines : MonoBehaviour
{
    //This guidelines are written in a very simple form. So there is no big text you have to go through.
    //And it was written extremely fast so there are tonns of spelling mistakes. If you find 'em fix 'em. xD :P

    //VISUALS

    //----Variables----//
    //in class/struct scope
    {
        //private variableName = defaultinitvalue;              //-> small first letter! (not accessible / not auto unity serialized)
        //protected variableName = defaultinitvalue;            //-> same declaration like private functions
        //[SerializeField] variableName = defaultinitvalue;     //-> use this way if you want it to be accesible via inspector

        //public VariableName = defaultinitvalue;               //-> try not to use public variables! Use getter and setter instead!
        //public gsVariableName //g == get s == set
        //{
        //    get { return privateVar; }
        //    set { privateVar = value; }
        //}
        //public static SHIT                                    //-> never use this! The only public static you should have in the project is inside of the MANAGER class!
    }

    //in function scope
    {
        //always use the same naming as the private one but insert also an identifier
        //t = temporary
        //c = counter
        //NOTE: this is just nice to have I personaly only use the t anc c identifier you can thing about your own and if so, use them consistantly!
    }

    //----Functions----//
    {
        //private TYPE function(param _ParamValue)
        //{
        //    return returnValue;
        //}

        //public TYPE Function(params _ParamValue)
        //{
        //    return returnValue;
        //}
    }

    //DO'S AND DONT'S
    {
        //GetSafeComponent()    //-> Always use this function if the reference should always be there if you "get" it!
        //GetComponent()        //-> Use this function otherwhise this wont Debug.LogError you a note. (return value can be null)
                                //-> Reason: this is just extremly convenient! It will save you a lot of Debug.Log() writing and test time.

        //AllUnityFunctions     //-> Never ever override a unity function! ( it is possible but just dont do it nest another virtual custom function inside of it!)
        //void Awake()          //-> Always use Awake only for initialization of this GameObject referenzes and self! Do not use GetComponent on other GameObjects!
        //void Start()          //-> Here you can initialize the references for everything that is not from this GameObject!
                                //-> Reason: That way you will never have a missmatch or "missing components" that are there but not initialized before another Object needs it!

        //void Update()         //-> For every Update: never use GetComponent() or GetSafeComponent() or Gameobject.Find() or anything like this during update get your external ref in Start() or internal ref in Awake()!
                                //-> Reason: You don't want to fight with the c# garbage collection.... yes just dont! ;)

        //MANAHER.GET.GetSingleton()    //-> Use this for Singleton management its an simplified IOC pattern for unity and keeps your project from breaking (when it gets realy big! And it will get realy big at some point).
        //Extensions            //-> there are a lot of extensions just use them they are tested (mostly), they save time and the code is better to understand with them.
                                //-> Example: this.transform.position = new Vector3(50f,0f,0f); -> this.transform.SetPositionX(50f);

        //IamSingleton Manager should not have any behaviour code ( checking and value update code is ok, of course ). Because that way you would not be able to use them
            //in scenes where references are missing. Managers should allways work in stand alone! They dont get references implanted this would break the IoC Pattern!
    }

    //GAMEOBJECT SETUP
        //All C# scripts (and I never want to se a J in our solution comprende? xD) have to be on the highest Parent of that Obejct!
        //3D Mesh Animators can be applied where they are needed
        //Unity Animation Animators are also on the highest Parent.
        //Colliders and other "Special" components are parented on an empty. The empty is parented on the logically correct position.
        //Do not Change transforms of a Mesh! A Mesh is always a child on the highest Parent. If you want to move something use the transform of the Highest parent.
        //If you use "Container" (empty transforms) to organize your szene. Add the TransformLock Component on them! This is very important.
            //if you dont do this you will habe a lot of problems with others moving the Container out of mistake and noone knows why everything is messed up.

    //thats it for now this document will be updated.
}
*/