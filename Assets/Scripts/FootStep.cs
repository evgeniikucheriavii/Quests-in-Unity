using UnityEngine;

public class FootStep : MonoBehaviour
{

	AudioSource audio;

	public void OnTriggerEnter(Collider other)
	{
		if(other.transform.tag == "Ground")
		{
			audio.Play();
		}
	}

	public void OnCollisionEnter(Collision other)
	{
		if(other.transform.tag == "Ground")
		{
			audio.Play();
		}
	}

	void Start()
	{
		audio = GetComponent<AudioSource>();
	}
}
