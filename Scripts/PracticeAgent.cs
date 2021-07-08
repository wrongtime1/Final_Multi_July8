using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class PracticeAgent : MonoBehaviour
{
    private NavMeshAgent agent;

    private Transform chest;
    private Transform rightWrist;
    private Transform rightArm;
    private Transform originalPosition;
    private Animator anim;
    //public Vector3 offset;

    // Start is called before the first frame update
    public Vector3 c_rot;
    public Vector3 anglesToRotate;

    public class QuaternionRotation : MonoBehaviour
    {

        public Vector3 c_rot;
        public Vector3 anglesToRotate;

        void Start()
        {
            //c_rot = new Vector3 (0f, 90f, 0f);

            Quaternion yRotation = Quaternion.AngleAxis(c_rot.y, Vector3.up);
            Quaternion xRotation = Quaternion.AngleAxis(c_rot.x, Vector3.right);
            Quaternion zRotation = Quaternion.AngleAxis(c_rot.z, Vector3.forward);
            this.transform.rotation = yRotation * xRotation * zRotation;

            //anglesToRotate = new Vector3 (0f, 0f, 90f);
        }
        void Update()
        {
            Quaternion yRotation = Quaternion.AngleAxis(anglesToRotate.y * Time.deltaTime, Vector3.up);
            Quaternion xRotation = Quaternion.AngleAxis(anglesToRotate.x * Time.deltaTime, Vector3.right);
            Quaternion zRotation = Quaternion.AngleAxis(anglesToRotate.z * Time.deltaTime, Vector3.forward);
            this.transform.rotation = yRotation * xRotation * zRotation * this.transform.rotation;

            c_rot += anglesToRotate * Time.deltaTime;
        }
    }
}