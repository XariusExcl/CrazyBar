using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public int defaultScore;
    
    public TMP_Text scoreUI;
    public GameObject scoreUIClone;
    TMP_Text scoreUICloneText;

    public TMP_Text comboUI;
    public GameObject comboUIClone;
    TMP_Text comboUICloneText;

    public AudioSource comboSound;
    int combo = 1;
    int score;
    GameObject lastProp;

    void Start()
    {
        scoreUICloneText = scoreUIClone.GetComponent<TMP_Text>();
        comboUICloneText = comboUIClone.GetComponent<TMP_Text>();
    }

    public void AddToScore(int value, bool comboing)
    {
        if(comboing)
        {
            comboSound.Play();
            comboSound.pitch = comboSound.pitch + 0.1f; 
            combo++;
            
            // Combo effect
            comboUIClone.SetActive(true);
            comboUICloneText.text = combo.ToString() + "x";
            
        } else {
            comboSound.pitch = 1f;
            combo = 1;
        }
        score += value + combo - 1;
        scoreUI.text = score.ToString();
        comboUI.text = combo.ToString() + "x";

        // Score effect
        scoreUIClone.SetActive(true);
        scoreUICloneText.text = scoreUI.text;
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
