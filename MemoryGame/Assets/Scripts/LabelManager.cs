using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LabelManager : MonoBehaviour
{
    private GameManager game;
    public string valueType;
    public bool isHighScore;

    private TextMeshProUGUI label;

    void Start()
    {
        game = FindObjectOfType<GameManager>();
        label = this.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isHighScore == false)
        {
            updateText(valueType);
        }
        else
        {
            if (this.isActiveAndEnabled == true)
            {
                updateHsText(valueType);
            }
        }
    }

    private void updateText(string valueType)
    {
        if (valueType == "score")
        {
            label.SetText(game.pairsMatched.ToString());
        }
        else if (valueType == "time")
        {
            float timeValue = (int)game.time;
            int min = (int)(timeValue / 60);
            int sec = (int)(timeValue % 60);

            string strSec;
            if (sec < 10) { strSec = "0" + sec.ToString(); }
            else { strSec = sec.ToString(); }

            label.SetText(min.ToString() + ":" + strSec) ;
        }
        else if (valueType == "tries")
        {
            label.SetText(game.numTries.ToString());
        }
    }

    private void updateHsText(string valueType)
    {
        if (valueType == "score")
        {
            int hsScore = PlayerPrefsController.GetHighScore(game.gameMode, game.rowNum * game.colNum, game.matchNum, 0);
            label.SetText(hsScore.ToString());
            Debug.Log(hsScore);
        }
        else if (valueType == "time")
        {
            int hsTime = PlayerPrefsController.GetHighScore(game.gameMode, game.rowNum * game.colNum, game.matchNum, 2);
            int min = hsTime / 60;
            int sec = hsTime % 60;

            string strSec;
            if (sec < 10) { strSec = "0" + sec.ToString(); }
            else { strSec = sec.ToString(); }

            label.SetText(min.ToString() + ":" + strSec);
            Debug.Log(hsTime);
        }
        else if (valueType == "tries")
        {
            int hsTries = PlayerPrefsController.GetHighScore(game.gameMode, game.rowNum * game.colNum, game.matchNum, 1);
            label.SetText(hsTries.ToString());
            Debug.Log(hsTries);
        }
    }
}
