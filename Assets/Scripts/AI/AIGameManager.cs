using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
public class AIGameManager : MonoBehaviour {
    public static AIGameManager instance;
    public TurnManager turnManager;
    public ResultPopup resultPopup;
    public LetsGoPopup letsGoPopup;
    public string[] board = new string[9];
    public bool gameOver = false;
    public bool gameStarted = false;

    void Start() {
        SetBoardInteractable(false);
        letsGoPopup.ShowPopup();
    }
    private void Awake() {
        instance = this;
    }
    public void SetBoardInteractable(bool value){
        ButtonClick[] buttons = FindObjectsByType<ButtonClick>();

        foreach (var b in buttons){
            b.GetComponent<UnityEngine.UI.Button>().interactable = value;
        }
    }
    public string GetTurn(int index) {
        if (gameOver) return "";
        // string value = (TurnManager.currentTurn == 0) ? "X" : "O";
        string value = "X";
        board[index] = value;
        CheckWin();
        if (!gameOver) {
            CheckDraw();
        }
        // if (!gameOver) {
        //     turnManager.SwitchTurn(); // ✅ ONLY HERE (LAST)
        // }
        if (!gameOver) {
            turnManager.SwitchTurn();
            // 🔥 AI TURN
            // if (TurnManager.currentTurn == 1) {
            //     SetBoardInteractable(false);
            //     ComputerAI.instance.PlayMove();
            // }
            if (TurnManager.currentTurn == 1) {
                SetBoardInteractable(false);
                StartCoroutine(DelayedAIMove());
            }
            else{
                SetBoardInteractable(true);
            }
        }
        return value;
    }

    public void CheckWin() {
        int[,] winPatterns = new int[,]{{0,1,2},{3,4,5},{6,7,8},{0,3,6},{1,4,7},{2,5,8},{0,4,8},{2,4,6}};

        for (int i = 0; i < 8; i++) {
            int a = winPatterns[i,0];
            int b = winPatterns[i,1];
            int c = winPatterns[i,2];

            if (!string.IsNullOrEmpty(board[a]) && board[a] == board[b] && board[b] == board[c]){
                Debug.Log("Winner: " + board[a]);
                gameOver = true;
                // resultPopup.ShowResult(board[a]); // 👈 THIS LINE
                if (board[a] == "X"){
                    resultPopup.ShowResult("YOU WIN");
                }
                else{
                    resultPopup.ShowResult("YOU LOSE");
                }
                break;
            }
        }
    }
    public void CheckDraw() {
        for (int i = 0; i < board.Length; i++) {
            if (string.IsNullOrEmpty(board[i])) return;
        }
        Debug.Log("Draw");
        gameOver = true;
        // resultPopup.ShowResult("Draw");
        resultPopup.ShowResult("DRAW");
    }
    public void RestartGame()
    {
        Debug.Log("RESTART CLICKED");
        // 🔥 Clear board data
        gameStarted = false;
        for (int i = 0; i < board.Length; i++) {
            board[i] = "";
        }

        gameOver = false;
        // turnManager.firstMoveStarted = false;

        // 🔥 Reset all buttons
        ButtonClick[] buttons =
            FindObjectsByType<ButtonClick>();

        foreach (var b in buttons)
        {
            b.ResetButton();
        }

        // 🔥 Random first turn
        TurnManager.currentTurn =
            Random.Range(0, 2);

        // 🔥 Update turn UI
        turnManager.UpdateUIManually();
        gameStarted = true;
        if (TurnManager.currentTurn == 1){
            SetBoardInteractable(false);
            StartCoroutine(DelayedAIStart());
        }
        else{
            SetBoardInteractable(true);
        }
    }
    IEnumerator DelayedAIStart(){
        // yield return new WaitForSeconds(0.1f);
        yield return new WaitForSeconds(0.35f);
        // ComputerAI.instance.PlayMove();
        if (ComputerAI.instance != null) {
            ComputerAI.instance.PlayMove();
        }
    }
    IEnumerator DelayedAIMove(){
        // yield return new WaitForSeconds(0.1f);
        yield return new WaitForSeconds(0.35f);
        // ComputerAI.instance.PlayMove();
        if (ComputerAI.instance != null) {
            ComputerAI.instance.PlayMove();
        }
    }
}