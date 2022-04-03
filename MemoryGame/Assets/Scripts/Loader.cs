using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Loader : MonoBehaviour
{
    private GameManager game;
    public float loadTime = 0.1f;

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

    public void goToMenu()
    {
        StartCoroutine(loadMenu());
    }

    private IEnumerator loadMenu()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("MenuScene");
        game.resetGameSettings();
    }
}
