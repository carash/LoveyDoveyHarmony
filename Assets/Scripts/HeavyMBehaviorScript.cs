using UnityEngine;
using System.Collections;

public class HeavyMBehaviorScript : MonoBehaviour {

	public int damage = 0;

	// choose the combo number
	public void Setting (int combo, int dmg, float lifeTime) {
//		GetComponent<Animator> ().SetTrigger (combo);
		damage = (int)(1.5f * dmg * (1 + 0.1f * combo));		// heavy swing
		Invoke ("KillMe", lifeTime);
		// TODO set Collider2D size
	}

	void KillMe () {
		Destroy (gameObject);
	}
}
