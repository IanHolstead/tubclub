using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;
using ControlWrapping;

public class PlayerController : MonoBehaviour {

    public AnimationCurve ac;

    [Header("Player Variables")]
    public int PlayerNum; //[1-4]

    [Header("Component References")]
    public Camera camera;

	Gamepad gamepad;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		// Debug.Log(gamepad.GetButton(ActionKeyCode.GamepadA));
        
        UpdateSteering();
        UpdateCamera();
	}

    public void Setup(int playerNum){ //Public call to setup our player
        this.PlayerNum = playerNum; //Set our player number
        this.name = "Player " + PlayerNum; //Set our name
        gamepad = ControllerManager.instance.RequestSpecificGamepad(PlayerNum-1); //Get our gamepad reference
    }

    void UpdateSteering(){
        // Debug.Log(gamepad.GetAxis(AxisCode.GamepadAxisLeftX));
    }

    void UpdateCamera(){
        // Debug.Log(gamepad.GetAxis(AxisCode.GamepadAxisRightX));
        // Debug.Log(gamepad.GetAxis(AxisCode.GamepadAxisRightY));
    }
}
