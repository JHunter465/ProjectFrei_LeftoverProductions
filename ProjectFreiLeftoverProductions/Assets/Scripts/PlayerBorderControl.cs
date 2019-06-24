using LevelSequences;
using UnityEngine;

public class PlayerBorderControl : BorderControl {
	protected override void StartPlayerInspection() {
		// Disable controls
		currentCar.GetComponent<CarControls>().enabled = false;
		
		// Load level sequence component for this border inspection point
		LevelSequence seq = GetComponent<LevelSequence>();
		
		// Start if a sequence component was found, else throw error
		if (seq) seq.StartSequence(this);
		else Debug.LogError("No LevelSequence found to run");
	}

	public void ReleasePlayerCar() {
		// Reenable controls
		currentCar.GetComponent<CarControls>().enabled = true;
		
		ReleaseCar(true);
	}
}