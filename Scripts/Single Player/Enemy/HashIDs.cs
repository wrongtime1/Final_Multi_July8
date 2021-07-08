using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HashIDs : MonoBehaviour
{
    public int dyingState;
    public int deadBool;
    public int locomotionState;
    public int speedFloat;
    public int playerInSIghtBool;
    public int shotFloat;
    public int aimWeightFloat;
    public int aimWeightFloat1;
    public int angularSpeedFloat;
    public int openBool;

    [Header("Injury")]
    public int injury1;
    public int injury2;
    public int injury3;
    public int injury1B;
    // Start is called before the first frame update

    public static HashIDs instance;

    void Awake()
    {
        if(instance != null)
        {
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            instance = this;
        }

           
    }
    void Start()
    {
        //dyingState = Animator.StringToHash('SinglePlayerLayer');
        //deadBool = Animator.StringToHash('');
        //openBool = Animator.StringToHash('');
        locomotionState = Animator.StringToHash("SinglePlayerLayer.Locomotion");
        speedFloat = Animator.StringToHash("Speed");
        shotFloat = Animator.StringToHash("ShotSing");
        aimWeightFloat = Animator.StringToHash("AimWeightSing");
        aimWeightFloat1 = Animator.StringToHash("AimWeightFromSingleEnemy");
        angularSpeedFloat= Animator.StringToHash("AngularSpeed");
        playerInSIghtBool = Animator.StringToHash("playerInSIghtBool");

        injury1 = Animator.StringToHash("IsInjured1");
        injury2 = Animator.StringToHash("Injury.IsInjured2");
        injury3 = Animator.StringToHash("Injury.IsInjured3");
        injury1B = Animator.StringToHash("InInjured1B");
    }


}
