using System.Collections;
using LevelSequences;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerBorderControl : BorderControl {
	protected override void StartPlayerInspection() {
		// Load level sequence component for this border inspection point
		LevelSequence seq = GetComponent<LevelSequence>();
		
		// Start if a sequence component was found, else throw error
		if (seq) seq.StartSequence(this);
		else Debug.LogError("No LevelSequence found to run");
	}
}