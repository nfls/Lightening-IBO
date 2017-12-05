using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

	public float damage = 20;
	public float lifespan = 5;
	public float lineSpeed = 0;
	public bool damagePlayer = false;
	public bool damageEnemy = false;
	public bool damageBoss = false;

	private Rigidbody2D rigidbody2d;

	void Start () {
		
		rigidbody2d = GetComponent <Rigidbody2D> ();
		Invoke ("Finish", lifespan);
	}

	void FixedUpdate () {

		float angle = transform.localEulerAngles.z;
		angle += 270;
		if (angle >= 360) {
			angle = angle - 360;
		}
		Vector2 velocity = Util.DegreeToVector2 (angle);
		float scale = lineSpeed / velocity.magnitude;
		velocity.Scale (new Vector2 (scale, scale));
		rigidbody2d.velocity = velocity;
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