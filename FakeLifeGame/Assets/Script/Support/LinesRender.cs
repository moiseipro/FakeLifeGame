using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinesRender : MonoBehaviour {

	public Color baseColor;
	public Material material;

	public Transform origin;
	public List<Transform> points = new List<Transform>();
	public List<Color> colors = new List<Color>();


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

	void RenderLines(List<Transform> points, List<Color> colors){
		if (!CorrectValues (points, colors)) {
			return;
		}

		GL.Begin (GL.LINES);
		material.SetPass(0);

		for (int i = 0; i < points.Count; i++) {
			GL.Color (baseColor);
			GL.Vertex (origin.position);
			GL.Color (colors[i]);
			GL.Vertex3 ((origin.position.x+points[i].position.x)/2f + 0.02f,(origin.position.y+points[i].position.y)/2f + 0.02f,(origin.position.z+points[i].position.z)/2f);
			GL.Vertex3 ((origin.position.x+points[i].position.x)/2f + 0.02f,(origin.position.y+points[i].position.y)/2f + 0.02f,(origin.position.z+points[i].position.z)/2f);
			GL.Vertex (points[i].position);
		}
		GL.End();

	}

	private bool CorrectValues (List<Transform> points, List<Color> colors){
		return points != null && colors != null && points.Count == colors.Count;
	}

}
