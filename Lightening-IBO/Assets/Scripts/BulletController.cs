using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

	public float damage = 20;
	public float lifespan = 5;
	public string orientedTag = null;
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

	void OnTriggerEnter (Collider2D collider2d) {
	
		GameObject collisionObject = collider2d.gameObject;
		string objectTag = gameObject.tag;

		if (damagePlayer) {
			if (objectTag.Equals ("Player")) {
				OnHitJet (collisionObject);
			}
		}

		if (damageEnemy) {
			if (objectTag.Equals ("Enemy")) {
				OnHitJet (collisionObject);
			}
		}

		if (damageBoss) {
			if (objectTag.Equals ("Boss")) {
				OnHitJet (collisionObject);
			}
		}
	}

	void OnTriggerStay (Collider2D collider2d) {
	
	}

	void OnTriggerExit (Collider2D collider2d) {

		GameObject collisionObject = collider2d.gameObject;

		if (gameObject.tag.Equals ("WorldEdge")) {
			OnHitWorldEdge ();
		}
	}

	void OnHitJet (GameObject jet) {
	
	}

	void OnHitWorldEdge () {
		
		Finish ();
	}
}