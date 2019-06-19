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

//	public bool IsWindowDown => windowMapping.value < windowDownValue; TODO temp
	public bool IsWindowDown => true;

	private void Awake() {
		car = GetComponent<Car>();
	}

	private void Update() {
		bool inTargetRange = HelperMethods.DistanceXZ(windowPosition.position, borderControlTarget.transform.position) < targetRadius;
		if (!borderControlTarget.CarRegistered(car) && inTargetRange) {
			// TODO disable movement
			borderControlTarget.RegisterCar(car);
		}
	}
}