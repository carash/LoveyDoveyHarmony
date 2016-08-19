using UnityEngine;
using System.Collections;

public class SocketFireScript : MonoBehaviour, Socket {

	public GameObject fCol;

	void Socket.Activate (ref int type, ref int heal, ref int damage, ref float lifeTime) {
		Debug.Log ("Fire");
		type = 2;
		damage = (int)(1.2f * damage);
	}

	void Socket.Spawn (int damage, float lifeTime, Transform parent) {
		Debug.Log ("Column");
		GameObject fireColumn =  Instantiate (fCol, parent.position, Quaternion.identity) as GameObject;
		fireColumn.GetComponent<FireColumnScript> ().Setting (damage / 5, 3f * lifeTime);
	}
}
