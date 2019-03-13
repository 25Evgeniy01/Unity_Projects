using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySql.Data.MySqlClient;
using System;
using Random = UnityEngine.Random;

//скрипт будет для каждого игрока
public class GetTactics : MonoBehaviour {

	string pathsql = "host=127.0.0.1;user=root;database=mydb";

	private int[] square = new int[11];
	//массив позиций в квадратах
	float[] arr;
	int [] playerPos;
	[SerializeField] private Canvas tableCanvas;
	private bool getTacticStart = false;

	public JSONObject expJSON;

	// Use this for initialization
	void Start () {
		StartCoroutine (GetPos ());
	}

	// Update is called once per frame
	void FixedUpdate () {
		
		if (Camera.main.fieldOfView == 36f && tableCanvas.enabled && GetComponent<GetTactics> ().enabled && arr != null) {
			if (name != "playerF9") {
				transform.position = new Vector3 (arr [0], transform.position.y, arr [1]);
			} else {
				transform.position = new Vector3 (0, transform.position.y, 0);
			}

			getTacticStart = true;


		}

		if (!tableCanvas.enabled && getTacticStart) {
			Invoke ("MovePlayerEnable", 3f);
			//тактическое местонахождение игрока
			GetComponent<GetTactics> ().enabled = false;
		}
	}

	IEnumerator GetPos()
	{
		string url;
		if (name.Substring (0, 7) == "playerF") {
			url = "http://localhost:3000/playerSettings";
		} else {
			url = "http://localhost:3000/playerSettingsS";
		}

		WWW hs_get = new WWW (url);
		yield return hs_get;
		string jsonData = "";
		if (hs_get.error != null) {
			Debug.Log ("There was an error getting the high score: " + hs_get.error);
		} else {
			JSONObject json = new JSONObject(hs_get.text);  
			expJSON = json;
			for (int i = 0; i < 11; i++) {
				square [i] = MoveBackPlayer (Convert.ToInt32(json [i] ["Square"].ToString()));

				for (int j = 0; j < i; j++) {
					if (square [i] == square [j]) {
						square [i] = square [i] + 5;
					}
				}
			}



			int x = Convert.ToInt32 (name.Substring (7));

			if (square [x] == 17 || square [x] > 19 && square [x] < 35)  {
				square [x] = square [x] - 5;
				if (square [x] == 17 || square [x] > 19 && square [x] < 35) {
					square [x] = square [x] - 5;
					if (square [x] == 17 || square [x] > 19 && square [x] < 35) {
						square [x] = square [x] - 5;
						if (square [x] == 17 || square [x] > 19 && square [x] < 35) {
							square [x] = square [x] - 5;
						}
					}
				}
			} 

			arr = RandomPositionForSquare (square [x]);



		}
	}

	void MovePlayerEnable() {
		GetComponent<MovePlayer> ().enabled = true;
	}


	int MoveBackPlayer(int pos) { //передвижение назад и перезапись элемента

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
