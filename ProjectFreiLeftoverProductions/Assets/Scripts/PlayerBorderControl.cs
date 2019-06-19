using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerBorderControl : BorderControl {
	[Header("Level sequence settings"), SerializeField] private BorderGuard guard;
	
	[Space(10), SerializeField] private InteractableItem passport;
	[SerializeField] private Container passportContainer;
	[SerializeField] private float passportWaitSuspicion = 10;
	[SerializeField] private float passportWaitRefreshTime = 5;

	public override void StartPlayerInspection() {
		Debug.Log("Starting player inspection");
		StartCoroutine(WaitForWindowDown());
	}

	private IEnumerator WaitForWindowDown() {
		while (!currentCar.GetComponent<PlayerCar>().IsWindowDown) {
			yield return null;
		}

		guard.Activate();
		StartCoroutine(WaitForPassport());
	}

	private IEnumerator WaitForPassport() {
//		while (passport.Container != passportContainer) {
		for (int i = 0; i < 3; i++) {
			yield return new WaitForSeconds(2);
			guard.AddSuspicion(25);
		}
		
		yield return new WaitForSeconds(10);
		guard.AddSuspicion(-40);
	}
}