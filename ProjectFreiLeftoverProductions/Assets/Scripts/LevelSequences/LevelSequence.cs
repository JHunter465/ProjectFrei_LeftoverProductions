using UnityEngine;

namespace LevelSequences {
	public abstract class LevelSequence : MonoBehaviour {
		public abstract void StartSequence(PlayerBorderControl pbc);
	}
}