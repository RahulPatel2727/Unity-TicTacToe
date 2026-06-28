using UnityEngine;
using UnityEngine.UI;

public class ButtonClick : MonoBehaviour
{
    public Image img;        // child image (X/O)
    public Sprite xSprite;
    public Sprite oSprite;

    private Button btn;
    public int index;
    private void Start() {
        btn = GetComponent<Button>();
        // STRICT check
        if (img == null) {
            Debug.LogError("Image not assigned in Inspector for " + gameObject.name);
            return;
        }
        img.enabled = false;
    }
    public void OnClick() {
        AudioManager.instance.Play(AudioManager.instance.moveSound);
        HapticManager.instance.LightTap();
        if (AIGameManager.instance != null) {
            if (!AIGameManager.instance.gameStarted)
                return;
            // 🔥 Only player can click in AI mode
            if (TurnManager.currentTurn != 0)
                return;
        }
        string turn = "";
        // 🔥 PvP mode
        if (GameManager.instance != null) {
            if (img.enabled || GameManager.instance.gameOver)
                return;
            turn = GameManager.instance.GetTurn(index);
        }
        // 🔥 AI mode
        else if (AIGameManager.instance != null) {
            if (img.enabled || AIGameManager.instance.gameOver)
                return;
            turn = AIGameManager.instance.GetTurn(index);
        }
        else {
            return;
        }
        // 🔥 Set sprite
        if (turn == "X") {
            img.sprite = xSprite;
            img.transform.localScale =
                new Vector3(1.1f, 1.1f, 1f);
        }
        else {
            img.sprite = oSprite;
            img.transform.localScale =
                new Vector3(1.5f, 1.5f, 1f);
        }
        img.enabled = true;
        btn.interactable = false;
    }

    public void ResetButton() {
        img.sprite = null;
        img.enabled = false;
        btn.interactable = true;
    }
    public void AIPlay() {
        Debug.Log("AI PLAY RUNNING");
        if (img.enabled)
            return;

        AudioManager.instance.Play(AudioManager.instance.moveSound);
        HapticManager.instance.LightTap();
        img.sprite = oSprite;

        img.transform.localScale =
            new Vector3(1.5f, 1.5f, 1f);

        img.enabled = true;

        btn.interactable = false;

        // 🔥 Update board
        AIGameManager.instance.board[index] = "O";

        // 🔥 Check game
        AIGameManager.instance.CheckWin();

        if (!AIGameManager.instance.gameOver)
        {
            AIGameManager.instance.CheckDraw();
        }

        // 🔥 Back to player
        if (!AIGameManager.instance.gameOver)
        {
            TurnManager.currentTurn = 0;

            AIGameManager.instance.turnManager.UpdateUIManually();

            AIGameManager.instance.SetBoardInteractable(true);
        }
    }
}