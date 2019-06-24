using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
	public const string _lvlMgrTag = "LevelManager";
	
	private void Awake() {
		tag = _lvlMgrTag;
	}

	public void GameOver() {
		// Load game over scene
		SceneManager.LoadScene("Death");
	}
}