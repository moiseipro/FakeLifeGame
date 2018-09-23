using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
	
	public static GameObject itemDragged;
	Vector3 startPos;
	private Transform StartParent;
	private bool Dragged;

	#region IBeginDragHandler implementation

	public void OnBeginDrag (PointerEventData eventData)
	{
		itemDragged = gameObject;
		startPos = transform.localPosition;
		StartParent = transform.parent;
		GetComponent<CanvasGroup> ().blocksRaycasts = false;
	}

	#endregion

	#region IDragHandler implementation

	public void OnDrag (PointerEventData eventData)
	{
		transform.localPosition += (Vector3)eventData.delta;
	}

	#endregion

	#region IEndDragHandler implementation

	public void OnEndDrag (PointerEventData eventData)
	{
		itemDragged = null;
		GetComponent<CanvasGroup> ().blocksRaycasts = true;
	}

	#endregion

	void Update(){
		if (transform.parent == StartParent && itemDragged == null) {
			transform.localPosition = Vector3.Lerp (transform.localPosition, startPos, Time.deltaTime * 20f);
		}
	}


}
