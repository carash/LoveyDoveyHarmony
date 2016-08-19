using UnityEngine;
using System.Collections;

public class SocketVampireScript : MonoBehaviour, Socket {
	
	void Socket.Activate (ref int type, ref int heal, ref int damage, ref float lifeTime) {
		Debug.Log ("Vamp");
		type = 0;		// check type anim
		if (Random.Range (200, 0) == 0) {
			Debug.Log (" Heal");
			heal = 1;
		}
		damage = (int)(1.1f * damage);
	}

	void Socket.Spawn (int damage, float lifeTime, Transform parent) {
		// do nothing
	}
}
