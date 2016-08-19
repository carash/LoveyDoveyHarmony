using UnityEngine;
using System.Collections;

public class SecondAttackScript : MonoBehaviour {

	public void Setting (int dmg, float lifeTime) {
		GetComponent<DamageScript> ().damage = dmg;
		Invoke ("KillMe", lifeTime);
	}

	void KillMe () {
		Destroy (gameObject);
	}
}
