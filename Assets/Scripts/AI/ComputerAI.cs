using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerAI : MonoBehaviour
{
    public static ComputerAI instance;

    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void PlayMove()
    {
        Debug.Log("AI STARTED");
        StartCoroutine(PlayComputerMove());
    }

    // IEnumerator PlayComputerMove()
    // {
    //     // AI thinking delay
    //     yield return new WaitForSeconds(0.6f);

    //     if (AIGameManager.instance == null)
    //         yield break;

    //     List<int> empty = new List<int>();

    //     for (int i = 0; i < AIGameManager.instance.board.Length; i++)
    //     {
    //         if (string.IsNullOrEmpty(AIGameManager.instance.board[i]))
    //         {
    //             empty.Add(i);
    //         }
    //     }

    //     if (empty.Count == 0)
    //         yield break;

    //     int randomIndex =
    //         empty[Random.Range(0, empty.Count)];

    //     // ButtonClick[] buttons =
    //     //     FindObjectsByType<ButtonClick>(FindObjectsSortMode.None);
    //     ButtonClick[] buttons = FindObjectsByType<ButtonClick>();

    //     foreach (ButtonClick btn in buttons)
    //     {
    //         if (btn.index == randomIndex)
    //         {
    //             // btn.OnClick();
    //             Debug.Log("AI CLICKING: " + randomIndex);
    //             btn.AIPlay();
    //             // TurnManager.currentTurn = 0;
    //             // AIGameManager.instance.turnManager.UpdateUIManually();
    //             // AIGameManager.instance.SetBoardInteractable(true);
    //             break;
    //         }
    //     }
    // }
    IEnumerator PlayComputerMove(){
        yield return new WaitForSeconds(0.6f);
        if (AIGameManager.instance == null)
            yield break;
        string[] board = AIGameManager.instance.board;
        int move = -1;
        // 🔥 Count filled cells
        int filled = 0;
        for (int i = 0; i < board.Length; i++){
            if (!string.IsNullOrEmpty(board[i])){
                filled++;
            }
        }
        // 🔥 FIRST AI MOVE RANDOM
        // if (filled <= 1){
        //     List<int> empty = new List<int>();
        //     for (int i = 0; i < board.Length; i++){
        //         if (string.IsNullOrEmpty(board[i])){
        //             empty.Add(i);
        //         }
        //     }
        //     move = empty[Random.Range(0, empty.Count)];
        // }
        // 🔥 FIRST AI MOVE RANDOM
        if (filled == 0) {
            List<int> empty = new List<int>();
            for (int i = 0; i < board.Length; i++){
                if (string.IsNullOrEmpty(board[i])){
                    empty.Add(i);
                }
            }
            // yield return new WaitForSeconds(0.1f);
            yield return new WaitForSeconds(0.35f);
            move = empty[Random.Range(0, empty.Count)];
        }
        else{
            // 🔥 WIN if possible
            move = FindBestMove("O");
            // 🔥 BLOCK player
            if (move == -1){
                move = FindBestMove("X");
            }
            // 🔥 TAKE CENTER
            if (move == -1 && string.IsNullOrEmpty(board[4])){
                move = 4;
            }
            // 🔥 RANDOM fallback
            if (move == -1){
                List<int> empty = new List<int>();
                for (int i = 0; i < board.Length; i++){
                    if (string.IsNullOrEmpty(board[i])){
                        empty.Add(i);
                    }
                }
                if (empty.Count == 0)
                    yield break;
                move = empty[Random.Range(0, empty.Count)];
            }
        }
        ButtonClick[] buttons = FindObjectsByType<ButtonClick>();
        foreach (ButtonClick btn in buttons){
            if (btn.index == move){
                btn.AIPlay();
                break;
            }
        }
    }
    int FindBestMove(string symbol){
        string[] board = AIGameManager.instance.board;
        int[,] winPatterns = new int[,]{
            {0,1,2},
            {3,4,5},
            {6,7,8},
            {0,3,6},
            {1,4,7},
            {2,5,8},
            {0,4,8},
            {2,4,6}
        };
        for (int i = 0; i < 8; i++){
            int a = winPatterns[i, 0];
            int b = winPatterns[i, 1];
            int c = winPatterns[i, 2];
            // 🔥 Two same + one empty
            if (board[a] == symbol && board[b] == symbol && string.IsNullOrEmpty(board[c])){
                return c;
            }
            if (board[a] == symbol && board[c] == symbol && string.IsNullOrEmpty(board[b])){
                return b;
            }
            if (board[b] == symbol && board[c] == symbol && string.IsNullOrEmpty(board[a])){
                return a;
            }
        }
        return -1;
    }
}