﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Lock();
	}

	void Lock() {
		Cursor.lockState = CursorLockMode.Locked	;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
