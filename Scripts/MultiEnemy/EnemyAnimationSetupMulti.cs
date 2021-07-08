using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationSetupMulti 
{
    public float speedDampTime = 0.1f;
    public float angularSpeedDampTime = 0.7f;
    public float angleResponseTime = 0.6f;

    private Animator anim;
    private HashIDs hash;

    public EnemyAnimationSetupMulti(Animator animator, HashIDs hashIds)
    {
        anim = animator;
        hash = hashIds;
    }

    public void Setup(float speed, float angle)
    {
        float angularSpeed = angle / angleResponseTime;

        anim.SetFloat(hash.speedFloat, speed, speedDampTime, Time.deltaTime);
        anim.SetFloat(hash.angularSpeedFloat, angularSpeed, angularSpeedDampTime, Time.deltaTime);


    
    }
    // Start is called before the first frame update

}
