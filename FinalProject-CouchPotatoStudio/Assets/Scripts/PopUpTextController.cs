using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpTextController : MonoBehaviour
{
    public string text;
    public int textSize;
    public UnityEngine.TextAnchor alignment;
    public Image textPanel;
    public Text uiText;

    //====Start===
    void Start()
    {
        
    }

    // ====Updates====
    void Update()
    {
        
    }
    //=====Colliders====
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            uiText.text = text;
            if (textSize != 0)
            {
                uiText.fontSize = textSize;
            }
            uiText.alignment = alignment;
            StopAllCoroutines();
            StartCoroutine(FadeInText());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StopAllCoroutines();
            StartCoroutine(FadeOutText());
        }
    }

    IEnumerator FadeInText()
    {
        for (float i = textPanel.color.a; i < 1.00f; i += 0.015f)
        {
            textPanel.color = new Color(0, 0, 0, i);
            uiText.color = new Color(1, 1, 1, i);
            yield return new WaitForEndOfFrame();
        }
        textPanel.color = new Color(0, 0, 0, 1);
        uiText.color = new Color(1, 1, 1, 1);
        yield break;
    }

    IEnumerator FadeOutText()
    {
        for (float i = textPanel.color.a; i > 0.00f; i -= 0.01f)
        {
            textPanel.color = new Color(0, 0, 0, i);
            uiText.color = new Color(1, 1, 1, i);
            yield return new WaitForEndOfFrame();
        }
        textPanel.color = new Color(0, 0, 0, 0);
        uiText.color = new Color(1, 1, 1, 0);
        yield break;
    }
}
