using UnityEngine;
using Valve.VR;

[RequireComponent(typeof(Car))]
public class CarControls : MonoBehaviour {
	private Car car;

	public SteamVR_Input_Sources handType;        // 1
	public SteamVR_Action_Boolean teleportAction; // 2
	public SteamVR_Action_Boolean grabAction;     // 3

	public bool GetGrab() // 2
	{
		return grabAction.GetState(handType);
	}

	private void Start() {
		car = GetComponent<Car>();
	}

	private void FixedUpdate() {
		// TODO replace with vr controller trigger as scaling value?
		if (GetGrab()) {
			Debug.Log("this shit works");
		}

		if (Input.GetKey(KeyCode.Space)) {
			car.Accelerate();
		}
		else if (Input.GetKey(KeyCode.LeftControl)) {
			if (!car.gameObject.GetComponent<Rigidbody>().isKinematic) {
				car.Brake();
			}
		}
	}
}