using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic : MonoBehaviour {

	Animator anim;
	public GameObject Rune;

	// Use this for initialization
	void Start () {

		anim = GetComponent<Animator> ();

	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.Mouse0)) {
			StartCoroutine (Cast (2f));
		}

	}

	private IEnumerator Cast(float CastTime){
		anim.SetInteger ("Cast", 1);
		yield return new WaitForSeconds(CastTime);
		Instantiate (Rune,transform.position,Rune.transform.rotation);
		anim.SetInteger ("Cast", 0);
	}

}
