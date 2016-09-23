using UnityEngine;
using System.Collections;
using System; //added to access enum

public class Character : MonoBehaviour
{
	////Character Data
	//Labels
	public string Name;
	public string Class;
	public string Species;
	public string Gender;
	public bool bearded = false;
	public bool masked = false;

	public bool UpdateColors = false;

	///Render Data
	public bool UpdateAnimation = true;
	public string AnimationName;
	public string DirectionName;
	public Texture2D characterTexture2D;
	public Sprite[] characterSprites;
	private string[] names;
	private string spritePath;

	public Color Hair = new Color32(96, 60, 40, 255);
	public Color Skin = new Color32(247, 207, 134, 255);
	public Color Pants = new Color32(55, 55, 55, 255);
	public Color ShirtL = new Color32(240, 240, 240, 255);
	public Color ShirtM = new Color32(240, 240, 240, 255);
	public Color ShirtR = new Color32(240, 240, 240, 255);
	public Color Object1 = new Color32(127, 127, 127, 255);
	public Color BeardMask = new Color32(247, 207, 134, 255);
	public Color Badge = new Color32(255, 255, 0, 255);
	public Color Outline = Color.black;

	// Use this for initialization
	void Start ()
	{
		TempStart ();
	}

	void TempStart()
	{
		Species = "Human";
		Gender = "Male";
		AnimationName = "Idle";
		DirectionName = "S";
		spritePath = ("Characters/" + Species + Gender);
		characterSprites = Resources.LoadAll<Sprite>(spritePath);
		names = new string[characterSprites.Length];
		UpdateCharacterTexture ();
		UpdateAnimationImage();
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
				if(copiedTexture.GetPixel(x,y) == new Color32(0,255,0, 255))
				{
					if(masked)
					{
						texture.SetPixel(x, y, BeardMask);
					}
					else if(bearded)
					{
						texture.SetPixel(x, y, Hair);
					}
					else
					{
						texture.SetPixel(x, y, Skin);
					}
				}
				else if(copiedTexture.GetPixel(x,y) == new Color32(255,0,0, 255))
				{
					texture.SetPixel(x, y, ShirtR);
				}
				else if(copiedTexture.GetPixel(x,y) == new Color32(0,0,255, 255))
				{
					texture.SetPixel(x, y, ShirtM);
				}
				else if(copiedTexture.GetPixel(x,y) == new Color32(255,255,0 , 255))
				{
					texture.SetPixel(x, y, ShirtL);
				}
				else if(copiedTexture.GetPixel(x,y) == new Color32(255,0,255, 255 ))
				{
					texture.SetPixel(x, y, Badge);
				}
				else if(copiedTexture.GetPixel(x,y) == new Color32(55,55,55, 255 ))
				{
					texture.SetPixel(x, y, Pants);
				}
				else if(copiedTexture.GetPixel(x,y) == new Color32(247,207,134, 255 ))
				{
					texture.SetPixel(x, y, Skin);
				}
				else if(copiedTexture.GetPixel(x,y) == new Color32(127,127,127, 255 ))
				{
					texture.SetPixel(x, y, Object1);
				}
				else if(copiedTexture.GetPixel(x,y) == new Color32(96,60,40, 255 ))
				{
					texture.SetPixel(x, y, Hair);
				}

				else if(copiedTexture.GetPixel(x,y) == new Color32(0,0,0, 255 ))
				{
					texture.SetPixel(x, y, Outline);
				}

				//DARK COLORS
				else if(copiedTexture.GetPixel(x,y) == new Color32(191,0,0, 255))
				{
					texture.SetPixel(x, y, DarkenColor(ShirtR, 0.8f));
				}
				else if(copiedTexture.GetPixel(x,y) == new Color32(0,0,200, 255))
				{
					texture.SetPixel(x, y, DarkenColor(ShirtM, 0.8f));
				}
				else if(copiedTexture.GetPixel(x,y) == new Color32(200,200,0 , 255))
				{
					texture.SetPixel(x, y, DarkenColor(ShirtL, 0.8f));
				}
				else if(copiedTexture.GetPixel(x,y) == new Color32(42,42,42, 255 ))
				{
					texture.SetPixel(x, y, DarkenColor(Pants, 0.8f));
				}
				else if(copiedTexture.GetPixel(x,y) == new Color32(242,184,77, 255 ) || copiedTexture.GetPixel(x,y) == new Color32(243,188,84, 255) )
				{
					texture.SetPixel(x, y, DarkenSkin(Skin, 0.95f));
				}
				else if(copiedTexture.GetPixel(x,y) == new Color32(64,64,64, 255 ))
				{
					texture.SetPixel(x, y, DarkenColor(Object1, 0.5f));
				}
				else if(copiedTexture.GetPixel(x,y) == new Color32(74,48,32, 255) || copiedTexture.GetPixel(x,y) == new Color32(72,45,30, 255) )
				{
					texture.SetPixel(x, y, DarkenColor(Hair, 0.8f));
				}

				else
				{
					texture.SetPixel(x, y, copiedTexture.GetPixel(x,y));
				}
				++x;
			}
			++y;
		}
		texture.name = (Species+Gender);
		texture.Apply();


		return texture;
	}

	public Color32 DarkenColor(Color32 color, float factor)
	{
		Color hexColor = color;
		hexColor.r *= factor;
		hexColor.g *= factor;
		hexColor.b *= factor;
		color = hexColor;

		return color;
	}

	public Color32 DarkenSkin(Color32 color, float factor)
	{
		//skin      0.969, 0.812, 0.525
		//dark skin 0.949, 0.722, 0.302
		Color hexColor = color;
		hexColor.r -= 0.02f;
		hexColor.g -= 0.09f;
		hexColor.b -= 0.223f;
		hexColor.r *= factor;
		hexColor.g *= factor;
		hexColor.b *= factor;
		color = hexColor;

		return color;
	}

	public void UpdateCharacterTexture()
	{
		Sprite[] loadSprite = Resources.LoadAll<Sprite> (spritePath);
		characterTexture2D = CopyTexture2D(loadSprite[0].texture);

		int i = 0;
		while(i != characterSprites.Length)
		{
			//SpriteRenderer sr = GetComponent<SpriteRenderer>();
			//string tempName = sr.sprite.name;
			//sr.sprite = Sprite.Create (characterTexture2D, sr.sprite.rect, new Vector2(0,1));
			//sr.sprite.name = tempName;

			//sr.material.mainTexture = characterTexture2D;
			//sr.material.shader = Shader.Find ("Sprites/Transparent Unlit");
			string tempName = characterSprites[i].name;
			characterSprites[i] = Sprite.Create (characterTexture2D, characterSprites[i].rect, new Vector2(0,1));
			characterSprites[i].name = tempName;
			names[i] = tempName;
			++i;
		}

		SpriteRenderer sr = GetComponent<SpriteRenderer>();
		sr.material.mainTexture = characterTexture2D;
		sr.material.shader = Shader.Find ("Sprites/Transparent Unlit");

	}

	public void UpdateAnimationImage()
	{
		SpriteRenderer sr = GetComponent<SpriteRenderer> ();
		string animname = DirectionName + "_" + AnimationName;
		sr.sprite = characterSprites [Array.IndexOf (names, animname)];
		UpdateAnimation = false;
	}

	public void Update()
	{
		if (UpdateAnimation)
		{
			UpdateAnimationImage();
		}

		if (UpdateColors)
		{
			UpdateCharacterTexture();
			UpdateColors = false;
		}
	}

}