using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterGroupController : MonoBehaviour {

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
	public float bulletRotation = 0;
	public float bulletRotationLeft = 0;
	public float bulletRotationRight = 0;
	public bool hasTarget = false;

	private int bulletCount = 1;

	public GameObject shooterLeft = null;
	public GameObject shooterRight = null;
	public GameObject bulletPrototype = null;
	public GameObject target = null;

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
			if (!hasTarget || target.Equals (null)) {
				Fire ();
			} else {
				FireTarget ();
			}
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
		float rotationLeft;
		float rotationRight;

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

		offsetLeft.x = shooterLeft.transform.position.x + offsetLeft.x;
		offsetLeft.y = shooterLeft.transform.position.y + offsetLeft.y;
		offsetRight.x = shooterRight.transform.position.x + offsetRight.x;
		offsetRight.y = shooterRight.transform.position.y + offsetRight.y;

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
				rotationRight = 360 - rotationRight;
			}
		} else {
			rotationLeft = bulletRotationLeft;
			rotationRight = bulletRotationRight;
		}

		GameObject bulletLeft = Instantiate (bulletPrototype);
		GameObject bulletRight = Instantiate (bulletPrototype);

		bulletLeft.transform.position = offsetLeft;
		bulletLeft.transform.localEulerAngles = new Vector3 (0, 0, rotationLeft);
		bulletLeft.GetComponent <Rigidbody2D>().velocity = accelerationLeft;
		bulletRight.transform.position = offsetRight;
		bulletRight.transform.localEulerAngles = new Vector3 (0, 0, rotationRight);
		bulletRight.GetComponent <Rigidbody2D>().velocity = accelerationRight;
		if (bulletRotationMirror) {
			bulletRight.transform.localScale = new Vector3 (-1, 1, 1);
		}
	}

	void FireTarget () {
		Vector2 offsetLeft;
		Vector2 offsetRight;
		Vector2 accelerationLeft;
		Vector2 accelerationRight;
		float rotationLeft;
		float rotationRight;

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

		offsetLeft.x = shooterLeft.transform.position.x + offsetLeft.x;
		offsetLeft.y = shooterLeft.transform.position.y + offsetLeft.y;
		offsetRight.x = shooterRight.transform.position.x + offsetRight.x;
		offsetRight.y = shooterRight.transform.position.y + offsetRight.y;

		Vector2 targetPosition = new Vector2 (target.transform.position.x, target.transform.position.y);

		if (bulletAccelerationSync) {
			if (!bulletAccelerationMirror) {
				float mx = (offsetLeft.x + offsetRight.x) / 2;
				float my = (offsetLeft.y + offsetRight.y) / 2;
				Vector2 initialPosition = new Vector2 (mx, my);
				accelerationLeft = targetPosition - initialPosition;
				accelerationLeft.Normalize ();
				float scale = bulletAcceleration.magnitude / accelerationLeft.magnitude;
				accelerationLeft.Scale (new Vector2 (scale, scale));
				accelerationRight = accelerationLeft;
			} else {
				if (target.transform.position.x < offsetRight.x) {
					accelerationLeft = targetPosition - offsetLeft;
					accelerationLeft.Normalize ();
					float scale = bulletAccelerationLeft.magnitude / accelerationLeft.magnitude;
					accelerationLeft.Scale (new Vector2 (scale, scale));
					accelerationRight = accelerationLeft;
					accelerationRight.x *= -1;
				} else {
					accelerationRight = targetPosition - offsetLeft;
					accelerationRight.Normalize ();
					float scale = bulletAccelerationRight.magnitude / accelerationRight.magnitude;
					accelerationRight.Scale (new Vector2 (scale, scale));
					accelerationLeft = accelerationRight;
					accelerationLeft.x *= -1;
				}
			}
		} else {
			accelerationLeft = targetPosition - offsetLeft;
			accelerationLeft.Normalize ();
			float scale = bulletAccelerationLeft.magnitude / accelerationLeft.magnitude;
			accelerationLeft.Scale (new Vector2 (scale, scale));
			accelerationRight = targetPosition - offsetRight;
			accelerationRight.Normalize ();
			scale = bulletAccelerationRight.magnitude / accelerationRight.magnitude;
			accelerationRight.Scale (new Vector2 (scale, scale));
		}

		if (bulletRotationSync) {
			rotationLeft = Vector2.SignedAngle (accelerationLeft, Vector2.zero);
			rotationRight = rotationLeft;
			if (bulletRotationMirror) {
				rotationRight = 360 - rotationRight;
			}
		} else {
			rotationLeft = Vector2.SignedAngle (accelerationLeft, Vector2.zero);
			rotationRight = rotationLeft;
		}

		GameObject bulletLeft = Instantiate (bulletPrototype);
		GameObject bulletRight = Instantiate (bulletPrototype);

		bulletLeft.transform.position = offsetLeft;
		bulletLeft.transform.localEulerAngles = new Vector3 (0, 0, rotationLeft);
		bulletLeft.GetComponent <Rigidbody2D>().velocity = accelerationLeft;
		bulletRight.transform.position = offsetRight;
		bulletRight.transform.localEulerAngles = new Vector3 (0, 0, rotationRight);
		bulletRight.GetComponent <Rigidbody2D>().velocity = accelerationRight;
		if (bulletRotationMirror) {
			bulletRight.transform.localScale = new Vector3 (-1, 1, 1);
		}
	}
}
