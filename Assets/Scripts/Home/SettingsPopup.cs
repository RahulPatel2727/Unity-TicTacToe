using UnityEngine;
using System.Collections;

public class SettingsPopup : MonoBehaviour
{
    public GameObject panel;
    public RectTransform container;
    public CanvasGroup canvasGroup;
    public CanvasGroup settingsButtonGroup;
    public float duration = 0.5f;
    private bool isAnimating = false;

    void Start() {
        // panel.SetActive(false);
    }
    public void Open() {
        panel.SetActive(true);
        if (isAnimating) return;
        isAnimating = true;

        AudioManager.instance.Play(AudioManager.instance.buttonClick);
        StopAllCoroutines();

        // 🔥 simple fade (no coroutine needed)
        settingsButtonGroup.alpha = 0.65f;
        settingsButtonGroup.interactable = false;
        settingsButtonGroup.blocksRaycasts = false;


        container.localScale = Vector3.zero;
        canvasGroup.alpha = 0;

        StartCoroutine(AnimateIn()); // ✅ THIS MUST BE HERE
    }

    IEnumerator AnimateIn()
    {
        float t = 0;

        while (t < duration)
        {
            t += Time.deltaTime;
            float progress = Mathf.SmoothStep(0, 1, t / duration);

            canvasGroup.alpha = progress;
            container.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, progress);

            yield return null;
        }

        canvasGroup.alpha = 1;
        container.localScale = Vector3.one;
        isAnimating = false; // ✅ done
    }

    public void Close()
    {
        if (isAnimating) return;
        isAnimating = true;
        AudioManager.instance.Play(AudioManager.instance.buttonClick);
        StopAllCoroutines(); // 🔥 important
        StartCoroutine(AnimateOut());
    }

    IEnumerator AnimateOut()
    {
        float t = duration;

        while (t > 0)
        {
            t -= Time.deltaTime;
            float progress = Mathf.SmoothStep(0, 1, t / duration);

            canvasGroup.alpha = progress;
            container.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, progress);

            yield return null;
        }

        canvasGroup.alpha = 0;
        panel.SetActive(false);

        // 🔥 restore button AFTER animation
        settingsButtonGroup.alpha = 1f;
        settingsButtonGroup.interactable = true;
        settingsButtonGroup.blocksRaycasts = true;
        isAnimating = false; // ✅ done 
    }
}