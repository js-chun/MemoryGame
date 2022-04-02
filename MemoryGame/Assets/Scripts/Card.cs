using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    private GameManager game;
    public int cardValue;

    private string bSkinPath;
    private string cSkinPath;

    private SpriteRenderer spriteR;

    private Sprite bSkin;
    private Sprite cSkin;

    public bool isRevealed;

    void Start()
    {
        game = FindObjectOfType<GameManager>();
        spriteR = gameObject.GetComponent<SpriteRenderer>();
        cSkinPath = game.cardSkin + '/' + game.cardSkin + cardValue ;
        bSkinPath = "back_/" + game.backSkin;

        bSkin = Resources.Load<Sprite>(bSkinPath);
        cSkin = Resources.Load<Sprite>(cSkinPath);
        isRevealed = false;

    }

    void Update()
    {
        cardState();
    }

    private void cardState()
    {
        if (isRevealed)
        {
            spriteR.sprite = cSkin;
        }
        else
        {
            spriteR.sprite = bSkin;
        }
    }

    private void OnMouseDown()
    {
        isRevealed = !isRevealed;
    }
}
