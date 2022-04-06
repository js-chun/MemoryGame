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
    private bool prevClickState;

    private void Start()
    {
        game = FindObjectOfType<GameManager>();
        audioP = FindObjectOfType<MusicPlayer>();
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
        StartCoroutine(loadMenu());
    }

    public void goToMenuFromSettings()
    {
        StartCoroutine(loadMenu());
        PlayerPrefs.Save();
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

}
