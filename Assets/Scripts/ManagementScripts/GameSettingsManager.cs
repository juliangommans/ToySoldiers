using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameSettingsManager : MonoBehaviour {

	public float gameSpeed;
	public float sfxVolume;
	public float musicVolume;
	private GameObject overlay;

	void Awake(){
		overlay = GameObject.Find ("OverlayPanel");
		gameSpeed = 3f;
		sfxVolume = 3f;
		musicVolume = 3f;
	}

	public void SaveSettings(){
		SetGameSpeed();
		SetSFXVolume ();
		SetMusicVolume ();
		overlay.SetActive (false);
	}

	public void SetGameSpeed(){
		gameSpeed = GameObject.Find ("GameSpeedSlider").GetComponent<Slider>().value;
	}
	public void SetSFXVolume(){
		sfxVolume = GameObject.Find ("SFXVolumeSlider").GetComponent<Slider>().value;
	}
	public void SetMusicVolume(){
		musicVolume = GameObject.Find ("MusicVolumeSlider").GetComponent<Slider>().value;
	}

	public void CloseWithoutSave(){
		overlay.SetActive (false);
	}
}
