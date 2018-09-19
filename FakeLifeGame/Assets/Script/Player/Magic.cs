using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic : MonoBehaviour {

	Animator anim;
	public GameObject[] Rune;
	public GameObject[] Spell;
	public int SpellID;
	public bool MoveCast;

	// Use this for initialization
	void Start () {

		anim = GetComponent<Animator> ();

	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.Mouse1)) {
			SpellID += 1;
			if (SpellID > 2)
				SpellID = 1;
		}

		if (Input.GetKeyDown (KeyCode.Mouse0)){
			if (anim.GetInteger ("Cast") == 0) {
				if(SpellID == 2) MoveCast = true;
				else MoveCast = false;
				StartCoroutine (Cast (2f));
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

	private IEnumerator Cast(float CastTime){
		anim.SetInteger ("Cast", SpellID);
		yield return new WaitForSeconds(CastTime);
		if (SpellID == 1)
			Instantiate (Rune [0], transform.position, Rune [0].transform.rotation);
		else if (SpellID == 2) {
			GameObject FireBall = Instantiate (Spell [0], transform.position + GameObject.Find("Main Camera").transform.forward + Vector3.up, GameObject.Find("Main Camera").transform.rotation);
			FireBall.GetComponent<Rigidbody> ().AddForce ((FireBall.transform.forward + Vector3.up*0.2f) * 10f, ForceMode.Impulse);
		}
		MoveCast = false;
		anim.SetInteger ("Cast", 0);
	}

}
