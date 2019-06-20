using UnityEngine;

public static class HelperMethods {
	public static float DistanceXZ(Vector3 a, Vector3 b) {
		// Calculate distance in XZ plane, discarding vertical distance
		Vector2 a2 = new Vector2(a.x, a.z);
		Vector2 b2 = new Vector2(b.x, b.z);

		return Vector2.Distance(a2, b2);
	}
}