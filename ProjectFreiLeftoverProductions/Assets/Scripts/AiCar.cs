﻿using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

[RequireComponent(typeof(Car))]
public class AiCar : MonoBehaviour {
	[SerializeField] private BorderControl borderControlTarget;
	[SerializeField] private float targetRadius = 0.4f;
	[Space(10)] [SerializeField] private Transform despawnTarget;
	[SerializeField] private float despawnRadius = 0.5f;
	[Space(10)] [SerializeField] private Transform windowPosition;
	[Space(10)] [SerializeField] private Transform front;
	[SerializeField] private float distanceToNextCar;
	[SerializeField] private LayerMask aiLayer;

	private bool inTargetRange;
	private bool passedInspection;

	private Car car;

	private void OnValidate() {
		// Do not allow despawnRadius and targetRadius values below zero
		if (despawnRadius < 0) despawnRadius = 0;
		if (targetRadius < 0) targetRadius = 0;
	}

	private void Awake() {
		car = GetComponent<Car>();
	}

	private void Update() {
		// Check if the AI car has reached the despawn point; car will be destroyed before it is moved
		DespawnCheck();

		MoveCar();
	}

	private void MoveCar() {
		// Tell the border control point that this car is now in range (passedInspection flag is to avoid duplicate registration)
		if (!passedInspection && DistanceXZ(windowPosition.position, borderControlTarget.transform.position) < targetRadius) {
			inTargetRange = true;
			borderControlTarget.RegisterCar(car);
		}

		if (Debug.isDebugBuild) Debug.DrawRay(front.transform.position, Vector3.forward, Color.green);
		if (Physics.Raycast(front.transform.position, Vector3.forward, distanceToNextCar, aiLayer)) {
			// Allow movement only if no car is blocking the way
			car.Brake();
		}
		else {
			// Accelerate if the car has not yet reached the borderControlTarget point with an allowance of the stopping distance
			// Or if the car has passed the borderControlTarget and is merrily on its way to its own destruction
			if (!inTargetRange) {
				car.Accelerate();
			}
			else {
				// Car needs to accelerate after it has passed the inspection
				if (inTargetRange && passedInspection) {
					// First wait until the gate is open
					if (borderControlTarget.LiftGate.Open && borderControlTarget.LiftGate.State == LiftGate.LiftGateState.Idle) {
						car.Accelerate();
					}
				}
				// Brake if the car is in range of the borderControlTarget (implied by if tree) and it needs to stop (inspection not passed)
				else if (car.Velocity.magnitude > 0) {
					car.Brake();
				}
			}
		}
	}

	public void SetInspectionPassed() {
		passedInspection = true;
	}

	private void DespawnCheck() {
		// Destroy this car if it gets too close (defined by despawnRadius) to the despawn point
		if (DistanceXZ(windowPosition.position, despawnTarget.position) < despawnRadius) {
			Destroy(gameObject);
		}
	}

	private float DistanceXZ(Vector3 a, Vector3 b) {
		// Calculate distance in XZ plane, discarding vertical distance
		Vector2 a2 = new Vector2(a.x, a.z);
		Vector2 b2 = new Vector2(b.x, b.z);

		return Vector2.Distance(a2, b2);
	}
}