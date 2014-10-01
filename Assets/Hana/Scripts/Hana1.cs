using UnityEngine;
using System.Collections;

public class Hana1 : MonoBehaviour
{
	Vector3 GetHanaPos ()
	{
		Vector3 pos = Random.insideUnitSphere;
		pos.x *= 2f;
		pos.y *= 10f;
		pos.z *= 2f;
		pos.y += 5f;
		Quaternion rot = Quaternion.Euler (Vector3.forward * Random.Range (0, 5) * 360f / 5f);
		return rot * pos;
		// Vector3 = Quateanion * Vector3 for rotate vec3
	}

	void Start ()
	{
		Light l = new GameObject ("light").AddComponent<Light> ();
		l.type = LightType.Directional;
		l.transform.LookAt (Random.insideUnitSphere + Vector3.forward);

		Camera.main.backgroundColor = Color.cyan;
		Camera.main.transform.position = -Vector3.forward * 25f;
	}
	
	// Update is called once per frame
	void Update ()
	{
		GameObject go = GameObject.CreatePrimitive (PrimitiveType.Cube);
		go.transform.position = GetHanaPos ();
		go.AddComponent<Rigidbody> ();
		go.rigidbody.drag = 10f;
		go.renderer.material.color = Color.Lerp (Color.yellow, Color.magenta, go.transform.position.sqrMagnitude * 0.06f);
		Destroy (go, 5f);
		Destroy (go.renderer.material, 5f);
	}
}
