using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKanims : MonoBehaviour {

	Animator anim;
	public bool PlayerJump = false;

	public float RunWeight;

	public float lookIKweight;
	public float eyesWeight;
	public float headWeight;
	public float bodyWeight;
	public float clampWeight;
	public Transform targetPos;

	public float angularSpeed;
	bool isPlayRot;
	public float luft;

	Vector3 leftFootPos;
	Vector3 rightFootPos;
	Quaternion leftFootRot;
	Quaternion rightFootRot;
	float leftFootWeight;
	float rightFootWeight;

	Transform leftFoot;
	Transform rightFoot;

	public float offsetYfoot;

	float HandWaight;
	public float MaxHandWaight;

	public KeyCode up;
	public KeyCode down;
	public KeyCode left;
	public KeyCode right;

	public float SpeedAxisY = 8f;
	public float SpeedAxisX = 8f;
	float x = 0;
	float y = 0;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();

		leftFoot = anim.GetBoneTransform (HumanBodyBones.LeftFoot);
		leftFootRot = leftFoot.rotation;
		rightFoot = anim.GetBoneTransform (HumanBodyBones.RightFoot);
		rightFootRot = rightFoot.rotation;
	}

	void OnTriggerStay(Collider col) {
		if (col.tag == "hop down low" && x>0.99f) {
			anim.SetBool ("Down", true);
			gameObject.GetComponent<IKanims> ().PlayerJump = true;
		}
	}
	void OnTriggerExit(Collider col) {
		if (col.tag == "hop down low") {
			anim.SetBool ("Down", false);
			gameObject.GetComponent<IKanims> ().PlayerJump = false;
		}
	}
	
	// Update is called once per frame
	void Update () {

		float Vert = Input.GetAxis ("Vertical");
		float Horiz = Input.GetAxis ("Horizontal");

		if (GetComponent<Magic> ().MoveCast == true) {
			HandWaight = Mathf.Lerp (HandWaight, MaxHandWaight, Time.deltaTime * 3f);
		} else {
			HandWaight = Mathf.Lerp (HandWaight, 0, Time.deltaTime * 3f);
		}

		FootIK ();

		if (Input.GetKey (up)) {
			x = Mathf.Lerp (x, 1f, Time.deltaTime * SpeedAxisX);
		} else if (Input.GetKey (down)) {
			x = Mathf.Lerp (x, -1f, Time.deltaTime * SpeedAxisX);
		} else x = Mathf.Lerp (x, 0, Time.deltaTime * SpeedAxisX);

		if (Input.GetKey (left)) {
			y = Mathf.Lerp (y, -1f, Time.deltaTime * SpeedAxisY);
		} else if (Input.GetKey (right)) {
			y = Mathf.Lerp (y, 1f, Time.deltaTime * SpeedAxisY);
		} else y = Mathf.Lerp (y, 0, Time.deltaTime * SpeedAxisY);

		x = (float)System.Math.Round (x,3);
		y = (float)System.Math.Round (y,3);

		anim.SetFloat ("Walk", x, 0.1f, Time.deltaTime);
		anim.SetFloat ("Strafe", y, 0.1f, Time.deltaTime);

		if ((Vert != 0 || Horiz != 0) && Input.GetKey (KeyCode.LeftShift) && (anim.GetFloat ("Fall") <= 0.1 && anim.GetFloat ("Fall") >= -0.1)) {
				RunWeight = Mathf.Lerp (RunWeight, 1f, Time.deltaTime * 3f);
				anim.SetLayerWeight (1, RunWeight);
		} else if (anim.GetFloat ("Fall") <= 0.2 && anim.GetFloat ("Fall") >= -0.2){
			RunWeight = Mathf.Lerp (RunWeight, 0f, Time.deltaTime * 3f);
			anim.SetLayerWeight (1, RunWeight);
		}

		Vector3 rot = transform.eulerAngles;
		transform.LookAt (targetPos.position);

		float angleBetween = Mathf.DeltaAngle (transform.eulerAngles.y, rot.y);
		if(Mathf.Abs(angleBetween) > luft || anim.GetFloat ("Speed") !=0 || anim.GetFloat ("StrafeSpeed") != 0){
			isPlayRot = true;
		}
		if (isPlayRot == true && PlayerJump == false) {
			float bodyY = Mathf.LerpAngle (rot.y, transform.eulerAngles.y, angularSpeed * Time.deltaTime);
			transform.eulerAngles = new Vector3 (rot.x, bodyY, rot.z);

			if ((x >= -0.1f && x <= 0.1f) && (y >= -0.1f && y <= 0.1f)) {
				//anim.SetBool ("Turn", true);
			} else {
				//anim.SetBool ("Turn", false);
			}

			if (Mathf.Abs (angleBetween) * Mathf.Deg2Rad <= Time.deltaTime * angularSpeed) {
				isPlayRot = false;
				anim.SetBool ("Turn", false);
			}
		} else {
			transform.eulerAngles = rot;
		}
		//Debug.Log ("X: " + x + " Y:" + y);
	}

	void FixedUpdate(){

		/*if (anim.GetCurrentAnimatorStateInfo (0).IsName ("Move")) {
			anim.applyRootMotion = false;
		} else if (anim.GetCurrentAnimatorStateInfo (0).IsName ("Down")) {
			anim.applyRootMotion = true;
		} else if (anim.GetCurrentAnimatorStateInfo (0).IsName ("CastRune")) {
			anim.applyRootMotion = true;
		} else if (anim.GetCurrentAnimatorStateInfo (0).IsName ("Jump over")) {
			anim.applyRootMotion = true;
		} else if (anim.GetCurrentAnimatorStateInfo (0).IsName ("jump")) {
			anim.applyRootMotion = true;
		} else if (anim.GetCurrentAnimatorStateInfo (0).IsName ("Fall")) {
			anim.applyRootMotion = true;
		}*/

	}

	void FootIK(){
		RaycastHit leftHit;
		Vector3 lpos = leftFoot.position;
		if (Physics.Raycast (lpos + Vector3.up * 0.5f, Vector3.down, out leftHit, 1)) {
			leftFootPos = Vector3.Lerp (lpos, leftHit.point + Vector3.up * offsetYfoot, Time.deltaTime * 20f);
			leftFootRot = Quaternion.FromToRotation(transform.up,leftHit.normal) * transform.rotation;
			//Debug.DrawLine (lpos + Vector3.up * 0.5f, leftFootPos, Color.red, 1f);
			//Debug.Log (leftHit.collider);
		}
		RaycastHit rightHit;
		Vector3 rpos = rightFoot.position;
		if (Physics.Raycast (rpos + Vector3.up * 0.5f, Vector3.down, out rightHit, 1)) {
			rightFootPos = Vector3.Lerp (rpos, rightHit.point + Vector3.up * offsetYfoot, Time.deltaTime * 20f);
			rightFootRot = Quaternion.FromToRotation(transform.up,rightHit.normal) * transform.rotation;
			//Debug.DrawLine (rpos + Vector3.up * 0.5f, rightFootPos, Color.red, 1f);
			//Debug.Log (rightHit.collider);
		}
	}

	void OnAnimatorIK(){
		anim.SetLookAtWeight (lookIKweight, bodyWeight, headWeight, eyesWeight, clampWeight);
		anim.SetLookAtPosition (targetPos.position);

		//leftFootWeight = anim.GetFloat ("LeftFoot");
		anim.SetIKPositionWeight (AvatarIKGoal.LeftFoot, leftFootWeight);
		anim.SetIKPosition(AvatarIKGoal.LeftFoot, leftFootPos);

		anim.SetIKRotationWeight (AvatarIKGoal.LeftFoot, leftFootWeight);
		anim.SetIKRotation(AvatarIKGoal.LeftFoot, leftFootRot);

		//rightFootWeight = anim.GetFloat ("RightFoot");
		anim.SetIKPositionWeight (AvatarIKGoal.RightFoot, rightFootWeight);
		anim.SetIKPosition(AvatarIKGoal.RightFoot, rightFootPos);

		anim.SetIKRotationWeight (AvatarIKGoal.RightFoot, rightFootWeight);
		anim.SetIKRotation(AvatarIKGoal.RightFoot, rightFootRot);

		anim.SetIKPositionWeight (AvatarIKGoal.RightHand, HandWaight);
		anim.SetIKPosition(AvatarIKGoal.RightHand, targetPos.position);
	
	}

}
