using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parallaxing : MonoBehaviour {

    public Transform[] backgrounds;
    private float[] parallaxScales;
    public float smoothing = 1f;
    private Transform cam;
    private Vector3 previousCamPos;



    private void Awake()
    {
        cam = Camera.main.transform;
    }


    // Use this for initialization
    void Start () {
        previousCamPos = cam.position;
        parallaxScales = new float[backgrounds.Length];

        for(int i = 0; i < backgrounds.Length; i++)
        {
            parallaxScales[i] = backgrounds[i].position.z*-1;
        }
    }
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < backgrounds.Length; i++)
        {
            //make parallax oposite of camera
            float parallax = (previousCamPos.x - cam.position.x) * parallaxScales[i];
            //make target x based on parallax
            float backgroundTargetPosX = backgrounds[i].position.x + parallax;
            //make target position (transform = vector 3) based on target pos x
            Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);
            //make background actually go to target position (fading using "Lerp")
            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);

        }
        //update previousCamPosition by the end of the frame (end of Update)
        previousCamPos = cam.position;
    }
}
