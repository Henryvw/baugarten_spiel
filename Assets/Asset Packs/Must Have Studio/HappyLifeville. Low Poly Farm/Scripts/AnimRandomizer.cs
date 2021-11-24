using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimRandomizer : MonoBehaviour
{
    Animator anim;
    public string[] animationNames;
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.Play(animationNames[Random.Range(0,animationNames.Length)],0,Random.Range(0f,0.3f));
    }

}
