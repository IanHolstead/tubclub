using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;
using ControlWrapping;

public class PlayerController : MonoBehaviour {

    public AnimationCurve ac;

    [Header("Player Variables")]
    public int PlayerNum; //[1-4]

    [Header("Camera Variables")]
    public float verticalRange = 45f;
    public float horizontalRange = 90f;

    public float cameraX = 0;
    public float cameraY = 0;

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

    void UpdateCamera()
    {
        float newCameraX = gamepad.GetAxis(AxisCode.GamepadAxisRightX);
        float newCameraY = gamepad.GetAxis(AxisCode.GamepadAxisRightY);

        newCameraX = Mathf.Lerp(cameraX, GameFunctions.MapRange(newCameraX, 0, 1, 0, horizontalRange), .4f);
        newCameraY = Mathf.Lerp(cameraY, GameFunctions.MapRange(newCameraY, 0, 1, 0, verticalRange), .4f);

        camera.transform.RotateAround(transform.position, Vector3.up, newCameraX - cameraX);
        camera.transform.RotateAround(transform.position, Vector3.right, newCameraY - cameraY);
        //camera.transform.rotation = Quaternion.Euler(camera.transform.rotation.x, camera.transform.rotation.y, 0);

        cameraX = newCameraX;
        cameraY = newCameraY;
        //transform.forward
        // Debug.Log(gamepad.GetAxis(AxisCode.GamepadAxisRightX));
        // Debug.Log(gamepad.GetAxis(AxisCode.GamepadAxisRightY));
    }
}
