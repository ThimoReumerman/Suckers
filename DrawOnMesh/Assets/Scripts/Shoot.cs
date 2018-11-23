using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shoot : MonoBehaviour {
	[SerializeField] float range;
	[SerializeField] Vector3 targetPoint;
	[SerializeField] Transform shootOrigin;
	PlayerBehaviour playerBehaviour;
	[SerializeField] float minSuckTime;
	[SerializeField] float maxSuckTime;
	[SerializeField] Transform suckPos;
	[SerializeField] Text amountText;
	int suckedAmount;

	void Start () {
		playerBehaviour = GetComponent<PlayerBehaviour> ();
	}

	IEnumerator Suck (GameObject toSuck, float t) {
		toSuck.transform.SetParent (suckPos);
		Collider[] colliders = toSuck.GetComponents<Collider> ();
		foreach (Collider c in colliders) {
			c.enabled = false;
		}

		float elapsedTime = 0.0f;
		Vector3 startingPos = toSuck.transform.position;
		Vector3 startingScale = toSuck.transform.localScale;

		while (elapsedTime < t) {
			toSuck.transform.position = Vector3.Lerp (startingPos, suckPos.position, elapsedTime / t);

			toSuck.transform.localScale = Vector3.Lerp (startingScale, Vector3.zero, elapsedTime / t);

			elapsedTime += Time.deltaTime;
			print ("Sucking...");
			yield return new WaitForEndOfFrame ();
		}
		Destroy (toSuck);
		AddSucked();
		yield return null;
	}

	void AddSucked() {
		suckedAmount += 1;
		amountText.text = suckedAmount.ToString();
	}

	void OnTriggerEnter (Collider c) {
		if (c.transform.GetComponent<Enemy> ()) {
			float suckTime = Random.Range(minSuckTime, maxSuckTime);
			StartCoroutine (Suck (c.transform.gameObject, suckTime));
		}
	}
}