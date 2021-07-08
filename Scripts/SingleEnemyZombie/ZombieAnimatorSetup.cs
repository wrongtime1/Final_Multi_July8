using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAnimatorSetup 
{
    // Start is called before the first frame update

    public float speedDampTime = 0.1f;
    public float angularSpeedDampTime = 0.7f;
    public float angleResponseTime = 0.6f;

    private Animator anim;
    private HashIDs hash;
    
    public ZombieAnimatorSetup(Animator animator, HashIDs hashIDs)
    {
        anim = animator;
        hash = hashIDs;       
    }

    public void Setup(float speed, float angle)
    {
        float angularSpeed = angle / angleResponseTime;
        anim.SetFloat(hash.speedFloat,speed, speedDampTime, Time.deltaTime);
        anim.SetFloat(hash.angularSpeedFloat, angularSpeed, angularSpeedDampTime, Time.deltaTime);
    
    }






}
