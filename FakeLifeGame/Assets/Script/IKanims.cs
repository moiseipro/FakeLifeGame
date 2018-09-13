using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKanims : MonoBehaviour {

	Animator anim;
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

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();

		leftFoot = anim.GetBoneTransform (HumanBodyBones.LeftFoot);
		leftFootRot = leftFoot.rotation;
		rightFoot = anim.GetBoneTransform (HumanBodyBones.RightFoot);
		rightFootRot = rightFoot.rotation;
	}
	
	// Update is called once per frame
	void Update () {

		float Vert = Input.GetAxis ("Vertical");
		float Horiz = Input.GetAxis ("Horizontal");

		FootIK ();



		if (Vert != 0 || Horiz != 0) {
			if (Input.GetKey (KeyCode.LeftShift)) {
				RunWeight = Mathf.Lerp (RunWeight, 1f, Time.deltaTime * 5f);
				anim.SetLayerWeight (1, RunWeight);
			} else {
				RunWeight = Mathf.Lerp (RunWeight, 0f, Time.deltaTime * 5f);
				anim.SetLayerWeight (1, RunWeight);
			}
		}

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

	void FootIK(){
		RaycastHit leftHit;
		Vector3 lpos = leftFoot.position;
		if (Physics.Raycast (lpos + Vector3.up * 0.5f, Vector3.down, out leftHit, 1)) {
			leftFootPos = Vector3.Lerp (lpos, leftHit.point + Vector3.up * offsetYfoot, Time.deltaTime * 20f);
			leftFootRot = Quaternion.FromToRotation(transform.up,leftHit.normal) * transform.rotation;
			Debug.DrawLine (lpos + Vector3.up * 0.5f, leftFootPos, Color.red, 1f);
			//Debug.Log (leftHit.collider);
		}
		RaycastHit rightHit;
		Vector3 rpos = rightFoot.position;
		if (Physics.Raycast (rpos + Vector3.up * 0.5f, Vector3.down, out rightHit, 1)) {
			rightFootPos = Vector3.Lerp (rpos, rightHit.point + Vector3.up * offsetYfoot, Time.deltaTime * 20f);
			rightFootRot = Quaternion.FromToRotation(transform.up,rightHit.normal) * transform.rotation;
			Debug.DrawLine (rpos + Vector3.up * 0.5f, rightFootPos, Color.red, 1f);
			//Debug.Log (rightHit.collider);
		}
	}

	void OnAnimatorIK(){
		anim.SetLookAtWeight (lookIKweight, bodyWeight, headWeight, eyesWeight, clampWeight);
		anim.SetLookAtPosition (targetPos.position);

		leftFootWeight = anim.GetFloat ("LeftFoot");
		anim.SetIKPositionWeight (AvatarIKGoal.LeftFoot, leftFootWeight);
		anim.SetIKPosition(AvatarIKGoal.LeftFoot, leftFootPos);

		anim.SetIKRotationWeight (AvatarIKGoal.LeftFoot, leftFootWeight);
		anim.SetIKRotation(AvatarIKGoal.LeftFoot, leftFootRot);

		rightFootWeight = anim.GetFloat ("RightFoot");
		anim.SetIKPositionWeight (AvatarIKGoal.RightFoot, rightFootWeight);
		anim.SetIKPosition(AvatarIKGoal.RightFoot, rightFootPos);

		anim.SetIKRotationWeight (AvatarIKGoal.RightFoot, rightFootWeight);
		anim.SetIKRotation(AvatarIKGoal.RightFoot, rightFootRot);
	
	}

}
