using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveHero : MonoBehaviour {

	Rigidbody rigidbody;
	public float JumpSpeed = 1.0f;
	public float speed = 1.0f;
	public float turnSpeed;
	public Transform PosTarget;
	public Camera camera;
	public Vector3 SumVect;

	// Use this for initialization
	void Start () {
		rigidbody = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 dir = PosTarget.position - transform.position;
		dir.y = 0;
		//transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (dir), turnSpeed * Time.deltaTime);
		if (Input.GetKeyDown (KeyCode.Space)) {
			rigidbody.AddForce(Vector3.up * JumpSpeed, ForceMode.Impulse);
		}
	}

	void FixedUpdate()
	{
		//reading the input:
		float horizontalAxis = Input.GetAxis("Horizontal");
		float verticalAxis = Input.GetAxis("Vertical");

		SumVect = (transform.right * horizontalAxis) + (transform.forward * verticalAxis);
		SumVect.Normalize ();
		Debug.Log ("Speed: " + rigidbody.velocity.magnitude);

		rigidbody.AddForce(SumVect * speed / Time.deltaTime);

	}

}
