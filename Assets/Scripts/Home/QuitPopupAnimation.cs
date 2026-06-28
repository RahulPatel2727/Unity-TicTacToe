using UnityEngine;
using System.Collections;

public class QuitPopupAnimation : MonoBehaviour
{
    public CanvasGroup canvasGroup;

    public RectTransform popup;

    public float speed = 8f;

    void OnEnable()
    {
        StopAllCoroutines();

        StartCoroutine(OpenAnimation());
    }

    IEnumerator OpenAnimation()
    {
        canvasGroup.alpha = 0f;

        popup.localScale = Vector3.zero;

        float t = 0;

        while (t < 1)
        {
            t += Time.unscaledDeltaTime * speed;

            float smooth =
                Mathf.SmoothStep(0, 1, t);

            canvasGroup.alpha = smooth;

            popup.localScale =
                Vector3.Lerp(
                    Vector3.zero,
                    Vector3.one,
                    smooth
                );

            yield return null;
        }

        canvasGroup.alpha = 1f;

        popup.localScale = Vector3.one;
    }

    public void ClosePopup()
    {
        StopAllCoroutines();

        StartCoroutine(CloseAnimation());
    }

    IEnumerator CloseAnimation()
    {
        float t = 1;

        while (t > 0)
        {
            t -= Time.unscaledDeltaTime * speed;

            float smooth =
                Mathf.SmoothStep(0, 1, t);

            canvasGroup.alpha = smooth;

            popup.localScale =
                Vector3.Lerp(
                    Vector3.zero,
                    Vector3.one,
                    smooth
                );

            yield return null;
        }

        gameObject.SetActive(false);
    }
}