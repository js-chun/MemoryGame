using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GMToggler : MonoBehaviour
{
    private GameManager game;
    private MusicPlayer audioP;

    public int gMode;
    public bool isBackSkin;
    public string skinName;

    void Start()
    {
        game = FindObjectOfType<GameManager>();
        audioP = FindObjectOfType<MusicPlayer>();
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
        else if (gMode == -1)
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
        else if(gMode == -2)
        {
            float onOrOff = PlayerPrefsController.GetMusicOnOff();
            if (onOrOff == 1f)
            {
                onColor();
            }
            else
            {
                offColor();
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
            if (game.gameMode != gMode)
            {
                audioP.playSound("select");
                game.gameMode = gMode;
            }
        }
    }

    public void switchCardSkin()
    {
        if (gMode == -1)
        {
            if (isBackSkin)
            {
                if (game.backSkin != skinName)
                {
                    audioP.playSound("select");
                    game.backSkin = skinName;
                    PlayerPrefsController.SetBackSkin(skinName);
                }
            }
            else
            {
                if (game.cardSkin != skinName)
                {
                    audioP.playSound("select");
                    game.cardSkin = skinName;
                    PlayerPrefsController.SetFrontSkin(skinName);
                }
            }
        }
    }

    public void switchMusic()
    {
        if (gMode == -2)
        {
            audioP.playSound("select");
            MusicPlayer mp = FindObjectOfType<MusicPlayer>();
            mp.turnMusicOnOff();
        }
    }
}
