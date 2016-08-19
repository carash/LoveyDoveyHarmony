using UnityEngine;
using System.Collections;

public class SocketWaveScript : MonoBehaviour, Socket {

	public GameObject wav;

	void Socket.Activate (ref int type, ref int heal, ref int damage, ref float lifeTime) {
		type = 0;		// check type anim
	}

	void Socket.Spawn (int damage, float lifeTime, Transform parent) {
		Debug.Log ("Wave");
		GameObject wave = Instantiate (wav, parent.position, Quaternion.identity) as GameObject;
		wave.GetComponent<WaveAttackScript> ().Setting (damage / 5, lifeTime);
		wave.GetComponent<Rigidbody2D> ().velocity = new Vector2 (10, 0);
		wave.transform.rotation = parent.rotation;
	}
}
