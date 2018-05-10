using UnityEngine;
using System;
using System.Collections;

[RequireComponent(typeof(Animator))]
[ExecuteInEditMode]
[AddComponentMenu("Dev6/CONTROLLER/IK Control")]
public class IKControl : MonoBehaviour
{
    [System.Serializable]
    public class CIKSet
    {
        public Transform Handle = null;
        public AvatarIKGoal IKBone = AvatarIKGoal.RightHand;

        public bool IKactive = false;
        [Range(0f, 1f)]
        public float TransitionValue = 1f;
    }

    protected Animator animator = null;
    public bool IKHead = false;
    public Transform LookTarget = null;

    public CIKSet[] IKSet = new IKControl.CIKSet[0];
    [SerializeField]

    void Start()
    {
        if (animator == null)
            animator = this.GetSafeComponent<Animator>();
    }

    //a callback for calculating IK
    void OnAnimatorIK()
    {
        if (animator)
        {
            //head
            // Set the look target position, if one has been assigned
            if (LookTarget != null && IKHead)
            {
                animator.SetLookAtWeight(1);
                animator.SetLookAtPosition(LookTarget.position);
            }
            else
            {
                animator.SetLookAtWeight(0);
            }

            //IKGoalPosition = animator.GetIKPosition(IKBone);
            for (int i = 0; i < IKSet.Length; i++)
            {
                //IK functions per set
                IKAnimate(IKSet[i]);  
            }
        }
    }

    void IKAnimate(CIKSet _Set)
    {
        //if the IK is active, set the position and rotation directly to the goal. 
        if (_Set.IKactive)
        {
            // Set the IK target position and rotation, if it is assigned
            if (_Set.Handle != null)
            {
                animator.SetIKPositionWeight(_Set.IKBone, _Set.TransitionValue);
                animator.SetIKRotationWeight(_Set.IKBone, _Set.TransitionValue);
                animator.SetIKPosition(_Set.IKBone, _Set.Handle.position);
                animator.SetIKRotation(_Set.IKBone, _Set.Handle.rotation);
            }

        }
        //if the IK is not active, set the position and rotation of the IK and head back to the original position
        else
        {
            animator.SetIKPositionWeight(_Set.IKBone, 0);
            animator.SetIKRotationWeight(_Set.IKBone, 0);
        }
    }
}