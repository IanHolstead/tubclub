using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[AddComponentMenu("Managers/GameManager")]

public class GameManager : Singleton<GameManager> {
    [Header("Game Variables")]
    [Range(2,4)]
    public int numberOfPlayers = 4;
    public float RoundTime;
    public AnimationCurve EndGameCameraLerp;

    [Header("Prefab References")]
    public GameObject PlayerPrefab;

    [Header("Debug Variables")]
    public List<PlayerController> Players; //Player #1 is at index 0

    public enum State { Null, Paused, Playing, MainMenu, Spawning, StartingRound, EndGame };
    public State state;

    float roundTimer;
    Camera gameOverCamera;
    float endGameTimer;
    public Vector3 EndGameCameraPosition;

    void Awake(){
        DontDestroyOnLoad(transform.gameObject); //Don't destroy us on loading new scenes
    }

    // Use this for initialization
    void Start () {
        //SpawnPlayers(numberOfPlayers);
    }
    
    // Update is called once per frame
    void Update () {
        switch(state){
            case State.Playing:
                UpdateRound();
            break;
            case State.EndGame:
                gameOverCamera.transform.position = Vector3.Lerp(new Vector3(0, -9, 0), EndGameCameraPosition, EndGameCameraLerp.Evaluate(endGameTimer));
                endGameTimer += Time.deltaTime;
                if(EndGameCameraLerp.Evaluate(endGameTimer) >= 1){
                    SharedUIManager.Instance.ShowGameOverMenu();
                }
            break;
        }
        
    }

    void OnLevelWasLoaded(){ //Called once level was loaded (duh)
        switch(state){ //Depending on state
            case State.Spawning: //If we're supposed to be spawning,
                gameOverCamera = GameObject.Find("GameOver Camera").GetComponent<Camera>(); //Get the camera reference
                SpawnPlayers(numberOfPlayers); //Spawn the players
            break;
        }
    }

    void UpdateRound(){
        roundTimer += Time.deltaTime;

        SharedUIManager.Instance.UpdateTimer(roundTimer, RoundTime);

        if(roundTimer > RoundTime){
            EndGame(-1);
            return;
        }

        PlayerController winner = null;
        int aliveCounter = 0;
        foreach(PlayerController player in Players){ //Foreach player,
            if(player.state == PlayerController.State.Alive){ //If that player is alive
                winner = player; //They are the current winner
                aliveCounter ++;
            }
        }

        if(aliveCounter == 1){ //If only one player is alive, then we know winner is set to them
            //TODO: End game, show winner
            EndGame(winner.PlayerNum);
            return;
        }
    }

    public void SpawnPlayers(int num){ //Spawn the players, given number of players to spawn

        Players = new List<PlayerController>(); //Clear the list

        float degPerPlayer = 2 * Mathf.PI / num; //Calculate angle between each player spawn in radians
        float spawnRadius = 10;

        for(int i = 1; i < num+1; i++){ //For each we want to
            Vector3 newPos = new Vector3(Mathf.Sin(degPerPlayer * i) * spawnRadius, 0, Mathf.Cos(degPerPlayer * i) * spawnRadius); //Calculate their spawn position
            GameObject spawnedPlayer = Instantiate(PlayerPrefab, newPos, Quaternion.identity); //Spawn the player object
            Players.Add(spawnedPlayer.GetComponent<PlayerController>()); //Add the newly spawned player to the players list
            Players[i - 1].Setup(i); //Tell them to set themselves up
        }

        //Setup viewports
        switch(num){ //Depending on the number of players,
            case 2:
                Players[0].camera.rect = new Rect(0.0f, 0.0f, 0.5f, 1.0f);
                Players[1].camera.rect = new Rect(0.5f, 0.0f, 0.5f, 1.0f);
            break;

            case 3:
                Players[0].camera.rect = new Rect(0.0f, 0.5f, 1.0f, 0.5f);
                Players[1].camera.rect = new Rect(0.0f, 0.0f, 0.5f, 0.5f);
                Players[2].camera.rect = new Rect(0.5f, 0.0f, 0.5f, 0.5f);
             break;

            case 4:
                Players[0].camera.rect = new Rect(0.0f, 0.5f, 0.5f, 0.5f);
                Players[1].camera.rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
                Players[2].camera.rect = new Rect(0.0f, 0.0f, 0.5f, 0.5f);
                Players[3].camera.rect = new Rect(0.5f, 0.0f, 0.5f, 0.5f);
            break;
        }

        state = State.StartingRound;
        StartRound(); //Then start the round
    }

    public void StartRound(){
        roundTimer = 0; //Reset timer to 0
        state = State.Playing; //Now we're playing!
        gameOverCamera.gameObject.SetActive(false);
        SharedUIManager.Instance.ShowTimer();
    }

    public void EndGame(int winner){
        SharedUIManager.Instance.HideTimer();
        gameOverCamera.gameObject.SetActive(true);
        gameOverCamera.depth = 1; //Set it to render at the front
        state = State.EndGame;

        if(winner == -1){ //Time ran out
            
        }
    }

    public void LoadLevel(string level){
        SceneManager.LoadScene(level);
    }

    public State GetState(){
        return state;
    }

    public void SetState(State newState){
        state = newState;
    }
    public void StartGame(int players){
        numberOfPlayers = players; //Set the number of players,
        state = State.Spawning; //Set the game state to be spawning on level load,
        LoadLevel("MainGame"); //Load the main game scene
    }
}
