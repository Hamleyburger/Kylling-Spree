using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Move : MonoBehaviour {


    public int enemySpeed = 10;
    private bool facingRight = true;
    //public int enemyJumpPower = 1250;
    public int xAxisMove;
    //public bool isGrounded;
    public Animator enemyAnimator;
    public float rayHitDistance = 0.7f;


	
	// Update is called once per frame
	void Update () {

        int enemyMask = ~LayerMask.GetMask("Enemy");

        // int layerMask = ~(LayerMask.GetMask("Player"));
        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(xAxisMove, 0), Mathf.Infinity, enemyMask);

        //this part is just for looking at the raycast.
        Debug.DrawRay(transform.position, new Vector2(xAxisMove, 0), Color.yellow, rayHitDistance);
        //Raycast looking is over

        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(xAxisMove, 0) * enemySpeed;
        if(hit.distance < rayHitDistance)
        {
            Flip();
        }


    }

    private void Flip()
    {

        facingRight = !facingRight;
        Vector2 localScale = gameObject.transform.localScale;
        ///localScale skal have den modsatte negativ/positiv end den har
        localScale.x *= -1;
        // retter gameobjekt til efter de nye oplysninger (baseret på gameobjektet)
        transform.localScale = localScale;



        if (xAxisMove <= 0)
            xAxisMove = 1;
        else xAxisMove = -1;
    }
}
