using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastMenu : MonoBehaviour {

	GameObject SpellPanel;
	GameObject SpellChanger;
	public GameObject Player;
	public GameObject Camera;

	public int TopLeftSpellID;
	public int TopRightSpellID;
	public int BotLeftSpellID;
	public int BotRightSpellID;

	bool SpellChangerActive = false;

	// Use this for initialization
	void Start () {
		SpellPanel = GameObject.Find ("SpellPanel");
		SpellChanger = GameObject.Find ("SpellChanger");
		SpellPanel.SetActive (false);
		SpellChanger.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.Q) && SpellChangerActive == false) {
			SpellPanel.SetActive (true);
			Camera.GetComponent<CameraRotateAround> ().CameraActive = false;
			Player.GetComponent<Magic> ().CastActive = false;
			Time.timeScale = 0.2f;
		}  
		if (Input.GetKeyUp (KeyCode.Q) && SpellChangerActive == false){
			SpellPanel.SetActive (false);
			Camera.GetComponent<CameraRotateAround> ().CameraActive = true;
			Player.GetComponent<Magic> ().CastActive = true;
			Time.timeScale = 1f;
		}

		if (Input.GetKeyDown (KeyCode.B)) {
			if (SpellChangerActive == true) {
				SpellChangerActive = false;
				Camera.GetComponent<CameraRotateAround> ().CameraActive = true;
				Player.GetComponent<Magic> ().CastActive = true;
				SpellChanger.SetActive (false);
				SpellPanel.SetActive (false);
			} else {
				SpellChangerActive = true;
				Camera.GetComponent<CameraRotateAround> ().CameraActive = false;
				Player.GetComponent<Magic> ().CastActive = false;
				SpellChanger.SetActive (true);
				SpellPanel.SetActive (true);
			}
		}

		if (SpellChangerActive == true) {
			
		} else {
			
		}

	}

	public void ChangeSpell(int IdButton){
		if(IdButton == 0) Player.GetComponent<Magic> ().SpellID = TopLeftSpellID;
		else if(IdButton == 1) Player.GetComponent<Magic> ().SpellID = TopRightSpellID;
		else if(IdButton == 2) Player.GetComponent<Magic> ().SpellID = BotLeftSpellID;
		else if(IdButton == 3) Player.GetComponent<Magic> ().SpellID = BotRightSpellID;
	}
		
}
