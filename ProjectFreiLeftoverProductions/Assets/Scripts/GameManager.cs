using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
	[SerializeField] private string menuSceneName = "MainMenu";
	[SerializeField] private string gamePlayStartSceneName = "";

	private int currentLevelIndex;

	private void Start() {
		// Make gamemanager persistent
		DontDestroyOnLoad(this);
	}

	public void LoadMenu() {
		// Load the main menu
		LoadLevel(SceneManager.GetSceneByName(menuSceneName).buildIndex);
	}

	public void NextLevel() {
		// Switch to the next level
		LoadLevel(currentLevelIndex + 1);
	}

	private void LoadLevel(int i) {
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		// Load the current scene after it has been switched
		currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
	}
}