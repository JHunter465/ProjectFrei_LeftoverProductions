using System.Collections;
using UnityEngine;

namespace LevelSequences {
	public class Level1Sequence : LevelSequence {
		[SerializeField] private BorderGuard guard;
		private PlayerBorderControl pbc;

		[Space(10), SerializeField] private InteractableItem passport;
		[SerializeField] private Container passportContainer;
		[SerializeField] private float passportWaitSuspicion = 20;
		[SerializeField] private float passportWaitRefreshTime = 5;

		public override void StartSequence(PlayerBorderControl pbc) {
			this.pbc = pbc;
			
			Debug.Log("Starting player inspection");
			StartCoroutine(WaitForWindowDown());
		}

		private IEnumerator WaitForWindowDown() {
			while (!pbc.currentCar.GetComponent<PlayerCar>().IsWindowDown) {
				yield return null;
			}

			guard.Activate();
			StartCoroutine(WaitForPassport());
		}

		private IEnumerator WaitForPassport() {
//			while (passport.Container != passportContainer) {
			for (int i = 0; i < 2; i++) {
				yield return new WaitForSeconds(passportWaitRefreshTime);
				guard.AddSuspicion(passportWaitSuspicion);
			}
			
			pbc.ReleasePlayerCar();
		}
	}
}