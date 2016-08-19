using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class PlayerBehaviorScript : MonoBehaviour {

	// public setting
	[Range(0, 11)]
	public int str = 0;
	[Range(0, 11)]
	public int spd = 0;
	[Range(0, 11)]
	public int lck = 0;
	[Range(3, 9)]
	public int health = 3;
	public int hel;
	[Range(0, 100)]
	public int fatigue = 100;
	public int fat;
	[Range(0, 100)]
	public int sanity = 100;
	public int san;
	[Range(0, 100)]
	public int damage = 10;
	[Range(3, 6)]
	public int comboMax = 3;

	// balancing
	[Range(0, 2)]
	public float attackRange = 1;
	[Range(1, 11)]
	public int maxSocket = 1;
	
	// public data
	public GameObject lightAtk;
	public GameObject heavyAtk;
	public GameObject evade;
	public List<GameObject> sockets;
	
	// private
	protected List<Socket> socketScripts;
	protected Rigidbody2D rb2d;
	protected Vector3 mouseLocation;
	protected float nextAction = 0f;
	protected float nextCombo = 0f;
	protected float nextAttack = 0f;
	protected int comboLength = 0;
	protected int movSpeed = 0;
	protected int atkSpeed = 0;
	protected float critChance = 0f;
	protected float dropChance = 0f;
	protected float lightDelay = 0f;
	protected float heavyDelay = 0f;
	[SerializeField]
	protected bool immortal = false;
	protected float x = 0;
	protected float y = 0;
	float socketDrop = 0f;
	[SerializeField]
	float socketDropDelay = 1f;

	// initialize
	protected virtual void Start () {
		rb2d = GetComponent<Rigidbody2D> ();
		sockets = new List<GameObject> ();
		socketScripts = new List<Socket> ();
		Fix ();
	}

	// stat check (TODO abstract method)
	protected virtual void Fix () {

		hel = health;
		fat = 0;
		san = sanity;
		GetComponentInParent<GameManagerScript> ().hpFix (hel, health);

		// to be balanced
		movSpeed = 5 + (3 * spd) / 2;
		atkSpeed = 2 + (spd + 1) / 2;
		critChance = 10 + 10 * lck;
		dropChance = 5 + 3 * lck;

		lightDelay = 1f / atkSpeed;
		heavyDelay = 1.5f / atkSpeed;
	}

	// process input
	protected virtual void Update () {
		
		// find mouse
		Vector3 mousePoint = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 0.0f);
		mouseLocation = Camera.main.ScreenToWorldPoint (mousePoint);

		// light attack check
		if (Input.GetMouseButtonDown (0)) {
			// light att
			if (Time.time > nextAttack) {
				comboLength = 1;
				Light ();
			}
			else if (Time.time > nextCombo && comboLength < comboMax) {
				++comboLength;
				Light ();
			}
		}

		// heavy attack check
		if (Input.GetMouseButtonDown (1)) {
			// heavy att
			if (Time.time > nextAttack) {
				comboLength = 1;
				Heavy ();
			}
			else if (Time.time > nextCombo && comboLength < comboMax) {
				++comboLength;
				Heavy ();
			}
		}

		// defensive action
		if (Input.GetKeyDown (KeyCode.Space)) {
			Act ();
		}

		// drop sockets
		if (Input.GetKeyDown (KeyCode.LeftControl)) {
			socketDrop = Time.time + socketDropDelay;
		}

		if (Input.GetKey (KeyCode.LeftControl) && Time.time > socketDrop) {
			Drop ();
		}

		// only move when no action attacking
		if (Time.time > nextAction) {
			x = Input.GetAxisRaw ("Horizontal");
			y = Input.GetAxisRaw ("Vertical");
		}

		// drop socket start
		if (Input.GetKeyDown (KeyCode.LeftControl)) {
			socketDrop = Time.time + socketDropDelay;
		}

		// drop socket drop
		if (sockets.Count > 0 && Input.GetKey (KeyCode.LeftControl) && Time.time > socketDrop) {
			// empty socket
			Debug.Log("Drop Socket");
			sockets.Clear ();
		}
	}

	// process physics
	void FixedUpdate () {
		rb2d.velocity = (new Vector2 (x, y)).normalized * movSpeed;
	}

	// light attack
	protected virtual void Light () {
		// stop moving when attacking
		x = 0;
		y = 0;

		nextCombo = Time.time + lightDelay;
		if (comboLength != comboMax)
			nextAttack = nextCombo + 0.3f;
		else
			nextAttack = nextCombo + 0.5f;
		nextAction = Time.time + 1.25f * lightDelay;
	}

	// heavy attack
	protected virtual void Heavy () {
		// stop moving when attacking
		x = 0;
		y = 0;

		nextCombo = Time.time + heavyDelay;
		if (comboLength != comboMax)
			nextAttack = nextCombo + 0.3f;
		else
			nextAttack = nextCombo + 0.5f;
		nextAction = Time.time + 1.25f * heavyDelay;
	}

	// action skill
	protected abstract void Act ();

	// die
	void Die () {
		// do something
	}

	// evade
	void Evade () {
		(Instantiate (evade, transform.position + Vector3.up, Quaternion.identity) as GameObject).transform.parent = transform;
	}

	// recieve damage
	void Damage (int dmg) {
		if (!immortal) {
			hel -= dmg;
		}
		if (hel == 0) {
			Die ();
		}
		GetComponentInParent<GameManagerScript> ().hpFix (hel, health);
	}

	// heal
	public void Heal (int heal){
		if (hel < health) {
			hel += heal;
		}
		GetComponentInParent<GameManagerScript> ().hpFix (hel, health);
	}

	void StatUp () {
		// 1 : str+1
		// 2 : spd+1
		// 3 : lck+1
		Fix ();
	}

	void Take (GameObject socket) {
		if (maxSocket <= sockets.Count) {
			socket.GetComponent<Rigidbody2D> ().velocity = (socket.transform.position - transform.position).normalized * 3f;
			return;
		}
		sockets.Add (socket);
		socketScripts.Add (socket.GetComponent<Socket> ());
		socket.GetComponent<SpriteRenderer> ().enabled = false;
//		Destroy (socket);
	}

	void Drop () {
		foreach (GameObject socket in sockets) {
			socket.transform.position = transform.position;
			socket.GetComponent<Rigidbody2D> ().velocity = (new Vector2(Random.Range(0f, 1f), Random.Range(0f, 1f))).normalized * 3f;
			socket.GetComponent<SpriteRenderer> ().enabled = true;
		}

		sockets.Clear ();
		socketScripts.Clear ();
	}

	void Teleport () {
		// do something

	}

	void OnTriggerStay2D (Collider2D other) {
		
		// take socket
		if (Input.GetKeyDown (KeyCode.E) && other.CompareTag("Socket")) {
			Debug.Log ("Take");
			Take (other.gameObject);
		}
	}

	// on trigger entertt
	void OnTriggerEnter2D (Collider2D other) {
		if (other.CompareTag ("EnemyDamage")) {
			// check LCK evasion
			if (Random.Range (0, 1) < Mathf.Pow (0.9f, lck)) {
				Damage (1);
			} else {
				Evade ();
			}
			Destroy (other.gameObject);
		}
		else if (other.CompareTag ("ComboUp")) {
			comboMax++;
		}
		else if (other.CompareTag ("StatUp")) {
			StatUp ();
		}
		else if (other.CompareTag ("Portal")) {
			Teleport ();
		}
	}
}
