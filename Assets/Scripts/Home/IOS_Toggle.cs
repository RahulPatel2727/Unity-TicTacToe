using UnityEngine;
using UnityEngine.UI;

public class IOS_Toggle : MonoBehaviour
{
    public Toggle toggle;

    public GameObject onObject;
    public GameObject offObject;

    public string prefKey = "toggle";

    void Start() {
        int raw = PlayerPrefs.GetInt(prefKey, -1);
        Debug.Log(prefKey + " saved value = " + raw);

        bool saved = PlayerPrefs.GetInt(prefKey, 1) == 1;
        toggle.isOn = saved;
        toggle.onValueChanged.AddListener(OnSwitch);
        UpdateVisual(saved);
        ApplyLogic(saved);
        // if (prefKey == "haptics")
        // {
        //     // HapticManager.instance.hapticsEnabled = saved;
        //     HapticManager.instance.hapticsEnabled = isOn;
        // }
    }

    void OnSwitch(bool isOn) {
        // 🔊 play click sound
        if (AudioManager.instance != null)
            AudioManager.instance.PlayUI(AudioManager.instance.buttonClick);
        PlayerPrefs.SetInt(prefKey, isOn ? 1 : 0);
        PlayerPrefs.Save(); // ✅ ensure saved
        UpdateVisual(isOn);
        ApplyLogic(isOn);
    }

    void UpdateVisual(bool isOn) {
        if (onObject != null) onObject.SetActive(isOn);
        if (offObject != null) offObject.SetActive(!isOn);
    }

    void ApplyLogic(bool isOn) {
        if (prefKey == "music")
            AudioManager.instance.ToggleMusic(isOn);
        if (prefKey == "sound") {
            // 🔊 play sound BEFORE muting
            if (isOn)
                AudioManager.instance.ToggleSFX(true);
            else {
                AudioManager.instance.PlayUI(AudioManager.instance.buttonClick);
                AudioManager.instance.ToggleSFX(false);
                return;
            }
        }
        if (prefKey == "haptics"){
            Debug.Log("Haptics: " + isOn);
        }
        // if (prefKey == "haptics")
        // {
        //     HapticManager.instance.hapticsEnabled = isOn;
        // }
    }
}