using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;
using ControlWrapping;

public class PlayerController : MonoBehaviour {

    [Header("Physics")]
    public AnimationCurve RotationSpeedOverRadius; //Left, 0 | Right, 1 Sample this curve for rotation speed in degrees per second
    public float radius; //Radius of our orbit, distance from 0, 0
    public float radiusVelocity; //Positive => increasing radius

    [Header("Player Variables")]
    public int PlayerNum; //[1-4]

    [Header("Component References")]
    public Camera camera;

    Gamepad gamepad;

    public enum State { Null, Paused, Playing };
    public State state;

    // Use this for initialization
    void Start () {

    }
    
    // Update is called once per frame
    void Update () {
        // Debug.Log(gamepad.GetButton(ActionKeyCode.GamepadA));
        switch(GameManager.Instance.state){
            case GameManager.State.Playing:
                UpdateSteering();
                UpdatePhysics();
                UpdateCamera();
            break;
        }
    }

    public void Setup(int playerNum){ //Public call to setup our player
        this.PlayerNum = playerNum; //Set our player number
        this.name = "Player " + PlayerNum; //Set our name
        gamepad = ControllerManager.instance.RequestSpecificGamepad(PlayerNum-1); //Get our gamepad reference
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
        Debug.Log(gamepad.GetAxis(AxisCode.GamepadAxisLeftX));
        radiusVelocity = gamepad.GetAxis(AxisCode.GamepadAxisLeftX) * 30;
    }

    void UpdateCamera(){
        // Debug.Log(gamepad.GetAxis(AxisCode.GamepadAxisRightX));
        // Debug.Log(gamepad.GetAxis(AxisCode.GamepadAxisRightY));
    }
}
