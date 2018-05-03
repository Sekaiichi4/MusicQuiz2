using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizScript : MonoBehaviour 
{
	public List<Oscillator> oscillators;
	public List<bool> availablePositions;
	public int level;
	public int currentRandom;
	public bool playing;

	// Use this for initialization
	void Start () 
	{
		level = 1;
		playing = false;

		for (int i = 0; i < 31; i++)
		{
			availablePositions.Add(true);
		}
		
		NewRandom();
	}

	public void NextLevel()
	{
		//Analytics stuff calling
		//....

		availablePositions[currentRandom] = false;
		level++;
		NewRandom();
	}

	void NewRandom()
	{
		int newValue = Random.Range(0, 31);

		if(newValue != currentRandom && availablePositions[newValue] == true)
		{
			currentRandom = newValue;
		}
		else
		{
			NewRandom();
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.Space) && playing == false)
        {
			playing = true;
			StartCoroutine(playLoop());
		}
	}

	IEnumerator playLoop()
	{
		WaitForSeconds quarterSec = new WaitForSeconds(0.25f);
		WaitForSeconds halfSec = new WaitForSeconds(0.5f);
		WaitForSeconds oneSec = new WaitForSeconds(1);
		WaitForSeconds twoSec = new WaitForSeconds(2);

		for (int j = 0; j < 31; j++) //All the tunes in the octave
		{
			for(int i = 0; i < oscillators.Count; i++)
			{
				oscillators[i].PlayTune(j);
			}
			yield return halfSec;

			for(int i = 0; i < oscillators.Count; i++)
			{
				oscillators[i].StopPlaying();
			}
			yield return quarterSec;
		}

		//Play the random one

		for(int i = 0; i < oscillators.Count; i++)
		{
			oscillators[i].PlayTune(currentRandom);
		}
		yield return oneSec;

		for(int i = 0; i < oscillators.Count; i++)
		{
			oscillators[i].StopPlaying();
		}

		playing = false;
	}
}
