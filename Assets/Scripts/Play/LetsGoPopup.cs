using System.Collections;
using UnityEngine;
public class LetsGoPopup : MonoBehaviour{
    public CanvasGroup canvasGroup;
    // 🔥 shorter popup time
    public float showTime = 2f;
    public TurnManager turnManager;
    public GameObject popup;
    public GameObject exitButton;
    private bool alreadyShown = false;
    public void ShowPopup(){
        if (alreadyShown) return;
        alreadyShown = true;
        gameObject.SetActive(true);
        popup.SetActive(true);
        canvasGroup.alpha = 0;
        popup.transform.localScale = Vector3.zero;
        StopAllCoroutines();
        StartCoroutine(PlayPopup());
    }
    IEnumerator PlayPopup() {
        float t = 0;
        // 🔥 Fade IN
        while (t < 1) {
            t += Time.deltaTime * 2f;
            float smooth = Mathf.Clamp01(t);
            canvasGroup.alpha = smooth;
            popup.transform.localScale =
                Vector3.Lerp(Vector3.zero, Vector3.one, smooth);
            yield return null;
        }
        canvasGroup.alpha = 1;
        popup.transform.localScale = Vector3.one;
        Debug.Log("WAIT START");
        // 🔥 ACTUAL WAIT
        yield return new WaitForSecondsRealtime(showTime);
        Debug.Log("WAIT END");
        // 🔥 Fade OUT
        t = 1;
        while (t > 0) {
            t -= Time.deltaTime * 2f;
            float smooth = Mathf.Clamp01(t);
            canvasGroup.alpha = smooth;
            popup.transform.localScale =
                Vector3.Lerp(Vector3.zero, Vector3.one, smooth);
            yield return null;
        }
        canvasGroup.alpha = 0;
        popup.transform.localScale = Vector3.zero;
        if (turnManager != null)
            turnManager.StartTurn();
        if (GameManager.instance != null) {
            GameManager.instance.SetBoardInteractable(true);
        }
        if (AIGameManager.instance != null)
        {
            AIGameManager.instance.SetBoardInteractable(true);
        }
        if (exitButton != null)
            exitButton.SetActive(true);
        popup.SetActive(false);
        if (AIGameManager.instance != null) {
            AIGameManager.instance.gameStarted = true;
        }
    }
}