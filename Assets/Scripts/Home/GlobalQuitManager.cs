using UnityEngine;
using UnityEngine.InputSystem;
public class GlobalQuitManager : MonoBehaviour
{
    public static GlobalQuitManager instance;

    public GameObject quitPopup;

    bool popupOpen = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        quitPopup.SetActive(false);

        Time.timeScale = 1f;
    }

    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (!popupOpen)
            {
                OpenPopup();
            }
            else
            {
                ResumeGame();
            }
        }
    }

    public void OpenPopup()
    {
        popupOpen = true;

        Time.timeScale = 0f;

        quitPopup.SetActive(true);
    }

    public void ResumeGame()
    {
        popupOpen = false;

        Time.timeScale = 1f;

        quitPopup.SetActive(false);
    }

    // public void QuitGame()
    // {
    //     Time.timeScale = 1f;

    //     if (AdManager.instance != null){
    //         AdManager.instance.ShowInterstitial();
    //     }

    //     Application.Quit();
    // }
    public void QuitGame(){
        Time.timeScale = 1f;
        if (AdManager.instance != null){
            AdManager.instance.ShowInterstitialAndQuit();
        }
        else{
            Application.Quit();
        }
    }
}