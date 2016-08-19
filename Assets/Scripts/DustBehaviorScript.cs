using UnityEngine;
using System.Collections;

public class DustBehaviorScript : MonoBehaviour {

	[Range(0, 1)]
	public float dustLife = 0.5f;

	void Start () {
		Invoke ("KillMe", dustLife);
	}

	void KillMe () {
		Destroy (gameObject);
	}
}
