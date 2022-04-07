using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXPlayer : MonoBehaviour
{
    AudioSource sfxSound;
    public float masterVolume = 0.3f;
    void Start()
    {
        StartCoroutine(playSoundOnce());
    }

    private IEnumerator playSoundOnce()
    {
        sfxSound = GetComponent<AudioSource>();
        sfxSound.volume = PlayerPrefsController.GetMusicOnOff() * masterVolume;
        yield return new WaitForSeconds(0.2f);
        Destroy(this.gameObject);
    }
}
