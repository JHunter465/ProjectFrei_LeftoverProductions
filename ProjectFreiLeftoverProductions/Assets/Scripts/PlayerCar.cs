using UnityEngine;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(Car))]
public class PlayerCar : MonoBehaviour {
	[SerializeField] private LinearMapping windowMapping;
	[SerializeField] private float windowDownValue = .1f;

	[SerializeField] private PlayerBorderControl borderControlTarget;
	[SerializeField] private float targetRadius = 0.4f;
	[SerializeField] private Transform windowPosition;

	private Car car;

//	public bool IsWindowDown => windowMapping.value < windowDownValue; TODO temp USE THIS CODE FOR TESTING ONLY
	public bool IsWindowDown => true;

	private void Awake() {
		car = GetComponent<Car>();
	}

	private void Update() {
		// Check if player is in target range in the XZ plane
		bool inTargetRange = HelperMethods.DistanceXZ(windowPosition.position, borderControlTarget.transform.position) < targetRadius;
		
		// Register car if it wasn't already registered and the car is in range
		if (!borderControlTarget.CarRegistered(car) && inTargetRange) {
			// TODO disable movement
			borderControlTarget.RegisterCar(car);
		}
	}
}