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
	public bool isListening, isPractise, isMajor, isMinor;
	public int indexListening, indexPractise, index;

	private string[] majorNames = { "majorA","majorAf","majorAsf","majorAsh","majorAssh","majorB","majorBf","majorBsf","majorBsshCf","majorC","majorCsfBsh",
									"majorCsh","majorCssh","majorD","majorDf","majorDsf","majorDsh","majorDssh","majorE","majorEf","majorEsf","majorFsfEsh",
									"majorEsshFf","majorF","majorFsh","majorFssh","majorG","majorGf","majorGsf","majorGsh","majorGssh"};	
	
	private string[] minorNames = { "minorA","minorAf","minorAsf","minorAsh","minorAssh","minorB","minorBf","minorBsf","minorBsshCf","minorC","minorCsfBsh",
									"minorCsh","minorCssh","minorD","minorDf","minorDsf","minorDsh","minorDssh","minorE","minorEf","minorEsf","minorFsfEsh",
									"minorEsshFf","minorF","minorFsh","minorFssh","minorG","minorGf","minorGsf","minorGsh","minorGssh"};	
	private List<Song> currentList = new List<Song>();

	private int randomInt;

	private AudioSource audioSource;

	private CsvReadWrite csvWriter;


	void Start()
	{
		MajorOrMinor(PlayerPrefs.GetInt("MajorOrMinor"));
		audioSource = GetComponent<AudioSource>();
		InitDatabase();
		index = 0;
		indexListening = 0;
		indexPractise = 0;

		csvWriter = GetComponent<CsvReadWrite>();
		SetTitle();	
	}

	public void InitDatabase()
	{
		//MAJOR
		for(int j = 0; j < 31; j++)
		{
			for(int i = 1; i <= 31; i++)
			{
				AudioClip tune;

				if (isMajor)
				{
					tune = (AudioClip) Resources.Load("Sounds/major/" + majorNames
					[j] + "/" + majorNames
					[j] +i);
					Song currentSong = new Song(tune, false, tune.name);
					currentList.Add(currentSong);
					Debug.Log("Added " + tune.name);
				}
				else if (isMinor)
				{
					tune = (AudioClip) Resources.Load("Sounds/minor/" + minorNames
					[j] + "/" + minorNames
					[j] +i);
					Song currentSong = new Song(tune, false, tune.name);
					currentList.Add(currentSong);
					Debug.Log("Added " + tune.name);
				}
			}
		}
	}

	void MajorOrMinor(int i)
	{
		if(i == 0)
		{
			isMajor = true;
			isMinor = false;
		}
		else if(i == 1)
		{
			isMinor = true;
			isMajor = false;
		}
	}


	public void PlaySong()
	{
		if(isListening)
		{
			playButton.SetActive(false);
			AudioClip currentTune = currentList[randomInt].audio; 	//Get audio from list at random 

			if(indexListening == 9)
			{
				isListening = false;
			}
			Invoke("GetPlayingScreen", currentTune.length+0.25f);

			indexListening++;
			
			audioSource.PlayOneShot(currentTune); 
		}
		else if(isPractise)
		{
			playButton.SetActive(false);
			AudioClip currentTune = currentList[randomInt].audio; 	//Get audio from list at random 

			Invoke("GetRatingScreen", currentTune.length+0.25f);
			
			audioSource.PlayOneShot(currentTune); 
		}
		else
		{
			randomInt = Random.Range(0, currentList.Count);
			
			while(currentList[randomInt].played)
			{
				randomInt = Random.Range(0, currentList.Count);
			}

			playButton.SetActive(false);
			AudioClip currentTune = currentList[randomInt].audio; 	//Get audio from list at random 
			currentList[randomInt].played = true;					//and enable the bool
			Invoke("GetRatingScreen", currentTune.length+0.25f);
			
			audioSource.PlayOneShot(currentTune); 
		}
	}

	public void SendRating (string _rating) 
	{
		if(isPractise)
		{
			if(indexPractise == 9)
			{
				isPractise = false;
			}
			indexPractise++;
			GetPlayingScreen();
		}
		else
		{
			csvWriter.Save(currentList[randomInt].name, _rating);
			index++;
			GetPlayingScreen();
		}
	}

	void SetTitle()
	{
		if(isListening)
		{
			title.text = "Listening";
		}
		else if(isPractise)
		{
			title.text = "Practise";
		}
		else
		{
			title.text = "Trial";
		}
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
		randomInt = Random.Range(0, currentList.Count);
	}

}
