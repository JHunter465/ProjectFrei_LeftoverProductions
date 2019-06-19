using UnityEngine;
using UnityEngine.UI;

public class SuspcicionMeter : MonoBehaviour {
	[SerializeField] private BorderGuard guard;

	private Slider slider;

	private void Awake() {
		slider = GetComponent<Slider>();
	}

	private void Update() {
		slider.value = guard.SuspicionLevel / 100;
	}
}