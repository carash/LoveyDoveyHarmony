using UnityEngine;
using System.Collections;

public class GameManagerScript : MonoBehaviour {

	// public setting
	[Range(0, 2)]
	public float switchDuration = 1f;
	[Range(0, 60)]
	public float switchCooldown = 10f;
	public bool inCombat = false;

	// public data
	public GameObject playerF;
	public GameObject playerM;
	public GameObject SwitchEffect;
	public GameObject playerIcon;
	public GameObject energyBar;
	public GameObject[] hp = new GameObject[6];
	public GameObject extraHp;
	public Sprite f;
	public Sprite m;
	public Sprite hpEmpty;
	public Sprite hpFull;
	public Sprite extra0;
	public Sprite extra7;
	public Sprite extra8;
	public Sprite extra;
	public Sprite empty;

	float nextSwitch = 0f;

	void Start () {
		if (playerF.activeSelf == playerM.activeSelf) {
			SwitchM ();
		}
	}

	// Update is called once per frame
	void Update () {
		if (inCombat) {
			if (Time.time > nextSwitch && Input.GetButtonDown ("Special")) {
				Switch ();
			}
		}
		else {
			if (Input.GetButton ("Special")) {
				Run ();
			}
		}
	}

	public void energyFix (int enr, int energy) {
		energyBar.GetComponent<UnityEngine.UI.Image> ().fillAmount = (float)enr / energy;
	}

	public void hpFix (int hel, int health) {
		for (int i = 0; i < Mathf.Min (hel, 6); i++) {
			hp [i].GetComponent<UnityEngine.UI.Image> ().sprite = hpFull;
		}
		for (int i = Mathf.Min (hel, 6); i < Mathf.Min (health, 6); i++) {
			hp [i].GetComponent<UnityEngine.UI.Image> ().sprite = hpEmpty;
		}
		for (int i = health; i < 6; i++) {
			hp [i].GetComponent<UnityEngine.UI.Image> ().sprite = empty;
		}

		if (hel < 7) {
			extraHp.GetComponent<UnityEngine.UI.Image> ().sprite = extra0;
		}
		else if (hel == 7) {
			extraHp.GetComponent<UnityEngine.UI.Image> ().sprite = extra7;
		}
		else if (hel == 8) {
			extraHp.GetComponent<UnityEngine.UI.Image> ().sprite = extra8;
		}
		else {
			extraHp.GetComponent<UnityEngine.UI.Image> ().sprite = extra;
		}
	}

	void Switch () {
		if (playerF.gameObject.activeSelf) {
			Instantiate (SwitchEffect, playerF.transform.position, Quaternion.identity);
			playerF.SetActive (false);
			Invoke ("SwitchM", switchDuration);
		} else {
			Instantiate (SwitchEffect, playerM.transform.position, Quaternion.identity);
			playerM.SetActive (false);
			Invoke ("SwitchF", switchDuration);
		}
	}

	void SwitchF () {
		nextSwitch = Time.time + switchCooldown;
		playerF.SetActive (true);
		playerM.SetActive (false);
		playerF.transform.position = playerM.transform.position;
		Camera.main.GetComponent<CameraBehaviorScript> ().player = playerF;
		playerIcon.GetComponent<UnityEngine.UI.Image> ().sprite = f;
	}

	void SwitchM () {
		nextSwitch = Time.time + switchCooldown;
		playerM.SetActive (true);
		playerF.SetActive (false);
		playerM.transform.position = playerF.transform.position;
		Camera.main.GetComponent<CameraBehaviorScript> ().player = playerM;
		playerIcon.GetComponent<UnityEngine.UI.Image> ().sprite = m;
	}

	void Run () {

	}

	public void Teleport (string target) {

	}
}
