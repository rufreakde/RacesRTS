using UnityEngine;
using System.Collections;

namespace Dev6
{
    [AddComponentMenu("Dev6/GAME OBJECTS/Model Detail View")]
    public class ViewModelInDetail : MonoBehaviour
    {
        [Range(0.1f,2f)]
        public float Range = 1f;
        [Tooltip("This option will snap the object back into start position after drag!")]
        public bool ResetSnap = true;

        private Collider myCollider = null;
        private Quaternion StoredBaseRotation; //used to keep pose if ResetSnap not enabled
        private Quaternion InitialStoredBaseRotation; //used to reset to pos
        private Vector3 AnchorHitPoint = Vector3.zero;
        private Vector3 DirectionOfMouse = Vector3.zero;
        //private Vector3 CameraRotation = Vector3.zero;

        // Use this for initialization
        void Start()
        {
            StoredBaseRotation = this.transform.rotation;
            InitialStoredBaseRotation = this.transform.rotation;

            myCollider = this.GetComponent<Collider>(); //Try to find a parent collider

            if (!myCollider) //no parent collider then find one in children or create one if there is a mesh on the parent
            {
                MeshCollider myMeshCollider;
                MeshFilter myMeshFilter = this.transform.GetComponent<MeshFilter>(); //try to get a mesh on the parent
                if (!myMeshFilter) //no mesh on parent? then try to gind a mesh in children
                {
                    myMeshFilter = this.transform.GetComponentInChildren<MeshFilter>(); //try to find a mesh
                    if (myMeshFilter)   //if you find a mesh
                    {
                        myCollider = myMeshFilter.transform.gameObject.GetComponent<Collider>(); // try to find a collider on the coresponding mesh
                        if (!myCollider) // no collider but we have a mesh? create one
                        {
                            myMeshCollider = myMeshFilter.transform.gameObject.AddComponent<MeshCollider>();
                            myMeshCollider.convex = true;
                        }
                    }
                }
                else
                {
                    //found a Mesh in parent:
                    myCollider = myMeshFilter.transform.gameObject.GetComponent<Collider>(); // try to find a collider on the coresponding mesh
                    if (!myCollider) // no collider but we have a mesh? create one
                    {
                        myMeshCollider = myMeshFilter.transform.gameObject.AddComponent<MeshCollider>();
                        myMeshCollider.convex = true;
                    }
                }
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0)) //start rotation
            {
                RaycastHit hit;
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 99f))  //cast a ray from mouse position over camera look Z
                {
                    AnchorHitPoint = Camera.main.WorldToScreenPoint(hit.point); // you have the hit point
                }
                else
                    AnchorHitPoint = Input.mousePosition; // if no hit but you still drag it we take the pivot as anchor point!
            }

            if (Input.GetMouseButton(0)) //rotation
            {
                DirectionOfMouse = (Input.mousePosition - AnchorHitPoint).normalized; // from pivot to anchor point direction that we have to add to the position

                //get the normal of DirectionOfMouse:
                DirectionOfMouse = new Vector3(DirectionOfMouse.y, -DirectionOfMouse.x, DirectionOfMouse.z);

                float DirectionOfMouseDistance = Vector3.Distance(Input.mousePosition, AnchorHitPoint);

                //Debug.Log("Length: " + DirectionOfMouseDistance + " Mouse: " + Input.mousePosition);

                this.transform.rotation = StoredBaseRotation.RotateInstantlyAroundAxis(DirectionOfMouseDistance * Range, DirectionOfMouse);
            }

            
            if(ResetSnap && Input.GetMouseButtonUp(0)) // on release snap back
            {
                this.transform.rotation = InitialStoredBaseRotation; // reset to the start position
                StoredBaseRotation = InitialStoredBaseRotation;
            }
            else if(Input.GetMouseButtonUp(0))
            {
                StoredBaseRotation = this.transform.rotation; // so if the resetsnap is disabled we always have to store the new "init" position
            }
        }

        //void OnDrawGizmos()
        //{
        //    Gizmos.color = Color.blue;
        //    Gizmos.DrawWireSphere(Camera.main.ScreenToWorldPoint(AnchorHitPoint), 0.4f);

        //    Gizmos.DrawLine(Camera.main.ScreenToWorldPoint(AnchorHitPoint), Camera.main.ScreenToWorldPoint(AnchorHitPoint + DirectionOfMouse * 400f));

        //    Gizmos.color = Color.red;
        //    Gizmos.DrawWireSphere(Camera.main.ScreenToWorldPoint( AnchorHitPoint + DirectionOfMouse * 40f), 0.4f);
        //}

    }
}
