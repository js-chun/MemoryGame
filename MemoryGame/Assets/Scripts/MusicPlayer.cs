using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    AudioSource audioS;
    public float masterVolume = 0.15f;
    public GameObject select_sfx;
    public GameObject correct_sfx;
    public GameObject error_sfx;
    public GameObject pause_in_sfx;
    public GameObject pause_out_sfx;

    private void Awake()
    {
        int sessionCount = FindObjectsOfType<MusicPlayer>().Length;
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
    private void Start()
    {
        {
            audioS = GetComponent<AudioSource>();
            audioS.volume = PlayerPrefsController.GetMusicOnOff() * masterVolume;
        }
    }

    public void turnMusicOnOff()
    {
        float onOrOff = PlayerPrefsController.GetMusicOnOff();
        if (onOrOff == 1f)
        {
            PlayerPrefsController.SetMusic(false);
            audioS.volume = 0f;
        }
        else
        {
            PlayerPrefsController.SetMusic(true);
            audioS.volume = masterVolume;
        }
    }

    public void playSound(string sfxname)
    {
        if (sfxname == "select") { GameObject SFXPlayer = Instantiate(select_sfx, new Vector3(0f, 0f, 0f), Quaternion.identity,transform); }
        else if (sfxname == "correct") { GameObject SFXPlayer = Instantiate(correct_sfx, new Vector3(0f, 0f, 0f), Quaternion.identity, transform); }
        else if (sfxname == "error") { GameObject SFXPlayer = Instantiate(error_sfx, new Vector3(0f, 0f, 0f), Quaternion.identity, transform); }
        else if (sfxname == "pause_in") { GameObject SFXPlayer = Instantiate(pause_in_sfx, new Vector3(0f, 0f, 0f), Quaternion.identity, transform); }
        else if (sfxname == "pause_out") { GameObject SFXPlayer = Instantiate(pause_out_sfx, new Vector3(0f, 0f, 0f), Quaternion.identity, transform); }
    }
}
