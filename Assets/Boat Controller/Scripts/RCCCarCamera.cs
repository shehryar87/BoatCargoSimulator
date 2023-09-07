using UnityEngine;
using System.Collections;

public class RCCCarCamera : MonoBehaviour{

	// The target we are following
	public Transform playerCar;
	private Rigidbody playerRigid;
	private Camera cam;

	// The distance in the x-z plane to the target
	public float distance = 6.0f;
	
	// the height we want the camera to be above the target
	public float height = 2.0f;
	
	public float heightOffset = .75f;
	public float heightDamping = 2.0f;
	public float rotationDamping = 3.0f;
	public bool useSmoothRotation = true;
	
	public float minimumFOV = 50f;
	public float maximumFOV = 70f;
	
	public float maximumTilt = 15f;
	private float tiltAngle = 0f;

	public bool moveCam;
	public bool moveCam2;
	public bool moveCam3;
	public bool levelComplete;


	void Start(){
		

		playerRigid = playerCar.GetComponent<Rigidbody>();
		cam = GetComponent<Camera>();

//		if(GetComponent<RCCCamManager>())
//			GetComponent<RCCCamManager>().target = playerCar;


		moveCam = false;
		moveCam2 = false;
		moveCam3 = false;
		levelComplete = false;
	}
	
	void Update(){
		
		// Early out if we don't have a target
		if (!playerCar)
			return;
		
		if(playerRigid != playerCar.GetComponent<Rigidbody>())
			playerRigid = playerCar.GetComponent<Rigidbody>();
		
		//Tilt Angle Calculation.
		tiltAngle = Mathf.Lerp (tiltAngle, (Mathf.Clamp (-playerCar.InverseTransformDirection(playerRigid.velocity).x, -35, 35)), Time.deltaTime * 2f);

		if(!cam)
			cam = GetComponent<Camera>();

		cam.fieldOfView = Mathf.Lerp (minimumFOV, maximumFOV, (playerRigid.velocity.magnitude * 3f) / 150f);

	}
	
	void LateUpdate (){
		
		// Early out if we don't have a target
		if (!playerCar || !playerRigid)
			return;
		
		float speed = (playerRigid.transform.InverseTransformDirection(playerRigid.velocity).z) * 3f;
		
		// Calculate the current rotation angles.
		float wantedRotationAngle = playerCar.eulerAngles.y;
		float wantedHeight = playerCar.position.y + height;
		float currentRotationAngle = transform.eulerAngles.y;
		float currentHeight = transform.position.y;

		if(useSmoothRotation)
			rotationDamping = Mathf.Lerp(0f, 3f, (playerRigid.velocity.magnitude * 3f) / 40f);
		
		if (speed < -10) {
			wantedRotationAngle = playerCar.eulerAngles.y + 180;
		} else if (moveCam) {
			wantedRotationAngle = (playerCar.eulerAngles.y + 120);
		} else if (moveCam2) {
			wantedRotationAngle = (playerCar.eulerAngles.y + 240);
		} else if (moveCam3) {
			wantedRotationAngle = currentRotationAngle + 175.0f;//(playerCar.eulerAngles.y + 360);
		} else if (levelComplete) {
			wantedRotationAngle = playerCar.eulerAngles.y - 90;
			wantedHeight = playerCar.position.y + 2.8f;
			distance = 6.5f;

		} 
		
		// Damp the rotation around the y-axis
		currentRotationAngle = Mathf.LerpAngle (currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);
		
		// Damp the height
		//
		currentHeight = Mathf.Lerp (currentHeight, wantedHeight , heightDamping * Time.deltaTime);
		
		// Convert the angle into a rotation
		Quaternion currentRotation = Quaternion.Euler (0, currentRotationAngle, 0);
		
		// Set the position of the camera on the x-z plane to:
		// distance meters behind the target
		transform.position = playerCar.position;
		transform.position -= currentRotation * Vector3.forward * distance;

		// Set the height of the camera
		transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z);
		
		// Always look at the target
		transform.LookAt (new Vector3(playerCar.position.x, playerCar.position.y + heightOffset, playerCar.position.z));
		transform.eulerAngles = new Vector3(transform.eulerAngles.x,transform.eulerAngles.y, Mathf.Clamp(tiltAngle, -10f, 10f));
		
	}

}