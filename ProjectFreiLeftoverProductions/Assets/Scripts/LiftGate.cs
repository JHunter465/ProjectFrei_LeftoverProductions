﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftGate : MonoBehaviour {
	public enum LiftGateState {
		Idle,
		Opening,
		Closing
	}

	[SerializeField] private float openAngle = 80;
	[SerializeField] private float closeAngle = 0;
	[SerializeField] [Range(0.1f, 100)] private float openSpeed = 2;

	[SerializeField] private Stoplight trafficLight;

	[SerializeField] private bool open;
	public bool Open => open;

	public LiftGateState State { get; private set; } = LiftGateState.Idle;

	private void Update() {
		// TODO this code can be optimized by checking against change rather than updating everything
		if (open) {
			// Set state to idle if openAngle was reached
			// TODO fix this dirty fix (stop using range and find an actual solution), original code in comment below
//			if (transform.localRotation.eulerAngles.z >= openAngle) {
			if (Mathf.Abs(transform.localRotation.eulerAngles.z - openAngle) < .1f) {
				State = LiftGateState.Idle;
			}
			else {
				State = LiftGateState.Opening;
			}

			trafficLight.CurrentState = Stoplight.TraficLightState.Green;
		}
		else {
			// Set state to idle if closeAngle was reached
			// TODO Fix this dirty fix #2
//			if (transform.localRotation.eulerAngles.z <= closeAngle) {
			if (Mathf.Abs(transform.localRotation.eulerAngles.z - closeAngle) < .1f) {
				State = LiftGateState.Idle;
			}
			else {
				State = LiftGateState.Closing;
			}

			trafficLight.CurrentState = Stoplight.TraficLightState.Red;
		}

		// Move to openAngle if state is opening, move to closeAngle if state is closing, do nothing if idle
		if (State == LiftGateState.Opening) {
			// TODO fix: Idle state can technically never be reached because this code makes it work like a limit (practically though, at high speeds this is no problem)
			// TODO fix temporary fix for the above problem (see above update method)
			transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(0, 0, openAngle), Time.deltaTime * openSpeed);
		}
		else if (State == LiftGateState.Closing) {
			transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(0, 0, closeAngle), Time.deltaTime * openSpeed);
		}
	}

	public void OpenGate(float time) {
		open = true;
		StartCoroutine(CloseGateWithTimeout(time));
	}

	private IEnumerator CloseGateWithTimeout(float time) {
		if (Debug.isDebugBuild) {
			for (int i = 0; i < time; i++) {
				Debug.Log("Gate open... " + (time - i));
				yield return new WaitForSeconds(1);
			}
		}
		else {
			yield return new WaitForSeconds(time);
		}

		open = false;
	}
}