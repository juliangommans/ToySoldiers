using UnityEngine;
using System.Collections;

public class HighlightBlack : MonoBehaviour {
	
	private Sprite sprite;
	public Color color;
	public bool triggerColor;
	public Texture2D characterTexture2D;

	void Start () {
		triggerColor = false;
		sprite = this.GetComponent<SpriteRenderer>().sprite;
	}

	public Texture2D CopyTexture2D(Texture2D copiedTexture)
	{
		Texture2D texture = new Texture2D(copiedTexture.width, copiedTexture.height);
		texture.filterMode = FilterMode.Point;
		texture.wrapMode = TextureWrapMode.Clamp;

		int y = 0;
		while (y < texture.height)
		{
			int x = 0;
			while (x < texture.width)
			{
				Debug.Log ("red = " + copiedTexture.GetPixel (x, y).r + " | green = " + copiedTexture.GetPixel (x, y).g + " | blue = " + copiedTexture.GetPixel (x, y).b);
				if (BlackishChecker(copiedTexture.GetPixel (x, y))) {
					texture.SetPixel (x, y, color);
				} else {
					texture.SetPixel(x, y, copiedTexture.GetPixel(x,y));
				}
				++x;
			}
			++y;
		}
		texture.Apply();
		return texture;
	}

	void UpdateSprite(){
		characterTexture2D = CopyTexture2D (sprite.texture);
		SpriteRenderer sr = GetComponent<SpriteRenderer>();
		Vector2 pos = new Vector2 (0.5f, 0.5f);
		Sprite newSprite = Sprite.Create(characterTexture2D, sprite.rect, pos,32f);
		sr.sprite = newSprite;
		sr.material.mainTexture = characterTexture2D;
//		sr.material.shader = Shader.Find ("Sprites/Transparent Unlit");
	}

	private bool BlackishChecker(Color pixel){
		// This is the cutoff for #1a1a1a in piskel
		bool red = pixel.r <= 0.099;
		bool blue = pixel.b <= 0.099;
		bool green = pixel.g <= 0.099;
		bool alpha = pixel.a >= 0.99;
		if (red && blue && green && alpha) {
			return true;
		} else {
			return false;
		}
	}
		
	void Update () {
		if (triggerColor) {
			UpdateSprite();
			triggerColor = false;
		}
	}
}
