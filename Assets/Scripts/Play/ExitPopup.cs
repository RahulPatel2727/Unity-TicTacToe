using System.Collections;
using UnityEngine;

public class ExitPopup : MonoBehaviour
{
    public RectTransform popup;
    public CanvasGroup canvasGroup;

    public TransitionManager transitionManager;

    bool isAnimating = false;

    public void Open()
    {
        if (isAnimating) return;
        AudioManager.instance.PlayUI(AudioManager.instance.buttonClick);

        gameObject.SetActive(true);

        popup.localScale = Vector3.zero;
        canvasGroup.alpha = 0;

        StopAllCoroutines();
        StartCoroutine(AnimateIn());
    }

    IEnumerator AnimateIn()
    {
        isAnimating = true;

        float t = 0;

        while (t < 1)
        {
            t += Time.deltaTime * 2f;

            float smooth = Mathf.SmoothStep(0, 1, t);

            popup.localScale = Vector3.one * smooth;
            canvasGroup.alpha = smooth;

            yield return null;
        }

        popup.localScale = Vector3.one;
        canvasGroup.alpha = 1;

        isAnimating = false;
    }

    public void Resume()
    {
        if (isAnimating) return;
        AudioManager.instance.PlayUI(AudioManager.instance.buttonClick);

        StopAllCoroutines();
        StartCoroutine(AnimateOut(false));
    }

    public void ExitHome()
    {
        if (isAnimating) return;
        AudioManager.instance.PlayUI(AudioManager.instance.buttonClick);
        StopAllCoroutines();
        StartCoroutine(AnimateOut(true));
    }

    IEnumerator AnimateOut(bool exit)
    {
        isAnimating = true;

        float t = 1;

        while (t > 0)
        {
            t -= Time.deltaTime * 2f;

            float smooth = Mathf.SmoothStep(0, 1, t);

            popup.localScale = Vector3.one * smooth;
            canvasGroup.alpha = smooth;

            yield return null;
        }

        gameObject.SetActive(false);

        isAnimating = false;

        if (exit) {
            AdManager.instance.ShowInterstitialAndLoadScene("home");
            // transitionManager.LoadScene("home");
        }
    }
}