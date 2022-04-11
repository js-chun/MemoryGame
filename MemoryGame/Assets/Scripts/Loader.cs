using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

//class for loading different scenes and screens
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

        //loader class might or might not be linked to gameover / high score board screen
        //below only procs if linked to set the linked screen active
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

    //used to go to the game from menu scene
    public void goToGame(string str_setting)
    {
        game.setGameSettings(str_setting);
        StartCoroutine(loadPlay());
    }

    //coroutine to go to game scene
    private IEnumerator loadPlay()
    {
        audioP.playSound("select");
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("GameScene");
    }

    //go to menu scene from game scene
    public void goToMenuFromGame()
    {
        Time.timeScale = 1f;
        StartCoroutine(loadMenu());
    }

    //go to menu scene from settings (locally saves setting values)
    public void goToMenuFromSettings()
    {
        StartCoroutine(loadMenu());
        PlayerPrefs.Save();
    }

    //used to restart game from game scene
    public void playAgainFromGame()
    {
        Time.timeScale = 1f;
        StartCoroutine(playAgain());
    }

    //coroutine to restart the game from game scene so player can play again
    private IEnumerator playAgain()
    {
        yield return new WaitForSeconds(loadTime);
        game.gameRestart();
    }

    //coroutine for going to menu scene
    private IEnumerator loadMenu()
    {
        audioP.playSound("select");
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("MenuScene");
        game.resetGameSettings();
    }

    //coroutine for going to settings scene
    public void goToSettings()
    {
        StartCoroutine(loadSettings());
    }

    //used to go from menu scene to settings scene
    private IEnumerator loadSettings()
    {
        audioP.playSound("select");
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("SettingsScene");
    }
    
    //used to pause and unpause game
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

    //used to determine when game is over to set game over screen active
    public void isGameOver(bool state)
    {
        gameOverState = state;
    }

    //used to determine if high score or no high score screen is used at end of game
    public void wonGame(bool state)
    {
        hasWonGame = state;
    }

    //screen orientation is kept portrait or upside down portrait. no landscape orientation
    private void screenOt()
    {
        Screen.autorotateToPortrait = true;
        Screen.autorotateToPortraitUpsideDown = true;
        Screen.autorotateToLandscapeLeft = false;
        Screen.autorotateToLandscapeRight = false;

        Screen.orientation = ScreenOrientation.AutoRotation;
    }

}
