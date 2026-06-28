// using UnityEngine;
// public class HapticManager : MonoBehaviour{
//     public static HapticManager instance;
//     public bool hapticsEnabled = true;
//     void Awake(){
//         instance = this;
//     }

//     public void LightTap()
//     {
//         if (!hapticsEnabled)
//             return;

//     #if UNITY_ANDROID && !UNITY_EDITOR
//         Vibration.Vibrate(25);
//     #endif
//     }

//     public void MediumTap()
//     {
//         if (!hapticsEnabled)
//             return;

//     #if UNITY_ANDROID && !UNITY_EDITOR
//         Vibration.Vibrate(50);
//     #endif
//     }

//     public void HeavyTap()
//     {
//         if (!hapticsEnabled)
//             return;

//     #if UNITY_ANDROID && !UNITY_EDITOR
//         Vibration.Vibrate(100);
//     #endif
//     }
// }

using UnityEngine;

public class HapticManager : MonoBehaviour
{
    public static HapticManager instance;

    void Awake()
    {
        instance = this;
    }

    bool HapticsEnabled()
    {
        return PlayerPrefs.GetInt("haptics", 1) == 1;
    }

    public void LightTap()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        if (!HapticsEnabled()) return;

        Vibration.Vibrate(30);
#endif
    }

    public void MediumTap()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        if (!HapticsEnabled()) return;

        Vibration.Vibrate(60);
#endif
    }

    public void HeavyTap()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        if (!HapticsEnabled()) return;

        Vibration.Vibrate(120);
#endif
    }
}