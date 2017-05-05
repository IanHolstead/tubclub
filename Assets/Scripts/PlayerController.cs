using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;
using ControlWrapping;

public class PlayerController : MonoBehaviour {

	Gamepad gamepad;

	// Use this for initialization
	void Start () {
		gamepad = ControllerManager.instance.RequestSpecificGamepad(0);
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log(gamepad.GetButton(ActionKeyCode.GamepadA));
	}
}
