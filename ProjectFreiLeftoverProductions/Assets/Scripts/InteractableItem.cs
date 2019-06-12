using System;
using UnityEngine;

public class InteractableItem : MonoBehaviour {
	private Vector3 startPos;
	[SerializeField] private float moveThreshold = 0.0f;

	private void Start() {
		// Save the object's position at start of level
		startPos = transform.position;
	}

	public bool HasMoved() {
		try {
			return Vector3.Distance(startPos, transform.position) > moveThreshold;
		}
		catch (NullReferenceException e) {
			throw new InvalidOperationException("No start position set");
		}
	}

	private void MoveToStartPos() {
		try {
			transform.position = startPos;
		}
		catch (NullReferenceException e) {
			throw new InvalidOperationException("No start position set");
		}
	}

	private void OnTriggerEnter(Collider other) {
		Container container = other.GetComponent<Container>();
		if (container) {
			if (Debug.isDebugBuild) Debug.Log(name + " entered container " + container.name);

			container.RegisterItem(this);
		}
	}
}