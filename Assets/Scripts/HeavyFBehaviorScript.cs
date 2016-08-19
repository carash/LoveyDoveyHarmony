using UnityEngine;
using System.Collections;

public class HeavyFBehaviorScript : MonoBehaviour {

	public int damage = 0;

	// choose the combo number
	public void Setting (int combo, int dmg, float lifeTime) {
//		GetComponent<Animator> ().SetTrigger (combo);
		damage = (int)(0.3f * dmg * (1 + 0.1f * combo));		// 5 SMG bullet
		Invoke ("KillMe", lifeTime);
		// TODO set Collider2D size
	}

	void KillMe () {
		Destroy (gameObject);
	}
}
