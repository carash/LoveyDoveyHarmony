using UnityEngine;
using System.Collections;

public class PlayerFBehaviorScript : PlayerBehaviorScript {

	// public data
	public GameObject dodgeTrail;	// female

	// private
	int dodgeSpeed = 0;			// female
	float dodgeDuration = 0f;	// female
	[SerializeField]
	int dodgeCharge = 0;		// female
	int dodgeChargeMax = 0;		// female
	[SerializeField]
	int dodgeCooldown = 0;		// female
	int dodgeCooldownMax = 0;	// female
	bool dodgeOn = false;		// female

	new void Start () {
		base.Start ();

		Fix ();
	}

	// special female fix
	new void Fix () {
		base.Fix ();

		dodgeSpeed = 3 * movSpeed;				// female
		dodgeDuration = 0.5f / atkSpeed;		// female
		dodgeChargeMax = 1 + str;				// female
		dodgeCooldownMax = 300 / atkSpeed;		// female
		dodgeCooldown = 0;						// female
	}

	// process special input condition
	new void Update () {
		base.Update ();

		if (dodgeCharge < dodgeChargeMax) {
			if (dodgeCooldown < dodgeCooldownMax) {
				++dodgeCooldown;
			} else {
				++dodgeCharge;
				dodgeCooldown = 0;
			}
		}

		if (dodgeOn) {
			GameObject trail = Instantiate (dodgeTrail, transform.position, Quaternion.identity) as GameObject;
			trail.transform.parent = transform;
		}
	}

	// female light attack
	protected override void Light () {
		base.Light ();

		Vector2 dir = mouseLocation - transform.position;
		Vector2 attackLocation = (Vector2)transform.position + dir.normalized * attackRange;
		float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		GameObject lightAttack = Instantiate(lightAtk, attackLocation, Quaternion.Euler(0, 0, angle)) as GameObject;
		lightAttack.transform.parent = transform;
		lightAttack.GetComponent<LightFBehaviorScript> ().Setting (comboLength, damage, 0.5f, socketScripts);
	}

	// female heavy attack
	protected override void Heavy () {
		base.Heavy ();

		Vector2 attackLocation = transform.position + (mouseLocation - transform.position).normalized * attackRange;
		GameObject heavyAttack = Instantiate(heavyAtk, attackLocation, Quaternion.identity) as GameObject;
		heavyAttack.transform.parent = transform;
		heavyAttack.GetComponent<HeavyFBehaviorScript> ().Setting (comboLength, damage, heavyDelay * 0.8f);
	}

	// action skill (dodge)
	protected override void Act () {
		if (dodgeCharge == 0) {
			return;
		}
		x = Input.GetAxisRaw ("Horizontal");
		y = Input.GetAxisRaw ("Vertical");
		if (x == 0 && y == 0) {
			return;
		}
		--dodgeCharge;
		dodgeOn = true;
		immortal = true;
		nextAction = Time.time + 1.75f * dodgeDuration;
		nextCombo = Time.time + dodgeDuration;
		Swap (ref movSpeed, ref dodgeSpeed);
		Invoke ("StopDodge", dodgeDuration);
	}

	void Swap (ref int num1, ref int num2) {
		int tmp = num1;
		num1 = num2;
		num2 = tmp;
	}

	void StopDodge () {
		dodgeOn = false;
		immortal = false;
		x = 0;
		y = 0;
		Swap (ref movSpeed, ref dodgeSpeed);
	}
}
