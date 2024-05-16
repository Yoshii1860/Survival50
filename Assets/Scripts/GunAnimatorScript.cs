using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class GunAnimatorScript : MonoBehaviour
{
    RigidbodyFirstPersonController fpc;
    Animator am;
    Vector3 localPos;

    void Awake() 
    {
        localPos = transform.localPosition;
    }

    void Start() 
    {
        fpc = FindObjectOfType<RigidbodyFirstPersonController>();
        am = GetComponent<Animator>();
    }

    void Update()
    {
        Debug.Log(!Input.anyKey);
        if(fpc.Running && fpc.Grounded)
        {
            am.SetTrigger("Run");
            am.ResetTrigger("Move");
            am.ResetTrigger("Idle");
            am.ResetTrigger("Jump");
        }
        else if(!fpc.Running && fpc.Grounded)
        {
            am.SetTrigger("Move");
            am.ResetTrigger("Run");
            am.ResetTrigger("Idle");
            am.ResetTrigger("Jump");
        }
        else if(fpc.Jumping)
        {
            am.SetTrigger("Jump");
            am.ResetTrigger("Move");
            am.ResetTrigger("Run");
            am.ResetTrigger("Idle");
        }
        else if(!Input.anyKey && fpc.Grounded)
        {
            am.SetTrigger("Idle");
            am.ResetTrigger("Move");
            am.ResetTrigger("Run");
            am.ResetTrigger("Jump");
        }
        else
        {
            am.SetTrigger("Idle");
            am.ResetTrigger("Move");
            am.ResetTrigger("Run");
            am.ResetTrigger("Jump");
        }
    }
}