using UnityEngine;
using System.Collections;

public class EnemyBehaviorScript : MonoBehaviour {

	int hp = 0;
	float nextDPS = 0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Damage (int dmg) {
		hp -= dmg;
	}

	void OnTriggerStay2D (Collider2D other) {
		Debug.Log ("HIT");
		if (other.CompareTag ("AllyDPS")) {
			if (Time.time > nextDPS) {
				Damage (other.GetComponent<DPSScript> ().damage);
				nextDPS = Time.time + other.GetComponent<DPSScript> ().lag;
			}
		} else if (other.CompareTag ("AllyPoison")) {
		}
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.CompareTag ("AllyDamage")) {
			Damage (other.GetComponent<DamageScript> ().damage);
		}else if (other.CompareTag ("AllyDPS")) {
			Damage (other.GetComponent<DPSScript> ().damage);
			nextDPS = Time.time + other.GetComponent<DPSScript> ().lag;
		}
	}
}
