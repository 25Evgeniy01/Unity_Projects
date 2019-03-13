using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;
using System;

public class StartMovePlayer : MonoBehaviour {

	private Animator anim;
	private int arrIndex = 0;
	[SerializeField] private GameObject camera;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	}

	// Update is called once per frame
	void FixedUpdate () {
		
		/*if  (transform.position.x <= 40.5) {
			transform.position.Set (transform.position.x, 0.12, transform.position.z);
		}*/
		string playerName = name.Substring (7);
		int x = Convert.ToInt32 (playerName);
		Vector3[] massPoint;
		float zPos = (0.51f + Random.Range (-1f,1f)/4f);
		if (name.Substring (0, 7) == "playerF") {
			massPoint = new Vector3[]{ new Vector3 (30f - zPos*6, transform.position.y, zPos * 1.5f), new Vector3 (24f, transform.position.y, 3f + 1.5f*x + zPos*1.5f) };
		} else {
			massPoint = new Vector3[]{ new Vector3 (30f + zPos*5,  transform.position.y, -zPos * 1.5f), new Vector3 (24f, transform.position.y, -3f - 1.5f*x + zPos*1.44f) };
		}
		PlayerMoveStartLine (massPoint);
	}

	void PlayerMoveStartLine(Vector3[] massPoint) {
		if (camera.transform.position.y <= 45) {
			bool newMove;
			if (arrIndex <= massPoint.Length - 1) {
				newMove = MovePosition (massPoint [arrIndex]);
				if (newMove) {
					arrIndex++;
				}
			} else {
				transform.LookAt (new Vector3 (transform.position.x + 1, transform.position.y, transform.position.z));
				GetComponent<GetTactics> ().enabled = true;
				GetComponent<StartMovePlayer> ().enabled = false;
			}
		}
	}



	bool MovePosition(Vector3 newPosition) {
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
			return false;
		} else {
			anim.SetFloat ("speed", 0f);
			return true;
		}
	}


}
