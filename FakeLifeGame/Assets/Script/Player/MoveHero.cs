using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveHero : MonoBehaviour {

	Rigidbody rigidbody;
	CharacterController ch;
	Animator anim;

	public float JumpSpeed = 1.0f;
	public float speed = 1.0f;
	public float MaxSpeed = 2f;
	public float turnSpeed;
	public Transform PosTarget;
	public float Gravity = 10f;
	private Vector3 GroundNormal; 
	private float gravityForce;

	GameObject ButText;
	RaycastHit Hit;

	// Use this for initialization
	void Start () {
		//rigidbody = GetComponent<Rigidbody> ();
		anim = GetComponent<Animator> ();
		ButText = GameObject.Find ("SpaceButText");
		ch = GetComponent<CharacterController> ();
		//anim.applyRootMotion = false;
	}


	
	// Update is called once per frame
	void Update () {
		//Vector3 dir = PosTarget.position - transform.position;
		//dir.y = 0;
		//transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (dir), turnSpeed * Time.deltaTime);
		CheckGroundStatus();
		//Debug.Log ("Down: " + anim.GetBool ("Down"));
		if (anim.GetFloat ("JumpForce") == 0) {
			PlayerGravity ();
		} else {
			gravityForce = anim.GetFloat ("JumpForce");
		}
		MoveCh ();



		if (Physics.Raycast (transform.position + Vector3.up * 0.2f, transform.forward, out Hit, 1.1f)) {
			if (Hit.collider.tag == "Pregrada") {
				if (Input.GetKey (KeyCode.Space)) {
					anim.SetBool ("Jump", true);
					if (gameObject.GetComponent<IKanims> ().RunWeight >= 0.6)
						gameObject.GetComponent<IKanims> ().RunWeight = 1;
					else gameObject.GetComponent<IKanims> ().RunWeight = 0;
				} 
			} else {
				anim.SetBool ("Jump", false);
			}

			if (Hit.collider.tag == "Jump over") {
				if (Input.GetKey (KeyCode.Space)) {
					anim.SetBool ("Jump over", true);
				} 
			} else {
				anim.SetBool ("Jump over", false);
			}
			Debug.DrawLine (transform.position + Vector3.up * 0.2f, transform.position + transform.forward + Vector3.up * 0.2f, Color.blue, 1f);
		} else {
			anim.SetBool ("Jump", false);
			anim.SetBool ("Jump over", false);
		}

		/*Запасной вариант просчета падений
		if (Physics.Raycast (transform.position + Vector3.up * 0.65f, Vector3.down * 0.6f + transform.forward * 0.65f, out leftHit, 1.5f)) {
			anim.SetBool ("Down", false);
			Debug.DrawLine (transform.position + Vector3.up * 0.65f, transform.position + transform.forward * 0.65f, Color.green, 1f);
		} else {
			anim.SetBool ("Down", true);
		}*/
	}

	void FixedUpdate()
	{
		//anim.SetFloat ("Fall", rigidbody.velocity.y); Не забыть переназначить падение на новую систему движения

		/*float horizontalAxis = Input.GetAxis("Horizontal");
		float verticalAxis = Input.GetAxis("Vertical");

		SumVect = (transform.right * horizontalAxis) + (transform.forward * verticalAxis);
		SumVect.Normalize ();
		Debug.Log ("Speed: " + rigidbody.velocity);

		if (rigidbody.velocity.magnitude < MaxSpeed) {
			rigidbody.AddForce(SumVect * speed / Time.deltaTime);
		}*/

	}

	void MoveCh(){
		Vector3 moveVector = Vector3.zero;
		moveVector.x = anim.GetFloat ("StrafeSpeed");
		moveVector.z = anim.GetFloat ("Speed");
		moveVector = transform.rotation * moveVector;
		moveVector.y = gravityForce;

		ch.Move (moveVector*Time.deltaTime);
	}

	void OnAnimatorMove()
	{
		if (anim.GetBool ("Jump")) {
			//transform.position = anim.rootPosition;
		}
	}

	private void PlayerGravity(){
		if (!ch.isGrounded) {
			gravityForce -= Gravity * Time.deltaTime;
		} else {
			gravityForce = 0f;
			//anim.SetBool ("Jump", false);
		}
	}

	void CheckGroundStatus()
	{
		RaycastHit hitInfo;

		#if UNITY_EDITOR
		Debug.DrawLine(transform.position + (Vector3.up * 0.1f), transform.position + (Vector3.up * 0.1f) + (Vector3.down * 0.1f));
		#endif

		if (Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, out hitInfo, 0.2f))
		{
			GroundNormal = hitInfo.normal;
			//Debug.Log ("GroundNormal: " + GroundNormal);
			anim.applyRootMotion = true;
		}
		else
		{
			GroundNormal = Vector3.up;
			anim.applyRootMotion = false;
		}
	}

}
