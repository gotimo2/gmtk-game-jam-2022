using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = System.Random;

public class StabilizerCubeBehaviour : MonoBehaviour
{
    public Animator animator;
    public List<Tilemap> possibleFillins = new List<Tilemap>();

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
        if (activated)
        {
            return;
        }
        if (collision2D.gameObject.CompareTag("Player"))
        {
            animator.Play("Turning_anim");
            Invoke("pickNumber", 1F);
        }
    }

    void pickNumber()
    {
        Random rnd = new Random();
        int roll = rnd.Next(1, 7);
        DiceBehaviour.PlayersDiceBehaviour.showNumber(roll);
        this.activated = true;

    }

    private void Awake()
    {
    }
}
