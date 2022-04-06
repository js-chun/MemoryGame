using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsController : MonoBehaviour
{
    const string CARD_FRONT_SKIN = "default card front skin";
    const string CARD_BACK_SKIN = "default card back skin";
    const string MUSIC_ON_OFF = "volume on or off";

    const string HIGHSCORE_G1_N1_M2 = "1match 2x4 match2 high score";
    const string HIGHSCORE_G1_N2_M2 = "1match 4x4 match2 high score";
    const string HIGHSCORE_G1_N3_M2 = "1match 4x6 match2 high score";
    const string HIGHSCORE_G1_N4_M2 = "1match 6x6 match2 high score";
    const string HIGHSCORE_G1_N5_M2 = "1match 6x8 match2 high score";

    const string HIGHSCORE_G2_N1_M2 = "100tries 2x4 match2 high score";
    const string HIGHSCORE_G2_N2_M2 = "100tries 4x4 match2 high score";
    const string HIGHSCORE_G2_N3_M2 = "100tries 4x6 match2 high score";
    const string HIGHSCORE_G2_N4_M2 = "100tries 6x6 match2 high score";
    const string HIGHSCORE_G2_N5_M2 = "100tries 6x8 match2 high score";

    const string HIGHSCORE_G3_N1_M2 = "3min 2x4 match2 high score";
    const string HIGHSCORE_G3_N2_M2 = "3min 4x4 match2 high score";
    const string HIGHSCORE_G3_N3_M2 = "3min 4x6 match2 high score";
    const string HIGHSCORE_G3_N4_M2 = "3min 6x6 match2 high score";
    const string HIGHSCORE_G3_N5_M2 = "3min 6x8 match2 high score";

    const string HIGHSCORE_G1_N1_M3 = "1match 3x4 match3 high score";
    const string HIGHSCORE_G1_N2_M3 = "1match 4x6 match3 high score";
    const string HIGHSCORE_G1_N3_M3 = "1match 6x6 match3 high score";
    const string HIGHSCORE_G1_N4_M3 = "1match 6x8 match3 high score";
    const string HIGHSCORE_G1_N5_M3 = "1match 6x10 match3 high score";

    const string HIGHSCORE_G2_N1_M3 = "100tries 3x4 match3 high score";
    const string HIGHSCORE_G2_N2_M3 = "100tries 4x6 match3 high score";
    const string HIGHSCORE_G2_N3_M3 = "100tries 6x6 match3 high score";
    const string HIGHSCORE_G2_N4_M3 = "100tries 6x8 match3 high score";
    const string HIGHSCORE_G2_N5_M3 = "100tries 6x10 match3 high score";

    const string HIGHSCORE_G3_N1_M3 = "3min 3x4 match3 high score";
    const string HIGHSCORE_G3_N2_M3 = "3min 4x6 match3 high score";
    const string HIGHSCORE_G3_N3_M3 = "3min 6x6 match3 high score";
    const string HIGHSCORE_G3_N4_M3 = "3min 6x8 match3 high score";
    const string HIGHSCORE_G3_N5_M3 = "3min 6x10 match3 high score";

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

    public static void SetHighScore(int gameMode, int numCards, int match, int sesMatches, int sesTries, int sesTime)
    {
        string highScore = sesMatches.ToString() + "," + sesTries.ToString() + "," + sesTime.ToString();
        if (gameMode == 0)
        {
            if (match == 2)
            {
                if (numCards == 8) { PlayerPrefs.SetString(HIGHSCORE_G1_N1_M2, highScore); }
                else if (numCards == 16) { PlayerPrefs.SetString(HIGHSCORE_G1_N2_M2, highScore); }
                else if (numCards == 24) { PlayerPrefs.SetString(HIGHSCORE_G1_N3_M2, highScore); }
                else if (numCards == 36) { PlayerPrefs.SetString(HIGHSCORE_G1_N4_M2, highScore); }
                else if (numCards == 48) { PlayerPrefs.SetString(HIGHSCORE_G1_N5_M2, highScore); }
            }
            else if (match == 3)
            {
                if (numCards == 12) { PlayerPrefs.SetString(HIGHSCORE_G1_N1_M3, highScore); }
                else if (numCards == 24) { PlayerPrefs.SetString(HIGHSCORE_G1_N2_M3, highScore); }
                else if (numCards == 36) { PlayerPrefs.SetString(HIGHSCORE_G1_N3_M3, highScore); }
                else if (numCards == 48) { PlayerPrefs.SetString(HIGHSCORE_G1_N4_M3, highScore); }
                else if (numCards == 60) { PlayerPrefs.SetString(HIGHSCORE_G1_N5_M3, highScore); }
            }
        }
        else if (gameMode == 1)
        {
            if (match == 2)
            {
                if (numCards == 8) { PlayerPrefs.SetString(HIGHSCORE_G2_N1_M2, highScore); }
                else if (numCards == 16) { PlayerPrefs.SetString(HIGHSCORE_G2_N2_M2, highScore); }
                else if (numCards == 24) { PlayerPrefs.SetString(HIGHSCORE_G2_N3_M2, highScore); }
                else if (numCards == 36) { PlayerPrefs.SetString(HIGHSCORE_G2_N4_M2, highScore); }
                else if (numCards == 48) { PlayerPrefs.SetString(HIGHSCORE_G2_N5_M2, highScore); }
            }
            else if (match == 3)
            {
                if (numCards == 12) { PlayerPrefs.SetString(HIGHSCORE_G2_N1_M3, highScore); }
                else if (numCards == 24) { PlayerPrefs.SetString(HIGHSCORE_G2_N2_M3, highScore); }
                else if (numCards == 36) { PlayerPrefs.SetString(HIGHSCORE_G2_N3_M3, highScore); }
                else if (numCards == 48) { PlayerPrefs.SetString(HIGHSCORE_G2_N4_M3, highScore); }
                else if (numCards == 60) { PlayerPrefs.SetString(HIGHSCORE_G2_N5_M3, highScore); }
            }
        }
        else if (gameMode == 2)
        {
            if (match == 2)
            {
                if (numCards == 8) { PlayerPrefs.SetString(HIGHSCORE_G3_N1_M2, highScore); }
                else if (numCards == 16) { PlayerPrefs.SetString(HIGHSCORE_G3_N2_M2, highScore); }
                else if (numCards == 24) { PlayerPrefs.SetString(HIGHSCORE_G3_N3_M2, highScore); }
                else if (numCards == 36) { PlayerPrefs.SetString(HIGHSCORE_G3_N4_M2, highScore); }
                else if (numCards == 48) { PlayerPrefs.SetString(HIGHSCORE_G3_N5_M2, highScore); }
            }
            else if (match == 3)
            {
                if (numCards == 12) { PlayerPrefs.SetString(HIGHSCORE_G3_N1_M3, highScore); }
                else if (numCards == 24) { PlayerPrefs.SetString(HIGHSCORE_G3_N2_M3, highScore); }
                else if (numCards == 36) { PlayerPrefs.SetString(HIGHSCORE_G3_N3_M3, highScore); }
                else if (numCards == 48) { PlayerPrefs.SetString(HIGHSCORE_G3_N4_M3, highScore); }
                else if (numCards == 60) { PlayerPrefs.SetString(HIGHSCORE_G3_N5_M3, highScore); }
            }
        }
    }

    public static string GetFrontSkin() { return PlayerPrefs.GetString(CARD_FRONT_SKIN); }

    public static string GetBackSkin() { return PlayerPrefs.GetString(CARD_BACK_SKIN); }

    public static float GetMusicOnOff() { return PlayerPrefs.GetFloat(MUSIC_ON_OFF); }

    public static int GetHighScore(int gameMode, int numCards, int match, int type)
    {
        string hsParse = "";
        if (gameMode == 0)
        {
            if (match == 2)
            {
                if(numCards == 8) { hsParse = PlayerPrefs.GetString(HIGHSCORE_G1_N1_M2); }
                else if(numCards == 16) { hsParse = PlayerPrefs.GetString(HIGHSCORE_G1_N2_M2); }
                else if (numCards == 24) { hsParse = PlayerPrefs.GetString(HIGHSCORE_G1_N3_M2); }
                else if (numCards == 36) { hsParse = PlayerPrefs.GetString(HIGHSCORE_G1_N4_M2); }
                else if (numCards == 48) { hsParse = PlayerPrefs.GetString(HIGHSCORE_G1_N5_M2); }

            }
            else if (match == 3)
            {
                if (numCards == 12) { hsParse = PlayerPrefs.GetString(HIGHSCORE_G1_N1_M3); }
                else if (numCards == 24) { hsParse = PlayerPrefs.GetString(HIGHSCORE_G1_N2_M3); }
                else if (numCards == 36) { hsParse = PlayerPrefs.GetString(HIGHSCORE_G1_N3_M3); }
                else if (numCards == 48) { hsParse = PlayerPrefs.GetString(HIGHSCORE_G1_N4_M3); }
                else if (numCards == 60) { hsParse = PlayerPrefs.GetString(HIGHSCORE_G1_N5_M3); }
            }
        }
        else if (gameMode == 1)
        {
            if (match == 2)
            {
                if (numCards == 8) { hsParse = PlayerPrefs.GetString(HIGHSCORE_G2_N1_M2); }
                else if (numCards == 16) { hsParse = PlayerPrefs.GetString(HIGHSCORE_G2_N2_M2); }
                else if (numCards == 24) { hsParse = PlayerPrefs.GetString(HIGHSCORE_G2_N3_M2); }
                else if (numCards == 36) { hsParse = PlayerPrefs.GetString(HIGHSCORE_G2_N4_M2); }
                else if (numCards == 48) { hsParse = PlayerPrefs.GetString(HIGHSCORE_G2_N5_M2); }

            }
            else if (match == 3)
            {
                if (numCards == 12) { hsParse = PlayerPrefs.GetString(HIGHSCORE_G2_N1_M3); }
                else if (numCards == 24) { hsParse = PlayerPrefs.GetString(HIGHSCORE_G2_N2_M3); }
                else if (numCards == 36) { hsParse = PlayerPrefs.GetString(HIGHSCORE_G2_N3_M3); }
                else if (numCards == 48) { hsParse = PlayerPrefs.GetString(HIGHSCORE_G2_N4_M3); }
                else if (numCards == 60) { hsParse = PlayerPrefs.GetString(HIGHSCORE_G2_N5_M3); }
            }
        }
        else if (gameMode == 2)
        {
            if (match == 2)
            {
                if (numCards == 8) { hsParse = PlayerPrefs.GetString(HIGHSCORE_G3_N1_M2); }
                else if (numCards == 16) { hsParse = PlayerPrefs.GetString(HIGHSCORE_G3_N2_M2); }
                else if (numCards == 24) { hsParse = PlayerPrefs.GetString(HIGHSCORE_G3_N3_M2); }
                else if (numCards == 36) { hsParse = PlayerPrefs.GetString(HIGHSCORE_G3_N4_M2); }
                else if (numCards == 48) { hsParse = PlayerPrefs.GetString(HIGHSCORE_G3_N5_M2); }

            }
            else if (match == 3)
            {
                if (numCards == 12) { hsParse = PlayerPrefs.GetString(HIGHSCORE_G3_N1_M3); }
                else if (numCards == 24) { hsParse = PlayerPrefs.GetString(HIGHSCORE_G3_N2_M3); }
                else if (numCards == 36) { hsParse = PlayerPrefs.GetString(HIGHSCORE_G3_N3_M3); }
                else if (numCards == 48) { hsParse = PlayerPrefs.GetString(HIGHSCORE_G3_N4_M3); }
                else if (numCards == 60) { hsParse = PlayerPrefs.GetString(HIGHSCORE_G3_N5_M3); }
            }
        }
        string[] parseS = hsParse.Split(",");
        hsParse = parseS[type];
        int hsNum = int.Parse(hsParse);
        return hsNum;
    }


}
