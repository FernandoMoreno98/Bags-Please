using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationReponedor : MonoBehaviour
{
    CharacterController cc;
    Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if ("")
        {
            anim.SetInteger("condition", 1);
        }
        else if ("")
        {
            anim.SetInteger("condition", 2);
        }
        else
        {
            anim.SetInteger("condition", 0);
        }
    }
}
