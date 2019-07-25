using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player_Score : MonoBehaviour {

    public float timeleft = 120;
    public int playerScore;
    /*Her er lavet nogle game object variable som public, som skal være de UI elementer, der skal behandle
     * time left og score. Der er altså en timeleft og en tilsvarende timeleftUI. 
     * Når man laver en public var kan trække noget ind i det/redigere det(alt efter type) ude fra Unity.
     * Det skal man gøre for at definere hvilket gameObjekt, det er. Jeg har trukket
     * UI objekterne (som er lavet ude i Unity som nye game objects) ind i dem. Fordi de er UI objekter har de et komponent der hedder
     * tekst, som man kan hente. Se i update hvordan det bliver brugt*/
    public GameObject timeLeftUI;
    public GameObject playerScoreUI;
    //bool flag bare til at sørge for at stageClear kun udføres én gang. Bruges i update i trigger.


    // Use this for initialization

    //private void Start()
    //{
    //    DataManagement.datamanagement.LoadData();
    //}


    // Update is called once per frame
    void Update () {
        timeleft -= Time.deltaTime;

        /*herunder indstiller jeg de gameObjects, jeg har puttet ind i variablene
         * til at skrive score og tid i deres tekstkomponenter. Timeleft er casted til
         * en int (ved at skrive "(int)" foran, så det vises uden decimaler. Det er
         * altså ikke nødvendigt, men ser pænere ud.*/
        timeLeftUI.gameObject.GetComponent<Text>().text = ("Time left: " + (int)timeleft);
        playerScoreUI.gameObject.GetComponent<Text>().text = ("Score: " + playerScore);

        if (timeleft < 0.1)
        {
            SceneManager.LoadScene("Main");
        }
        

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "collectible")
        {
            Debug.Log("Touching collectible");
            playerScore += 10;
            Destroy(collision.gameObject);
        }

        /*Kode med flag for at sørge for at det kun sker en gang, til at regne
         * score ud ved end stage*/
        if (collision.gameObject.tag == "endStage") {
            Debug.Log("Player is touching endStage. Counting score");
            CountScore();
            Debug.Log("Player Score = " + playerScore);
            //Destroy(collision.gameObject);
            DataManagement.datamanagement.SaveData();
        }
    }

    public void GivePlayerPoints(int points)
    {
        playerScore += points;
    }

    void CountScore()
    {
        Debug.Log("changing highscore from " + DataManagement.datamanagement.highScore + " to ");
        playerScore = playerScore + (int) timeleft;
        DataManagement.datamanagement.highScore = playerScore; //i videoen vil han igen gange det med timeleft osv
        Debug.Log(DataManagement.datamanagement.highScore);
        

    }
}
