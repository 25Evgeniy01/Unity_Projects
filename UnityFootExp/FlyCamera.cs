using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FlyCamera : MonoBehaviour {

	public float angle; // угол 
	public float radius; // радиус
	private bool isCircle = true; // условие движения по кругу
	private bool moveToStadion = true;// перемещение камеры к полю
	private bool downToField = true;// перемещение камеры вниз
	public float speed; 
	private bool startGameCameraPoistion = false;
	[SerializeField] private Canvas tableCanvas;
	public AudioClip music;

	// Use this for initialization
	void Start () {
		AudioSource audio = GetComponent<AudioSource>();
		audio.clip = music;
		audio.Play ();

	}

	// Update is called once per frame
	void FixedUpdate () {
		//ВКЛЮЧИТЬ
		//StartGameMoveCamera ();
		//if (transform.position.y <= 45 &&  !startGameCameraPoistion) {
		//	CameraFocusStartGame ();
		//}
		StartGameCameraPosition ();
	}

	void StartGameCameraPosition() {
		if (tableCanvas.enabled) {
			transform.position.Set (-46, 14, 0);
			startGameCameraPoistion = true;
			Quaternion rotationPoint = Quaternion.Euler(new Vector3(23, 90, 0));
			Camera.main.fieldOfView = 36f;
			transform.rotation = rotationPoint;
			Invoke ("TableCanvasFalse", 2f);

		}
	}

	void TableCanvasFalse() {
		tableCanvas.enabled = false;
		AudioSource audio = GetComponent<AudioSource>();
		audio.clip = music;
		audio.Stop ();
		GetComponent<FlyCamera> ().enabled = false;
		//GetComponent<CameraLiveMove> ().enabled = true;
	}

	void CameraFocusStartGame() {
		Camera.main.fieldOfView = Mathf.Lerp (Camera.main.fieldOfView, 20f, 0.1f*Time.deltaTime);
		Quaternion rotationPoint = Quaternion.Euler(new Vector3(8f, 90f, 0f));
		transform.rotation = Quaternion.Lerp (transform.rotation, rotationPoint,  0.05f*Time.deltaTime);
	}

	void StartGameMoveCamera() {
		if (moveToStadion && angle < -15.7) {
			if (isCircle) isCircle = false;

			Quaternion rotationPoint = Quaternion.Euler(new Vector3(23, 90, 0));
			Vector3 positionPoint = new Vector3 (-46, 46, 0);
			float speed = 0.3f;

			transform.rotation = Quaternion.Lerp (transform.rotation, rotationPoint, 1.5f * Time.deltaTime);
			transform.position = Vector3.Lerp (transform.position, positionPoint, speed * Time.deltaTime);
		}
			
		if (downToField && !isCircle && transform.position.x >= -73) {
			if (moveToStadion) moveToStadion = false;
			Vector3 destinationPoint = new Vector3(-46, 14 , 0);
			float smoothing = 0.5f;
			transform.position = Vector3.Lerp (transform.position, destinationPoint, smoothing * Time.deltaTime);
		}

		if (transform.position.y <= 14.1) {
			downToField = false;
		}
			
		if (isCircle) {
			transform.Rotate( Time.deltaTime, 11*Time.deltaTime, -8f*Time.deltaTime);
			angle -= Time.deltaTime; 
			float x = Mathf.Cos (angle * speed) * radius ;
			float y = Mathf.Sin (angle * speed) * radius ;
			transform.position = new Vector3(x, 0, y) + new Vector3(189, 130, -9);
		}
	}

}
