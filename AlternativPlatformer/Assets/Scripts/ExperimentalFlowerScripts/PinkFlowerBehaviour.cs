using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinkFlowerBehaviour : FlowerBehaviourParent
{


    //must have public "custom" animator (drag and drop) and can have its own random int (uncomment).
    public Animator animator;
    //int randomInt;

    public override void Dance()
    {
        Debug.Log("child has been made to dance, too!");
        animator.SetBool("peeFlag", true);

    }


    //In case of need for random int, uncomment this Update method
    //void Update () {
    //    randomInt = Random.Range(1, 10);
    //    animator.SetInteger("randomInt", randomInt);

    //}

}
