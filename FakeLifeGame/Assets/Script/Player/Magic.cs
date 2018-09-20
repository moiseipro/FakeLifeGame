using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic : MonoBehaviour {

	Animator anim;
	public GameObject[] Rune;
	public GameObject[] Spell;
	public GameObject RightHand;
	public int SpellID;
	public float CastTime = 2.1f;
	public bool MoveCast;

	public bool CastActive;

	// Use this for initialization
	void Start () {

		anim = GetComponent<Animator> ();

	}
	
	// Update is called once per frame
	void Update () {

		if ((Input.GetKeyDown (KeyCode.Mouse0))&& CastActive){
			if (anim.GetInteger ("Cast") == 0) {
				if(SpellID == 2) MoveCast = true;
				else MoveCast = false;
				StartCoroutine (Cast (CastTime, SpellID));
			}
		}

	}

	void FixedUpdate(){
		if (MoveCast) {
			anim.SetLayerWeight (2, Mathf.Lerp (anim.GetLayerWeight (2), 1f, Time.deltaTime * 3f));
		} else {
			anim.SetLayerWeight (2, Mathf.Lerp (anim.GetLayerWeight (2), 0f, Time.deltaTime * 3f));
		}
	}

	private IEnumerator Cast(float CastTime, int ID){
		anim.SetInteger ("Cast", ID);
		yield return new WaitForSeconds(CastTime);
		if (ID == 1)
			Instantiate (Rune [0], transform.position, Rune [0].transform.rotation);
		else if (ID == 2) {
			GameObject FireBall = Instantiate (Spell [0], RightHand.transform.position, GameObject.Find("Main Camera").transform.rotation);
			FireBall.GetComponent<Rigidbody> ().AddForce ((FireBall.transform.forward + Vector3.up*0.2f) * 10f, ForceMode.Impulse);
		}
		MoveCast = false;
		anim.SetInteger ("Cast", 0);
	}



}
