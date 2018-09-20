using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastMenu : MonoBehaviour {

	GameObject SpellPanel;
	public GameObject Player;
	public GameObject Camera;

	public int TopLeftSpellID;
	public int TopRightSpellID;
	public int BotLeftSpellID;
	public int BotRightSpellID;



	// Use this for initialization
	void Start () {
		SpellPanel = GameObject.Find ("SpellPanel");
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKey (KeyCode.Q)) {
			SpellPanel.SetActive (true);
			Camera.GetComponent<CameraRotateAround> ().CameraActive = false;
			Player.GetComponent<Magic> ().CastActive = false;
			Time.timeScale = 0.2f;
		} else {
			SpellPanel.SetActive (false);
			Camera.GetComponent<CameraRotateAround> ().CameraActive = true;
			Player.GetComponent<Magic> ().CastActive = true;
			Time.timeScale = 1f;
		}

	}

	public void ChangeSpell(int IdButton){
		if(IdButton == 0) Player.GetComponent<Magic> ().SpellID = TopLeftSpellID;
		else if(IdButton == 1) Player.GetComponent<Magic> ().SpellID = TopRightSpellID;
		else if(IdButton == 2) Player.GetComponent<Magic> ().SpellID = BotLeftSpellID;
		else if(IdButton == 3) Player.GetComponent<Magic> ().SpellID = BotRightSpellID;
	}
		
}
