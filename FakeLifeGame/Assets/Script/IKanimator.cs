using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKanimator : MonoBehaviour {

	Animator IKanim;
	Vector3 leftFootPos;
	Vector3 rightFootPos;
	Quaternion leftFootRot;
	Quaternion rightFootRot;
	float leftFootWeight;
	float rightFootWeight;

	Transform leftFoot;
	Transform rightFoot;

	Vector3 leftHandPos;
	Vector3 rightHandPos;
	Quaternion leftHandRot;
	Quaternion rightHandRot;
	float leftHandWeight;
	float rightHandWeight;

	Transform leftHand;
	Transform rightHand;
	Transform rightArm;
	Transform leftArm;

	public float offsetYfoot;
	public float offsetYhand;

	[Range(0f, 1f)][SerializeField]public float lookIKweight;
	[Range(0f, 1f)][SerializeField]public float eyesWeight;
	[Range(0f, 1f)][SerializeField]public float headWeight;
	[Range(0f, 1f)][SerializeField]public float bodyWeight;
	[Range(0f, 1f)][SerializeField]public float clampWeight;
	public Transform targetPos;

	// Use this for initialization
	void Start () {
		IKanim = gameObject.GetComponent<Animator> ();
		leftFoot = IKanim.GetBoneTransform (HumanBodyBones.LeftFoot);
		leftFootRot = leftFoot.rotation;
		rightFoot = IKanim.GetBoneTransform (HumanBodyBones.RightFoot);
		rightFootRot = rightFoot.rotation;

		rightArm = IKanim.GetBoneTransform (HumanBodyBones.RightUpperArm);
		leftArm = IKanim.GetBoneTransform (HumanBodyBones.LeftUpperArm);

		leftHand = IKanim.GetBoneTransform (HumanBodyBones.LeftHand);
		leftHandRot = leftHand.rotation;
		rightHand = IKanim.GetBoneTransform (HumanBodyBones.RightHand);
		rightHandRot = rightHand.rotation;
	}
	
	// Update is called once per frame
	void Update () {
		FootIK ();
		//HandIK ();
	}

	void HandIK(){
		RaycastHit leftHit;
		Vector3 lpos = leftHand.position;
		Vector3 Arm1 = leftArm.position;
		if (Physics.Raycast (Arm1+Vector3.down *0.1f, transform.forward, out leftHit, 1)) {
			leftHandPos = Vector3.Lerp (lpos, leftHit.point + Vector3.forward * offsetYhand, Time.deltaTime * 20f);
			leftHandRot = Quaternion.FromToRotation(transform.up,leftHit.normal) * transform.rotation;
			Debug.DrawLine (lpos + transform.forward, leftHandPos, Color.green, 2f);
			leftHandWeight = 1;
			//Debug.Log (leftHit.collider);
		} else leftHandWeight = 0;
		RaycastHit rightHit;
		Vector3 rpos = rightHand.position;
		Vector3 Arm2 = rightArm.position;
		if (Physics.Raycast (Arm2+Vector3.down *0.1f, transform.forward, out rightHit, 1)) {
			rightHandPos = Vector3.Lerp (rpos, rightHit.point + Vector3.forward * offsetYhand, Time.deltaTime * 20f);
			rightHandRot = Quaternion.FromToRotation(transform.up,rightHit.normal) * transform.rotation;
			Debug.DrawLine (rpos + transform.forward, rightHandPos, Color.green, 2f);
			rightHandWeight = 1;
			//Debug.Log (leftHit.collider);
		} else rightHandWeight = 0;
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
		IKanim.SetLookAtWeight (lookIKweight, bodyWeight, headWeight, eyesWeight, clampWeight);
		IKanim.SetLookAtPosition (targetPos.position);

		leftFootWeight = IKanim.GetFloat ("LeftFoot");
		IKanim.SetIKPositionWeight (AvatarIKGoal.LeftFoot, leftFootWeight);
		IKanim.SetIKPosition(AvatarIKGoal.LeftFoot, leftFootPos);

		IKanim.SetIKRotationWeight (AvatarIKGoal.LeftFoot, leftFootWeight);
		IKanim.SetIKRotation(AvatarIKGoal.LeftFoot, leftFootRot);

		rightFootWeight = IKanim.GetFloat ("RightFoot");
		IKanim.SetIKPositionWeight (AvatarIKGoal.RightFoot, rightFootWeight);
		IKanim.SetIKPosition(AvatarIKGoal.RightFoot, rightFootPos);

		IKanim.SetIKRotationWeight (AvatarIKGoal.RightFoot, rightFootWeight);
		IKanim.SetIKRotation(AvatarIKGoal.RightFoot, rightFootRot);


		//leftHandWeight = IKanim.GetFloat ("LeftFoot");
		IKanim.SetIKPositionWeight (AvatarIKGoal.LeftHand, leftHandWeight);
		IKanim.SetIKPosition(AvatarIKGoal.LeftHand, leftHandPos);

		IKanim.SetIKRotationWeight (AvatarIKGoal.LeftHand, leftHandWeight);
		IKanim.SetIKRotation(AvatarIKGoal.LeftHand, leftHandRot);


		IKanim.SetIKPositionWeight (AvatarIKGoal.RightHand, rightHandWeight);
		IKanim.SetIKPosition(AvatarIKGoal.RightHand, rightHandPos);

		IKanim.SetIKRotationWeight (AvatarIKGoal.RightHand, rightHandWeight);
		IKanim.SetIKRotation(AvatarIKGoal.RightHand, rightHandRot);
			
	}
}
