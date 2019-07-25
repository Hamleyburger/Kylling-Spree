using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script needs to refer to the ridgidbody and be a component in the player.
//Unity's Input is also required for reading input.
//Mission: Apple needs to have gravity, so it can fall down if there is no ground.
//It must NOT, however, be knocked down into the ground when being jumped on. When the
//player has jumped on it and bounced off it once, only the animation of its dying should be left
//and it must not move. It's technically gone, but there is a dying animation. HELP?
//
//Mission: make a ground check raycast instead of collision.

public class Player_Movement : MonoBehaviour {


    public int playerSpeed = 10;
    public int playerJumpPower = 1250;
    private float xAxisMove;
    public bool isGrounded;
    public Animator animator;
    public float rayHitDistance = 1.33f;


    private void Start()
    {
        GetComponent<SpriteRenderer>().flipX = true;
    }


    // Update is called once per frame
    void Update()
    {
        PlayerMove();
        PlayerRayCast();
        animator.SetFloat("Speed", Mathf.Abs(xAxisMove));
    }

    //Jump is NOT inside move. But move may be affected by jump?
    void PlayerMove()
    {
        //Controls (håndtering af input - her får vi en float fra input, som assignes til xAxis variablen.

        //Pee actions/animation triggers/interactions are in the Player_ObjectInteraction script. Here we're 
        //only checking if the pee animation is running, because then the player can't move.
        if (this.animator.GetCurrentAnimatorStateInfo(0).IsName("Kyl_pee"))
        {
            xAxisMove = 0;
        }

        else xAxisMove = Input.GetAxis("Horizontal");

//player direction
        if (xAxisMove < 0.0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (xAxisMove > 0.0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }


        if (Input.GetButtonDown("Jump") && isGrounded == true){
            Jump();
        }

      

        //Physics (bevægelse på x og y / adfærd
        //fortæller at ridgitbody's velocity(x og y) skal være xAxisMove på x og det, den er i forvejen på y.
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(xAxisMove * playerSpeed, gameObject.GetComponent<Rigidbody2D>().velocity.y);
    }

    void Jump()
    {
        //Jumping code
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * playerJumpPower);
            isGrounded = false;
    }




    private void OnCollisionEnter2D(Collision2D col)
    {

        if (col.gameObject.tag == "ground")
        {
            isGrounded = true;
        }
        else isGrounded = false;
    }


    //This raycast was meant for checking whatever the player is landing on. Eventually to detect enemies for bouncing off and killing them.
    public void PlayerRayCast()
    {
        //Ray stops when hitting something
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down);

        //this part is just for looking at the raycast.
        Debug.DrawRay(transform.position, Vector2.down*rayHitDistance);
        //Raycast looking is over

        if (hit.distance < rayHitDistance)
        {
            if (hit.collider == null) return;
            if (hit.collider.gameObject.tag == "Enemy")
            {
                Debug.Log("messed up enemy's hair so far");
                //call Die from touched enemy
                hit.collider.gameObject.GetComponent<betterEnemy_move>().Die();
                //bounce off its body
                GetComponent<Rigidbody2D>().AddForce(Vector2.up * 1000);
                //do not bounce again when landing. This is not working. Commenting out,
                //but keeping for later review.
                //Physics.IgnoreLayerCollision(gameObject.layer, hit.collider.gameObject.layer);
            }
        }
    }
}
