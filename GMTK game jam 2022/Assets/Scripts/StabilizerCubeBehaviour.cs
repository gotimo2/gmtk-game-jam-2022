using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StabilizerCubeBehaviour : MonoBehaviour
{
    public Animator animator;

    public bool activated;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (activated &&  !animator.GetCurrentAnimatorStateInfo(0).IsName("Turning_anim"))
        {
            animator.Play("Restabled_anim");
        }
        
    }

    public void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (collision2D.gameObject.CompareTag("Player"))
        {
            animator.Play("Turning_anim");
        }
    }

    private void Awake()
    {
    }
}
