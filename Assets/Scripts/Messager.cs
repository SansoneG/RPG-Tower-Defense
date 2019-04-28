using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Messager : MonoBehaviour
{

    private static Messager instance;

    [SerializeField]
    private TextMeshProUGUI messageText;

    [SerializeField]
    private float showMessageTime = 3f;

    private float lastMessageCountdown = 0f;

    private bool isShowingMessage = false;

    private Coroutine currentFadeIn;
    private Coroutine currentFadeOut;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if(lastMessageCountdown <= 0 && isShowingMessage)
        {
            if(currentFadeIn != null)
                StopCoroutine(currentFadeIn);
            currentFadeOut = StartCoroutine(FadeOut(1f, messageText));
            isShowingMessage = false;
        }
        else if(lastMessageCountdown > 0)
        {
            lastMessageCountdown -= Time.deltaTime;
        }
    }

    private void ShowMessage(string message)
    {
        messageText.text = message;
        lastMessageCountdown = showMessageTime;

        isShowingMessage = true;
        currentFadeIn = StartCoroutine(FadeIn(1f, messageText));
        if(currentFadeOut != null)
            StopCoroutine(currentFadeOut);
    }
    
    public static void NewMessage(string message)
    {
        instance.ShowMessage(message);
    }


    // Helper function to fade text in and out
    private IEnumerator FadeIn(float t, TextMeshProUGUI i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
        while (i.color.a < 1.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
            yield return null;
        }
    }
 
    private IEnumerator FadeOut(float t, TextMeshProUGUI i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
        }
    }

}
