using UnityEngine;
using System.Collections;

public class HitBehaviorScript : MonoBehaviour {

	public float delay = 0.5f;

	SpriteRenderer sr;

	// Use this for initialization
	void Start () {
		sr = GetComponent<SpriteRenderer> ();
		GetComponent<Rigidbody2D> ().velocity = 2 * Vector2.up;
		transform.rotation = Quaternion.Euler(0, 0, Random.Range(-30, 30));
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time < delay)
			return;

		sr.color = new Color (1, 1, 1, sr.color.a - 0.02f);

		if (sr.color.a < Mathf.Epsilon)
			Destroy (gameObject);
	}
}
