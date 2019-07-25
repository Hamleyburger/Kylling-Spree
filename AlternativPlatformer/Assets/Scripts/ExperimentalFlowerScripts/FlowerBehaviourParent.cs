using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO at some point you'll probably want the flower to stop dancing. Now it's dancing for all eternity.


public class FlowerBehaviourParent : MonoBehaviour {


    //public Animator animator;
    int randomInt;
    GameObject player;
    Player_Score playerScore;
    bool canGivePoints = true;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        playerScore = player.GetComponent<Player_Score>();

    }
	
	//Update is called once per frame
	void Update () {
	}

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerBackside")
        {
            Debug.Log("parent flower is being noticed by player");
        }

    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerBackside")
        {
            Debug.Log("parent flower is being left by player");
        }
    }

    public virtual void Dance()
    {
        Debug.Log("parent flower is dancing");
        //animator.SetBool("peeFlag", true);
    }

    // the number of points is currently being passed from dog's behind.
    //Should probably be defined in flower child class to make individual point values possible.
    public virtual void GivePlayerPoints(int points)
    {
        if (canGivePoints == true)
        {
            playerScore.GivePlayerPoints(points);
            canGivePoints = false;
        }

    }

    public virtual bool AlreadyHasPee(bool yes)
    {
        return yes;
    }



}
