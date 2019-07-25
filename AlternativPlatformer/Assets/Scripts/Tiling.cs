using System.Collections;
using System.Collections.Generic;
using UnityEngine;


////TODO drawray skifter z position. Dette er uden for terapeutisk rækkevidde. Slet og brug enkelt billede i stedet.
/// desuden er der en custom Bounds /myTotalBounds som  bare er en x bounds baseret
/// på min og max. Har fundet dens center ved at lægge halvdelen af max til min.

[RequireComponent (typeof(SpriteRenderer))]

public class Tiling : MonoBehaviour {

    public int offsetX = 2; // offset so obj is instantiated before it comes into view

    //for checking if wee need to instantiate a buddy (left or right copy)
    public bool hasARightBuddy = false;
    public bool hasALeftBuddy = false;

    public bool reverseScale = false; //in case the buddy needs to be flipped

    private float spriteWidth = 0f; //width of sprite. Bracket's version, but prob. wouldn't work here.
    private float totalWidth = 0f; //har selv tilføjet til at finde ud af vidde med børn. Definieres i Start()
    private Bounds myTotalBounds; //ikke sikker hvad jeg skal bruge den til, men definerer center og størrelse ud fra totalWidth.
    //bounds kan bruges til at finde min og max

    private Camera cam; //to refer to the camera

    private Transform myTransform; //to refer to current position


    private void Awake()
    {
        cam = Camera.main;
        myTransform = transform;
    }

    // Use this for initialization
    void Start () {
    
        SpriteRenderer sRenderer = GetComponent<SpriteRenderer>();
        //spriteWidth = sRenderer.sprite.bounds.size.x; //det er denne er width, vi skal lave om på

        findWidth();

        Debug.Log("position X: " + transform.position.x);




    }
	
	// Update is called once per frame
	void Update () {
        Debug.Log("position X before find width: " + transform.position.x);
        findWidth();
        Debug.Log("position X: after find width" + transform.position.x);
        //Denne linje er tegnet for at demonstrere hvad der opfattes som repeatableObject's vidde.
        //Debug.DrawRay(new Vector3(GetComponent<SpriteRenderer>().sprite.bounds.min.x, transform.position.y, transform.position.z), new Vector3(spriteWidth, transform.position.y, transform.position.z), Color.black);
        Debug.DrawRay(new Vector3(myTotalBounds.min.x, 0, transform.position.z), new Vector3(myTotalBounds.max.x, 0, transform.position.z), Color.red);
        Debug.Log("position X after drawray: " + transform.position.x);

    }


    void findWidth()
    {

        ///Her skal gøres noget, så der refereres til spriterenderers i children of findes min og max
        //her får jeg en array af alle renderers. Skal finde min og max.
        SpriteRenderer[] renderers = GetComponentsInChildren<SpriteRenderer>();

        //sætter en min og max variaben til at udregne bounds med foreach loop
        //sætter min til this.position i mangel af bedre
        float xMin = 0;
        float xMax = 0;

        bool flag = false;

        foreach (SpriteRenderer renderer in renderers)
        {
            if (flag == false)
            {
                xMin = renderer.bounds.min.x;
                xMax = renderer.bounds.max.x;
               
                flag = true;
            }
            float currentMin = renderer.bounds.min.x;
            if (currentMin < xMin) xMin = currentMin;

            float currentMax = renderer.bounds.max.x;
            if (currentMax > xMax) xMax = currentMax;
            //Sådan, nu er xMin det laveste og xMax det højeste punkt i alle repeatableObj's
            //sprite renderers
        }

        totalWidth = xMax - xMin;

        myTotalBounds = new Bounds(new Vector3(xMin + (xMax * 0.5f), 0, 0), new Vector3(totalWidth, 0, 0));
    }
}
