using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GMToggler : MonoBehaviour
{
    private GameManager game;

    public int gMode;
    public bool isBackSkin;
    public string skinName;

    void Start()
    {
        game = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        checkOn();
    }

    private void checkOn()
    {
        if (gMode >= 0)
        {
            if (gMode == game.gameMode)
            {
                onColor();
            }
            else
            {
                offColor();
            }
        }
        else
        {
            if (isBackSkin)
            {
                if (skinName == game.backSkin)
                {
                    onColor();
                }
                else
                {
                    offColor();
                }
            }
            else
            {
                if (skinName == game.cardSkin)
                {
                    onColor();
                }
                else
                {
                    offColor();
                }
            }
        }
    }

    private void onColor()
    {
        this.GetComponent<Image>().color = new Color(1f, 1f, 1f);
    }

    private void offColor()
    {
        this.GetComponent<Image>().color = new Color(0.3773585f, 0.3773585f, 0.3773585f);
    }

    public void switchMode()
    {
        if (gMode >= 0)
        {
            game.gameMode = gMode;
        }
    }

    public void switchCardSkin()
    {
        if (isBackSkin)
        {
            game.backSkin = skinName;
            PlayerPrefsController.SetBackSkin(skinName);
        }
        else
        {
            game.cardSkin = skinName;
            PlayerPrefsController.SetFrontSkin(skinName);
        }
    }

}
