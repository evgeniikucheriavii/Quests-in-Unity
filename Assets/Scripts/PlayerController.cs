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

	public Rigidbody rb;

	private bool landed = false;
	private bool jumped = false;
	private float jumpSpeed = 250f;


	public void OnCollisionEnter(Collision collision)
	{
		if(collision.transform.tag == "Ground")
		{
			Land();
		}
	}

	public void OnCollisionExit(Collision collision) 
	{
		if(collision.transform.tag == "Ground")
		{
			Fly();
		}
	}

	void Start()
	{
		rb = GetComponent<Rigidbody>();
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
		Jump();
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


	private void Jump()
	{
		if(Input.GetKeyDown(KeyCode.Space))
		{
			if(landed)
			{
				landed = false;
				jumped = true;

				animator.SetBool("Landed", landed);
				animator.SetBool("Jumped", jumped);

				rb.AddForce(Vector3.up * jumpSpeed);
			}
		}
	}

	private void Fly()
	{
		landed = false;
		jumped = false;

		animator.SetBool("Landed", landed);
		animator.SetBool("Jumped", jumped);
	}

	private void Land()
	{
		landed = true;
		jumped = false;
		
		animator.SetBool("Landed", landed);
		animator.SetBool("Jumped", jumped);
	}

}
