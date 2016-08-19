using UnityEngine;
using System.Collections;

public class PoisonCloudScript : MonoBehaviour {

	public void Setting (int dmg, float lifeTime) {
		GetComponent<PDPSScript> ().damage = dmg;
		GetComponent<PDPSScript> ().lag = lifeTime / 5f;
		GetComponent<PDPSScript> ().dur = lifeTime * 10f;
		Invoke ("KillMe", lifeTime);
	}

	void KillMe () {
		Destroy (gameObject);
	}
}
