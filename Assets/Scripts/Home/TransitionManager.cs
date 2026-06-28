// using UnityEngine;
// using UnityEngine.SceneManagement;
// using UnityEngine.Video;
// using System.Collections;

// public class TransitionManager : MonoBehaviour{
//     public GameObject fadePanel;
//     public VideoPlayer videoPlayer;
//     public CanvasGroup canvasGroup;
//     public float fadeDuration = 0.5f;
//     void Start(){
//         fadePanel.SetActive(false);
//         canvasGroup.alpha = 0;
//     }

//     public void LoadScene(string sceneName){
//         StartCoroutine(PlayTransition(sceneName));
//     }
//     IEnumerator PlayTransition(string sceneName){
//         fadePanel.SetActive(true);
//         // 🔥 Fade In
//         float t = 0;
//         while (t < fadeDuration){
//             t += Time.deltaTime;
//             canvasGroup.alpha = Mathf.Lerp(0, 1, t / fadeDuration);
//             yield return null;
//         }
//         canvasGroup.alpha = 1;
//         // 🔥 Prepare + Play Video
//         videoPlayer.Prepare();

//         while (!videoPlayer.isPrepared){
//             yield return null;
//         }
//         videoPlayer.Play();
//         // 🔥 transition duration
//         yield return new WaitForSeconds(2f);
//         // 🔥 DIRECTLY LOAD NEXT SCENE
//         SceneManager.LoadScene(sceneName);
//     }



// }




using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class TransitionManager : MonoBehaviour
{
    public GameObject fadePanel;
    public CanvasGroup fadeGroup;

    public float fadeDuration = 0.5f;

    void Start()
    {
        fadePanel.SetActive(false);
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(FadeOut(sceneName));
    }

    IEnumerator FadeOut(string sceneName)
    {
        // 🔥 enable panel first
        fadePanel.SetActive(true);

        // 🔥 start invisible
        fadeGroup.alpha = 0;

        float t = 0;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;

            fadeGroup.alpha = t / fadeDuration;

            yield return null;
        }

        fadeGroup.alpha = 1;

        SceneManager.LoadScene(sceneName);
    }
}