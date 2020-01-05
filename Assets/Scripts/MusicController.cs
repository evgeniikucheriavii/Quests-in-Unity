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
				inHouse = true;
			}
		}
	}

	public void OnTriggerExit(Collider other)
	{
		if(other.transform.tag == "House")
		{
			if(!pc.Battle)
			{
				audio.clip = pieceTheme;
				inHouse = false;
			}
			
		}
	}

	void FixedUpdate()
	{
		if(!battle)
		{
			if(pc.Battle)
			{
				audio.clip = battleTheme;
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
				}
				else
				{
					audio.clip = pieceTheme;
				}
			}
		}
	}
}
