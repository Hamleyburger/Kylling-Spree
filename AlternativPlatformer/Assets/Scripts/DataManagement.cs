using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//These name spaces are added for the purpose of saving and encrypting, so anyone can't go and change things. 
//No idea how it works though;
//high score . close skrives uden parenteser i videoen (video 7, ca. 27 minutter inde) Hvordan? Er det det, man skal?
//Men jeg får error hvis jeg prøver.

using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class DataManagement : MonoBehaviour {

    public static DataManagement datamanagement;
    public int highScore;

    private void Awake()
    {
        if (datamanagement == null)
        {
            DontDestroyOnLoad(gameObject);
            datamanagement = this;
        }
        else if (datamanagement != this)
        {
            Destroy(gameObject);
        }
    }


    public void SaveData()
    {
        BinaryFormatter BinForm = new BinaryFormatter(); //creates binformatter
        FileStream file = File.Create(Application.persistentDataPath + "/gameInfo.dat"); //creates file
        gameData data = new gameData(); //creates container for data
        data.highscore = highScore; //man kan putte en high score ind i den, fordi den klasse har en plads (variabel)
        //til high score
        BinForm.Serialize(file, data);
        file.Close();

    }

    public void LoadData()
    {
        if(File.Exists(Application.persistentDataPath + "/gameInfo.dat"))
        {
            BinaryFormatter BinForm = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gameInfo.dat", FileMode.Open);
            gameData data = (gameData)BinForm.Deserialize(file);
            file.Close(); //hvorfor skriver han uden parentes i videoen?
            highScore = data.highscore;
        }
    }

}

[Serializable]
class gameData
{
    public int highscore;
}