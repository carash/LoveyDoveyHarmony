using UnityEngine;
using System.Collections;

public class SocketPoisonScript : MonoBehaviour, Socket {

	public GameObject psn;

	void Socket.Activate (ref int type, ref int heal, ref int damage, ref float lifeTime) {
		type = 0;		// check type anim
	}

	void Socket.Spawn (int damage, float lifeTime, Transform parent) {
		Debug.Log ("Cloud");
		GameObject attack = Instantiate (psn, parent.position, Quaternion.identity) as GameObject;
		attack.GetComponent<SecondAttackScript> ().Setting (damage / 10, 3f * lifeTime);
	}
}
