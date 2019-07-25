using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//INFO: This script sits in Kyllings bum and makes stuff happen when he pees on things. It has the player
//and the player_score script. Detects which specific flower the bum is next to and makes it dance and makes
//it give points. The dance and give points method is defined in the flower parent script.
//How many points are added is defined from Kylling's bum. It should be changed to being defined from the thing
//which is being peed on itself. Could make an interface of peeables.

public class Player_ObjectInteraction : MonoBehaviour {


    public Animator animator;
    public Player_Movement playerMovement;
    private GameObject currentFlower;
    bool isGrounded;
    bool canPeeOn = false;




    // Update is called once per frame
    void Update () {

        if (playerMovement.isGrounded == true)
        {
            isGrounded = true;
        }
        else isGrounded = false;

        if (Input.GetButtonDown("Pee") && isGrounded == true)
        {
            Debug.Log("Pee btn pressed");
            Pee();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "flower")
        {
            Debug.Log("Player noticed flower!");
            canPeeOn = true;
            currentFlower = collision.gameObject;

        }

    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "flower")
        {
            Debug.Log("Player decided to leave flower");
            canPeeOn = false;
        }
    }

    void Pee()
    {

        animator.SetTrigger("Pee");

        if (canPeeOn == true)
        {
            currentFlower.SendMessage("GivePlayerPoints", 15);
            //probably calling "Dance" from here is bad practice, since it's gonna keep calling it after it has
            //already been triggered. The right way to do it is probably to make one dance/point method from
            //the receiving object and set a flag here.
            currentFlower.SendMessage("Dance");
        }
    }
}
