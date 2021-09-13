using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private float distanceAway;
    [SerializeField]
    private float distanceUp;
    [SerializeField]
    private float smooth;
    [SerializeField]
    public Transform follow;
    private Vector3 targetPosition;
    public Vector3 cameraOFfset;
    Scene d;
    string sceneName;
     public LayerMask mask;

    public SphereCollider spherCollider;
    private bool contact;

    private bool nonContact;
    private Vector3 localpositionV;
    private Vector3 gameoBjectLo;
    // Start is called before the first frame update
    void Start()
    {
        //camera = GameObject.FindGameObjectWithTag("MainCamera");
       
        mask =LayerMask.GetMask("World");
        Scene d = SceneManager.GetActiveScene();
        sceneName = d.name;
        //transform.position.x = Vector3.Lerp(transform.position, cameraOFfset, Time.deltaTime * smooth);

        localpositionV = this.transform.localPosition;


    }

    // Update is called once per frame
    
    
    private void COmpenstateForWalls(Vector3 fromObject, ref Vector3 toTarget)
    {
      
      
        Debug.DrawLine(fromObject, toTarget, Color.white);
        RaycastHit wallHits = new RaycastHit();
        if(Physics.Linecast(fromObject, toTarget, out wallHits, mask))
        {
          //  Debug.DrawRay(wallHits.point, Vector3.left, Color.black);
           // Debug.Log(wallHits.collider.name);
            toTarget = new Vector3(wallHits.point.x, toTarget.y, wallHits.point.z);
        }
    }

    void LateUpdate()
    {
       // Debug.Log("position " + transform.position);
       // Debug.Log("local " + transform.localPosition);
        if (contact)
        {
            transform.position = new Vector3(0.0f, 0.0f, 0.0f);
            //RaycastHit hit;
            //if (Physics.Raycast(transform.position, gameoBjectLo, out hit, mask))
            //{
            //    //Debug.Log("hit " + hit.point);
            //    Debug.Log("hit " + hit.collider.name);
            //    Debug.DrawRay(transform.position, gameoBjectLo * hit.distance, Color.yellow);
            //    Debug.Log(" hit.distance " + hit.distance);
            //    transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
            //}

        }
        else
        {
           
        }
        // Vector3 characterOffset = follow.position + new Vector3(1f, distanceUp, 0f);

      
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "World")
        {
            contact = true;

           // gameoBjectLo = other.gameObject.transform.position;

        }
       
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "World")
        {
            Debug.Log("exit " + localpositionV);
            transform.localPosition = localpositionV;

        }
    }
}


