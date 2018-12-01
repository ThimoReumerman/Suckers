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
	Color currentColor = Color.black; //Currently used color
	Material currentMaterial;
	float offset = -0.01f; //Offset so the brush doesn't clip
	[SerializeField] float scale = 0.1f; //Scale of brush
	[SerializeField] Text scaleText; //UI Text of scale
	[SerializeField] RenderTexture drawnTexture; //Render texture of camera
	[SerializeField] Material matToGive; //Material to give when applied
	[SerializeField] Shader unlitTex;

	[SerializeField] Material baseMaterial;

	[SerializeField] Image previewImg;

	string pngName;

	bool isBlocked; //Bool to check if you can draw

	void Start () {
		uvPos = quad.position;
		//		SetSize (scale.ToString ());
		currentMaterial = beginBrush;
		pngName = "Unnamed.png";
		baseMaterial.mainTexture = null;

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

	int index;

	void Draw () {
		//Make pixel
		GameObject newPixel = Instantiate (drawObject, Vector3.zero, drawObject.transform.rotation, drawParent);

		//Set material
		newPixel.GetComponent<Renderer> ().material = currentMaterial;

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
		newPixel.name = index + "pixel";
		index += 1;

		if (index > 1000) {
			MergeTexture ();
			index = 0;
		}
	}

	public void SetRed (string colorValue) {
		float red;
		float.TryParse (colorValue, out red);
		if (red > 255) {
			red = 255;
		}
		currentColor.r = red / 255;

		UpdateColorPreview ();
	}

	public void SetBlue (string colorValue) {
		float blue;
		float.TryParse (colorValue, out blue);
		if (blue > 255) {
			blue = 255;
		}
		currentColor.b = blue / 255;
		UpdateColorPreview ();
	}

	public void SetGreen (string colorValue) {
		float green;
		float.TryParse (colorValue, out green);
		if (green > 255) {
			green = 255;
		}

		currentColor.g = green / 255;

		UpdateColorPreview ();
	}

	void UpdateColorPreview () {
		offset -= 0.01f;
		previewImg.color = currentColor;
	}

	public void SetBrush (BrushHolder holder) {
		currentMaterial = holder.brush;
		offset += -0.01f;
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

	Texture2D mergedTexture;

	void MergeTexture () {
		print ("Merging...");
		//Make render texture a Texture2d
		RenderTexture.active = drawnTexture;
		int width = drawnTexture.width;
		int height = drawnTexture.height;
		Texture2D tex = new Texture2D (width, height, TextureFormat.RGB24, false);
		tex.ReadPixels (new Rect (0, 0, width, height), 0, 0);
		tex.Apply ();
		RenderTexture.active = null;
		baseMaterial.mainTexture = tex;

		foreach (Transform child in drawParent) {
			Destroy (child.gameObject);
		}

		mergedTexture = tex;
	}

	public void Save () {
		MergeTexture ();

		SaveTexture (mergedTexture);
	}

	public void SetImageName (string imgName) {
		pngName = imgName + ".png";
	}

	public void SaveTexture (Texture2D texture) {
		byte[] bytes = texture.EncodeToPNG ();
		string _path = Application.dataPath + "/../SavedImages/";

		if (!Directory.Exists (_path)) {
			Directory.CreateDirectory (_path);
		}

		string fullPath = _path + pngName;

		File.WriteAllBytes (fullPath, bytes);

		print ("Saving worked! You can find the PNG image at " + fullPath);
	}

}