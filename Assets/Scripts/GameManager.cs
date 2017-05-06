using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[AddComponentMenu("Managers/GameManager")]

public class GameManager : Singleton<GameManager> {
    [Header("Game Variables")]
    [Range(2,4)]
    public int numberOfPlayers = 4;

    [Header("Prefab References")]
    public GameObject PlayerPrefab;

    [Header("Debug Variables")]
    public List<PlayerController> Players; //Player #1 is at index 0

    public enum State { Null, Paused, Playing };
    public State state;

    void Awake(){
        DontDestroyOnLoad(transform.gameObject); //Don't destroy us on loading new scenes
    }

    // Use this for initialization
    void Start () {
        SpawnPlayers(numberOfPlayers);
    }
    
    // Update is called once per frame
    void Update () {
        
    }

    public void SpawnPlayers(int num){ //Spawn the players, given number of players to spawn

        //TODO: remove me, should no longer be needed
        //if(num < 2 || num > 4) { //If we're trying to spawn an invalid # of player
        //    Debug.LogError("Invalid number of players: " + num);
        //    return;
        //}

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
}
