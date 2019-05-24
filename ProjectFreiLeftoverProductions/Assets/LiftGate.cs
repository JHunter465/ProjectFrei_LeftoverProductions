using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftGate : MonoBehaviour {
	private enum LiftGateState {
		Idle,
		Opening,
		Closing
	}
	
	[SerializeField] private float openAngle = 80;
	[SerializeField] private float closeAngle = 0;
	[SerializeField] [Range(0.1f, 100)] private float openSpeed = 2;

	[SerializeField] private Stoplight trafficLight;

	[SerializeField] private bool open;
	
	private LiftGateState state = LiftGateState.Idle;

	private void Update() {
		if (open) {
			// Set state to idle if openAngle was reached
			if (transform.localRotation.eulerAngles.z >= openAngle) {
				state = LiftGateState.Idle;	
			}
			else {
				state = LiftGateState.Opening;
			}

			trafficLight.CurrentState = Stoplight.TraficLightState.Green;
		}
		else {
			// Set state to idle if closeAngle was reached
			if (transform.localRotation.eulerAngles.z <= closeAngle) {
				state = LiftGateState.Idle;	
			}
			else {
				state = LiftGateState.Closing;
			}
			
			trafficLight.CurrentState = Stoplight.TraficLightState.Red;
		}

		// Move to openAngle if state is opening, move to closeAngle if state is closing, do nothing if idle
		if (state == LiftGateState.Opening) {
			// TODO fix: Idle state can technically never be reached because this code makes it work like a limit
			transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(0, 0, openAngle), Time.deltaTime * openSpeed);
		}
		else if (state == LiftGateState.Closing) {
			transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(0, 0, closeAngle), Time.deltaTime * openSpeed);
		}
	}
}