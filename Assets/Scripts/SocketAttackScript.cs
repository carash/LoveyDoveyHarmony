using UnityEngine;
using System.Collections;

public class SocketAttackScript : MonoBehaviour, Socket {

	public GameObject atk;

	void Socket.Activate (ref int type, ref int heal, ref int damage, ref float lifeTime) {
		type = 0;		// check type anim
	}

	void Socket.Spawn (int damage, float lifeTime, Transform parent) {
		Debug.Log ("Attack");
		GameObject attack = Instantiate (atk, parent.parent.position, Quaternion.identity) as GameObject;
		attack.GetComponent<SecondAttackScript> ().Setting (damage / 2, 0.5f * lifeTime);
	}
}
