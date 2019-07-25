using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartFlowerBehaviour : FlowerBehaviourParent
{



    //must have public "custom" animator (drag and drop) and can have its own random int (uncomment).
    public Animator animator;
    private int randomInt;

    public override void Dance()
    {
        Debug.Log("child has been made to dance, too!");
        animator.SetBool("peeFlag", true);

    }

    //making its own random int in update
    private void Update () {
        randomInt = Random.Range(1, 10);
        animator.SetInteger("randomInt", randomInt);

    }

}
