using UnityEngine;
using System.Collections;
public class HowToPlayPopup : MonoBehaviour {
    public RectTransform container;
    public CanvasGroup canvasGroup;
    public GameObject panel;
    public float duration = 2f;
    private bool isAnimating = false;
    void Start() {
        // gameObject.SetActive(false);
    }

    public void Open() {
        gameObject.SetActive(true); // ✅ FIXED
        if (isAnimating) return;
        isAnimating = true;

        AudioManager.instance.PlayUI(AudioManager.instance.buttonClick);


        container.localScale = Vector3.zero;
        canvasGroup.alpha = 0;

        StartCoroutine(AnimateIn());
    }

    IEnumerator AnimateIn() {
        float t = 0;
        while (t < duration) {
            t += Time.deltaTime;
            float p = Mathf.SmoothStep(0, 1, t / duration);
            canvasGroup.alpha = p;
            container.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, p);
            yield return null;
        }
        isAnimating = false;
    }
    public void Close() {
        if (isAnimating) return;
        isAnimating = true;
        AudioManager.instance.PlayUI(AudioManager.instance.buttonClick);
        StopAllCoroutines();
        StartCoroutine(AnimateOut());
    }
    IEnumerator AnimateOut() {
        float t = duration;
        while (t > 0) {
            t -= Time.deltaTime;
            float p = Mathf.SmoothStep(0, 1, t / duration);
            canvasGroup.alpha = p;
            container.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, p);
            yield return null;
        }
        gameObject.SetActive(false); // ✅ FIXED
        isAnimating = false;
    }
}