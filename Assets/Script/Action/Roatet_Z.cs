using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roatet_Z : MonoBehaviour {

	[SerializeField]
	private int speed;
	// Update is called once per frame
	void FixedUpdate () {
		transform.Rotate (0,0,Time.deltaTime*speed);
	}
}
