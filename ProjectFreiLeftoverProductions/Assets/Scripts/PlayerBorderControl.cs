using System.Collections;
using LevelSequences;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerBorderControl : BorderControl {
	public override void StartPlayerInspection() {
		LevelSequence seq = GetComponent<LevelSequence>();
		
		if (seq) seq.StartSequence(this);
		else Debug.LogError("No LevelSequence found to run");
	}
}