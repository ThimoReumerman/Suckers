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
	[SerializeField] float rotateSpeed;
	[SerializeField] Transform camOrbit;

	[SerializeField] Transform ball;

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
		Vector3 toLookAt = new Vector3(ball.position.x, camOrbit.position.y, ball.position.z);
		//camOrbit.LookAt(toLookAt);
	}



	void Walk () {
		float ver = Input.GetAxis("Vertical") * speed * Time.deltaTime;
		float hor = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
		float rotate = Input.GetAxisRaw ("Mouse X") * rotateSpeed * Time.deltaTime;

		Vector3 verMovement = transform.forward * ver;
		Vector3 horMovement = transform.right * hor;
		transform.Rotate(new Vector3(0.0f, rotate, 0.0f), Space.World);
		parentTransform.Translate (verMovement + horMovement);
	}

	void Jump () {
		Vector3 jumpV3 = new Vector3 (0.0f, jumpForce, 0.0f);
		rb.AddForce (jumpV3);
	}

	void Interact () {

	}

}