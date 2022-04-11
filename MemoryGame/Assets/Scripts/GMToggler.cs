using UnityEngine;
using UnityEngine.UI;

//class to manage game modes, skins, music as on/off toggles
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

    //used to show button as toggle
    //gmode -1 used for back/front skins
    //gmode -2 used for music
    //gmode 0-2 used for actual game modes
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

    //used to show button as a toggle if selected
    private void onColor()
    {
        this.GetComponent<Image>().color = new Color(1f, 1f, 1f);
    }

    //used to grey out the button as a toggle if not selected
    private void offColor()
    {
        this.GetComponent<Image>().color = new Color(0.3773585f, 0.3773585f, 0.3773585f);
    }

    //switches the game mode on menu scene and greys out the unselected modes in the menu scene
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

    //switches the back or front card skin and greys out the unselected cards in the settings
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

    //switches the music on or off and greys/ungreys the button based on music on/off
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
