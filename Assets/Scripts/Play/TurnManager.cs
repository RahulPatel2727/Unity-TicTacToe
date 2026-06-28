using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
public class TurnManager : MonoBehaviour
{
    public CanvasGroup xTurnUI;
    public CanvasGroup xTurnBG;
    public CanvasGroup oTurnUI;
    public CanvasGroup oTurnBG;
    public CanvasGroup yourTurnUI;

    public GameObject xTurnObj;
    public GameObject oTurnObj;

    public float activeAlpha = 1f;
    public float inactiveAlpha = 0.3f;

    public static int currentTurn; // 0 = Player/X, 1 = O/AI

    bool isAI;
    public bool firstMoveStarted = false;

    void Start()
    {
        isAI =
            SceneManager.GetActiveScene().name == "play_ai";

        // 🔥 AI MODE
        if (isAI){
            if (xTurnObj != null)
                xTurnObj.SetActive(false);
            if (oTurnObj != null)
                oTurnObj.SetActive(false);

            if (yourTurnUI != null)
                yourTurnUI.gameObject.SetActive(true);

            yourTurnUI.alpha = inactiveAlpha;
        }
        else
        {
            // 🔥 PvP MODE
            if (yourTurnUI != null){
                yourTurnUI.gameObject.SetActive(false);
            }
            xTurnUI.alpha = 0;
            oTurnUI.alpha = 0;
            xTurnBG.alpha = 0;
            oTurnBG.alpha = 0;
        }

        Invoke(nameof(StartTurn), 3f);
    }

    public void StartTurn(){
            if (firstMoveStarted)
                return;

            firstMoveStarted = true;

            currentTurn = Random.Range(0, 2);

            UpdateUI();

            StartCoroutine(DelayedFirstMoveCheck());
    }
    IEnumerator DelayedFirstMoveCheck()
    {
        yield return new WaitForSeconds(0.2f);

        if (
            UnityEngine.SceneManagement.SceneManager
            .GetActiveScene().name == "play_ai"
        )
        {
            if (currentTurn == 1)
            {
                if (ComputerAI.instance != null)
                {
                    AIGameManager.instance
                        .SetBoardInteractable(false);

                    ComputerAI.instance.PlayMove();
                }
            }
        }
    }

    public void SwitchTurn()
    {
        currentTurn = 1 - currentTurn;

        UpdateUI();
    }
    void UpdateUI()
    {
        // 🔥 AI MODE UI
        if (isAI) {
            if (currentTurn == 0) {
                yourTurnUI.alpha = activeAlpha;
            }
            else{
                yourTurnUI.alpha = inactiveAlpha;
            }
            return;
        }

        // 🔥 PvP UI
        if (currentTurn == 0){
            xTurnUI.alpha = activeAlpha;
            oTurnUI.alpha = inactiveAlpha;
            xTurnBG.alpha = activeAlpha;
            oTurnBG.alpha = inactiveAlpha;
        }
        else{
            xTurnUI.alpha = inactiveAlpha;
            oTurnUI.alpha = activeAlpha;
            xTurnBG.alpha = inactiveAlpha;
            oTurnBG.alpha = activeAlpha;
        }
    }

    public void UpdateUIManually()
    {
        UpdateUI();
    }
}