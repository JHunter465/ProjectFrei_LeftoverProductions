﻿using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class BorderControl : MonoBehaviour {
	private readonly Queue<Car> queue = new Queue<Car>();
	private Car currentCar;

	[SerializeField] private float aiInspectionTime = 5;
	[SerializeField] private LiftGate liftGate;
	[SerializeField] private float liftGateOpenTime = 3;

	public LiftGate LiftGate => liftGate;

	public void RegisterCar(Car car) {
		queue.Enqueue(car);

		UpdateCurrentCar();
	}

	private void UpdateCurrentCar() {
		// Only update to a new car if there is no current car
		if (!currentCar) {
			if (queue.Count > 0) {
				currentCar = queue.Dequeue();
				StartInspection();
			}
		}
	}

	private void StartInspection() {
		if (currentCar.IsPlayerCar) {
			// Start player inspection
		}
		else {
			StartAiInspection();
		}
	}

	private void StartAiInspection() {
		StartCoroutine(WaitForInspection());
	}

	private IEnumerator WaitForInspection() {
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
		ReleaseCar();
	}

	private void ReleaseCar() {
		// Open lift gate
		liftGate.OpenGate(liftGateOpenTime);

		// Remove car
		currentCar = null;
		// TODO This almost creates a loop. Almost. It's freaky
		// TODO this code messes up, will probably not work for multiple cars but might
//		UpdateCurrentCar();
	}
}