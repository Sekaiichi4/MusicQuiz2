using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour 
{
	//public List<AudioClip> allAudio;
	public GameObject playingScreen;
	public GameObject playButton;
	public GameObject ratingScreen;
	public Text title;
	public bool isTutorial;


	private AudioSource audioSource;
	public int position;

	private CsvReadWrite csvWriter;


	void Start()
	{
		audioSource = GetComponent<AudioSource>();

		if(!isTutorial)
		{
			csvWriter = GetComponent<CsvReadWrite>();
			SetTitle();
		}	
	}

	public void PlaySong()
	{
		playButton.SetActive(false);
		AudioClip currentClip = (AudioClip) Resources.Load("Sounds/"+position);
		Invoke("GetRatingScreen", currentClip.length+0.25f);
		
		audioSource.PlayOneShot(currentClip); 
	}

	public void SendRating (string _rating) 
	{
		csvWriter.Save(position, _rating);
		GetPlayingScreen();
	}

	void SetTitle()
	{
		title.text = "Trial " + position.ToString();
	}

	void GetRatingScreen()
	{
		playingScreen.SetActive(false);
		ratingScreen.SetActive(true);
		playButton.SetActive(true);
	}

	void GetPlayingScreen()
	{
		NextMelody();
		ratingScreen.SetActive(false);
		SetTitle();
		playingScreen.SetActive(true);
	}

	void NextMelody()
	{
		position++;
	}

}
