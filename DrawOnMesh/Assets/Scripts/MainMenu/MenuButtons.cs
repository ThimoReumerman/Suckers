﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour {
	public void Play () {

	}

	public void Customize () {

	}

	public void Quit () {
		print ("Quitting...");
		Application.Quit ();
	}
}