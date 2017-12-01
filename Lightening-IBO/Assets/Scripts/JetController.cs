using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetController : MonoBehaviour {

	public float healthMax = 100;

	private float health;

	void Start () {
		health = healthMax;
	}

	void FixedUpdate () {
		
	}

	public void OnDamage (float damage) {
		health -= damage;
		if (health <= 0) {
			health = 0;
			Die ();
		}
	}

	public void OnRecovery (float recovery) {
		health += recovery;
		if (health > healthMax) {
			health = healthMax;
		}
	}

	public bool isPlayer () {
		return gameObject.tag.Equals ("Player");
	}

	void Die () {
		Destroy (gameObject);
	}
}
