using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootersController : MonoBehaviour {

	public const int LEFT = 0;
	public const int RIGHT = 1;

	public bool fireEnabled = true;
	public float fireInterval = 2;
	public float bulletInterval = 0.2f;
	public int bulletsPerShot = 5;
	public bool bulletOffsetSync = true;
	public bool bulletOffsetMirror = true;
	public Vector2 bulletOffset = new Vector2 (0, -1);
	public Vector2 bulletOffsetLeft = new Vector2 (0, -1);
	public Vector2 bulletOffsetRight = new Vector2 (0, -1);
	public bool bulletAccelerationSync = true;
	public bool bulletAccelerationMirror = true;
	public Vector2 bulletAcceleration = new Vector2 (0, -5);
	public Vector2 bulletAccelerationLeft = new Vector2 (0, -5);
	public Vector2 bulletAccelerationRight = new Vector2 (0, -5);
	public bool bulletRotationSync = true;
	public bool bulletRotationMirror = true;
	public Vector2 bulletRotation = new Vector2 (0, 0);
	public Vector2 bulletRotationLeft = new Vector2 (0, 0);
	public Vector2 bulletRotationRight = new Vector2 (0, 0);

	private int bulletCount = 1;

	public GameObject shooterLeft = null;
	public GameObject shooterRight = null;
	public GameObject bulletPrototype = null;

	void Start () {
		
		Invoke ("FireTimer", fireInterval);
	}

	void FixedUpdate () {
		
	}

	void FireTimer () {

		if (fireEnabled) {
			if (bulletsPerShot > 0) {
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

		if (bulletCount == bulletsPerShot) {
			bulletCount = 1;
			Invoke ("FireTimer", fireInterval); 
		} else {
			bulletCount += 1;
			Invoke ("BulletTimer", bulletInterval);
		}
	}

	void Fire () {

		Vector2 offsetLeft;
		Vector2 offsetRight;
		Vector2 accelerationLeft;
		Vector2 accelerationRight;
		Vector2 rotationLeft;
		Vector2 rotationRight;

		if (bulletOffsetSync) {
			offsetLeft = bulletOffset;
			offsetRight = bulletOffset;
			if (bulletOffsetMirror) {
				offsetRight.x *= -1;
			}
		} else {
			offsetLeft = bulletOffsetLeft;
			offsetRight = bulletOffsetRight;
		}

		if (bulletAccelerationSync) {
			accelerationLeft = bulletAcceleration;
			accelerationRight = bulletAcceleration;
			if (bulletAccelerationMirror) {
				accelerationRight.x *= -1;
			}
		} else {
			accelerationLeft = bulletAccelerationLeft;
			accelerationRight = bulletAccelerationRight;
		}

		if (bulletRotationSync) {
			rotationLeft = bulletRotation;
			rotationRight = bulletRotation;
			if (bulletRotationMirror) {
				rotationRight.x *= -1;
			}
		} else {
			rotationLeft = bulletRotationLeft;
			rotationRight = bulletRotationRight;
		}

		GameObject bulletLeft = Instantiate (bulletPrototype);
		GameObject bulletRight = Instantiate (bulletPrototype);

		bulletLeft.transform.position = offsetLeft;
		bulletLeft.transform.eulerAngles = rotationLeft;
		bulletLeft.GetComponent <Rigidbody2D>().velocity = accelerationLeft;
		bulletRight.transform.position = offsetRight;
		bulletRight.transform.eulerAngles = rotationRight;
		bulletRight.GetComponent <Rigidbody2D>().velocity = accelerationRight;
	}
}
