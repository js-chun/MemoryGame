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

    void Update()
    {
        //if label is not for a highscore, will continuously update the value
        if (isHighScore == false)
        {
            updateText(valueType);
        }
        else
        {
            //if label is for a highscore, will update continuously only if high score section is active
            if (this.isActiveAndEnabled == true)
            {
                updateHsText(valueType);
            }
        }
    }

    //updates the appropriate text with type of value it is (score, time, number of tries)
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

    //updates the appropriate high score text with type of valuee it is (score, time, number of tries)
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
