using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Loader : MonoBehaviour
{
    private GameManager game;
    public float loadTime = 0.1f;

    public GameObject pauseScreen;
    private bool prevClickState;

    private void Start()
    {
        game = FindObjectOfType<GameManager>();
    }

    public void goToGame(string str_setting)
    {
        game.setGameSettings(str_setting);
        StartCoroutine(loadPlay());
    }

    private IEnumerator loadPlay()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("GameScene");
    }

    public void goToMenuFromGame()
    {
        StartCoroutine(loadMenu());
        game.resetGameSettings();
    }

    public void goToMenuFromSettings()
    {
        StartCoroutine(loadMenu());
        PlayerPrefs.Save();
    }

    private IEnumerator loadMenu()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("MenuScene");
    }

    public void goToSettings()
    {
        StartCoroutine(loadSettings());
    }

    private IEnumerator loadSettings()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("SettingsScene");
    }

    public void pauseGame(bool pauseIt)
    {
        if (pauseIt) { prevClickState = game.canClickCards; }
        game.pauseState(pauseIt, prevClickState);
        if (pauseScreen != null)
        {
            pauseScreen.SetActive(pauseIt);
        }
    }

}
