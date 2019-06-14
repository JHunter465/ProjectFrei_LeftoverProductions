using UnityEngine;

[RequireComponent(typeof(Car))]
public class CarControls : MonoBehaviour {
	private Car car;

	private void Start() {
		car = GetComponent<Car>();
	}

	private void FixedUpdate() {
		// TODO replace with vr controller trigger as scaling value
		if (Input.GetKey(KeyCode.Space)) {
			car.Accelerate();
		}
		else if (Input.GetKey(KeyCode.LeftControl)) {
			if (!car.gameObject.GetComponent<Rigidbody>().isKinematic) {
				car.Brake();
			}
		}
	}
}