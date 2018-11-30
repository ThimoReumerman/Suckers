using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToNext : MonoBehaviour {
	[SerializeField] Transform cam;
	int index = 0;
	List<GameObject> players;
	int count;

	public void SetList (List<GameObject> playerList) {
		players = playerList;
		count = players.Count;
	}

	public void Left () {
		GoToPlayer (-1);
	}

	public void Right () {
		GoToPlayer (1);
	}

	void GoToPlayer (int leftOrRight) {
		index += leftOrRight;

		if (index < 0) {
			index = 0;
		} else if (index >= count) {
			index = count - 1;
		}

		Vector3 playerPos = players[index].transform.position;

		cam.position = new Vector3 (playerPos.x, cam.position.y, cam.position.z);

	}
}