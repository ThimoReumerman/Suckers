using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerSpawners : MonoBehaviour {
	[SerializeField] GameObject player;
	[SerializeField] Shader shader;
	[SerializeField] float spaceBetween;
	byte[] fileData;

	// Use this for initialization
	void Start () {
		string path;
		path = Application.dataPath + "/../SavedImages/";
		int index = 0;
		Vector3 playerPos = Vector3.zero;

		foreach (string file in System.IO.Directory.GetFiles (path)) {
			print ("Current index: " + index);
			GameObject newPlayer = Instantiate (player);
			playerPos.x = index * spaceBetween;
			newPlayer.transform.position = playerPos;
			newPlayer.GetComponent<Rotator> ().enabled = true;
			Renderer rend = newPlayer.GetComponent<Renderer> ();
			rend.material = new Material (shader);

			fileData = File.ReadAllBytes (file);
			Texture2D tex = new Texture2D (2, 2);
			tex.LoadImage (fileData);
			rend.material.mainTexture = tex;

			index += 1;
		}

	}

	// Update is called once per frame
	void Update () {

	}
}