using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Ticker : MonoBehaviour
{
    TMP_Text tmpText;
    void Start()
    {
        tmpText = GetComponentInChildren<TMP_Text>();
    }

    public void SetText(string text)
    {
        tmpText.text = text;
    }

    public void SetColor(Color color)
    {
        tmpText.color = color;
    }

    public void SetPosition(Vector2 position)
    {
        tmpText.transform.position = position;
    }

    public void Destroy()
    {
        Destroy(this.gameObject);
    }
}
