using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
	
	public static GameObject itemDragged;
	Vector3 startPos;
	public static Transform StartParent;
	private bool Draggable;

	#region IBeginDragHandler implementation

	public void OnBeginDrag (PointerEventData eventData)
	{
		if (Draggable == true) {
			itemDragged = gameObject;
			startPos = transform.localPosition;
			StartParent = transform.parent;
			GetComponent<CanvasGroup> ().blocksRaycasts = false;
			GameObject.Find ("Manager").GetComponent<CastMenu> ().DragItem = true;
		}
	}

	#endregion

	#region IDragHandler implementation

	public void OnDrag (PointerEventData eventData)
	{
		if (Draggable == true) {
			transform.localPosition += (Vector3)eventData.delta/1.5f;
		}
	}

	#endregion

	#region IEndDragHandler implementation

	public void OnEndDrag (PointerEventData eventData)
	{
		if (Draggable == true) {
			itemDragged = null;
			GetComponent<CanvasGroup> ().blocksRaycasts = true;
			GameObject.Find ("Manager").GetComponent<CastMenu> ().DragItem = false;

		}
	}

	#endregion

	void Update(){
		if (transform.parent == StartParent && itemDragged == null) {
			transform.localPosition = Vector3.Lerp (transform.localPosition, startPos, Time.deltaTime * 20f);
		}
		if (Input.GetKey (KeyCode.Q) && GameObject.Find ("Manager").GetComponent<CastMenu> ().SpellChangerActive == false) {
			Draggable = false;
		} else {
			Draggable = true;
		}
	}

}
