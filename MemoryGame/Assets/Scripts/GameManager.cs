using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int rowNum;
    public int colNum;
    public int matchNum = 2;
    public string cardSkin = "animals_";
    public string backSkin = "back_1";

    public int numRevealed = 0;

    public Card cardOne;
    public Card cardTwo;
    public Card cardThree;
    public bool canCheck;
    public bool canClickCards;

    private float waitTime = 0.5f;
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
        numRevealed = 0;
        matchNum = 2;
        canCheck = false;
        canClickCards = true;

        cardOne = null;
        cardTwo = null;
        cardThree = null;
    }

    void Update()
    {
        checkCards();
    }

    public void gameModeSelect(string modeData)
    {
        string[] splitData = modeData.Split(',');
        colNum = int.Parse(splitData[0]);
        rowNum = int.Parse(splitData[1]);
        matchNum = int.Parse(splitData[2]);
        Debug.Log(colNum + "x" + rowNum + ". Match " + matchNum);
        StartCoroutine(loadPlay());
    }

    private IEnumerator loadPlay()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("GameScene");
    }

    private void checkCards()
    {
        if (numRevealed >= matchNum)
        {
            canClickCards = false;
            if (matchNum == 2)
            {
                if(canCheck)
                {
                    canCheck = false;
                    if (cardOne.cardValue == cardTwo.cardValue)
                    {
                        StartCoroutine(matchResult());
                    }
                    else
                    {
                        StartCoroutine(noMatchResult());
                    }
                }
            }
            else if (matchNum == 3)
            {
                if(canCheck)
                {
                    canCheck = false;
                    if (cardOne.cardValue != cardTwo.cardValue || cardOne.cardValue != cardThree.cardValue)
                    {
                        StartCoroutine(noMatchResult());
                    }
                    else
                    {
                        StartCoroutine(matchResult());
                    }
                }
            }
        }
    }

    private IEnumerator matchResult()
    {
        yield return new WaitForSeconds(waitTime);
        if (cardOne != null)
        {
            Destroy(cardOne.gameObject);
        }
        if (cardTwo != null)
        {
            Destroy(cardTwo.gameObject);
        }
        if (cardThree != null && matchNum == 3)
        {
            Destroy(cardThree.gameObject);
        }
        cardOne = null;
        cardTwo = null;
        cardThree = null;
        numRevealed = 0;
        canClickCards = true;
    }

    private IEnumerator noMatchResult()
    {
        yield return new WaitForSeconds(waitTime);
        if (cardOne != null) cardOne.isRevealed = false;
        if (cardTwo != null) cardTwo.isRevealed = false;
        if (cardThree != null) cardThree.isRevealed = false;
        cardOne = null;
        cardTwo = null;
        cardThree = null;
        numRevealed = 0;
        canClickCards = true;
    }
}
