using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SlotToUseSpell : MonoBehaviour,IDropHandler {

	public GameObject item {
		get{ 
			if (transform.childCount > 0) {
				return transform.GetChild (0).gameObject;
			}
			return null;
		}
	}

	#region IDropHandler implementation
	public void OnDrop (PointerEventData eventData)
	{
		if (!item) {
			GameObject.FindWithTag ("GamePlayer").GetComponent<Magic> ().SpellID = 0;
			GameObject.FindWithTag ("GamePlayer").GetComponent<Magic> ().CastTime = 0;
			GameObject.FindWithTag ("GamePlayer").GetComponent<Magic> ().MoveCast = false;
			if (DragDrop.StartParent.name == "TopLeftSpell") GameObject.Find ("Manager").GetComponent<CastMenu> ().TopLeftSpell = null;
			else if(DragDrop.StartParent.name == "TopRightSpell") GameObject.Find ("Manager").GetComponent<CastMenu> ().TopRightSpell = null;
			else if(DragDrop.StartParent.name == "BotLeftSpell") GameObject.Find ("Manager").GetComponent<CastMenu> ().BotLeftSpell = null;
			else if(DragDrop.StartParent.name == "BotRightSpell") GameObject.Find ("Manager").GetComponent<CastMenu> ().BotRightSpell = null;
			DragDrop.itemDragged.transform.SetParent (transform);
			if (gameObject.name == "TopLeftSpell") {
				GameObject.Find ("Manager").GetComponent<CastMenu> ().TopLeftSpell = DragDrop.itemDragged;
			} else if (gameObject.name == "TopRightSpell") {
				GameObject.Find ("Manager").GetComponent<CastMenu> ().TopRightSpell = DragDrop.itemDragged;
			} else if (gameObject.name == "BotLeftSpell") {
				GameObject.Find ("Manager").GetComponent<CastMenu> ().BotLeftSpell = DragDrop.itemDragged;
			} else if (gameObject.name == "BotRightSpell") {
				GameObject.Find ("Manager").GetComponent<CastMenu> ().BotRightSpell = DragDrop.itemDragged;
			}
		}
	}
	#endregion

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
