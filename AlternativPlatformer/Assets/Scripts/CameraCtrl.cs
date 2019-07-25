using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script needs to refer to the player and be part of the camera object.

public class CameraCtrl : MonoBehaviour {

    private GameObject player;
    public float xMin;
    public float xMax;
    public float yMin;
    public float yMax;



	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void LateUpdate () {

        float x = Mathf.Clamp(player.transform.position.x, xMin, xMax);
        float y = Mathf.Clamp(player.transform.position.y, yMin, yMax);

        gameObject.transform.position = new Vector3(x, y, gameObject.transform.position.z);

    }
}
