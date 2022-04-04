using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LabelManager : MonoBehaviour
{
    private GameManager game;
    public string valueType;

    private TextMeshProUGUI label;

    void Start()
    {
        game = FindObjectOfType<GameManager>();
        label = this.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        updateText(valueType);
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
}
