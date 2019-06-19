using UnityEngine;
using UnityEngine.UI;

public class SuspcicionMeter : MonoBehaviour {
	[SerializeField] private BorderGuard guard;

	private Slider slider;

	private void Awake() {
		// Initialize slider reference
		slider = GetComponent<Slider>();
	}

	private void Update() {
		// Update visual to current suspicion level
		slider.value = guard.SuspicionLevel / 100;
	}
}