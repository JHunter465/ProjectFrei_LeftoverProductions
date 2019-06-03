using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

[RequireComponent(typeof(Car))]
public class AiCar : MonoBehaviour {
	[SerializeField] private BorderControl target;
	[SerializeField] private float targetRadius = .4f;
	[SerializeField] private Transform despawnTarget;
	[SerializeField] private float despawnRadius = 0.1f;
	[SerializeField] private Transform windowPosition;

	[SerializeField] private LiftGate liftGate;

	private bool inTargetRange;
	private bool passedInspection;

	private Car car;

	private void OnValidate() {
		if (despawnRadius < 0) {
			despawnRadius = 0;
		}
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
		if (!passedInspection && DistanceXZ(windowPosition.position, target.transform.position) < targetRadius) {
			inTargetRange = true;
			target.RegisterCar(car);
		}
		
		// Accelerate if the car has not yet reached the target point with an allowance of the stopping distance
		// Or if the car has passed the target and is merrily on its way to its own destruction
		if (!inTargetRange) {
			car.Accelerate();
		}
		else {
			// Car needs to accelerate after it has passed the inspection
			if (inTargetRange && passedInspection) {
				// First wait until the gate is open
				if (target.LiftGate.Open && target.LiftGate.State == LiftGate.LiftGateState.Idle) {
					car.Accelerate();
				}
			}
			// Brake if the car is in range of the target (implied by if tree) and it needs to stop (inspection not passed)
			else if (car.Velocity.magnitude > 0) {
				car.Brake();
			}
		}
	}

	public void SetInspectionPassed() {
		passedInspection = true;
	}
	
	private void DespawnCheck() {
		if (DistanceXZ(windowPosition.position, despawnTarget.position) < despawnRadius) {
			Destroy(this);
		}
	}

	private float DistanceXZ(Vector3 a, Vector3 b) {
		Vector2 a2 = new Vector2(a.x, a.z);
		Vector2 b2 = new Vector2(b.x, b.z);

		return Vector2.Distance(a2, b2);
	}
}