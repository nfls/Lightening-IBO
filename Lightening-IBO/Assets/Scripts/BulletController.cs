using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

	public float damage = 20;
	public float lifespan = 5;
	public bool damagePlayer = false;
	public bool damageEnemy = false;
	public bool damageBoss = false;

	void Start () {
		
		Invoke ("Finish", lifespan); 
	}

	void FixedUpdate () {
		
	}

	void Finish () {
		
		Destroy (gameObject);
	}

	void OnTriggerEnter2D (Collider2D collider2d) {
	
		GameObject collisionObject = collider2d.gameObject;
		string objectTag = gameObject.tag;

		if (damagePlayer) {
			if (objectTag.Equals ("Player")) {
				OnHitJet (collisionObject);
			}
		} else if (damageEnemy) {
			if (objectTag.Equals ("Enemy")) {
				OnHitJet (collisionObject);
			}
		} else if (damageBoss) {
			if (objectTag.Equals ("Boss")) {
				OnHitJet (collisionObject);
			}
		}
	}

	void OnTriggerStay2D (Collider2D collider2d) {
	
	}

	void OnTriggerExit2D (Collider2D collider2d) {

		GameObject collisionObject = collider2d.gameObject;

		if (collisionObject.tag.Contains ("Edge")) {
			OnHitWorldEdge ();
		}
	}

	void OnHitJet (GameObject jet) {
		JetController controller = jet.GetComponent <JetController> ();
		controller.OnDamage (damage);
	}

	void OnHitWorldEdge () {
		
		Finish ();
	}
}