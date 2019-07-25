using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//TODO: 
//Notes: pacified comes in at different points to disable functions while dead,
//before object has been destroyed (while playing the dying animation)

public class betterEnemy_move : MonoBehaviour {


    public float speed;
    //this particular layerMask is set to hit certain layers. Can it act differently depending what layer is hit?
    public LayerMask enemyMask;
    Rigidbody2D myBody;
    Transform myTransform;
    private float myWidth;
    public float lineCastDistanceGround = 0.2f;
    public float lineCastDistanceBlocked = 0.2f;
    public float rayHitDistance = 1f;
    bool pacified = false;
    public Animator animator;
    private Vector2 lineCastStart;


	// Use this for initialization
	void Start () {

        myBody = this.GetComponent<Rigidbody2D>();
        myTransform = this.transform;
        myWidth = this.GetComponent<SpriteRenderer>().bounds.extents.x;

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {
        //setting linecast and drawing debug
        lineCastStart = myTransform.position + myTransform.right * myWidth + myTransform.up * 0.5f;
        //now basing new parametres on linecast (isgrounded, isblocked)
        //The "isGrounded" must be false on reaction since it reacts to NOT hitting ground)
        bool IsGrounded = Physics2D.Linecast(lineCastStart, lineCastStart + Vector2.down * lineCastDistanceGround, enemyMask);
        Debug.DrawLine(lineCastStart, lineCastStart + Vector2.down * lineCastDistanceGround, Color.magenta);
        //The "isBlocked" must be true on reaction since it reacts to HITTINH something.
        bool IsBlocked = Physics2D.Linecast(lineCastStart, lineCastStart + new Vector2(myTransform.right.x, myTransform.right.y) * lineCastDistanceBlocked, enemyMask);
        Debug.DrawLine(lineCastStart, lineCastStart + new Vector2(myTransform.right.x, myTransform.right.y) * lineCastDistanceBlocked, Color.blue);

        RaycastHit2D hit = Physics2D.Raycast(myTransform.position + (myTransform.up * 0.5f), new Vector2(myTransform.right.x, 0), Mathf.Infinity, enemyMask);
        Debug.DrawRay(myTransform.position + (myTransform.up * 0.5f), new Vector2(myTransform.right.x, 0) * rayHitDistance, Color.black);

        if((hit.collider != null) && (hit.distance < rayHitDistance && pacified == false))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                Debug.Log("Enemy hit player!");
                Destroy(hit.collider.gameObject);
            }

                Debug.Log("Hit is hitting " + hit.collider.name);
           // if (hit.collider.gameObject.tag == null) return;
        }

    
        if (!IsGrounded || IsBlocked)
        {
            if (!pacified)
            {
                Vector3 currentRotation = myTransform.eulerAngles;
                currentRotation.y += 180;
                myTransform.eulerAngles = currentRotation;

            }
        }



        //Always Move Forward
        if(myBody != null)
        {
            Vector2 myVelocity = myBody.velocity;
            myVelocity.x = myTransform.right.x * speed;
            myBody.velocity = myVelocity;
        }
        

        if (animator.GetCurrentAnimatorStateInfo(0).IsTag("AppleDeath"))
        {
            
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9)
            {
                Destroy(gameObject);
            }
            
        }

    }


    public void Die()
    {
        Destroy(myBody);
        Debug.DrawLine(lineCastStart, lineCastStart + Vector2.down * lineCastDistanceGround, Color.red);
        pacified = true;
        animator.SetBool("Death", true);


    }

}
