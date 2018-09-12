using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKanims : MonoBehaviour {

	Animator anim;

	public float lookIKweight;
	public float eyesWeight;
	public float headWeight;
	public float bodyWeight;
	public float clampWeight;
	public Transform targetPos;

	public float angularSpeed;
	bool isPlayRot;
	public float luft;




	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();

	}
	
	// Update is called once per frame
	void Update () {

		float Vert = Input.GetAxis ("Vertical");
		float Horiz = Input.GetAxis ("Horizontal");

		anim.SetFloat ("Walk", Vert);
		anim.SetFloat ("Strafe", Horiz);

		Vector3 rot = transform.eulerAngles;
		transform.LookAt (targetPos.position);

		float angleBetween = Mathf.DeltaAngle (transform.eulerAngles.y, rot.y);
		if(Mathf.Abs(angleBetween) > luft || gameObject.GetComponent<MoveHero> ().SumVect != Vector3.zero){
			isPlayRot = true;
		}
		if (isPlayRot == true) {
			float bodyY = Mathf.LerpAngle (rot.y, transform.eulerAngles.y, angularSpeed * Time.deltaTime);
			transform.eulerAngles = new Vector3 (rot.x, bodyY, rot.z);

			if (Vert == 0 && Horiz == 0) {
				anim.SetBool ("Turn", true);
			} else {
				anim.SetBool ("Turn", false);
			}

			if (Mathf.Abs (angleBetween) * Mathf.Deg2Rad <= Time.deltaTime * angularSpeed) {
				isPlayRot = false;
				anim.SetBool ("Turn", false);
			}
		} else {
			transform.eulerAngles = rot;
		}

	}

	void OnAnimatorIK(){

		anim.SetLookAtWeight (lookIKweight, bodyWeight, headWeight, eyesWeight, clampWeight);
		anim.SetLookAtPosition (targetPos.position);
	
	}

}
