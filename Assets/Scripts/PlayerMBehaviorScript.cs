using UnityEngine;
using System.Collections;

public class PlayerMBehaviorScript : PlayerBehaviorScript {

	// public data
	public GameObject shld;		// male

	// private
	GameObject shield;			// male
	[SerializeField]
	int shieldDuration = 0;		// male
	int shieldDurationMax = 0;	// male
	[SerializeField]
	float shieldCooldown = 0f;	// male
	float shieldNext = 0f;		// male
	bool shieldOn = false;		// male

	new void Start () {
		base.Start ();

		Fix ();
	}

	// special male fix
	new void Fix () {
		base.Fix ();

		shieldDurationMax = 250 + 100 * str;	// male
		shieldCooldown = 1.5f / atkSpeed;		// male
	}
	
	// process special input condition
	new void Update () {
		base.Update ();

		if (shieldOn) {
			shieldDuration -= 5;
			GetComponentInParent<GameManagerScript> ().energyFix (shieldDuration, shieldDurationMax);
		}
		else if (shieldDuration < shieldDurationMax) {
			shieldDuration += str + 1;
			GetComponentInParent<GameManagerScript> ().energyFix (shieldDuration, shieldDurationMax);
		}

		if (shieldOn && (Input.GetKeyUp ("space")) || shieldDuration <= 0) {
			StopShield ();
		}

		if (shieldOn) {
			x = 0;
			y = 0;
		}
	}

	// male light attack
	protected override void Light () {
		base.Light ();

		Vector2 dir = mouseLocation - transform.position;
		Vector2 attackLocation = (Vector2)transform.position + dir.normalized * attackRange;
		float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		GameObject lightAttack = Instantiate(lightAtk, attackLocation, Quaternion.Euler(0, 0, angle)) as GameObject;
		lightAttack.transform.parent = transform;
		lightAttack.GetComponent<LightMBehaviorScript> ().Setting (comboLength, damage, 0.5f, socketScripts);
	}

	// male heavy attack
	protected override void Heavy () {
		base.Heavy ();

		Vector2 attackLocation = transform.position + (mouseLocation - transform.position).normalized * attackRange;
		GameObject heavyAttack = Instantiate(heavyAtk, attackLocation, Quaternion.identity) as GameObject;
		heavyAttack.transform.parent = transform;
		heavyAttack.GetComponent<HeavyMBehaviorScript> ().Setting (comboLength, damage, 0.5f);
	}

	// action skill (shield)
	protected override void Act () {
		if (Time.time < shieldNext) {
			return;
		}
		shieldOn = true;
		immortal = true;
		shield = Instantiate(shld, transform.position, Quaternion.identity) as GameObject;
	}

	void StopShield () {
		shieldOn = false;
		immortal = false;
		shieldNext = Time.time + shieldCooldown;
		shield.GetComponent<ShieldBehaviorScript> ().Stop ();
	}
}
