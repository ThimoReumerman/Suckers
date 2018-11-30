using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetName : MonoBehaviour {
	[SerializeField] Text nameText;
	[SerializeField] Transform player;

	// Use this for initialization
	void Start () {

		nameText.text = player.name;
	}

}