using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIController : MonoBehaviour
{
    public GameObject retryPrompt;
    public TMP_Text scoreText;
    public GameObject tutorialPrompt;
    public GameObject blackFade;

    public void ShowPopup(int score, int hiScore, bool isHighScore)
    {
        scoreText.text = "Score: " + score.ToString() + "\n" + "HiScore: " + hiScore.ToString();

        Animation anim = retryPrompt.GetComponent<Animation>();
        anim.Play("PopIn");
    }

    public void ShowTutorial()
    {
        tutorialPrompt.SetActive(true);
    }

    public void StartBtn()
    {
        blackFade.GetComponent<Animator>().SetTrigger("FadeOut");
        StartCoroutine("RestartLevelCoroutine"); // :)
    }

    public void RetryBtn()
    {
        blackFade.GetComponent<Animator>().SetTrigger("FadeOut");
        StartCoroutine("RestartLevelCoroutine");
    }

    IEnumerator RestartLevelCoroutine()
    {
        yield return new WaitForSeconds(0.6f);
        LoadLevel("SampleScene");
    }

    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
}
