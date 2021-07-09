using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;
using Photon.Pun;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(RigidbodyFirstPersonController))]
public class PlayerMoveSingle :  MonoBehaviourPunCallbacks
{
    [Header("UI ELements")]
    [SerializeField]
    public Joystick joystick;

    [SerializeField]
    public RigidbodyFirstPersonController rigidBodyController;

    [SerializeField]
    public FixedTouchField fixedTouchField;

    [SerializeField]
    private float lookSensitivity = 3;
    //Git  hub
    //public Button fireButn;

   
    private Animator animator;

    private AnimatorClipInfo[] m_CurrentClipInfo;
    private string m_ClipName;

    private Transform position1;
    public GameObject FPsCamera;
    private int layerMask = 1 << 9;

    public GameObject hitEffectPrefa;

    public bool detect;

    private AudioSource sm;

    
    
   

    [Header("Health Related Stuff")]
    public float startHealth = 100f;

    private float health;
    public Image healthBar;

    public float shootForce;
    private GameObject clone;

    public bool jump;

    [Header("Button UI")]
    private Vector3 rotation = Vector3.zero;

    public static PlayerMoveSingle instance;
    [HideInInspector]
    public RaycastHit hit;
    private LayerMask layermask;

    public GameObject raycaster;
    private Rigidbody rb;
    private float verticalVelocity;
   
   
    [Range(0, 1f)]
    public float DistanceToGround;
    public LayerMask layerMask1;
    public bool preventJump;
    private AudioSource audio;

    public GameObject playerUI;

    [HideInInspector]
    public PhotonView photonView;
    Scene scene;
    public void Awake()
    {

        //rigidBodyController = GameObject.Find("Fixed Joystick");
        int currentIndex = SceneManager.GetActiveScene().buildIndex;

          //SinglePlayerSce1SciFi
            photonView = GetComponent<PhotonView>();
            if (photonView.IsMine)
            {
                //  FPsCamera.enabled = true;
                //enable camera game object
                FPsCamera.SetActive(true);
                playerUI.SetActive(true);
            }
            else if (!photonView.IsMine)
            {
                FPsCamera.SetActive(false);
            playerUI.SetActive(false);
        }
            if (!photonView.IsMine && GetComponent<PlayerMoveSingle>() != null)
            {

               // Destroy(GetComponent<PlayerMoveSingle>());
            }

       // FPsCamera.SetActive(true);
       // playerUI.SetActive(true);
    }
    private void Start()
    {
      
      

        instance = this;
        if (instance != null)
        {
            DontDestroyOnLoad(this.gameObject);
        }

     
            rb = GetComponent<Rigidbody>();
            health = startHealth;
            healthBar.fillAmount = health / startHealth;
            healthBar.fillAmount = health;
            position1 = GetComponent<Transform>();
            animator = GetComponent<Animator>();
            sm = GetComponent<AudioSource>();

            position1 = GetComponent<Transform>();

            rigidBodyController = GetComponent<RigidbodyFirstPersonController>();
            sm = this.GetComponent<AudioSource>();

            float _yrotation = Input.GetAxis("Mouse X");
            Vector3 _rotationVector = new Vector3(0, _yrotation, 0) * lookSensitivity;
            Rotate(_rotationVector);

            audio = GetComponent<AudioSource>();

       
      
       
        }
    

   
     void FixedUpdate()
    {

       

      
            //photonView.RPC("AnimatorV", RpcTarget.All);
            // RpcTarget.All( AnimatorV());
            AnimatorV();
            //AnimatorInfo();

            ////if (Physics.Raycast(this.transform.position, Vector3.down, out hit, layerMask1))
            {
            if ((m_ClipName != "walking backward" || m_ClipName != "injury1" || m_ClipName != "injury3" || m_ClipName != "injury2") && hit.distance > 1.2f) //addinjure
            {
                if (hit.distance > 1.19)  // 0.968807 , 1.188961f
                {
                   

                    //preventJump = true;
                    //rb.AddForce(Vector3.up * 0.7f, ForceMode.Impulse);
                    //animator.SetBool("PlayerJump", true);
                    //transform.position += (transform.forward + Vector3.down) / 17f;

                    //StartCoroutine(JUmpDown());
                }
            }
        }
    }

  

    private void AnimatorInfo()
    {
        m_CurrentClipInfo = this.animator.GetCurrentAnimatorClipInfo(0);
        m_ClipName = m_CurrentClipInfo[0].clip.name;
       // Debug.Log(m_ClipName);
    }



    public IEnumerator JUmpDown()
    {
       // jump = false;
        yield return new WaitForSeconds(0.3f);
        jump = false;
     rb.useGravity = true;
       
        RaycastHit hit;
       // Debug.Log(" hit.first " );
        if (Physics.Raycast(transform.position, Vector3.down, out hit, layerMask1))
        {
            //Debug.Log(" hit.second " );
            //transform.position += transform.forward / 40;
            // rb.AddForce(-Vector3.up + transform.forward * 500f);

            //Debug.Log(" hit.distance " + hit.distance);
            //Debug.DrawLine(transform.position, hit.point, Color.cyan);
            if(hit.distance <= 1.21f)  // 0.968807 , 1.188961f
            {
                preventJump = false;
                jump = false;
              
               
                animator.SetBool("PlayerJump", false);
            }
            
           
        }
    
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag=="Floor")
        {
           // jump = false;
        }
    }
    private void Rotate(Vector3 rotateVector)
    {
       
            if (GetComponent<PhotonView>().IsMine)
            {
                rotation = rotateVector;
            }
        
        else
        {
            rotation = rotateVector;
        }
    }

  
  
    public void AnimatorV()
    {

      
     
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
            if (joystick != null)
            {
                rigidBodyController.joystickInputAxis.x = joystick.Horizontal;
                rigidBodyController.joystickInputAxis.y = joystick.Vertical;

                rigidBodyController.mouseLook.lookInputAxis = fixedTouchField.TouchDist;

                animator.SetFloat("Horizontal", joystick.Horizontal);
                animator.SetFloat("Vertical", joystick.Vertical);
         
        }
    }

    //void OnAnimatorIK()
    //{
    //    if (animator)
    //    {
            
    //        animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1f);
    //        animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 1f);

    //        RaycastHit hit;
    //        Ray ray = new Ray(animator.GetIKPosition(AvatarIKGoal.LeftFoot) + Vector3.up, Vector3.down);
    //            Debug.DrawRay(animator.GetIKPosition(AvatarIKGoal.LeftFoot), Vector3.down + new Vector3(0, DistanceToGround + 1f, 0), Color.blue,30f);

    //        if (Physics.Raycast(animator.GetIKPosition(AvatarIKGoal.LeftFoot), Vector3.down + new Vector3(0, DistanceToGround + 1f, 0), out hit, layerMask1))
    //        {

    //            if (hit.transform.tag=="Walkable")
    //            {
    //                Debug.Log("walkable");
    //                Vector3 footPosition = hit.point;
    //                footPosition.y += DistanceToGround;
    //                animator.SetIKPosition(AvatarIKGoal.LeftFoot, footPosition);
    //            }
    //        }
        
    //    }
    //}
}