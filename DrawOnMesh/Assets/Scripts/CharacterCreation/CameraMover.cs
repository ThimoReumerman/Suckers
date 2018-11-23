using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour {
	[SerializeField] float moveSensitivity;
	[SerializeField] float zoomSensitivity;

	[SerializeField] Transform orbit;
	[SerializeField] GameObject cam;
	float startingFov;
	Quaternion startRotation;
	[SerializeField] float minFov;
	[SerializeField] float maxFov;

	// Use this for initialization
	void Start () {
		startingFov = cam.GetComponent<Camera> ().fieldOfView;
		startRotation = orbit.rotation;
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetButton ("Fire3")) {
			MoveCam ();
		}

		if (Input.GetAxis ("Mouse ScrollWheel") != 0) {
			ZoomCam ();
		}
	}

	void MoveCam () {
		float xAxis = Input.GetAxis ("Mouse Y") * -1f * moveSensitivity;
		orbit.Rotate (xAxis * Vector3.right, Space.Self);

		float yAxis = Input.GetAxis ("Mouse X") * moveSensitivity;
		orbit.Rotate (yAxis * Vector3.up, Space.World);
	}

	void ZoomCam () {
		float toAdd = -1 * Input.GetAxis ("Mouse ScrollWheel") * zoomSensitivity * Time.deltaTime;
		float newFov = Camera.main.fieldOfView + toAdd;

		if (newFov >= maxFov) {
			newFov = maxFov;
		} else if (newFov <= minFov) {
			newFov = minFov;
		}

		Camera.main.fieldOfView = newFov;
	}

	public void Reset () {
		cam.GetComponent<Camera> ().fieldOfView = startingFov;
		orbit.rotation = startRotation;
	}
}