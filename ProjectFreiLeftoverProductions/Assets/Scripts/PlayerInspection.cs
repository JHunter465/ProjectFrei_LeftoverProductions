//using System;
//using System.Collections;
//
//[Serializable]
//public class PlayerInspection {
//	private readonly BorderControl bc;
//	private readonly Car car;
//
//	public PlayerInspection(BorderControl bc, Car car) {
//		this.bc = bc;
//		this.car = car;
//	}
//
//	public void Start() {
//		bc.StartCoroutine(WaitForWindowDown());
//	}
//
//	private IEnumerator WaitForWindowDown() {
//		while (!car.GetComponent<PlayerCar>().IsWindowDown) {
//			yield return null;
//		}
//
//		guard.Activate();
//		
//	}
//}