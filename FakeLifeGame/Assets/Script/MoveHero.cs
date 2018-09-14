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
	RaycastHit Hit;

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

		if (Physics.Raycast (transform.position + Vector3.up * 0.2f, transform.forward, out Hit, 1.1f)) {
			if (Hit.collider.tag == "Pregrada") {
				if (Input.GetKey (KeyCode.Space)) {
					//rigidbody.AddForce (Vector3.up * JumpSpeed, ForceMode.Impulse);
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
		float horizontalAxis = Input.GetAxis("Horizontal");
		float verticalAxis = Input.GetAxis("Vertical");



		anim.SetFloat ("Fall", rigidbody.velocity.y);

		SumVect = (transform.right * horizontalAxis) + (transform.forward * verticalAxis);
		//SumVect.Normalize ();
		Debug.Log ("Speed: " + rigidbody.velocity);

		if (rigidbody.velocity.magnitude < MaxSpeed) {
			rigidbody.AddForce(SumVect * speed / Time.deltaTime);
		}

	}

}
