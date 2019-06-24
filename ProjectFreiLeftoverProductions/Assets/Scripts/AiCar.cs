using System.Collections;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

[RequireComponent(typeof(Car))]
[RequireComponent(typeof(AudioSource))]
public class AiCar : MonoBehaviour {
	[SerializeField] private BorderControl borderControlTarget;
	[SerializeField] private float targetRadius = 0.4f;

	[Space(10)] [SerializeField] private Transform despawnTarget;
	[SerializeField] private float despawnRadius = 0.5f;

	[Space(10)] [SerializeField] private Transform windowPosition;

	[Space(10)] [SerializeField] private Transform front;
	[SerializeField] private float distanceToNextCar;
	[SerializeField] private LayerMask aiLayer;
	[SerializeField] private LayerMask playerLayer;

	[Space(10)] [SerializeField] private AudioClip[] honkAudio;

	private bool inTargetRange;
	private bool passedInspection;
	private bool blockedByPlayer => Physics.Raycast(front.transform.position, transform.forward, distanceToNextCar, playerLayer);
	private bool isWaiting;

	private Car car;
	private new AudioSource audio;

	private void OnValidate() {
		// Do not allow despawnRadius and targetRadius values below zero
		if (despawnRadius < 0) despawnRadius = 0;
		if (targetRadius < 0) targetRadius = 0;
	}

	private void Awake() {
		car = GetComponent<Car>();
		audio = GetComponent<AudioSource>();
	}

	private void Update() {
		// Check if the AI car has reached the despawn point; car will be destroyed before it is moved
		DespawnCheck();

		MoveCar();
	}

	private void MoveCar() {
		// Set flag that this car is in range of the border control target point
		inTargetRange = HelperMethods.DistanceXZ(windowPosition.position, borderControlTarget.transform.position) < targetRadius;

		// Tell the border control point that this car is now in range (passedInspection flag is to avoid duplicate registration)
		if (!passedInspection && !borderControlTarget.CarRegistered(car) && inTargetRange) {
			borderControlTarget.RegisterCar(car);
		}

		// Draw a simple debug ray to visualize distance that is being kept
		if (Debug.isDebugBuild) Debug.DrawRay(front.transform.position, Vector3.forward, Color.green);
		if (Physics.Raycast(front.transform.position, transform.forward, distanceToNextCar, aiLayer | playerLayer)) {
			// Allow movement only if no car is blocking the way
			car.Brake();

			if (blockedByPlayer) WaitForPlayerMovement();
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
		if (HelperMethods.DistanceXZ(windowPosition.position, despawnTarget.position) < despawnRadius) {
			Destroy(gameObject);
		}
	}

	private void WaitForPlayerMovement() {
		if (!isWaiting) StartCoroutine(HonkOnWaitForPlayer());
	}

	private IEnumerator HonkOnWaitForPlayer() {
		// Wait time base and variance
		const float waitTime = 3;
		const float waitTimeVariance = 2;

		// Set flag to avoid honking cacophony
		isWaiting = true;
		// Initial wait time
		yield return new WaitForSeconds(5); // TODO customize/randomize this value

		// Start loop that keeps honking
		while (blockedByPlayer) {
			AudioClip honk = GetRandomHonkSound();
			
			Honk(honk);

			// Wait a random amount of time between the base +/- the variance including the base length of the clip
			yield return new WaitForSeconds(
				Random.Range(waitTime - waitTimeVariance, waitTime + waitTimeVariance) + honk.length // TODO customize/randomize this value
			);
		}

		isWaiting = false;
	}

	private AudioClip GetRandomHonkSound() {
		return honkAudio[Random.Range(0, honkAudio.Length)];
	}

	public void Honk(AudioClip honkingSound = null) {
		// If unset, load a random honking sound
		if (!honkingSound) honkingSound = GetRandomHonkSound();
		
		// Let the guard know this car honked to increase suspicion
		BorderGuard guard = GameObject.FindWithTag(BorderGuard._borderGuardTag).GetComponent<BorderGuard>();
		guard.RegisterHonking();

		audio.PlayOneShot(honkingSound);
	}
}