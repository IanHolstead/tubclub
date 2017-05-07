using System.Collections;
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
    public float maxRadius;
    public float minRadius;
    public AnimationCurve StearingAmountOverRadius; //Left, 0 | Right, 1 Sample this curve for rotation speed in degrees per second

    [Header("Prefab References")]
    public GameObject HarpoonPrefab;

    [Header("Player Variables")]
    public int PlayerNum; //[1-4]
    public float HarpoonInitialVelocityMagnitude; //Initial velocity magnitude for when the harpoon is shot
    public int HarpoonTrajectorySamples; //Number of points to sample when calculating trajectory

    [Header("Camera Variables")]
    public float stearingAmount = 5;

    [Header("Camera Variables")]
    public float verticalRange = 25f;
    public float horizontalRange = 50f;

    public float cameraX = 0;
    public float cameraY = 0;

    [Header("Component References")]
    public GameObject LeftCannon;
    public GameObject RightCannon;
    public GameObject meshRef;
    public Camera camera;
    public GameObject UICanvas;

    LineRenderer lr;

    Gamepad gamepad;

    public enum State { Null, Paused, Disabled, Alive, Dead };
    public State state;

    public float disabledTime = 3f;
    float timeSinceLastHit;

    void Awake(){
        lr = GetComponent<LineRenderer>();
    }

    // Use this for initialization
    void Start ()
    {
        timeSinceLastHit = disabledTime;
    }

    public Vector3 SampleTrajectory (Vector3 initialPosition, Vector3 initialVelocity, float time) {
        return initialPosition + (initialVelocity*time) + (Physics.gravity*time*time*0.5f);
    }

    public List<Vector3> SampleTrajectoryPoints (Vector3 initialPosition, Vector3 initialVelocity, float timeStep, float maxTime) {
        List<Vector3> points = new List<Vector3>();
        float t = 0;
        while(t < maxTime){ //While we're not done sampling
            Vector3 pos = SampleTrajectory (initialPosition, initialVelocity, t); //Get the position of the sample point
            // if (Physics.Linecast (prev,pos)){
            //     break;
            // } 
            points.Add(pos); //Add it to the list
            t += timeStep; //Increment timestep
        }

        return points;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.state == GameManager.State.Countdown)
        {
            UpdatePhysics();
            UpdateCamera(true);
        }
        else if (GameManager.Instance.state == GameManager.State.Playing )
        {
            switch (state)
            {
                case State.Disabled:
                    timeSinceLastHit += Time.deltaTime;
                    if (timeSinceLastHit < disabledTime)
                    {
                        UpdatePhysics();
                        UpdateCamera(true);
                        //radiusVelocity /= .5f*Time.deltaTime;
                    }
                    else
                    {
                        state = State.Alive;
                    }
                    break;
                case State.Alive:
                    UpdateSteering();
                    UpdateCamera(true);
                    UpdateHarpoon();
                    UpdatePhysics();
                    break;

                case State.Paused:

                    break;
                case State.Dead:
                    UpdateCamera(false);
                    break;
            }
        }
    }

    void OnDestroy(){
        gamepad.EndVibration();
        ControllerManager.instance.ReturnGamePad(PlayerNum-1);
    }

    public void Setup(int playerNum){ //Public call to setup our player
        this.PlayerNum = playerNum; //Set our player number
        this.name = "Player " + PlayerNum; //Set our name
        this.state = State.Alive;
        gamepad = ControllerManager.instance.RequestSpecificGamepad(PlayerNum-1); //Get our gamepad reference
        gamepad.TriggerSensitivity = .35f;
        SetLayerRecursive(UICanvas.transform, "UI Player " + PlayerNum); //Set the player's UI's layer
        
        //Turn off all the layer masks for each player UI
        camera.cullingMask &= ~(1 << LayerMask.NameToLayer("UI Player 1"));
        camera.cullingMask &= ~(1 << LayerMask.NameToLayer("UI Player 2"));
        camera.cullingMask &= ~(1 << LayerMask.NameToLayer("UI Player 3"));
        camera.cullingMask &= ~(1 << LayerMask.NameToLayer("UI Player 4"));

        //Turn on ONLY our layer mask
        camera.cullingMask |= (1 << LayerMask.NameToLayer("UI Player " + PlayerNum ));
    }

    void Die()
    {
        gamepad.SetVibration(1, 1, 1);
        state = State.Dead;
        //gameObject.AddComponent<Floaties>();
        Destroy(this, .5f);
    }

    public void HitByHarpoon(){
        Die();
        //TODO: fade to black
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
        if(radius >= maxRadius){
            radius = maxRadius;
        }
        if (radius < minRadius)
        {
            Die();
        }
        float angle = Mathf.Atan2(transform.position.x, transform.position.z) * Mathf.Rad2Deg; //Get our angle relative to 0,0
        if(angle < 0){ //If our angle is negative, we're under the x-axis
            angle += 360; //Add 360 degrees
        }
        float deltaAngle = RotationSpeedOverRadius.Evaluate(radius); //Sample our curve to get our change in angle
        angle -= deltaAngle;
        angle = angle * Mathf.Deg2Rad; //Convert to radians
        Vector3 newPos = new Vector3(Mathf.Sin(angle) * radius, transform.position.y, Mathf.Cos(angle) * radius); //Calculate their new position
        transform.position = newPos; //Set the new position
        transform.rotation = Quaternion.AngleAxis((angle * Mathf.Rad2Deg) - 90, Vector3.up);

        //gamepad.SetVibration(0f, GameFunctions.MapRangeClamped(radius, 1, 7, .75f, 0));
    }

    void UpdateSteering(){
        //radius = Mathf.Sqrt(Mathf.Pow(transform.position.x, 2) + Mathf.Pow(transform.position.z, 2));
        radiusVelocity = gamepad.GetAxis(AxisCode.GamepadAxisLeftX) * stearingAmount* StearingAmountOverRadius.Evaluate(radius);
    }

    void UpdateCamera(bool allowPlayerControl){
        float newCameraX = 0;
        float newCameraY = 0;
        if (allowPlayerControl)
        {
            newCameraX = gamepad.GetAxis(AxisCode.GamepadAxisRightX);
            newCameraY = -gamepad.GetAxis(AxisCode.GamepadAxisRightY);

        }
        //TODO: this should depend on delta time
        newCameraX = Mathf.Lerp(cameraX, GameFunctions.MapRange(newCameraX, 0, 1, 0, horizontalRange), .4f);
        newCameraY = Mathf.Lerp(cameraY, GameFunctions.MapRange(newCameraY, 0, 1, 0, verticalRange), .4f);

        camera.transform.RotateAround(transform.position, Vector3.up, newCameraX - cameraX);
        camera.transform.RotateAround(transform.position, camera.transform.right, newCameraY - cameraY);

        cameraX = newCameraX;
        cameraY = newCameraY;
    }

    void UpdateHarpoon(){
        Vector3 start = (LeftCannon.transform.position + RightCannon.transform.position)/2;// + meshRef.transform.up * .3f; //The initial positon of the harpoon
        Vector3 vel = (camera.transform.forward*2 + camera.transform.up).normalized * HarpoonInitialVelocityMagnitude; //The initial velocity vector of the harpoon

        //Calculate timestep, sample points on line to find trajectory, set linerenderer points
        float maxTime = 5.0f;
        float timeStep = maxTime/HarpoonTrajectorySamples;
        List<Vector3> points = SampleTrajectoryPoints(start, vel, timeStep, maxTime);
        lr.positionCount = points.Count;
        lr.SetPositions(points.ToArray());
        UpdateCanonAngles(start, vel);

        if(gamepad.GetButtonDown(ActionKeyCode.GamepadRightTrigger)){
            HarpoonController harpoonSpawn = Instantiate(HarpoonPrefab, start, Quaternion.identity).GetComponent<HarpoonController>();
            harpoonSpawn.Fire(PlayerNum, vel, HarpoonInitialVelocityMagnitude);
        }
    }

    public void Stun()
    {
        gamepad.SetVibration(.75f, .25f, .75f);
        state = State.Disabled;
        timeSinceLastHit = 0f;
        lr.positionCount = 0;
    }
    
    void UpdateCanonAngles(Vector3 start, Vector3 vel)
    {
        GameObject cannon;
        if (cameraX >= 0) 
        {
            cannon = RightCannon;
        }
        else
        {
            cannon = LeftCannon;
        }

        Debug.DrawRay(start, Quaternion.LookRotation(vel).eulerAngles, Color.red);
        
        cannon.transform.rotation = Quaternion.Euler(Vector3.Lerp(cannon.transform.rotation.eulerAngles.normalized, Quaternion.LookRotation(vel).eulerAngles, 1f));
        
    }
}
