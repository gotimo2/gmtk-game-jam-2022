using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceBehaviour : MonoBehaviour
{

    public static DiceBehaviour PlayersDiceBehaviour;

    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        PlayersDiceBehaviour = this;
    }

    public void showNumber(int number)
    {
        if (number < 1 || number > 6) {Debug.Log("dice number out of bounds");}
        animator.Play("Dice_" + number);
    }

    public void Update()
    {
        
        
    }
}
