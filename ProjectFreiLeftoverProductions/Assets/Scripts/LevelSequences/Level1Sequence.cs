using System.Collections;
using UnityEngine;

namespace LevelSequences {
	public class Level1Sequence : LevelSequence {
		[SerializeField] private BorderGuard guard;
		private PlayerBorderControl pbc;

		[Space(10)] [SerializeField] private string passportQuestion;
		[SerializeField] private float passportLostSuspicion = 20;
		[SerializeField] private InteractableItem passport;
		[SerializeField] private Container passportContainer;
		[SerializeField] private float passportWaitSuspicion = 20;
		[SerializeField] private float passportWaitRefreshTime = 5;

		[Space(10)] [SerializeField] private string registrationPapersQuestion;
		[SerializeField] private float papersLostSuspicion = 20;
		[SerializeField] private InteractableItem papers;
		[SerializeField] private Container papersContainer;
		[SerializeField] private float papersWaitSuspicion = 20;
		[SerializeField] private float papersWaitRefreshTime = 5;
		
		[Space(10)] [SerializeField] private string manifestQuestion;
		[SerializeField] private float manifestLostSuspicion = 20;
		[SerializeField] private InteractableItem manifest;
		[SerializeField] private Container manifestContainer;
		[SerializeField] private float manifestWaitSuspicion = 20;
		[SerializeField] private float manifestWaitRefreshTime = 5;

//		[Space(10), SerializeField] private 

		private string passportAnswer;
		private string papersAnswer;
		private string manifestAnswer;
		
		public void SetPassportAnswer(string answer) {
			passportAnswer = answer;
		}
		
		public void SetPapersAnswer(string answer) {
			papersAnswer = answer;
		}
		
		public void SetManifestAnswer(string answer) {
			manifestAnswer = answer;
		}

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
			StartCoroutine(WaitForPassportAnswer());
		}

		private IEnumerator WaitForPassportAnswer() {
			yield return new WaitUntil(() => passportAnswer != null);

			if (passportAnswer == "lost") {
				guard.AddSuspicion(passportLostSuspicion);
			} 
			
			StartCoroutine(WaitForPassportGiven());
		}

		private IEnumerator WaitForPassportGiven() {
			yield return new WaitForSeconds(passportWaitRefreshTime);
			
			while (passport.Container != passportContainer) {
				guard.AddSuspicion(passportWaitSuspicion);
				yield return new WaitForSeconds(passportWaitRefreshTime);
			}

			StartCoroutine(WaitForPapersAnswer());
		}
		
		private IEnumerator WaitForPapersAnswer() {
			yield return new WaitUntil(() => papersAnswer != null);

			if (papersAnswer == "lost") {
				guard.AddSuspicion(papersLostSuspicion);
			} 
			
			StartCoroutine(WaitForPapersGiven());
		}

		private IEnumerator WaitForPapersGiven() {
			yield return new WaitForSeconds(papersWaitRefreshTime);
			
			while (papers.Container != papersContainer) {
				guard.AddSuspicion(papersWaitSuspicion);
				yield return new WaitForSeconds(papersWaitRefreshTime);
			}

			StartCoroutine(WaitForManifestAnswer());
		}
		
		private IEnumerator WaitForManifestAnswer() {
			yield return new WaitUntil(() => manifestAnswer != null);

			if (manifestAnswer == "lost") {
				guard.AddSuspicion(manifestLostSuspicion);
			} 
			
			StartCoroutine(WaitForManifestGiven());
		}

		private IEnumerator WaitForManifestGiven() {
			yield return new WaitForSeconds(manifestWaitRefreshTime);
			
			while (manifest.Container != manifestContainer) {
				guard.AddSuspicion(manifestWaitSuspicion);
				yield return new WaitForSeconds(manifestWaitRefreshTime);
			}
			
			End();
		}

		private void End() {
			pbc.ReleasePlayerCar();
		}
	}
}