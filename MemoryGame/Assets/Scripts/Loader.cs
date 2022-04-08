using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Loader : MonoBehaviour
{
    private GameManager game;
    private MusicPlayer audioP;

    public float loadTime = 0.1f;

    public GameObject pauseScreen;
    public GameObject gameOverScreen;
    public GameObject gameOverWin;
    public GameObject gameOverLose;
    private bool prevClickState;
    private bool gameOverState;
    private bool hasWonGame;

    private void Start()
    {
        game = FindObjectOfType<GameManager>();
        audioP = FindObjectOfType<MusicPlayer>();
        gameOverState = false;
        hasWonGame = false;
    }

    private void Update()
    {
        screenOt();
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(gameOverState);
        }
        if (gameOverWin != null && gameOverLose != null)
        {
            if (gameOverScreen.activeSelf == true)
            {
                gameOverWin.SetActive(hasWonGame);
                gameOverLose.SetActive(!hasWonGame);
            }
        }
    }

    public void goToGame(string str_setting)
    {
        game.setGameSettings(str_setting);
        StartCoroutine(loadPlay());
    }

    private IEnumerator loadPlay()
    {
        audioP.playSound("select");
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("GameScene");
    }

    public void goToMenuFromGame()
    {
        Time.timeScale = 1f;
        StartCoroutine(loadMenu());
    }

    public void goToMenuFromSettings()
    {
        StartCoroutine(loadMenu());
        PlayerPrefs.Save();
    }

    public void playAgainFromGame()
    {
        Time.timeScale = 1f;
        StartCoroutine(playAgain());
    }

    private IEnumerator playAgain()
    {
        yield return new WaitForSeconds(loadTime);
        game.gameRestart();
    }

    private IEnumerator loadMenu()
    {
        audioP.playSound("select");
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("MenuScene");
        game.resetGameSettings();
    }

    public void goToSettings()
    {
        StartCoroutine(loadSettings());
    }

    private IEnumerator loadSettings()
    {
        audioP.playSound("select");
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("SettingsScene");
    }

    public void pauseGame(bool pauseIt)
    {
        if (pauseIt)
        {
            audioP.playSound("pause_in");
            prevClickState = game.canClickCards;
        }
        else { audioP.playSound("pause_out"); }
        game.pauseState(pauseIt, prevClickState);
        if (pauseScreen != null)
        {
            pauseScreen.SetActive(pauseIt);
        }
    }

    public void isGameOver(bool state)
    {
        gameOverState = state;
    }

    public void wonGame(bool state)
    {
        hasWonGame = state;
    }

    private void screenOt()
    {
        Screen.autorotateToPortrait = true;
        Screen.autorotateToPortraitUpsideDown = true;
        Screen.autorotateToLandscapeLeft = false;
        Screen.autorotateToLandscapeRight = false;

        Screen.orientation = ScreenOrientation.AutoRotation;
    }

}
