using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public GameObject retryPrompt;
    public GameObject blackFade;

    public void ShowPopup()
    {
        Animation anim = retryPrompt.GetComponent<Animation>();
        anim.Play("PopIn");
    }

    public void RetryBtn()
    {
        blackFade.GetComponent<Animator>().SetTrigger("FadeOut");
        StartCoroutine("RestartLevelCoroutine");
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene("SampleScene");
    }

    IEnumerator RestartLevelCoroutine()
    {
        yield return new WaitForSeconds(0.6f);
        RestartLevel();
    }
}
