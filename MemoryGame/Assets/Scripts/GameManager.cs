using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//class to manage game and settings for the game session
public class GameManager : MonoBehaviour
{
    private MusicPlayer audioP;
    private float waitTime = 0.5f;

    public int rowNum;
    public int colNum;
    public int matchNum;
    public int gameMode;
    public string cardSkin;
    public string backSkin;

    public int numRevealed = 0;
    public Card cardOne;
    public Card cardTwo;
    public Card cardThree;
    public bool canCheck;
    public bool canClickCards;
    public bool isPlaying;
    public int pairsMatched;
    public float time;
    public int numCards;
    public int numTries;

    public float timeMode = 30f;
    public int triesMode = 100;

    private void Awake()
    {
        //to make sure only one game manager exists at a time
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
        audioP = FindObjectOfType<MusicPlayer>();
        getDefaultSettings();
        gameMode = 0;
        resetGameSettings();
    }

    void Update()
    {
        if (isPlaying)
        {
            gameState();
            checkCards();
        }
    }

    //gets the locally saved front skin and back skin
    private void getDefaultSettings()
    {
        string skin = PlayerPrefsController.GetFrontSkin();
        List<string> skinsList = new List<string>() { "animals_", "farm_", "fashion_", "veggies_", "flags_", "music_", "korea_", "nature_" };
        if (skinsList.Contains(skin))
        {
            cardSkin = skin;
        }
        else
        {
            cardSkin = "animals_";
        }
        skin = PlayerPrefsController.GetBackSkin();
        skinsList.Clear();
        skinsList = new List<string>() { "back_1", "back_2", "back_3", "back_4" };
        if (skinsList.Contains(skin))
        {
            backSkin = skin;
        }
        else
        {
            backSkin = "back_1";
        }
    }

    //sets the game mode / number of cards / match 2or3 based on button selected for a single game session
    public void setGameSettings(string modeData)
    {
        string[] splitData = modeData.Split(',');
        colNum = int.Parse(splitData[0]);
        rowNum = int.Parse(splitData[1]);
        matchNum = int.Parse(splitData[2]);
        canClickCards = true;
    }

    //resets all the settings related to a game session
    public void resetGameSettings()
    {
        rowNum = 0;
        colNum = 0;
        numRevealed = 0;
        matchNum = 0;
        numCards = 0;
        canCheck = false;
        canClickCards = false;
        cardOne = null;
        cardTwo = null;
        cardThree = null;
        pairsMatched = 0;
        isPlaying = false;
        numTries = triesMode;
    }

    //checks cards revealed against each other to see if they match or not
    private void checkCards()
    {
        numCards = FindObjectsOfType<Card>().Length;
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

    //result if checked cards match, destroys the matched cards
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
        pairsMatched += 1;
        if (gameMode == 1)
        {
            numTries -= 1;
        }
        else
        {
            numTries += 1;
        }
        audioP.playSound("correct");
    }

    //result if checked cards do not match
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
        if (gameMode == 1)
        {
            numTries -= 1;
        }
        else
        {
            numTries += 1;
        }
        audioP.playSound("error");
    }

    //starting time/number of tries depending on selected game mode
    public void gameStart()
    {
        if (gameMode == 0)
        {
            time = 0f;
            numTries = 0;
        }
        else if (gameMode == 1)
        {
            time = 0f;
            numTries = triesMode;
        }
        else if (gameMode == 2) 
        { 
            time = timeMode;
            numTries = 0;
        }
        isPlaying = true;
        numRevealed = 0;
        pairsMatched = 0;
    }

    //used to restart a game from the game scene
    public void gameRestart()
    {
        canClickCards = true;
        gameStart();
        Loader ldr = FindObjectOfType<Loader>();
        ldr.isGameOver(false);
        CardManager cm = FindObjectOfType<CardManager>();
        cm.spawnCards();
    }

    //used to control if time tracked increases or decreases depending on game mode
    //used to trigger game over state
    private void gameState()
    {
        if (gameMode == 2)
        {
            time -= Time.deltaTime;
            if (time <= 0) { gameOver(); }
        }
        else
        {
            time += Time.deltaTime;
            
            if(gameMode == 1)
            {
                if (numTries <= 0) { gameOver(); }
            }
            else if (gameMode == 0)
            {
                if (pairsMatched == (rowNum * colNum) / matchNum)
                {
                    gameOver();
                }
            }
        }
    }

    //used to load right game over screen and check/set for high score
    private void gameOver()
    {;
        Time.timeScale = 0f;
        canClickCards = false;
        isPlaying = false;
        Loader ldr = FindObjectOfType<Loader>();
        ldr.isGameOver(true);
        ldr.wonGame(false);

        if (gameMode == 0)
        {
            int hsTries = PlayerPrefsController.GetHighScore(gameMode, rowNum * colNum, matchNum, 1);
            int hsTime = PlayerPrefsController.GetHighScore(gameMode, rowNum * colNum, matchNum, 2);

            int curTime = (int)time;

            if (curTime < hsTime) 
            { 
                PlayerPrefsController.SetHighScore(gameMode, rowNum * colNum, matchNum, pairsMatched, numTries, curTime);
                ldr.wonGame(true);
            }
            else if (curTime == hsTime)
            {
                if (numTries < hsTries) 
                {
                    PlayerPrefsController.SetHighScore(gameMode, rowNum * colNum, matchNum, pairsMatched, numTries, curTime);
                    ldr.wonGame(true);
                }
            }
        }
        else if (gameMode == 1)
        {
            int hsMatches = PlayerPrefsController.GetHighScore(gameMode, rowNum * colNum, matchNum, 0);
            int hsTime = PlayerPrefsController.GetHighScore(gameMode, rowNum * colNum, matchNum, 2);

            int curTime = (int)time;

            if (pairsMatched > hsMatches) 
            {
                PlayerPrefsController.SetHighScore(gameMode, rowNum * colNum, matchNum, pairsMatched, triesMode, curTime);
                ldr.wonGame(true);
            }
            else if (pairsMatched == hsMatches)
            {
                if (curTime < hsTime) 
                {
                    PlayerPrefsController.SetHighScore(gameMode, rowNum * colNum, matchNum, pairsMatched, triesMode, curTime);
                    ldr.wonGame(true);
                }
            }
        }
        else if (gameMode == 2)
        {
            int hsMatches = PlayerPrefsController.GetHighScore(gameMode, rowNum * colNum, matchNum, 0);
            int hsTries = PlayerPrefsController.GetHighScore(gameMode, rowNum * colNum, matchNum, 2);

            int curTime = (int)timeMode; 

            if (pairsMatched > hsMatches) 
            {
                PlayerPrefsController.SetHighScore(gameMode, rowNum * colNum, matchNum, pairsMatched, numTries, curTime);
                ldr.wonGame(true);
            }
            else if (pairsMatched == hsMatches)
            {
                if (numTries < hsTries) 
                {
                    PlayerPrefsController.SetHighScore(gameMode, rowNum * colNum, matchNum, pairsMatched, numTries, curTime);
                    ldr.wonGame(true);
                }
            }
        }

    }

    //used to pause or unpause the game
    public void pauseState(bool isPaused, bool prevClickState)
    {
        if (isPaused)
        {
            isPlaying = false;
            canClickCards = false;
            Time.timeScale = 0f;
        }
        else
        {
            isPlaying = true;
            canClickCards = prevClickState;
            Time.timeScale = 1f;
        }
    }
}
