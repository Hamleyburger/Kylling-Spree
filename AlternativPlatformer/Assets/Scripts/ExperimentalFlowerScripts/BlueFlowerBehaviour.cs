using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueFlowerBehaviour : FlowerBehaviourParent {

    public Animator animator;
    //int randomInt;

    public override void Dance()
    {
        Debug.Log("child has been made to dance, too!");
        animator.SetBool("peeFlag", true);

    }

    //private void Update() {}
}