using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Hana2 : MonoBehaviour
{
	public Spline[] splines;
	public int sections = 10;
	bool copy;
	Vector3[,] vec3ss;
	Mesh mesh;

	void Start ()
	{
		if (copy)
			return;
		List<Vector3[]> vec3List = new List<Vector3[]> ();
		for (int i = 0; i < splines.Length; i++) {
			Vector3[] v3s = new Vector3[sections + 1];
			Spline s = splines [i];
			for (int j = 0; j < sections+1; j++) {
				v3s [j] = s.GetPoint ((float)j / (float)sections);
			}
			vec3List.Add (v3s);
		}
		vec3ss = new Vector3[sections + 1, sections + 1];

		List<Vector3[]> vec3List2 = new List<Vector3[]> ();

		for (int y = 0; y < sections+1; y++) {
			Vector3[] v3s = new Vector3[splines.Length];
			for (int x = 0; x < splines.Length; x++) {
				v3s [x] = vec3List [x] [y];
			}
			vec3List2.Add (v3s);
		}

		for (int y = 0; y < sections+1; y++) {
			for (int x = 0; x < sections+1; x++) {
				vec3ss [x, y] = Spline.GetPointOfPath (vec3List2 [y], (float)x / (float)sections);
			}
		}
		vec3List.Clear ();
		vec3List2.Clear ();

		if (mesh == null)
			mesh = new Mesh ();
		Vector3[] vertices = new Vector3[vec3ss.Length];
		int[] indeces = new int[vec3ss.Length];
		for (int y = 0; y < sections+1; y++) {
			for (int x = 0; x < sections+1; x++) {
				vertices [x + y * (sections + 1)] = vec3ss [x, y];
				indeces [x + y * (sections + 1)] = x + y * (sections + 1);
			}
		}
		mesh.vertices = vertices;
		mesh.SetIndices (indeces, MeshTopology.Points, 0);
		GetComponent<MeshFilter> ().mesh = mesh;

		for (int i = 0; i < 4; i++) {
			GameObject go = (GameObject)Instantiate (gameObject);
			go.transform.Rotate (Vector3.up * (i + 1) * 72f);
			go.GetComponent<Hana2> ().copy = true;
		}
	}

	void Update ()
	{
		transform.Rotate (Vector3.up * 20f * Time.deltaTime);
	}

	void OnDrawGizmos ()
	{
		foreach (var s in splines) {
			Spline.DrawPathHelper (s.points, Color.white);
		}
	}
}
