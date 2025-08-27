using UnityEngine;
using System.Collections; 

public class FadeInUI : MonoBehaviour
{
    public float duration = 1.0f; 
    private CanvasGroup canvasGroup;
    public bool fadeOut = false;
    private float textDuration = 2f;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0f;
    }

    void OnEnable()
    {
        if (canvasGroup != null)
        {
            
            StartCoroutine(FadeInCoroutine());
            if (fadeOut)
            {
                StartCoroutine(FadeOutCoroutine());
            }
            
        }
    }

    IEnumerator FadeInCoroutine()
    {
        for (float t = 0f; t < duration; t += Time.deltaTime)
        {
            float normalizedTime = t / duration;
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, normalizedTime);
            yield return null; 
        }
        canvasGroup.alpha = 1f; 
    }

    IEnumerator FadeOutCoroutine()
    {
        yield return new WaitForSeconds(textDuration);
        for (float t = 0f; t < duration; t += Time.deltaTime)
        {
            float normalizedTime = t / duration;
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, normalizedTime);
            yield return null;
        }
        canvasGroup.alpha = 0f;
        this.gameObject.SetActive(false);
    }
}