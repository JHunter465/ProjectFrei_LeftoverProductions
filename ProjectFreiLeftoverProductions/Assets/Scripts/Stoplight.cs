using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Stoplight : MonoBehaviour {
	public enum TraficLightState {
		Off,
		Red,
		Green
	}

	[SerializeField] private Light red;
	[SerializeField] private Light green;

	[SerializeField] private TraficLightState state;
	public TraficLightState CurrentState {
		set {
			state = value;
			switch (value) {
				case TraficLightState.Red:
					SetRed(true);
					SetGreen(false);
					break;
				case TraficLightState.Green:
					SetGreen(true);
					SetRed(false);
					break;
				default:
					SetRed(false);
					SetGreen(false);
					break;
			}
		}
	}

	private void Start() {
		CurrentState = TraficLightState.Off;
	}

	private void OnValidate() {
		CurrentState = state;
	}

	private void SetGreen(bool val) {
		green.enabled = val;
	}

	private void SetRed(bool val) {
		red.enabled = val;
	}
}