using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusic : MonoBehaviour 
{
	public List<AudioClip> allAudio;
	private AudioSource audioSource;
	public int position;


	void Start()
	{
		audioSource = GetComponent<AudioSource>();
		position = 0;
	}

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Space))
		{
			audioSource.PlayOneShot(allAudio[position]); 
		}
	}

	public void NextMelody()
	{
		position++;
	}

}
