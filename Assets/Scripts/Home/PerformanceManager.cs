using UnityEngine;

public class PerformanceManager : MonoBehaviour
{
    static bool initialized = false;

    void Awake()
    {
        if (initialized)
        {
            Destroy(gameObject);
            return;
        }

        initialized = true;

        DontDestroyOnLoad(gameObject);

        // Disable VSync
        QualitySettings.vSyncCount = 0;

        // High FPS
        Application.targetFrameRate = 120;
    }
}