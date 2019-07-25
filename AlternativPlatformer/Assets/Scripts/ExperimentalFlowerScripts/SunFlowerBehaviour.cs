using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunFlowerBehaviour : FlowerBehaviourParent {

    public Animator animator;
    int randomInt;

    public override void Dance()
    {
        Debug.Log("child has been made to dance, too!");
        animator.SetBool("peeFlag", true);

    }

    private void Update()
    {
        randomInt = Random.Range(1, 10);
        animator.SetInteger("randomInt", randomInt);
    }
}
