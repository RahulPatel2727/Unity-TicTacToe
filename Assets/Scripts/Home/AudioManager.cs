using UnityEngine;
public class AudioManager : MonoBehaviour {
    public static AudioManager instance;
    [Header("Sources")]
    public AudioSource musicSource; // 🎵 background music
    public AudioSource sfxSource;   // 🔊 sound effects

    [Header("Clips")]
    public AudioClip bgMusic;
    public AudioClip buttonClick;
    public AudioClip moveSound;
    public AudioClip winSound;
    public AudioClip drawSound;

    void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }
    void Start() {
        bool musicOn = PlayerPrefs.GetInt("music", 1) == 1;
        if (musicOn) {
            PlayMusic(); // 🔥 start music
        }
    }
    // 🔊 SFX (your old Play method, just using sfxSource now)
    public void Play(AudioClip clip) {
        if (clip != null && !sfxSource.mute)
            sfxSource.PlayOneShot(clip);
    }

    // 🎵 MUSIC
    public void PlayMusic() {
        if (bgMusic != null) {
            musicSource.clip = bgMusic;
            musicSource.loop = true;
            musicSource.Play();
        }
    }
    public void ToggleMusic(bool isOn) {
        if (musicSource == null) return;

        if (isOn) {
            if (!musicSource.isPlaying)
                PlayMusic(); // 🔥 start if not playing
            else
                musicSource.UnPause();
        }
        else {
            musicSource.Pause();
        }
    }

    public void ToggleSFX(bool value) {
        sfxSource.mute = !value;
    }
    public void PlayUI(AudioClip clip) {
        if (clip != null)
            sfxSource.PlayOneShot(clip); // 🔥 ALWAYS plays
    }
}