using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuckBall : MonoBehaviour {
	[SerializeField] Transform suckPos;
	[SerializeField] float suckSpeed;
	[SerializeField] Transform ball;
	[SerializeField] float maxDistance;
	[SerializeField] float shootForce;
	bool ballInRange;
	bool hasBall;

	void Start () {

	}

	void Update () {
		if (Input.GetButtonDown ("Fire1")) {
			if (hasBall) {
				Shoot ();
			} else if (ballInRange) {
				StartSuck ();
			}
		}

		if(hasBall) {
			ball.localPosition = Vector3.zero;	
		}
	}

	void StartSuck () {
		if (!hasBall) {
			ball.GetComponent<Collider> ().enabled = false;
			ball.GetComponent<Rigidbody> ().useGravity = false;
			StartCoroutine (Suck ());
		}
	}

	IEnumerator Suck () {
		ball.GetComponent<Rigidbody> ().detectCollisions = false;
		float step;
		step = suckSpeed * Time.deltaTime;

		float distance = Vector3.Distance (suckPos.position, ball.position);

		while (distance >= maxDistance) {
			ball.position = Vector3.MoveTowards (ball.position, suckPos.position, step);
			distance = Vector3.Distance (suckPos.position, ball.position);
			print ("Stepping...");

			yield return null;
		}

		ball.GetComponent<Rigidbody> ().velocity = Vector3.zero;
		ball.GetComponent<Rigidbody> ().freezeRotation = true;

		ball.parent = suckPos;
		ball.localPosition = Vector3.zero;
		hasBall = true;

		yield return null;
	}

	void Shoot () {
		hasBall = false;
		ball.parent = null;
		ball.GetComponent<Rigidbody> ().detectCollisions = true;
		ball.GetComponent<Collider> ().enabled = true;
		ball.GetComponent<Rigidbody> ().useGravity = true;
		ball.GetComponent<Rigidbody> ().freezeRotation = false;
		ball.GetComponent<Rigidbody> ().AddForce (transform.forward * shootForce);
	}

	void OnTriggerEnter (Collider c) {
		if (c.transform.tag == "Ball") {
			ballInRange = true;
		}
	}

	void OnTriggerExit (Collider c) {
		if (c.transform.tag == "Ball") {
			ballInRange = false;
		}
	}
}