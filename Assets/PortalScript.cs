using UnityEngine;
using System.Collections;

public class PortalScript : MonoBehaviour {

	public float teleportDelay = 1f;
	float teleportTime = Mathf.Infinity;

	public string targetName;
	public int targetNumber;

	public Animator anim;

	void Start () {
//		anim = GetComponent<Animator> ();
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.CompareTag ("Player")) {
			Debug.Log ("on");
			Invoke ("Teleport", teleportDelay);
//			anim.SetTrigger ("on");
		}
	}

	void OnTriggerExit2D (Collider2D other) {
		if (other.CompareTag ("Player")) {
			Debug.Log ("off");
			CancelInvoke ("Teleport");
//			anim.SetTrigger ("off");
		}
	}

	void Teleport () {
		Debug.Log ("teleport");
		teleportTime = Mathf.Infinity;
		GetComponentInParent<GameManagerScript> ().Teleport (targetName);
		//		anim.SetTrigger ("teleport");
	}
}
