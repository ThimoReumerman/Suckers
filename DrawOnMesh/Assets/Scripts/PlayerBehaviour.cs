using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour {
	[SerializeField] float speed;
	[SerializeField] float jumpForce;
	[SerializeField] float interactionRange;
	public bool canJump;
	[SerializeField] Transform parentTransform;
	[SerializeField] Rigidbody rb;

	public Vector3 targetPoint;
	void Update () {

		if (canJump) {
			if (Input.GetButtonDown ("Jump")) {
				print ("ayy lmao");
				Jump ();
			}
		}
	}

	void FixedUpdate () {
		Walk ();
		Rotate ();

	}

	void Rotate () {
		Plane playerPlane = new Plane (Vector3.up, transform.position);

		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

		float hitdist = 0.0f;

		if (playerPlane.Raycast (ray, out hitdist)) {

			targetPoint = ray.GetPoint (hitdist);

			Quaternion targetRotation = Quaternion.LookRotation (targetPoint - transform.position);

			transform.rotation = targetRotation;

		}
	}

	void Walk () {
		float hor = Input.GetAxis ("Horizontal");
		float ver = Input.GetAxis ("Vertical");
		Vector3 movement = new Vector3 (hor, 0.0f, ver);

		parentTransform.Translate (movement * speed * Time.deltaTime);
	}

	void Jump () {
		Vector3 jumpV3 = new Vector3 (0.0f, jumpForce, 0.0f);
		rb.AddForce (jumpV3);
	}

	void Interact () {

	}

}