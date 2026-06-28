using UnityEngine;
using GoogleMobileAds.Api;
using UnityEngine.SceneManagement;

public class AdManager : MonoBehaviour{
    public static AdManager instance;
    private BannerView bannerView;
    private InterstitialAd interstitialAd;
    // public TransitionManager transitionManager;

    private int gameCount = 0;
    void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
            return;
        }
        MobileAds.Initialize(initStatus => {
            LoadBannerAd();
            LoadInterstitialAd();
        });
    }

    void OnEnable() {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable(){
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode){
        if (scene.name == "home"){
            ShowBanner();
        } else{
            HideBanner();
        }
    }

    void LoadBannerAd(){
        #if UNITY_ANDROID
            string adUnitId = "ca-app-pub-3940256099942544/6300978111";
        #elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-3940256099942544/2934735716";
        #else
            string adUnitId = "unused";
        #endif
        // Small professional banner
        bannerView = new BannerView(
            adUnitId,
            AdSize.Banner,
            AdPosition.Bottom
        );
        AdRequest request = new AdRequest();
        bannerView.LoadAd(request);
        bannerView.Show();
    }

    public void ShowBanner(){
        if (bannerView != null){
            bannerView.Show();
        }
    }

    public void HideBanner(){
        if (bannerView != null){
            bannerView.Hide();
        }
    }

    private void OnDestroy(){
        if (bannerView != null){
            bannerView.Destroy();
        }
    }
    void LoadInterstitialAd() {
        #if UNITY_ANDROID
            string adUnitId = "ca-app-pub-3940256099942544/1033173712";
        #elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-3940256099942544/4411468910";
        #else
            string adUnitId = "unused";
        #endif

        InterstitialAd.Load(adUnitId, new AdRequest(), (InterstitialAd ad, LoadAdError error) => {
            if (error != null || ad == null) {
                Debug.Log("Interstitial load failed");
                return;
            }
            interstitialAd = ad;
        });
    }

    public void ShowInterstitialAndQuit(){
        if (interstitialAd != null && interstitialAd.CanShowAd()){
            interstitialAd.OnAdFullScreenContentClosed += () =>{
                Application.Quit();
            };
            interstitialAd.Show();
            interstitialAd = null;
            LoadInterstitialAd();
        }
        else{
            Application.Quit();
        }
    }

    // public void ShowInterstitialAndLoadScene(string sceneName){
    //     if (interstitialAd != null && interstitialAd.CanShowAd()){
    //         interstitialAd.OnAdFullScreenContentClosed += () =>{
    //             // SceneManager.LoadScene(sceneName);
    //             transitionManager.LoadScene(sceneName);
    //             LoadInterstitialAd();
    //         };

    //         interstitialAd.Show();
    //     }
    //     else{
    //         // SceneManager.LoadScene(sceneName);
    //         transitionManager.LoadScene(sceneName);
    //     }
    // }
    public void ShowInterstitialAndLoadScene(string sceneName) {
        if (interstitialAd != null && interstitialAd.CanShowAd()){
            interstitialAd.OnAdFullScreenContentClosed += () => {
                Debug.Log("AD CLOSED");
                TransitionManager tm = FindAnyObjectByType<TransitionManager>();
                if (tm != null) {
                    tm.LoadScene(sceneName);
                }
                else {
                    SceneManager.LoadScene(sceneName);
                }
                LoadInterstitialAd();
            };
            interstitialAd.Show();
            interstitialAd = null;
        }
        else {
            TransitionManager tm = FindAnyObjectByType<TransitionManager>();
            if (tm != null) {
                tm.LoadScene(sceneName);
            }
            else {
                SceneManager.LoadScene(sceneName);
            }
        }
    }
        
    public void ShowInterstitial() {
        if (interstitialAd != null && interstitialAd.CanShowAd()){
            interstitialAd.Show();
            interstitialAd = null;
            LoadInterstitialAd();
        }
    }
    public void RegisterGameFinished(){
        gameCount++;
        Debug.Log("Games Played: " + gameCount);
        if (gameCount % 5 == 0) {
            ShowInterstitial();
        }
    }
}