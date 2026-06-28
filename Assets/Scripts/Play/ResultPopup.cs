using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class ResultPopup : MonoBehaviour {
    public GameObject panel;
    public Sprite winBanner;
    public Sprite loseBanner;
    public Sprite drawBanner;
    public Image resultImage;
    public Sprite xWinSprite;
    public Sprite oWinSprite;
    public Sprite drawSprite;
    public CanvasGroup canvasGroup;
    public TransitionManager transitionManager;
    void Start() {
        // panel.SetActive(false); // ✅ hidden initially
    }
    public void ShowResult(string result) {
        if (AdManager.instance != null) {
            AdManager.instance.RegisterGameFinished();
        }

        panel.SetActive(true); // ✅ enable first
        resultImage.preserveAspect = true;


        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "play_ai") {
            if(result == "YOU WIN"){
                AudioManager.instance.Play(AudioManager.instance.winSound);
                HapticManager.instance.MediumTap();
            }
            else{
                AudioManager.instance.Play(AudioManager.instance.drawSound);
                HapticManager.instance.HeavyTap();
            }
            if (result == "YOU WIN"){
                resultImage.sprite = winBanner;
            }
            else if (result == "YOU LOSE"){
                resultImage.sprite = loseBanner;
            }
            else{
                resultImage.sprite = drawBanner;
            }
        }
        else{
            // 🔊 PLAY WIN / DRAW SOUND
            if (result == "X" || result == "O"){
                AudioManager.instance.Play(AudioManager.instance.winSound);
                HapticManager.instance.MediumTap();
            }
            else{
                AudioManager.instance.Play(AudioManager.instance.drawSound);
                HapticManager.instance.HeavyTap();
            }
            // set image
            if (result == "X")
                resultImage.sprite = xWinSprite;
            else if (result == "O")
                resultImage.sprite = oWinSprite;
            else
                resultImage.sprite = drawSprite;
        }

        StopAllCoroutines();
        StartCoroutine(PopupAnimation());
    }
    IEnumerator PopupAnimation() {
        panel.transform.localScale = Vector3.zero;
        float t = 0;
        while (t < 1) {
            t += Time.deltaTime * 4f;
            float scale = Mathf.SmoothStep(0, 1, t);
            panel.transform.localScale = Vector3.one * scale;
            yield return null;
        }
        panel.transform.localScale = Vector3.one;
    }
    IEnumerator CloseAnimation(System.Action onComplete) {
        float t = 1;
        while (t > 0) {
            t -= Time.deltaTime * 4f;
            float scale = Mathf.SmoothStep(0, 1, t);
            panel.transform.localScale = Vector3.one * scale;
            yield return null;
        }
        panel.transform.localScale = Vector3.zero;
        panel.SetActive(false);
        onComplete?.Invoke();
    }
    public void Replay() {
        AudioManager.instance.Play(AudioManager.instance.buttonClick); // 🔊
        HapticManager.instance.LightTap();
        StopAllCoroutines();
        StartCoroutine(CloseAnimation(() => {
            // GameManager.instance.RestartGame();
            if (GameManager.instance != null){
                GameManager.instance.RestartGame();
            }
            if (AIGameManager.instance != null){
                AIGameManager.instance.RestartGame();
            }
        }));
    }
    public void ExitToHome() {
        AudioManager.instance.Play(AudioManager.instance.buttonClick);
        HapticManager.instance.LightTap();
        StopAllCoroutines();
        StartCoroutine(CloseAnimation(() => {
            if (AdManager.instance != null) {
                AdManager.instance.ShowInterstitial();
            }
            transitionManager.LoadScene("home");
        }));
    }
}