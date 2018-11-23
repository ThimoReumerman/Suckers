using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckForJump : MonoBehaviour {

	PlayerBehaviour playerBehaviour;

	void Start () {
		playerBehaviour = GetComponentInChildren<PlayerBehaviour> ();
	}
	void OnCollisionEnter (Collision c) {
		if (c.transform.GetComponent<Jumpable> ()) {
			playerBehaviour.canJump = true;
		}
	}

	void OnCollisionStay (Collision c) {
		if (c.transform.GetComponent<Jumpable> ()) {
			playerBehaviour.canJump = true;
		}
	}

	void OnCollisionExit (Collision c) {
		if (c.transform.GetComponent<Jumpable> ()) {
			playerBehaviour.canJump = false;
		}
	}
}