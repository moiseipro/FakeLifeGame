using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spells : MonoBehaviour {

	public Color baseColor;
	public Material material;

	public Transform origin;
	public Transform[] points;
	public Color[] colors;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnPostRender(){
		RenderLines (points, colors);
	}

	void OnRenderObject(){
		RenderLines (points, colors);
	}

	void OnDrawGizmos(){
		RenderLines (points, colors);
	}

	void RenderLines(Transform[] points, Color[] colors){
		if (!CorrectValues (points, colors)) {
			return;
		}
			
		GL.Begin (GL.LINES);
		material.SetPass(0);

		for (int i = 0; i < points.Length; i++) {
			GL.Color (baseColor);
			GL.Vertex (origin.position);
			GL.Color (colors[i]);
			GL.Vertex3 ((origin.position.x+points[i].position.x)/2f + 0.02f,(origin.position.y+points[i].position.y)/2f + 0.02f,(origin.position.z+points[i].position.z)/2f);
			GL.Vertex3 ((origin.position.x+points[i].position.x)/2f + 0.02f,(origin.position.y+points[i].position.y)/2f + 0.02f,(origin.position.z+points[i].position.z)/2f);
			GL.Vertex (points[i].position);
		}
		GL.End();

	}

	private bool CorrectValues (Transform[] points, Color[] colors){
		return points != null && colors != null && points.Length == colors.Length;
	}

}
