using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Health : MonoBehaviour {

    public bool hasDied;
    public int health;

	// Use this for initialization
	void Start () {

        hasDied = false;
        health = 10;

		
	}
	
	// Update is called once per frame
	void Update () {
        if (gameObject.transform.position.y < -10)
        {
            Die();
        }


    }

    void Die(){
        hasDied = true;
        SceneManager.LoadScene("Main");
    }

    public void LoseHealth(int damage)
    {
        health -= damage;

        if(health < 0)
        {
            Die();
        }
    }

}
