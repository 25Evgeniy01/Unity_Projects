using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

using MySql.Data.MySqlClient;
using System;
using System.IO;

public class TableGame : MonoBehaviour {

	Text playerF0, playerF1, playerF2, playerF3, playerF4, playerF5, playerF6, playerF7, playerF8, playerF9, playerF10;
	Text playerS0, playerS1, playerS2, playerS3, playerS4, playerS5, playerS6, playerS7, playerS8, playerS9, playerS10;
	[SerializeField] private GameObject player11;

	private Canvas tableCanvas;

	void Awake() {
		playerF0 = GameObject.Find ("PlayerF0").GetComponent <Text> ();
		playerF1 = GameObject.Find ("PlayerF1").GetComponent <Text> ();
		playerF2 = GameObject.Find ("PlayerF2").GetComponent <Text> ();
		playerF3 = GameObject.Find ("PlayerF3").GetComponent <Text> ();
		playerF4 = GameObject.Find ("PlayerF4").GetComponent <Text> ();
		playerF5 = GameObject.Find ("PlayerF5").GetComponent <Text> ();
		playerF6 = GameObject.Find ("PlayerF6").GetComponent <Text> ();
		playerF7 = GameObject.Find ("PlayerF7").GetComponent <Text> ();
		playerF8 = GameObject.Find ("PlayerF8").GetComponent <Text> ();
		playerF9 = GameObject.Find ("PlayerF9").GetComponent <Text> ();
		playerF10 = GameObject.Find ("PlayerF10").GetComponent <Text> ();


		playerS0 = GameObject.Find ("PlayerS0").GetComponent <Text> ();
		playerS1 = GameObject.Find ("PlayerS1").GetComponent <Text> ();
		playerS2 = GameObject.Find ("PlayerS2").GetComponent <Text> ();
		playerS3 = GameObject.Find ("PlayerS3").GetComponent <Text> ();
		playerS4 = GameObject.Find ("PlayerS4").GetComponent <Text> ();
		playerS5 = GameObject.Find ("PlayerS5").GetComponent <Text> ();
		playerS6 = GameObject.Find ("PlayerS6").GetComponent <Text> ();
		playerS7 = GameObject.Find ("PlayerS7").GetComponent <Text> ();
		playerS8 = GameObject.Find ("PlayerS8").GetComponent <Text> ();
		playerS9 = GameObject.Find ("PlayerS9").GetComponent <Text> ();
		playerS10 = GameObject.Find ("PlayerS10").GetComponent <Text> ();


		tableCanvas = GetComponent<Canvas> ();
	}



	// Use this for initialization
	void Start () {
		StartCoroutine(GetScores());
		StartCoroutine(GetScoresS());
	}
	
	// Update is called once per frame
	void Update () {
		if (player11.transform.position.z >= 18 && Camera.main.fieldOfView <= 21.5f && Camera.main.fieldOfView >= 20f) {
			tableCanvas.enabled = true;
		}
	}
		
	IEnumerator GetScores()
	{

		WWW hs_get = new WWW("http://localhost:3000/playerSettings");
		yield return hs_get;
		string jsonData = "";
		if (hs_get.error != null)
		{
			Debug.Log("There was an error getting the high score: " + hs_get.error);
		}
		else
		{
			//Нашел наконец-то Парсер - скачал - потсавил - https://assetstore.unity.com/packages/tools/input-management/json-object-710
			//если json не обьектом можно также использовать JsonUtilites
			JSONObject json = new JSONObject(hs_get.text);  

			playerF0.text = json[0]["PlayerName"].ToString();
			playerF1.text = json[1]["PlayerName"].ToString();
			playerF2.text = json[2]["PlayerName"].ToString();
			playerF3.text = json[3]["PlayerName"].ToString();
			playerF4.text = json[4]["PlayerName"].ToString();
			playerF5.text = json[5]["PlayerName"].ToString();
			playerF6.text = json[6]["PlayerName"].ToString();
			playerF7.text = json[7]["PlayerName"].ToString();
			playerF8.text = json[8]["PlayerName"].ToString();
			playerF9.text = json[9]["PlayerName"].ToString();
			playerF10.text = json[10]["PlayerName"].ToString();

		}
	}

	IEnumerator GetScoresS()
	{
		
		WWW hs_get = new WWW("http://localhost:3000/playerSettingsS");
		yield return hs_get;
		string jsonData = "";
		if (hs_get.error != null)
		{
			Debug.Log("There was an error getting the high score: " + hs_get.error);
		}
		else
		{
			Debug.Log (1);
			JSONObject json = new JSONObject(hs_get.text);  

			playerS0.text = json[0]["PlayerName"].ToString();
			playerS1.text = json[1]["PlayerName"].ToString();
			playerS2.text = json[2]["PlayerName"].ToString();
			playerS3.text = json[3]["PlayerName"].ToString();
			playerS4.text = json[4]["PlayerName"].ToString();
			playerS5.text = json[5]["PlayerName"].ToString();
			playerS6.text = json[6]["PlayerName"].ToString();
			playerS7.text = json[7]["PlayerName"].ToString();
			playerS8.text = json[8]["PlayerName"].ToString();
			playerS9.text = json[9]["PlayerName"].ToString();
			playerS10.text = json[10]["PlayerName"].ToString();
			StopAllCoroutines ();
		}
	}
}
