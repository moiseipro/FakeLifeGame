using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastMenu : MonoBehaviour {

	GameObject SpellPanel;
	GameObject SpellChanger;
	public GameObject Player;
	public GameObject Camera;

	public GameObject TopLeftSpell;
	public GameObject TopRightSpell;
	public GameObject BotLeftSpell;
	public GameObject BotRightSpell;

	public bool SpellChangerActive = false;
	public bool SpellChoseActive = false;
	public bool DragItem = false;

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
			SpellChoseActive = true;
			Camera.GetComponent<CameraRotateAround> ().CameraActive = false;
			Player.GetComponent<Magic> ().CastActive = false;
			Time.timeScale = 0.2f;
		}  
		if (Input.GetKeyUp (KeyCode.Q) && SpellChangerActive == false){
			SpellPanel.SetActive (false);
			SpellChoseActive = false;
			Camera.GetComponent<CameraRotateAround> ().CameraActive = true;
			Player.GetComponent<Magic> ().CastActive = true;
			Time.timeScale = 1f;
			var InfoPanel = GameObject.FindWithTag("InfoPanel");
			if (InfoPanel != null) {
				GameObject.Destroy (InfoPanel);
			}
		}

		if (Input.GetKeyDown (KeyCode.B) && SpellChoseActive == false) {
			if (SpellChangerActive == true && !DragItem) {
				SpellChangerActive = false;
				Camera.GetComponent<CameraRotateAround> ().CameraActive = true;
				Player.GetComponent<Magic> ().CastActive = true;
				SpellChanger.SetActive (false);
				SpellPanel.SetActive (false);
				var InfoPanel = GameObject.FindWithTag("InfoPanel");
				if (InfoPanel != null) {
					GameObject.Destroy (InfoPanel);
				}
			} else {
				SpellChangerActive = true;
				Camera.GetComponent<CameraRotateAround> ().CameraActive = false;
				Player.GetComponent<Magic> ().CastActive = false;
				SpellChanger.SetActive (true);
				SpellPanel.SetActive (true);
			}
		}



	}

	public void ChangeSpell(int IdButton){
		if (IdButton == 0 && TopLeftSpell != null) {
			Player.GetComponent<Magic> ().SpellID = TopLeftSpell.GetComponent<MagicOptions> ().ID;
			Player.GetComponent<Magic> ().CastTime = TopLeftSpell.GetComponent<MagicOptions> ().CastTime;
			Player.GetComponent<Magic> ().MoveCast = TopLeftSpell.GetComponent<MagicOptions> ().IsMove;
		} else if (IdButton == 1 && TopRightSpell != null) {
			Player.GetComponent<Magic> ().SpellID = TopRightSpell.GetComponent<MagicOptions> ().ID;
			Player.GetComponent<Magic> ().CastTime = TopRightSpell.GetComponent<MagicOptions> ().CastTime;
			Player.GetComponent<Magic> ().MoveCast = TopRightSpell.GetComponent<MagicOptions> ().IsMove;
		} else if (IdButton == 2 && BotLeftSpell != null) {
			Player.GetComponent<Magic> ().SpellID = BotLeftSpell.GetComponent<MagicOptions> ().ID;
			Player.GetComponent<Magic> ().CastTime = BotLeftSpell.GetComponent<MagicOptions> ().CastTime;
			Player.GetComponent<Magic> ().MoveCast = BotLeftSpell.GetComponent<MagicOptions> ().IsMove;
		} else if (IdButton == 3 && BotRightSpell != null) {
			Player.GetComponent<Magic> ().SpellID = BotRightSpell.GetComponent<MagicOptions> ().ID;
			Player.GetComponent<Magic> ().CastTime = BotRightSpell.GetComponent<MagicOptions> ().CastTime;
			Player.GetComponent<Magic> ().MoveCast = BotRightSpell.GetComponent<MagicOptions> ().IsMove;
		} else {
			Player.GetComponent<Magic> ().SpellID = 0;
			Player.GetComponent<Magic> ().CastTime = 0;
			Player.GetComponent<Magic> ().MoveCast = false;
		}
	}
		
}
