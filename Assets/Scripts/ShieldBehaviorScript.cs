using UnityEngine;
using System.Collections;

public class ShieldBehaviorScript : MonoBehaviour {

	Animator anim;

	void Start () {
		anim = GetComponent<Animator> ();
	}

	public void Stop () {
//		anim.SetTrigger ("stop");
		Invoke ("KillMe", 0.1f);
	}

	void KillMe () {
		Destroy (gameObject);
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.CompareTag ("EnemyAttack")) {
//			anim.SetTrigger ("hit");
			Destroy (other.gameObject);
		}
	}
}
