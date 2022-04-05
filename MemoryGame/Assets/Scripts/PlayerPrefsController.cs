using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsController : MonoBehaviour
{
    const string CARD_FRONT_SKIN = "default card front skin";
    const string CARD_BACK_SKIN = "default card back skin";
    const string MUSIC_ON_OFF = "volume on or off";

    public static void SetMusic(bool isVolOn)
    {
        if (isVolOn) { PlayerPrefs.SetFloat(MUSIC_ON_OFF, 1f); }
        else { PlayerPrefs.SetFloat(MUSIC_ON_OFF, 0f); }
    }

    public static void SetFrontSkin(string skinName)
    {
        List<string> frontSkins = new List<string>() { "animals_", "farm_", "fashion_", "veggies_", "flags_", "music_", "korea_", "nature_" }; 
        if (frontSkins.Contains(skinName)) { PlayerPrefs.SetString(CARD_FRONT_SKIN, skinName); }
        else { Debug.LogError("Skin doesn't exist"); }
    }
    
    public static void SetBackSkin(string skinName)
    {
        List<string> backSkins = new List<string>() { "back_1", "back_2", "back_3", "back_4" };
        if (backSkins.Contains(skinName)) { PlayerPrefs.SetString(CARD_BACK_SKIN, skinName); }
        else { Debug.LogError("Skin doesn't exist"); }
    }
    public static string GetFrontSkin() { return PlayerPrefs.GetString(CARD_FRONT_SKIN); }

    public static string GetBackSkin() { return PlayerPrefs.GetString(CARD_BACK_SKIN); }

    public static float GetMusicOnOff() { return PlayerPrefs.GetFloat(MUSIC_ON_OFF); }
}
