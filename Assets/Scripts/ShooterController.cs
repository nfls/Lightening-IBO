using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterController : MonoBehaviour {

	public bool fireEnabled = true;
	public float fireInterval = 2;
	public float bulletInterval = 0.2f;
	public int bulletsPerLine = 5;
	public float lineNumber = 1; 
	public float lineInterval = 0;
	public Vector2 bulletOffset = new Vector2 (0, 0);
	public float bulletAcceleration = 5;
	public float bulletAngularAcceleration = 0;

	public GameObject shooter = null;
	public GameObject bulletPrototype = null;

	private int bulletCount = 1;

	void Start () {
		Invoke ("FireTimer", fireInterval);
	}

	void FireTimer () {
	
		if (fireEnabled) {
			if (bulletsPerLine > 0) {
				BulletTimer ();
				return;
			}
		}

		Invoke ("FireTimer", fireInterval);
	}

	void BulletTimer () {
	
		if (!bulletPrototype.Equals (null)) {
			Fire (); 
		}

		if (bulletCount == bulletsPerLine) {
			bulletCount = 1;
			Invoke ("FireTimer", fireInterval); 
		} else {
			bulletCount += 1;
			Invoke ("BulletTimer", bulletInterval);
		}
	}

	void Fire () {

		float startRotation = (float) -0.5 * lineInterval * (lineNumber - 1);

		Vector2 shooterPosition;

		if (shooter.Equals (null)) {
			shooterPosition = transform.position;
		} else {
			shooterPosition = shooter.transform.position;
		}

		for (int i = 0; i < lineNumber; i ++) {

			float rotation = startRotation + i * lineInterval;

			GameObject bullet = Instantiate (bulletPrototype);

			bullet.transform.position = shooterPosition + bulletOffset;
			bullet.GetComponent <Rigidbody2D> ().angularVelocity = bulletAngularAcceleration;
			bullet.transform.localEulerAngles = new Vector3 (0, 0, rotation);
			bullet.GetComponent <BulletController> ().lineSpeed = bulletAcceleration;
		}
	}
}
