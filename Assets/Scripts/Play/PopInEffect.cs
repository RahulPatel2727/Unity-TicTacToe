using System.Collections;
using UnityEngine;

public class PopInEffect : MonoBehaviour
{
    public float speed = 6f;
    public float delay = 0f;

    void OnEnable()
    {
        StopAllCoroutines();
        StartCoroutine(Animate());
    }

    IEnumerator Animate()
    {
        yield return new WaitForSeconds(delay);

        transform.localScale = Vector3.zero;

        float t = 0;

        while (t < 1)
        {
            t += Time.deltaTime * speed;

            float scale = Mathf.SmoothStep(0, 1, t);

            transform.localScale = Vector3.one * scale;

            yield return null;
        }

        transform.localScale = Vector3.one;
    }
}