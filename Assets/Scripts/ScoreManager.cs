using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public int defaultScore;
    public TMP_Text scoreUI;
    public TMP_Text comboUI;
    int score;
    int combo = 1;
    GameObject lastProp;

    public void AddToScore(int value, bool comboing)
    {
        if(comboing)
        {
            combo++;
        } else {
            combo = 1;
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

    public int GetScore()
    {
        return score;
    }
}
