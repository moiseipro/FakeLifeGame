using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveHero : MonoBehaviour {

	Rigidbody rigidbody;
	Animator anim;
	public float JumpSpeed = 1.0f;
	public float speed = 1.0f;
	public float MaxSpeed = 2f;
	public float turnSpeed;
	public Transform PosTarget;
	public Camera camera;
	public Vector3 SumVect;

	GameObject ButText;

	// Use this for initialization
	void Start () {
		rigidbody = GetComponent<Rigidbody> ();
		anim = GetComponent<Animator> ();
		ButText = GameObject.Find ("SpaceButText");
	}
	
	// Update is called once per frame
	void Update () {
		//Vector3 dir = PosTarget.position - transform.position;
		//dir.y = 0;
		//transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (dir), turnSpeed * Time.deltaTime);
		RaycastHit leftHit;

		if (Physics.Raycast (transform.position + Vector3.up * 0.2f, transform.forward, out leftHit, 1.1f)) {
			if (leftHit.collider.tag == "Pregrada") {
				ButText.SetActive (true);
				if (Input.GetKeyDown (KeyCode.Space)) {
					//rigidbody.AddForce (Vector3.up * JumpSpeed, ForceMode.Impulse);
					anim.SetBool ("Jump", true);
					ButText.SetActive (false);
				} 
			} else ButText.SetActive (false);
			Debug.DrawLine (transform.position + Vector3.up * 0.2f, transform.position + transform.forward + Vector3.up * 0.2f, Color.blue, 1f);
		} else {
			anim.SetBool ("Jump", false);
			ButText.SetActive (false);
		}


	}

	void FixedUpdate()
	{
		//reading the input:
		float horizontalAxis = Input.GetAxis("Horizontal");
		float verticalAxis = Input.GetAxis("Vertical");

		SumVect = (transform.right * horizontalAxis) + (transform.forward * verticalAxis);
		//SumVect.Normalize ();
		Debug.Log ("Speed: " + rigidbody.velocity.magnitude);

		if (rigidbody.velocity.magnitude < MaxSpeed) {
			rigidbody.AddForce(SumVect * speed / Time.deltaTime);
		}

	}

}
