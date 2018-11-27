using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuckBall : MonoBehaviour {
	bool isHolding;
	GameObject ball;
	Transform parent;

	void HoldBall() {
		ball.transform.parent = parent;
		ball.transform.localPosition = Vector3.zero;

	}
}
