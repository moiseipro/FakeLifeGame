using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MagicOptions : MonoBehaviour,IPointerEnterHandler, IPointerExitHandler {

	public int ID;
	public string Name;
	public string Description;
	public float CastTime;
	public bool IsMove;
	private GameObject Info;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		Info = Instantiate (Resources.Load("SpellInfo") as GameObject, GameObject.Find("Canvas").transform);
		Info.transform.position = transform.parent.transform.position+ new Vector3(110f,80f,0f);
		Text[] t = Info.GetComponentsInChildren<Text> ();
		t [0].text = Name;
		t [1].text = Description;
		Debug.Log ("Over");
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		GameObject.Destroy (Info);
	}
}
