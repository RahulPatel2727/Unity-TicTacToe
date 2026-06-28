using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class GameManager : MonoBehaviour {
    public static GameManager instance;
    public TurnManager turnManager;
    public ResultPopup resultPopup;
    public LetsGoPopup letsGoPopup;
    public string[] board = new string[9];
    public bool gameOver = false;

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
        string value = (TurnManager.currentTurn == 0) ? "X" : "O";
        board[index] = value;
        CheckWin();
        if (!gameOver) {
            CheckDraw();
        }
        if (!gameOver) {
            turnManager.SwitchTurn(); // ✅ ONLY HERE (LAST)
        }
        return value;
    }

    void CheckWin() {
        int[,] winPatterns = new int[,]{{0,1,2},{3,4,5},{6,7,8},{0,3,6},{1,4,7},{2,5,8},{0,4,8},{2,4,6}};

        for (int i = 0; i < 8; i++) {
            int a = winPatterns[i,0];
            int b = winPatterns[i,1];
            int c = winPatterns[i,2];

            if (!string.IsNullOrEmpty(board[a]) && board[a] == board[b] && board[b] == board[c]){
                Debug.Log("Winner: " + board[a]);
                gameOver = true;
                resultPopup.ShowResult(board[a]); // 👈 THIS LINE
                break;
            }
        }
    }
    void CheckDraw() {
        for (int i = 0; i < board.Length; i++) {
            if (string.IsNullOrEmpty(board[i])) return;
        }
        Debug.Log("Draw");
        gameOver = true;
        resultPopup.ShowResult("Draw");
    }
    public void RestartGame() {
        Debug.Log("RESTART CLICKED"); // 👈 ADD THIS
        for (int i = 0; i < board.Length; i++){
            board[i] = "";
        }
        gameOver = false;
        TurnManager.currentTurn = Random.Range(0, 2); // or 0 if you want fixed start
        ButtonClick[] buttons = FindObjectsByType<ButtonClick>();
        foreach (var b in buttons){
            b.ResetButton();
        }
    }
}