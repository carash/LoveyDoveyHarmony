using UnityEngine;
using System.Collections;

public class FireColumnScript : MonoBehaviour {

	public void Setting (int dmg, float lifeTime) {
		GetComponent<DPSScript> ().damage = dmg;
		GetComponent<DPSScript> ().lag = lifeTime / 5f;
		Invoke ("KillMe", lifeTime);
	}

	void KillMe () {
		Destroy (gameObject);
	}
}
