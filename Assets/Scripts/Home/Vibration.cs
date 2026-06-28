using UnityEngine;

// public static class Vibration
// {
//     public static void Vibrate(long milliseconds)
//     {
// #if UNITY_ANDROID && !UNITY_EDITOR

//         using (AndroidJavaClass unityPlayer =
//             new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
//         {
//             AndroidJavaObject currentActivity =
//                 unityPlayer.GetStatic<AndroidJavaObject>(
//                     "currentActivity");

//             AndroidJavaObject vibrator =
//                 currentActivity.Call<AndroidJavaObject>(
//                     "getSystemService",
//                     "vibrator");

//             if (vibrator != null)
//             {
//                 vibrator.Call("vibrate", milliseconds);
//             }
//         }

// #endif
//     }
// }


public static class Vibration
{
    public static void Vibrate(long milliseconds)
    {
#if UNITY_ANDROID && !UNITY_EDITOR

        try
        {
            Handheld.Vibrate();
        }
        catch (System.Exception e)
        {
            Debug.Log("Vibration Error: " + e.Message);
        }

#endif
    }
}