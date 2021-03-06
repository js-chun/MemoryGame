using UnityEngine;

public class Card : MonoBehaviour
{
    private GameManager game;
    private MusicPlayer audioP;
    public int cardValue;

    private string bSkinPath;
    private string cSkinPath;

    private SpriteRenderer spriteR;

    private Sprite bSkin;
    private Sprite cSkin;

    public bool isRevealed;

    //loads the right front and back skin from the resources folder
    void Start()
    {
        game = FindObjectOfType<GameManager>();
        audioP = FindObjectOfType<MusicPlayer>();
        spriteR = gameObject.GetComponent<SpriteRenderer>();
        cSkinPath = "front_/" + game.cardSkin + '/' + game.cardSkin + cardValue ;
        bSkinPath = "back_/" + game.backSkin;

        bSkin = Resources.Load<Sprite>(bSkinPath);
        cSkin = Resources.Load<Sprite>(cSkinPath);
        isRevealed = false;
    }

    void Update()
    {
        cardState();
    }

    //checks if card is revealed or not and loads back skin if not, front skin if true
    private void cardState()
    {
        if (isRevealed == true)
        {
            spriteR.sprite = cSkin;
        }
        else
        {
            spriteR.sprite = bSkin;
        }
    }

    //reveals card if limit on revealed cards is not met
    private void OnMouseDown()
    {
        if (game.canClickCards)
        {
            if (game.numRevealed < game.matchNum)
            {
                if (isRevealed == false)
                {
                    audioP.playSound("select");
                    game.numRevealed = game.numRevealed + 1;
                    isRevealed = true;
                    returnCard();
                }
            }
        }
    }

    //used to check the revealed cards against each other
    private void returnCard()
    {
        if (game.cardOne == null)
        {
            game.cardOne = this;
            
        }
        else
        {
            if (game.matchNum == 2)
            {
                game.cardTwo = this;
                game.canCheck = true;
            }
            else if (game.matchNum == 3)
            {
                if (game.cardTwo == null)
                {
                    game.cardTwo = this;
                }
                else
                {
                    game.cardThree = this;
                    game.canCheck = true;
                }
            }
        }
    }

}
