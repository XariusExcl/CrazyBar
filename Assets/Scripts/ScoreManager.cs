using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public int defaultScore;
    public TMP_Text scoreUI;
    public TMP_Text comboUI;
    public AudioSource comboSound;
    int combo = 1;
    int score;
    GameObject lastProp;

    public void AddToScore(int value, bool comboing)
    {
        if(comboing)
        {
            comboSound.Play();
            comboSound.pitch = comboSound.pitch + 0.1f; 
            combo++;
        } else {
            comboSound.pitch = 1f;
            combo = 1;
        }
        score += value + combo - 1;
        scoreUI.text = score.ToString();
        comboUI.text = combo.ToString() + "x";
    }

    public GameObject GetLastProp()
    {
        return lastProp;
    }

    public void AddLastProp(GameObject newProp)
    {
        lastProp = newProp;
    }

    public int GetScore()
    {
        return score;
    }
}
