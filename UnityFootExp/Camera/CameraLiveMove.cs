using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLiveMove : MonoBehaviour {

	[SerializeField] private float posX, posY, posZ;
	[SerializeField] private float rotX, rotY, rotZ, fieldOfView, speed;
	[SerializeField] private bool camMove; 
	public GameObject camera;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (camMove) {
			CameraNewPos (posX, posY, posZ, rotX, rotY, rotZ);
		}
	}

	void CameraNewPos(float posX, float posY, float posZ, float rotX, float rotY, float rotZ) {
		Vector3 newPosition = new Vector3 (posX, posY, posZ);
		Quaternion rotationPoint = Quaternion.Euler(new Vector3(rotX, rotY, rotZ));

		float t = Vector3.Distance (camera.transform.position, newPosition) / speed;

		camera.transform.position = Vector3.MoveTowards (camera.transform.position, newPosition, speed);
		camera.transform.rotation = Quaternion.RotateTowards (camera.transform.rotation, rotationPoint,  Quaternion.Angle(camera.transform.rotation, rotationPoint)/t);
		Camera.main.fieldOfView = Mathf.MoveTowards (Camera.main.fieldOfView, fieldOfView, speed);
	}
}
