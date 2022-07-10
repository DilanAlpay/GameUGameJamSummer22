using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fader : MonoBehaviour
{

    [SerializeField] CanvasGroup fadeGroup;
    [SerializeField] float fadeTime;

    public GameEvent onFadeInComplete;
    public void SetAlpha(float alpha)
    {
        fadeGroup.alpha = alpha;
    }

    public void StartFadeIn()
    {
        StartCoroutine(FadeInCO());
    }

    public void StartFadeOut()
    {
        StartCoroutine(FadeOutCO());
    }

    public void StartFadeFromBlack()
    {
        StartCoroutine(FadeInFromBlackCO());
    }


    public IEnumerator FadeCO(float targetAlpha, float fadeTime)
    {
        float startAlpha = fadeGroup.alpha;

        while (!Mathf.Approximately(fadeGroup.alpha, targetAlpha))
        {
            fadeGroup.alpha -= (startAlpha - targetAlpha) / fadeTime * Time.deltaTime;

            yield return new WaitForSecondsRealtime(0);
        }

        fadeGroup.alpha = targetAlpha;
    }

    public IEnumerator FadeInCO() 
    {
        yield return FadeCO(0, fadeTime);
        onFadeInComplete?.Call();
    
    }

    public IEnumerator FadeOutCO()
    {
        yield return FadeCO(1, fadeTime);
    }

    public IEnumerator FadeInFromBlackCO()
    {
        SetAlpha(1);
        yield return FadeInCO();
    }

}
