using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO at some point you'll probably want the flower to stop dancing. Now it's dancing for all eternity.

public class FlowerBehaviour : MonoBehaviour {


    public Animator animator;
    int randomInt;

	// Use this for initialization
	void Start () {
		
	}
	
	//Update is called once per frame
	void Update () {
        randomInt = Random.Range(1, 10);
        animator.SetInteger("randomInt", randomInt);

	}

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerBackside")
        {
            Debug.Log("Flower trigger has been triggered by player's backside");
        }

    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerBackside")
        {
            Debug.Log("Player left with back side");
        }
    }

    public void Dance()
    {
        Debug.Log("Flower is dancing");
        animator.SetBool("peeFlag", true);
    }



}
