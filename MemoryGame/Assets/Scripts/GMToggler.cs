using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GMToggler : MonoBehaviour
{
    private GameManager game;

    public int gMode;

    // Start is called before the first frame update
    void Start()
    {
        game = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        checkOn();
    }

    private void checkOn()
    {
        if (gMode == game.gameMode)
        {
            this.GetComponent<Image>().color = new Color(1f, 1f, 1f);
        }
        else
        {
            this.GetComponent<Image>().color = new Color(0.7843137f, 0.7843137f, 0.7843137f);
        }
    }

    public void switchMode()
    {
        game.gameMode = gMode;
    }
}
