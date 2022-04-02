using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int rowNum;
    public int colNum;
    public int matchNum;
    public string cardSkin = "animals_";
    public string backSkin = "back_1";

    public List<int> cardSet;

    private void Awake()
    {
        int sessionCount = FindObjectsOfType<GameManager>().Length;
        if (sessionCount > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else 
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void gameModeSelect(string modeData)
    {
        string[] splitData = modeData.Split(',');
        colNum = int.Parse(splitData[0]);
        rowNum = int.Parse(splitData[1]);
        matchNum = int.Parse(splitData[2]);
        Debug.Log(colNum + "x" + rowNum + ". Match " + matchNum);
    }


}
