using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = System.Random;

public class StabilizerCubeBehaviour : MonoBehaviour
{
    public Animator animator;
    public List<GameObject> possibleFillins = new List<GameObject>();
    public Grid gridToInstantiateOn;

    public bool activated;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

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
            pickNumber();
        }
    }

    public void placeTilemap(int rolled)
    {
        rolled -= 1;
        GameObject fillinToInstantiate = possibleFillins[rolled];
        GameObject instantiated = Instantiate(fillinToInstantiate, gridToInstantiateOn.transform, true);
    }

    void pickNumber()
    {
        Random rnd = new Random();
        int roll = rnd.Next(1, 7);
        DiceBehaviour.PlayersDiceBehaviour.showNumber(roll);
        this.activated = true;
        placeTilemap(roll);

    }

    private void Awake()
    {
    }
}
