using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverController : MonoBehaviour {
    bool isGameEnding;
    float roundTimer;
    float endGameTimer;
    public Vector3 StartGameCameraPosition;
    public Vector3 EndGameCameraPosition;
    public AnimationCurve EndGameCameraLerp;

    [Header("Component References")]
	public CanvasGroup GameOverCanvasGroup;
    public Camera gameOverCamera;

    // Use this for initialization
    void Start () {

        gameOverCamera.gameObject.SetActive(false);
        isGameEnding = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameEnding)
        {
            gameOverCamera.transform.position = Vector3.Lerp(StartGameCameraPosition, EndGameCameraPosition, EndGameCameraLerp.Evaluate(endGameTimer));
            endGameTimer += Time.deltaTime;
            if (EndGameCameraLerp.Evaluate(endGameTimer) >= 1)
            {
                SharedUIManager.Instance.ShowGameOverMenu();
            }
        }
    }

	public void GoToMainMenu(){
		GameManager.Instance.LoadLevel("MainMenu");
	}

	public void Show(){
		GameOverCanvasGroup.alpha = 1;
	}

    public void EndGame(int winner)
    {
        isGameEnding = true;
        SharedUIManager.Instance.HideTimer();
        gameOverCamera.gameObject.SetActive(true);
        gameOverCamera.depth = 1; //Set it to render at the front
        GameManager.Instance.state = GameManager.State.EndGame;

        if (winner == -1)
        { //Time ran out

        }
    }
}
