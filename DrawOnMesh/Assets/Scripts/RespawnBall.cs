using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnBall : MonoBehaviour {
	[SerializeField] Transform respawnPos;
	[SerializeField] Transform ball;

	void Start () {
		Respawn ();
	}

	void Respawn () {
		ball.parent = null;
		ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
		ball.position = respawnPos.position;
	}

	void OnCollisionEnter (Collision c) {
		if (c.transform == ball) {
			Respawn ();
		}
	}

	void OnCollisionExit (Collision c) {
		if (c.transform == ball) {
			Respawn ();

		}
	}
}