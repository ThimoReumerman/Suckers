using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DrawOnMesh : MonoBehaviour {
	public Camera sceneCamera; //Main scene camera
	[SerializeField] Transform quad; //Quad to draw on
	[SerializeField] Transform drawParent; //Parent of the pixels
	Vector3 uvPos; //Position of UV in world space
	[SerializeField] GameObject drawObject; //Object to instantiate when painted
	[SerializeField] Material beginBrush;
	Color currentColor; //Currently used color
	Material currentMaterial;
	float offset = -0.01f; //Offset so the brush doesn't clip
	[SerializeField] float scale = 0.1f; //Scale of brush
	[SerializeField] Text scaleText; //UI Text of scale
	[SerializeField] RenderTexture drawnTexture; //Render texture of camera
	[SerializeField] Material matToGive; //Material to give when applied
	
	bool isBlocked; //Bool to check if you can draw

	void Start () {
		uvPos = quad.position;
		currentColor = Color.black;
		SetSize (scale.ToString ());
		currentMaterial = beginBrush;

	}

	void Update () {
		if (Input.GetButton ("Fire1")) {
			if (!isBlocked) {

				if (HitUV (ref uvPos)) {
					Draw ();
				}
			}
		}
	}

	public void SetBlocked (bool isOver) {
		isBlocked = isOver;
	}

	void Draw () {
		//Make pixel
		GameObject newPixel = Instantiate (drawObject, Vector3.zero, drawObject.transform.rotation, drawParent);

		//Set material
		newPixel.GetComponent<Renderer>().material = currentMaterial;

		//Get material
		Material pixelMat = newPixel.GetComponent<Renderer> ().material;
		
		//Set color
		pixelMat.color = currentColor;

		//Set scale
		Vector3 scaleSet = new Vector3 (scale, scale, scale);
		newPixel.transform.localScale = scaleSet;

		//Set position
		newPixel.transform.localPosition = uvPos;

		//Set name
		newPixel.name = "F";
	}

	public void SetColor (ColorHolder holder) {
		currentColor = holder.color;
		offset -= 0.01f;
	}

	public void SetBrush(BrushHolder holder) {
		currentMaterial = holder.brush;
	}

	public void SetSize (string sizeText) {
		float size;
		float.TryParse (sizeText, out size);
		print ("Size is " + size);
		scale = size / 10;
		scaleText.text = size.ToString ();
	}

	public void Reset () {
		foreach (Transform child in drawParent) {
			Destroy (child.gameObject);
		}
	}

	bool HitUV (ref Vector3 uvWorldPosition) {
		//Make raycast
		RaycastHit hit;

		//Get cursor pos
		Vector3 mousePos = Input.mousePosition;
		Vector3 cursorPos = new Vector3 (mousePos.x, mousePos.y, 0.0f);

		//Make cursor ray
		Ray cursorRay = sceneCamera.ScreenPointToRay (cursorPos);

		//Shoot cursor ray
		if (Physics.Raycast (cursorRay, out hit, 200)) {

			//Get collider of mesh
			MeshCollider meshCollider = hit.collider as MeshCollider;

			//Check if collider is null
			if (meshCollider == null || meshCollider.sharedMesh == null) {
				return false;
			}

			//Get and set position of new brush
			Vector2 pixelUV = new Vector2 (hit.textureCoord.x, hit.textureCoord.y);
			uvWorldPosition.x = pixelUV.x;
			uvWorldPosition.y = pixelUV.y;
			uvWorldPosition.z = offset;
			return true;
		} else {
			return false;
		}
	}

	public void Save () {
		//Make render texture a Texture2d
		RenderTexture.active = drawnTexture;
		int width = drawnTexture.width;
		int height = drawnTexture.height;
		Texture2D tex = new Texture2D (width, height, TextureFormat.RGB24, false);
		tex.ReadPixels (new Rect (0, 0, width, height), 0, 0);
		tex.Apply ();
		RenderTexture.active = null;

		//Give it to a material
		matToGive.mainTexture = tex;

		//Go to main scene
		SceneManager.LoadScene ("Sample1");
	}

}