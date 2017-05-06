﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;
using ControlWrapping;

[AddComponentMenu("Controllers/PlayerController")]

[RequireComponent(typeof(LineRenderer))]
public class PlayerController : MonoBehaviour {

    [Header("Physics")]
    public AnimationCurve RotationSpeedOverRadius; //Left, 0 | Right, 1 Sample this curve for rotation speed in degrees per second
    public float radius; //Radius of our orbit, distance from 0, 0
    public float radiusVelocity; //Positive => increasing radius

    [Header("Prefab References")]
    public GameObject HarpoonPrefab;

    [Header("Player Variables")]
    public int PlayerNum; //[1-4]

    [Header("Camera Variables")]
    public float verticalRange = 45f;
    public float horizontalRange = 90f;

    public float cameraX = 0;
    public float cameraY = 0;

    [Header("Component References")]
    public Camera camera;
    public GameObject UICanvas;

    LineRenderer lr;

    Gamepad gamepad;

    public enum State { Null, Paused, Alive, Dead };
    public State state;

    void Awake(){
        lr = GetComponent<LineRenderer>();
    }

    // Use this for initialization
    void Start () {

    }

    public Vector3 PlotTrajectoryAtTime (Vector3 start, Vector3 startVelocity, float time) {
        return start + startVelocity*time + Physics.gravity*time*time*0.5f;
    }

    public void PlotTrajectory (Vector3 start, Vector3 startVelocity, float timestep, float maxTime) {
        Vector3 prev = start;
        for (int i=1;;i++) {
            float t = timestep*i;
            if (t > maxTime) break;
            Vector3 pos = PlotTrajectoryAtTime (start, startVelocity, t);
            if (Physics.Linecast (prev,pos)) break;
            Debug.DrawLine (prev,pos,Color.red);
            prev = pos;
        }
    }

    // Update is called once per frame
 void Update () {
        // Debug.Log(gamepad.GetButton(ActionKeyCode.GamepadA));
    switch (GameManager.Instance.state){
        case GameManager.State.Playing:

        UpdateSteering();
        UpdatePhysics();
        UpdateCamera();

        PlotTrajectory(transform.position + Vector3.up * 3, camera.transform.forward * 30000, 0.01f, 10.0f);
        if(gamepad.GetButton(ActionKeyCode.GamepadA)){
            HarpoonController testSpawn = Instantiate(HarpoonPrefab, transform.position + Vector3.up * 3, Quaternion.identity).GetComponent<HarpoonController>();
            testSpawn.Fire(PlayerNum, camera.transform.forward * 30000);
        }

        break;
    }
}

    public void Setup(int playerNum){ //Public call to setup our player
        this.PlayerNum = playerNum; //Set our player number
        this.name = "Player " + PlayerNum; //Set our name
        this.state = State.Alive;
        gamepad = ControllerManager.instance.RequestSpecificGamepad(PlayerNum-1); //Get our gamepad reference
        SetLayerRecursive(UICanvas.transform, "UI Player " + PlayerNum); //Set the player's UI's layer
        
        //Turn off all the layer masks for each player UI
        camera.cullingMask &= ~(1 << LayerMask.NameToLayer("UI Player 1"));
        camera.cullingMask &= ~(1 << LayerMask.NameToLayer("UI Player 2"));
        camera.cullingMask &= ~(1 << LayerMask.NameToLayer("UI Player 3"));
        camera.cullingMask &= ~(1 << LayerMask.NameToLayer("UI Player 4"));

        //Turn on ONLY our layer mask
        camera.cullingMask |= (1 << LayerMask.NameToLayer("UI Player " + PlayerNum ));
    }

    public void HitByHarpoon(){
        this.state = State.Dead;
    }

    void SetLayerRecursive(Transform obj, string layerName){
        obj.gameObject.layer = LayerMask.NameToLayer("UI Player " + PlayerNum);
        foreach(Transform child in obj.transform){
            SetLayerRecursive(child, layerName);
        }
    }

    float nfmod(float a, float b){
        return a - b * Mathf.Floor(a / b);
    }

    void UpdatePhysics(){
        radius = Mathf.Sqrt(Mathf.Pow(transform.position.x, 2) + Mathf.Pow(transform.position.z, 2)); //Get radius
        radius += radiusVelocity * Time.deltaTime;
        float angle = Mathf.Atan2(transform.position.x, transform.position.z) * Mathf.Rad2Deg; //Get our angle relative to 0,0
        if(angle < 0){ //If our angle is negative, we're under the x-axis
            angle += 360; //Add 360 degrees
        }
        float deltaAngle = RotationSpeedOverRadius.Evaluate(radius); //Sample our curve to get our change in angle
        angle += deltaAngle; //Add the delta
        angle = angle * Mathf.Deg2Rad; //Convert to radians
        Vector3 newPos = new Vector3(Mathf.Sin(angle) * radius, 0, Mathf.Cos(angle) * radius); //Calculate their new position
        transform.position = newPos; //Set the new position
        transform.rotation = Quaternion.AngleAxis((angle * Mathf.Rad2Deg) + 90, Vector3.up);
    }

    void UpdateSteering(){
        radiusVelocity = gamepad.GetAxis(AxisCode.GamepadAxisLeftX) * 50;
    }

    void UpdateCamera()
    {
        float newCameraX = gamepad.GetAxis(AxisCode.GamepadAxisRightX);
        float newCameraY = gamepad.GetAxis(AxisCode.GamepadAxisRightY);

        //TODO: this should depend on delta time
        newCameraX = Mathf.Lerp(cameraX, GameFunctions.MapRange(newCameraX, 0, 1, 0, horizontalRange), .4f);
        newCameraY = Mathf.Lerp(cameraY, GameFunctions.MapRange(newCameraY, 0, 1, 0, verticalRange), .4f);

        camera.transform.RotateAround(transform.position, Vector3.up, newCameraX - cameraX);
        camera.transform.RotateAround(transform.position, camera.transform.right, newCameraY - cameraY);

        cameraX = newCameraX;
        cameraY = newCameraY;
    }
}
