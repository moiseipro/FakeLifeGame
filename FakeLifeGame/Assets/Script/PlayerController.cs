using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float Speed = 5f;
	public float Veloscity = 0.1f;
	public float jumpPower = 10f;

	private float gravityForce;
	private Vector3 moveVector;
	float posPl = 0f;

	private CharacterController ch_controller;
	private Animator ch_animator;
	private Camera ch_camera;


	// Use this for initialization
	void Start () {
		ch_controller = GetComponent<CharacterController> ();
		ch_animator = GetComponent<Animator> ();
		ch_camera = GameObject.FindWithTag ("MainCamera").GetComponent<Camera> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Razgon ();
		MoveHero ();
		Gravity ();
	}

	public void Razgon(){
		if (Input.GetKey (KeyCode.W) || Input.GetKey (KeyCode.S) || Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.D)) {
			if (Input.GetKey (KeyCode.LeftShift)) {
				if (Veloscity < Speed/2.8f)
					Veloscity += 0.1f;
				if (Veloscity > Speed / 2.8f) {
					Veloscity -= 0.1f;
				} else Veloscity = Speed / 2.8f;
			} else {
				if (Veloscity <= Speed)
					Veloscity += 0.1f;
			}

		} else {
			if (Veloscity > 0.2)
				Veloscity -= 0.2f;
			else
				Veloscity = 0.0f;
		}
	}

	private void MoveHero (){
		moveVector = transform.forward;
		if (ch_controller.isGrounded) {
			if (Input.GetKey (KeyCode.W)) {
				//moveVector = transform.forward;
				moveVector = (new Vector3 (ch_camera.transform.forward.x, 0, ch_camera.transform.forward.z));
				//transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation (moveVector), 6f * Time.deltaTime);
			}
			if (Input.GetKey (KeyCode.S)) {
				moveVector = -(new Vector3 (ch_camera.transform.forward.x, 0, ch_camera.transform.forward.z));
				//transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation (moveVector), 6f * Time.deltaTime);
			}
			if (Input.GetKey (KeyCode.D)) {
				//moveVector = transform.right;
				if (Input.GetKey (KeyCode.W))
					moveVector = (new Vector3 (ch_camera.transform.right.x + ch_camera.transform.forward.x, 0, ch_camera.transform.right.z + ch_camera.transform.forward.z)) / 1.5f;
				else if (Input.GetKey (KeyCode.S))
					moveVector = (new Vector3 (ch_camera.transform.right.x - ch_camera.transform.forward.x, 0, ch_camera.transform.right.z - ch_camera.transform.forward.z)) / 1.5f;
				else {
					moveVector = (new Vector3 (ch_camera.transform.right.x, 0, ch_camera.transform.right.z));
				}
				//transform.rotation = Quaternion.LerpUnclamped (transform.rotation, Quaternion.LookRotation (moveVector), 6f * Time.deltaTime);
			}
			if (Input.GetKey (KeyCode.A)) {
				if (Input.GetKey (KeyCode.W))
					moveVector = (new Vector3 (ch_camera.transform.forward.x - ch_camera.transform.right.x, 0, ch_camera.transform.forward.z - ch_camera.transform.right.z)) / 1.5f;
				else if (Input.GetKey (KeyCode.S))
					moveVector = -(new Vector3 (ch_camera.transform.forward.x + ch_camera.transform.right.x, 0, ch_camera.transform.forward.z + ch_camera.transform.right.z)) / 1.5f;
				else {
					moveVector = -(new Vector3 (ch_camera.transform.right.x, 0, ch_camera.transform.right.z));
				}
				//transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation (moveVector), 6f * Time.deltaTime);
			}
			/*if(Vector3.Angle(transform.forward,moveVector)>10)ch_animator.SetFloat ("Rotate", ch_animator.GetFloat ("Rotate")-0.1f);
			else if(Vector3.Angle(transform.forward,moveVector)<=10 && Vector3.Angle(transform.forward,moveVector)> 5) ch_animator.SetFloat ("Rotate", ch_animator.GetFloat ("Rotate")+0.1f);
			else ch_animator.SetFloat ("Rotate", 0);*/
			transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation (moveVector), 4f * Time.deltaTime);
			Debug.Log (Vector3.Angle(transform.forward,moveVector));

		}
		ch_animator.SetFloat ("Speed", Veloscity);
		moveVector.y = gravityForce;
		ch_controller.Move (moveVector * Veloscity * Time.deltaTime);
	}

	private void Gravity(){
		if (!ch_controller.isGrounded) {
			gravityForce -= 14f * Time.deltaTime;
			ch_animator.SetBool ("IsGrounded", true);
		} else {
			gravityForce = -1f;
		}
		if (Input.GetKeyDown (KeyCode.Space) && ch_controller.isGrounded && ch_animator.GetBool("IsGrounded")) {
			ch_animator.SetBool ("IsGrounded", false);
			gravityForce = jumpPower;
		}
	}
}
