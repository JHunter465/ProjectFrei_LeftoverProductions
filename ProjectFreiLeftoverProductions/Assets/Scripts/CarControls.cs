using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Car))]
public class CarControls : MonoBehaviour {

	private Car car;

	private void Start() {
		car = GetComponent<Car>();
	}

	private void FixedUpdate() {
		// TODO replace with vr controller trigger as scaling value
		if (Input.GetKey("w")) {
			car.Accelerate();
		}
		else if (Input.GetKey("s")) {
			car.Brake();
		}
	}
}