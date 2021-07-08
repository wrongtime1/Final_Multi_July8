using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace FootAnkle
{
    public class CharacterIK : MonoBehaviour
    {
        protected Animator anim;
        AnimatorClipInfo[] m_CurrentCLipInfo;
        string m_ClipName;
        // Start is called before the first frame update
        [SerializeField]
        public float y;
        [SerializeField]
        public float z;
        [SerializeField]
        public float x;

        private float speed;
        private Vector3 footIk_offset;

        public Transform Target;
        public Vector3 Offset;

        Transform chest;
        public static bool active;

        private NavMeshAgent nav;
        private Rigidbody rigid;

        Transform spine;
        Transform foot;
        public Vector3 currentEulerAngles;
        public Quaternion currentRotation;
        public float rotationSpeed;
        public Transform aim;
        public Vector3 aimDirection;

        void Start()
        {
            rigid = GetComponent<Rigidbody>();
            nav = GetComponent<NavMeshAgent>();
            anim = GetComponent<Animator>();
            // chest = anim.GetBoneTransform(HumanBodyBones.Neck);
            m_CurrentCLipInfo = anim.GetCurrentAnimatorClipInfo(0);
            m_ClipName = m_CurrentCLipInfo[0].clip.name;
            speed = anim.GetFloat("Vertical");

            spine = anim.GetBoneTransform(HumanBodyBones.UpperChest);
            foot = anim.GetBoneTransform(HumanBodyBones.LeftFoot);



        }
        private void LateUpdate()
        {
            // string g= Vector3.Normalize(transform.position).ToString();
            //Debug.Log(" normalize  " + g);

           // aimDirection = aim.transform.position - this.transform.position;
           // float sqrLen = aimDirection.sqrMagnitude;
           // Debug.Log(" aimDirection offset" + sqrLen);
          //  Vector3 offset = new Vector3(30, transform.position.y, transform.position.z);
            //Quaternion rotation = Quaternion.Euler(0, 30, 0);
           // Vector3 currentEulerAngles = new Vector3(-30, 0, 0);



            // spine.eulerAngles = Quaternion.Euler(offset);
            // spine.eulerAngles = currentEulerAngles;


            //Vector3 rotationVector = new Vector3(0, 30, 0);
            // Quaternion rotation = Quaternion.Euler(rotationVector);
            // spine.rotation = rotation;

            //Vector3 rotationFoot = new Vector3(0, 0, 25f);
            //Quaternion rotation1 = Quaternion.Euler(rotationFoot);
            //foot.rotation = rotation1;

            //foot.transform.localEulerAngles = currentEulerAngles;
            //foot.localEulerAngles = new Vector3(-30, 0, 0);
        }
        void Update()
        {

            //modifying the Vector3, based on input multiplied by speed and time
            // currentEulerAngles += new Vector3(x, y, z) * Time.deltaTime * rotationSpeed;

            //moving the value of the Vector3 into Quanternion.eulerAngle format
            // currentRotation.eulerAngles = currentEulerAngles;

            //apply the Quaternion.eulerAngles change to the gameObject
            // transform.rotation = currentRotation;
            //Debug.Log(" m_ClipName " + m_ClipName);

            //Vector3 L_foot = anim.GetBoneTransform(HumanBodyBones.LeftFoot).position;
            // Debug.Log(anim.GetFloat("Vertical"));
            //Debug.Log(" nav " + nav.speed); ;
            // Debug.Log("rigid " + rigid.velocity.x);

            //Vector3 f = transform.TransformDirection(Vector3.forward);
            //  Debug.Log("speed " + Mathf.CeilToInt(anim.GetFloat("Vertical")));
        }


        void OnTriggerStay(Collider other)
        {

            // 0.9996755f;
            if (other.gameObject.name == "RampSmall" && Mathf.CeilToInt(anim.GetFloat("Vertical")) < 0.0f)//&& speed < 0.0f
            {
                Debug.Log(other.gameObject.name);
                active = true;

            }
            else
            {
                active = false;
            }
            if (other.gameObject.name == "RampSmall" && speed >= 0.9523675f && m_ClipName != "Idle")
            {

                // active = false;
                // Debug.Log(active);
            }
            if (other.gameObject.name != "RampSmall")
            {
                // Debug.Log(other.gameObject.name);
                // active = false;

            }
        }
        void OnAnimatorIK()
        {
            // anim.GetBoneTransform(HumanBodyBones.Neck).position;

            //anim.SetIKPositionWeight(AvatarIKGoal.ne, 1f);
            // anim.SetIKPosition(AvatarIKGoal.LeftFoot, L_foot);

            Vector3 neck = anim.GetBoneTransform(HumanBodyBones.Neck).position;
            //anim.SetIKPosition(neck, 1f);

            if (active == true)
            {
                Vector3 L_foot = anim.GetBoneTransform(HumanBodyBones.LeftFoot).position;
                Vector3 R_foot = anim.GetBoneTransform(HumanBodyBones.RightFoot).position;

                L_foot = GetHitPoint(L_foot + Vector3.up, L_foot - Vector3.up * 5) + footIk_offset;
                R_foot = GetHitPoint(L_foot + Vector3.up, R_foot - Vector3.up * 5) + footIk_offset;

                anim.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1f);
                anim.SetIKPosition(AvatarIKGoal.LeftFoot, L_foot);


                anim.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1f);
                anim.SetIKPosition(AvatarIKGoal.RightFoot, R_foot);
                // UnityEngine.Vector3 p_leftfoot = anim.GetBoneTransform(HumanBodyBones.LeftFoot).position;
                // anim.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1f);
                //anim.SetIKPosition(AvatarIKGoal.LeftFoot, new UnityEngine.Vector3(p_leftfoot.x -x, p_leftfoot.y - y, p_leftfoot.z -z));
            }
            if (active == false)
            {
                Vector3 L_foot = anim.GetBoneTransform(HumanBodyBones.LeftFoot).position;
                //  Vector3 R_foot = anim.GetBoneTransform(HumanBodyBones.RightFoot).position;

                //  L_foot = GetHitPoint(L_foot + Vector3.up, L_foot - Vector3.up * 5) + footIk_offset;
                //  R_foot = GetHitPoint(L_foot + Vector3.up, R_foot - Vector3.up * 5) + footIk_offset;

                anim.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 0f);
                anim.SetIKPosition(AvatarIKGoal.LeftFoot, this.transform.position);


                anim.SetIKPositionWeight(AvatarIKGoal.RightFoot, 0f);
                anim.SetIKPosition(AvatarIKGoal.RightFoot, this.transform.position);
                // UnityEngine.Vector3 p_leftfoot = anim.GetBoneTransform(HumanBodyBones.LeftFoot).position;
                // anim.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1f);
                //anim.SetIKPosition(AvatarIKGoal.LeftFoot, new UnityEngine.Vector3(p_leftfoot.x -x, p_leftfoot.y - y, p_leftfoot.z -z));
            }

        }

        private Vector3 GetHitPoint(Vector3 start, Vector3 end)
        {
            RaycastHit hit;
            if (Physics.Linecast(start, end, out hit))
            {
                return hit.point;
            }
            return end;
        }
    }


}