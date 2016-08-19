using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LightMBehaviorScript : MonoBehaviour {

	public int type = 1;
	public int heal = 0;
	public int damage = 0;
	public float lifeTime = 0;

	// choose the combo number
	public void Setting (int combo, int dmg, float duration, List<Socket> socketList) {
		damage = (int)(dmg * (1 + 0.1f * combo));				// light swing
		// transfer data to readable
		GetComponent<DamageScript> ().damage = damage;

		lifeTime = duration;
		foreach (Socket socket in socketList) {
			socket.Activate (ref type, ref heal, ref damage, ref lifeTime);
			socket.Spawn (damage, lifeTime, transform);
		}
		Debug.Log (GetComponentInParent<PlayerMBehaviorScript> ());
//		GetComponentInParent<PlayerMBehaviorScript> ().Heal (heal);

		GetComponent<Animator> ().SetInteger ("Type", type);
		GetComponent<Animator> ().SetInteger ("Combo", combo);
		GetComponent<Animator> ().SetTrigger ("Attack");
		Invoke ("KillMe", lifeTime);
	}

	void KillMe () {
		Destroy (gameObject);
	}
}
