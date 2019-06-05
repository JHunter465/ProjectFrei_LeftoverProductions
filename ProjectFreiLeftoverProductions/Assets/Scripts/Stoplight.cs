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

	// Changing the State property allows control over the traffic light
	[SerializeField] private TraficLightState state;
	public TraficLightState State {
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
		State = TraficLightState.Off;
	}

	private void OnValidate() {
		// Allow visual feedback of state during edit mode
		State = state;
	}

	private void SetGreen(bool val) {
		green.enabled = val;
	}

	private void SetRed(bool val) {
		red.enabled = val;
	}
}