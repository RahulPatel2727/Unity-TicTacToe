using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeManager : MonoBehaviour {
    public TransitionManager transitionManager;
    public HowToPlayPopup howToPlayPopup;
    public void PlayGame() {
        AudioManager.instance.PlayUI(AudioManager.instance.buttonClick);
        HapticManager.instance.LightTap();
        PlayerPrefs.SetInt("Mode", 0);
        transitionManager.LoadScene("play_xo");
    }

    public void VsComputer() {
        AudioManager.instance.PlayUI(AudioManager.instance.buttonClick);
        HapticManager.instance.LightTap();
        PlayerPrefs.SetInt("Mode", 1);
        transitionManager.LoadScene("play_ai");
    }
    public SettingsPopup settingsPopup;
    public void HowToPlay() {
        AudioManager.instance.PlayUI(AudioManager.instance.buttonClick);
        HapticManager.instance.LightTap();
        howToPlayPopup.Open(); // ✅ called from active object
    }
    public void ExitGame() {
        AudioManager.instance.PlayUI(AudioManager.instance.buttonClick);
        HapticManager.instance.LightTap();
        Application.Quit();
        Debug.Log("Game Closed");
    }
}