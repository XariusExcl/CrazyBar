using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public float defaultScore;
    public TMP_Text scoreUI;
    public TMP_Text comboUI;
    public AudioSource comboSound;
    float score;
    float combo = 1f;
    GameObject lastProp;

    public void AddToScore(float value, bool comboing)
    {
        if(comboing)
        {
            comboSound.Play();
            comboSound.pitch = comboSound.pitch + 0.1f; 
            combo = combo + 1f;
        } else {
            comboSound.pitch = 1f;
            combo = 1f;
        }
        score = score + combo;
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
}
