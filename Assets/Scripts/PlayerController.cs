using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	private bool walking = false;
	private bool walkingBack = false;

	private float speed = 2f;
	private float sideSpeed = 35f;

	private float mouseSpeed = 10f;
	private float mouseX = 0f;
	private float mouseY = 0f;
	public GameObject cam;

	public Animator animator;



	void Start()
	{

	}

	void Update()
	{

	}

	void FixedUpdate()
	{
		Controll();
	}

	private void Controll()
	{
		ControllKeyboard();
	}

	private void ControllKeyboard()
	{
		float dSide = Input.GetAxis("Horizontal");
		float dForw = Input.GetAxis("Vertical");

		if(dForw != 0)
		{
			transform.Translate(Vector3.forward * speed * dForw * Time.deltaTime);

			if(!walking)
			{
				walking = true;
				animator.SetBool("Walking", walking);	
			}

			if(dForw < 0)
			{
				if(!walkingBack)
				{
					walkingBack = true;
					animator.SetBool("WalkingBack", walkingBack);
				}
			}
			else
			{
				if(walkingBack)
				{
					walkingBack = false;
					animator.SetBool("WalkingBack", walkingBack);	
				}
			}
		}
		else
		{
			if(walking)
			{
				walking = false;
				animator.SetBool("Walking", walking);	
			}

			if(walkingBack)
			{
				walkingBack = false;
				animator.SetBool("WalkingBack", walkingBack);	
			}
		}

		if(dSide != 0)
		{
			transform.Rotate(new Vector3(0f, dSide * Time.deltaTime * sideSpeed, 0f));
		}
	}


}
