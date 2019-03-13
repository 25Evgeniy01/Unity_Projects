using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;
//В БД ДОЛЖНЫ ЗАПИСЫВАТЬСЯ ВНАЧАЛЕ ВРАТАРЬ ПОТОМ ЗАЩИТА -> НАПАДЕНИЕ
public class MovePlayer : MonoBehaviour {

	string pathsql = "host=127.0.0.1;user=root;database=mydb";

	private int[] square = new int[11], squareFront = new int[11];
	private Animator anim;
	//массив позиций в квадратах
	int [] playerPos;

	private bool spaceEvent = false, spaceEvent1 = false;

	float[] arrBack, arrFront;

	[SerializeField] private Canvas tableCanvas;

	// Use this for initialization
	void Start () {
		GetPos ();
		anim = GetComponent<Animator> ();
	}

	// Update is called once per frame
	void FixedUpdate () {

		if (!GetComponent<GetTactics> ().enabled && !GetComponent<StartMovePlayer> ().enabled && !tableCanvas.enabled) {
			Vector3 newPosition = new Vector3 (arrBack [0], transform.position.y, arrBack [1]);

			if (Input.GetKey (KeyCode.Space)) {
				spaceEvent = !spaceEvent;
				spaceEvent1 = true;
			}
				
			if (spaceEvent1) {
				if (name.Substring (0, 7) == "playerF") {
					if (!spaceEvent) newPosition = new Vector3 (arrBack [0], transform.position.y, arrBack [1]); 
					else newPosition = new Vector3 (arrFront [0], transform.position.y, arrFront [1]);
				} else {
					if (spaceEvent) newPosition = new Vector3 (arrBack [0], transform.position.y, arrBack [1]); 
					else newPosition = new Vector3 (arrFront [0], transform.position.y, arrFront [1]);
				}
			}


			Move (newPosition);
		}
	}

	void Move(Vector3 newPosition) {
		if ((int)transform.position.x != (int)newPosition.x || (int)transform.position.y != (int)newPosition.y || (int)transform.position.z != (int)newPosition.z) {
			anim.SetFloat ("speed", 0.3f);

			int x = Convert.ToInt32 (name.Substring (7));
			if (x > 3 && x <= 7) {
				x = x - 4;
			} else {
				if (x > 7) {
					x = x - 8;
				}
			}
			anim.SetFloat ("typeWalk", x);

			transform.position = Vector3.MoveTowards (transform.position, newPosition, 0.05f);
			transform.LookAt (newPosition);
			//тактическое местонахождение игрока
		} else {
			anim.SetFloat ("speed", 0f);
			transform.LookAt (new Vector3 (0, transform.position.y, 0));
		}
	}

	void GetPos()
	{
		 
		JSONObject json = GetComponent<GetTactics>().expJSON;

		//находим квадраты З
		for (int i = 0; i < 11; i++) {
			square [i] = MoveBackPlayer (Convert.ToInt32(json [i] ["Square"].ToString()));
			
			for (int j = 0; j < i; j++) {
				if (square [i] == square [j]) {
					square [i] = square [i] + 5;
				}
			}
		}

		//находим квадраты А
		for (int i = 10; i >= 0; i--) {
			squareFront [i] = MoveFrontPlayer (Convert.ToInt32(json [i] ["Square"].ToString()));

			for (int j = 10; j > i; j--) {
				if (squareFront [i] == squareFront [j]) {
					squareFront [i] = squareFront [i] - 5;
				}
			}
		}

		int x = Convert.ToInt32 (name.Substring (7));

		arrBack = RandomPositionForSquare (square [x]);
		arrFront = RandomPositionForSquare (squareFront [x]);

	}

	int MoveBackPlayer(int pos) { //нахождение квадрата для точки З - назад и перезапись элемента

		if (pos >= 0 && pos <= 9 || pos == 35) {
			return pos; 
		} else {
			if (pos >= 10 && pos <= 14) {
				return pos - 5;
			} else {
				if ((pos >= 15 && pos <= 19) || (pos >= 20 && pos <= 34)) {
					return pos - 10;
				} 
			}
		}
		return 0;
	}

	int MoveFrontPlayer(int pos) { //нахождение квадрата для точки А - вперед
		if (pos >= 25 && pos <= 35 ) {
			return pos; 
		} else {
			if (pos >= 20 && pos <= 24) {
				return pos + 5;
			} else {
				if (pos >= 0 && pos <= 19) {
					return pos + 10;
				} 
			}
		}
		return 0;
	}



	float[] RandomPositionForSquare (int sq) {
		float posX = 0 , posZ = 0;
		if (name.Substring (0, 7) == "playerF") {
			switch (sq) {
			case 0:
				posX = Random.Range (20.5f, 31.5f);
				posZ = Random.Range (39f, 44.5f);
				break;
			case 1:
				posX = Random.Range (4.5f, 18.5f);
				posZ = Random.Range (39f, 44.5f);
				break;
			case 2:
				posX = Random.Range (-2.5f, 2.5f);
				posZ = Random.Range (39f, 44.5f);
				break;
			case 3:
				posX = Random.Range (-18.5f, -4.5f);
				posZ = Random.Range (39f, 44.5f);
				break;
			case 4:
				posX = Random.Range (-31.5f, -20.5f);
				posZ = Random.Range (39f, 44.5f);
				break;
				//////////////////////////////////////////////
			case 5:
				posX = Random.Range (20.5f, 31.5f);
				posZ = Random.Range (26.5f, 33f);
				break;
			case 6:
				posX = Random.Range (4.5f, 18.5f);
				posZ = Random.Range (26.5f, 33f);
				break;
			case 7:
				posX = Random.Range (-2.5f, 2.5f);
				posZ = Random.Range (26.5f, 33f);
				break;
			case 8:
				posX = Random.Range (-18.5f, -4.5f);
				posZ = Random.Range (26.5f, 33f);
				break;
			case 9:
				posX = Random.Range (-31.5f, -20.5f);
				posZ = Random.Range (26.5f, 33f);
				break;
				//////////////////////////////////////////////
			case 10:
				posX = Random.Range (20.5f, 31.5f);
				posZ = Random.Range (13f, 20f);
				break;
			case 11:
				posX = Random.Range (4.5f, 18.5f);
				posZ = Random.Range (13f, 20f);
				break;
			case 12:
				posX = Random.Range (-2.5f, 2.5f);
				posZ = Random.Range (13f, 20f);
				break;
			case 13:
				posX = Random.Range (-18.5f, -4.5f);
				posZ = Random.Range (13f, 20f);
				break;
			case 14:
				posX = Random.Range (-31.5f, -20.5f);
				posZ = Random.Range (13f, 20f);
				break;
				///////////////////////////////////////////
			case 15:
				posX = Random.Range (20.5f, 31.5f);
				posZ = Random.Range (0f, 6.5f);
				break;
			case 16:
				posX = Random.Range (4.5f, 18.5f);
				posZ = Random.Range (0f, 6.5f);
				break;
			case 17:
				posX = Random.Range (-2.5f, 2.5f);
				posZ = Random.Range (0f, 6.5f);
				break;
			case 18:
				posX = Random.Range (-18.5f, -4.5f);
				posZ = Random.Range (0f, 6.5f);
				break;
			case 19:
				posX = Random.Range (-31.5f, -20.5f);
				posZ = Random.Range (0f, 6.5f);
				break;
				////////////////////////////////////////////////
			case 20:
				posX = Random.Range (20.5f, 31.5f);
				posZ = Random.Range (-13f, -6.5f);
				break;
			case 21:
				posX = Random.Range (4.5f, 18.5f);
				posZ = Random.Range (-13f, -6.5f);
				break;
			case 22:
				posX = Random.Range (-2.5f, 2.5f);
				posZ = Random.Range (-13f, -6.5f);
				break;
			case 23:
				posX = Random.Range (-18.5f, -4.5f);
				posZ = Random.Range (-13f, -6.5f);
				break;
			case 24:
				posX = Random.Range (-31.5f, -20.5f);
				posZ = Random.Range (-13f, -6.5f);
				break;
				////////////////////////////////////////////
			case 25:
				posX = Random.Range (20.5f, 31.5f);
				posZ = Random.Range (-26.5f, -20f);
				break;
			case 26:
				posX = Random.Range (4.5f, 18.5f);
				posZ = Random.Range (-26.5f, -20f);
				break;
			case 27:
				posX = Random.Range (-2.5f, 2.5f);
				posZ = Random.Range (-26.5f, -20f);
				break;
			case 28:
				posX = Random.Range (-18.5f, -4.5f);
				posZ = Random.Range (-26.5f, -20f);
				break;
			case 29:
				posX = Random.Range (-31.5f, -20.5f);
				posZ = Random.Range (-26.5f, -20f);
				break;
				////////////////////////////////////////////
			case 30:
				posX = Random.Range (20.5f, 31.5f);
				posZ = Random.Range (-39f, -33f);
				break;
			case 31:
				posX = Random.Range (4.5f, 18.5f);
				posZ = Random.Range (-39f, -33f);
				break;
			case 32:
				posX = Random.Range (-2.5f, 2.5f);
				posZ = Random.Range (-39f, -33f);
				break;
			case 33:
				posX = Random.Range (-18.5f, -4.5f);
				posZ = Random.Range (-39f, -33f);
				break;
			case 34:
				posX = Random.Range (-31.5f, -20.5f);
				posZ = Random.Range (-39f, -33f);
				break;
			case 35:
				posX = 0f;
				posZ = 49f;
				break;
			default:
				Debug.Log ("ERROR");
				break;
			}
		} else {
			switch (sq) {
			case 4:
				posX = Random.Range (20.5f, 31.5f);
				posZ = Random.Range (-44.5f, -39f);
				break;
			case 3:
				posX = Random.Range (4.5f, 18.5f);
				posZ = Random.Range (-44.5f, -39f);
				break;
			case 2:
				posX = Random.Range (-2.5f, 2.5f);
				posZ = Random.Range (-44.5f, -39f);
				break;
			case 1:
				posX = Random.Range (-18.5f, -4.5f);
				posZ = Random.Range (-44.5f, -39f);
				break;
			case 0:
				posX = Random.Range (-31.5f, -20.5f);
				posZ = Random.Range (-44.5f, -39f);
				break;
				//////////////////////////////////////////////
			case 9:
				posX = Random.Range (20.5f, 31.5f);
				posZ = Random.Range (-33f, -26.5f);
				break;
			case 8:
				posX = Random.Range (4.5f, 18.5f);
				posZ = Random.Range (-33f, -26.5f);
				break;
			case 7:
				posX = Random.Range (-2.5f, 2.5f);
				posZ = Random.Range (-33f, -26.5f);
				break;
			case 6:
				posX = Random.Range (-18.5f, -4.5f);
				posZ = Random.Range (-33f, -26.5f);
				break;
			case 5:
				posX = Random.Range (-31.5f, -20.5f);
				posZ = Random.Range (-33f, -26.5f);
				break;
				//////////////////////////////////////////////
			case 14:
				posX = Random.Range (20.5f, 31.5f);
				posZ = Random.Range (-20f, -13f);
				break;
			case 13:
				posX = Random.Range (4.5f, 18.5f);
				posZ = Random.Range (-20f, -13f);
				break;
			case 12:
				posX = Random.Range (-2.5f, 2.5f);
				posZ = Random.Range (-20f, -13f);
				break;
			case 11:
				posX = Random.Range (-18.5f, -4.5f);
				posZ = Random.Range (-20f, -13f);
				break;
			case 10:
				posX = Random.Range (-31.5f, -20.5f);
				posZ = Random.Range (-20f, -13f);
				break;
				///////////////////////////////////////////
			case 19:
				posX = Random.Range (20.5f, 31.5f);
				posZ = Random.Range (-6.5f, 0f);
				break;
			case 18:
				posX = Random.Range (4.5f, 18.5f);
				posZ = Random.Range (-6.5f, 0f);
				break;
			case 17:
				posX = Random.Range (-2.5f, 2.5f);
				posZ = Random.Range (-6.5f, 0f);
				break;
			case 16:
				posX = Random.Range (-18.5f, -4.5f);
				posZ = Random.Range (-6.5f, 0f);
				break;
			case 15:
				posX = Random.Range (-31.5f, -20.5f);
				posZ = Random.Range (-6.5f, 0f);
				break;
				////////////////////////////////////////////////
			case 24:
				posX = Random.Range (20.5f, 31.5f);
				posZ = Random.Range (6.5f, 13f);
				break;
			case 23:
				posX = Random.Range (4.5f, 18.5f);
				posZ = Random.Range (6.5f, 13f);
				break;
			case 22:
				posX = Random.Range (-2.5f, 2.5f);
				posZ = Random.Range (6.5f, 13f);
				break;
			case 21:
				posX = Random.Range (-18.5f, -4.5f);
				posZ = Random.Range (6.5f, 13f);
				break;
			case 20:
				posX = Random.Range (-31.5f, -20.5f);
				posZ = Random.Range (6.5f, 13f);
				break;
				////////////////////////////////////////////
			case 29:
				posX = Random.Range (20.5f, 31.5f);
				posZ = Random.Range (20f, 26.5f);
				break;
			case 28:
				posX = Random.Range (4.5f, 18.5f);
				posZ = Random.Range (20f, 26.5f);
				break;
			case 27:
				posX = Random.Range (-2.5f, 2.5f);
				posZ = Random.Range (20f, 26.5f);
				break;
			case 26:
				posX = Random.Range (-18.5f, -4.5f);
				posZ = Random.Range (20f, 26.5f);
				break;
			case 25:
				posX = Random.Range (-31.5f, -20.5f);
				posZ = Random.Range (20f, 26.5f);
				break;
				////////////////////////////////////////////
			case 34:
				posX = Random.Range (20.5f, 31.5f);
				posZ = Random.Range (33f, 39f);
				break;
			case 33:
				posX = Random.Range (4.5f, 18.5f);
				posZ = Random.Range (33f, 39f);
				break;
			case 32:
				posX = Random.Range (-2.5f, 2.5f);
				posZ = Random.Range (33f, 39f);
				break;
			case 31:
				posX = Random.Range (-18.5f, -4.5f);
				posZ = Random.Range (33f, 39f);
				break;
			case 30:
				posX = Random.Range (-31.5f, -20.5f);
				posZ = Random.Range (33f, 39f);
				break;
			case 35:
				posX = 0f;
				posZ = -49f;
				break;
			default:
				Debug.Log ("ERROR");
				break;
			}
		}

		float[] arr = {posX, posZ};

		return arr;
	}

}
