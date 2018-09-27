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

	private string[] noteNames = { 	"majorA","majorAf","majorAsf","majorAsh","majorAssh","majorB","majorBf","majorBsf","majorBsshCf","majorC","majorCsfBsh",
									"majorCsh","majorCssh","majorD","majorDf","majorDsf","majorDsh","majorDssh","majorE","majorEf","majorEsf","majorFsf",
									"majorEssh","majorF","majorFsh","majorFssh","majorG","majorGf","majorGsf","majorGsh","majorGssh"};	
	private List<Song> majorList = new List<Song>();
	private int randomInt;

	private AudioSource audioSource;

	private CsvReadWrite csvWriter;


	void Start()
	{
		audioSource = GetComponent<AudioSource>();

		if(!isTutorial)
		{
			InitDatabase();
			csvWriter = GetComponent<CsvReadWrite>();
			SetTitle();
		}	
	}

	public void InitDatabase()
	{
		for(int j = 0; j < 31; j++)
		{
			for(int i = 1; i <= 31; i++)
			{
				AudioClip tune = (AudioClip) Resources.Load("Sounds/major/" + noteNames[j] + "/" + noteNames[j] +i);
				Song currentSong = new Song(tune, false, "name");
				majorList.Add(currentSong);
				Debug.Log("Added " + tune.name);
			}
		}
		
	}


	public void PlaySong()
	{
		if(!isTutorial)
		{
			randomInt = Random.Range(0, majorList.Count);
			
			while(majorList[randomInt].played)
			{
				randomInt = Random.Range(0, majorList.Count);
			}

			playButton.SetActive(false);
			AudioClip currentTune = majorList[randomInt].audio; //Get audio from list at random 
			majorList[randomInt].played = true;					//and enable the bool
			Invoke("GetRatingScreen", currentTune.length+0.25f);
			
			audioSource.PlayOneShot(currentTune); 
		}
		else
		{
			AudioClip currentTune = (AudioClip) Resources.Load("Sounds/1"); 
			Invoke("GetRatingScreen", currentTune.length+0.25f);
			
			audioSource.PlayOneShot(currentTune); 
		}
	}

	public void SendRating (string _rating) 
	{
		csvWriter.Save(majorList[randomInt].name, _rating);
		GetPlayingScreen();
	}

	void SetTitle()
	{
		title.text = "Trial " + randomInt.ToString();
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
		randomInt = Random.Range(0, majorList.Count);
	}

}
