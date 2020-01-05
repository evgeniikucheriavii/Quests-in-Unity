using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	private bool walking = false;
	private bool walkingBack = false;
	private bool startedWalking = false;

	private float speed = 2f;
	private float sideSpeed = 35f;

	public GameObject cam;
	public GameObject camWrapper;
	public GameObject camHolder;
	public Camera camera;

	private float mouseSpeed = 100f;

	private float mouseRotationSpeed = 10f;
	private float maxCamX = 25f;
	private float minCamX = 5f;
	private float camX = 0f;

	public Animator animator;

	public Rigidbody rb;

	private bool landed = false;
	private bool jumped = false;

	private float jumpSpeed = 250f;

	private AudioSource useSound;

	private bool battle = false;

	private HUDController hud;

	private GameObject aim;


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
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = (false);

		camX = cam.transform.rotation.x;
		useSound = GetComponent<AudioSource>();

		hud = GetComponent<HUDController>();
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
		ControllMouse();
		Jump();
	}

	private void ControllKeyboard()
	{
		ControllMovement();
		ControllInteractions();
	}

	#region Movement

	private void ControllMovement()
	{
		float dSide = Input.GetAxis("Horizontal");
		float dForw = Input.GetAxis("Vertical");

		if(!animator.GetCurrentAnimatorStateInfo(0).IsName("JumpEnd"))
		{
			if(dForw != 0)
			{
				transform.Translate(Vector3.forward * speed * dForw * Time.fixedDeltaTime);

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

				startedWalking = false;
			}

			if(dSide != 0)
			{
				transform.Rotate(new Vector3(0f, dSide * Time.fixedDeltaTime * sideSpeed, 0f));
			}
		}
	}

	#endregion

	private void ControllInteractions()
	{
		if(Input.GetKeyUp(KeyCode.E))
		{
			useSound.Play();
			
		}
	}

	private void ControllMouse()
	{
		MouseMovement();
		Aim();

	}

	private void MouseMovement()
	{
		float mouseX = Input.GetAxis("Mouse X");
		float mouseY = Input.GetAxis("Mouse Y") * -1;

		if(mouseX != 0)
		{
			if(walking)
			{
				transform.Rotate(new Vector3(0f, mouseX * Time.fixedDeltaTime * mouseSpeed, 0f));
			}
			else
			{
				camWrapper.transform.RotateAround(transform.position, Vector3.up, mouseX * Time.fixedDeltaTime * mouseSpeed);
			}
		}

		if(walking && !startedWalking)
		{
			startedWalking = true;
			transform.rotation = Quaternion.LookRotation(camWrapper.transform.forward, camWrapper.transform.up);

			camWrapper.transform.rotation = camHolder.transform.rotation;
			camWrapper.transform.position = camHolder.transform.position;


		}

		if(mouseY != 0)
		{
			camX += mouseY * Time.fixedDeltaTime * mouseRotationSpeed;
			camX = Mathf.Clamp(camX, minCamX, maxCamX);

			cam.transform.localRotation = Quaternion.AngleAxis(camX, Vector3.right);
		}
	}

	private void Aim()
	{
		Ray ray = camera.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));

		RaycastHit hit;

		if (Physics.Raycast(ray, out hit))
		{

			float dist = Vector3.Distance(hit.transform.position, transform.position);

			if(dist < 1.5)
			{
				aim = hit.transform.gameObject;
			}
			else
			{
				aim = null;
			}
		}
		else
		{
			aim = null;
		}

		if(aim != null)
		{
			hud.SetCursorText($"{aim.transform.name}");
		}
		else
		{
			hud.SetCursorText("");	
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

	public bool Battle 
	{
		get 
		{
			return this.battle;
		} 
	}

}
