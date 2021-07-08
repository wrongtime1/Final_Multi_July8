using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimSetupBoth : MonoBehaviour
{
    public float speedDampTime=0.1f;
    public float angularSpeedDampTime=0.7f;
    public float angleResponseTime = 0.6f;
    // Start is called before the first frame update


    private Animator anim;
    private HashIDs hash;

    public AnimSetupBoth(Animator animator, HashIDs hashID)
    {
        anim = animator;
        hash = hashID;
    }

    public void SetUp(float speed, float angle)
    {
        float angularSPeed = angle / angleResponseTime;
        anim.SetFloat(hash.speedFloat, speed, speedDampTime, Time.deltaTime);
        anim.SetFloat(hash.angularSpeedFloat, angularSPeed, angularSpeedDampTime, Time.deltaTime);

    }

}
