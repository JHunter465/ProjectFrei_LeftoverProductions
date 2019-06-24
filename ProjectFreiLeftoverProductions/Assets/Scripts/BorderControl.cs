using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderControl : Target {
	private readonly Queue<Car> queue = new Queue<Car>();
	public Car currentCar;

	[SerializeField] private float aiInspectionTime = 5;
	[SerializeField] private LiftGate liftGate;
	[SerializeField] private float liftGateOpenTime = 3;
	[SerializeField] private float liftGateOpenTimePlayer = 10;

	public LiftGate LiftGate => liftGate;

	public void RegisterCar(Car car) {
		Debug.Log("Registering car " + car.gameObject.name);
		// Add car to the queue
		queue.Enqueue(car);

		// Load the next car
		UpdateCurrentCar();
	}

	private void UpdateCurrentCar() {
		// Only update to a new car if there is no current car
		if (!currentCar) {
			if (queue.Count > 0) {
				// Load next car from the queue
				currentCar = queue.Dequeue();
				StartInspection();
			}
		}
	}

	private void StartInspection() {
		if (currentCar.IsPlayerCar) {
			StartPlayerInspection();
		}
		else if (currentCar.IsAiCar) {
			StartAiInspection();
		}
		else {
			Debug.LogError("Missing controller component (AiCar or PlayerCar)");
		}
	}

	protected virtual void StartPlayerInspection() {
		throw new System.NotImplementedException();
	}

	private void StartAiInspection() {
		StartCoroutine(WaitForInspection());
	}

	private IEnumerator WaitForInspection() {
		// Wait till the time it takes to inspect an ai car has passed (debug version counts down in console)
		if (Debug.isDebugBuild) {
			for (int i = 0; i < aiInspectionTime; i++) {
				Debug.Log("Inspecting... " + (aiInspectionTime - i));
				yield return new WaitForSeconds(1);
			}
		}
		else {
			yield return new WaitForSeconds(aiInspectionTime);
		}

		ReleaseAiCar();
	}

	private void ReleaseAiCar() {
		if (Debug.isDebugBuild) Debug.Log("Releasing car");

		// Set inspectionPassed flag on the AI car so it knows to move along
		currentCar.GetComponent<AiCar>().SetInspectionPassed();
		ReleaseCar(false);
	}

	protected void ReleaseCar(bool waitUntilPlayerPassed) {
		if (waitUntilPlayerPassed) {
			// Open lift gate waiting until car passed
//			liftGate.OpenGate(() => currentCar.GetComponent<PlayerCar>().ExitedBorderControl); TODO fix predicate opening
			liftGate.OpenGate(liftGateOpenTimePlayer);
		}
		else {
			// Open lift gate timed
			liftGate.OpenGate(liftGateOpenTime);
		}

		// Remove car
		currentCar = null;
	}

	public bool CarRegistered(Car car) {
		return currentCar == car || queue.Contains(car);
	}
}