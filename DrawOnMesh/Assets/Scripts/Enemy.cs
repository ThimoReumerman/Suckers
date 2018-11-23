using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
	Transform player;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").transform;
	}

	// Update is called once per frame
	void Update () {
		Vector3 target = new Vector3 (
			player.position.x,
			transform.position.y,
			player.position.z
		);
		transform.LookAt (target);
	}
}