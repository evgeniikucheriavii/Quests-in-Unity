using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
	private AudioSource audio;

	public AudioClip pieceTheme;
	public AudioClip battleTheme;
	public AudioClip houseTheme;

	public PlayerController pc;
	private bool battle = false;
	private bool inHouse = false;

	// Start is called before the first frame update
	void Start()
	{
		audio = GetComponent<AudioSource>();
	}

	public void OnTriggerEnter(Collider other)
	{
		if(other.transform.tag == "House")
		{
			if(!pc.Battle)
			{
				audio.clip = houseTheme;
				audio.Play();
			}
			
			inHouse = true;
		}
	}

	public void OnTriggerExit(Collider other)
	{
		if(other.transform.tag == "House")
		{
			if(!pc.Battle)
			{
				audio.clip = pieceTheme;
				audio.Play();	
			}

			inHouse = false;
		}
	}

	void FixedUpdate()
	{
		if(!battle)
		{
			if(pc.Battle)
			{
				audio.clip = battleTheme;
				audio.Play();
				battle = true;
			}
		}
		else
		{
			if(!pc.Battle)
			{
				if(inHouse)
				{
					audio.clip = houseTheme;
					audio.Play();
				}
				else
				{
					audio.clip = pieceTheme;
					audio.Play();
				}
				battle = false;
			}
		}
	}
}
