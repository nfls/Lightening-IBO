using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Util {

	public static Vector2 DegreeToVector2 (float angle) {
		float i;
		float j;

		angle = Mathf.Deg2Rad * angle;

		i = Mathf.Cos (angle);
		j = Mathf.Sin (angle);

		return new Vector2 (i, j);
	}
}
